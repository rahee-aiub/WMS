USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateTransitPartyOpBalSingle]    Script Date: 01/17/2019 2:17:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[SpM_CSGenerateTransitPartyOpBalSingle](@AccNo int, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @xDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

    DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

    DECLARE @pYear INT;
	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @atyClass INT;

    DECLARE @ReadFlag INT;
    DECLARE @processDate SMALLDATETIME;

    DECLARE @tmm INT;
    DECLARE @tdd INT;
    DECLARE @xFlag INT;
    DECLARE @yFlag INT;
    
    DECLARE @xShow INT;

	---EXECUTE SpM_CSGenerateTransitPartyOpBalSingle 130001,'2019-01-01','2019-01-12',2
----------------------------------------------------------------------

    TRUNCATE TABLE WFA2ZTRANSACTION;

    UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE AccNo = @AccNo;

-----------------------------------------------------------------------
	SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

    SET @IYear = YEAR(@fDate);

    IF @IYear > @BegYear
       BEGIN
            SET @opDate = CAST(@BegYear AS VARCHAR(4)) + '-01-01';
       END
    ELSE
       BEGIN
	        SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';

	        SET @nYear = YEAR(@nDate);

      --   	IF MONTH(@nDate) < 7
	     --   	BEGIN
			   --      SET @nYear = @nYear - 1;

			   --      SET @opDate = CAST(@nYear AS VARCHAR(4)) + '-07-01';
		    --END	
       END

    
	SET @openTable = 'A2ZACWMST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZACOPBALANCE';	

	SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE ' + @openTable + '.AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND ' + @openTable + '.AccLocation = ' + CAST(@nFlag AS VARCHAR(2)) +
		' AND ' + @openTable + '.AccKarat = A2ZACCOUNT.AccKarat' +
		'),0) FROM A2ZACCOUNT,' + @openTable +
		' WHERE A2ZACCOUNT.AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND A2ZACCOUNT.AccLocation = ' + CAST(@nFlag AS VARCHAR(2)) +
		' AND ' + @openTable + '.AccKarat = A2ZACCOUNT.AccKarat';
	
	
	EXECUTE (@strSQL);

	
--==========  Get Transaction Data For Opening Balance ==========
	SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (TrnDate,VchNo,AccNo,FuncOpt,FuncOptDesc,' +
				'AccKarat,Location,PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,AccNo,FuncOpt,FuncOptDesc,' +
				'AccKarat,Location,PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +	
				' FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +	
                ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND Location = ' + CAST(@nFlag AS VARCHAR(2)) +
       	        ' AND (TrnDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'


					
           
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (TrnDate,VchNo,AccNo,FuncOpt,FuncOptDesc,' +
				'AccKarat,Location,PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,AccNo,FuncOpt,FuncOptDesc,' +
				'AccKarat,Location,PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +		
				' FROM A2ZACWMS..A2ZTRANSACTION ' + 
        ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND Location = ' + CAST(@nFlag AS VARCHAR(2)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'

		       
		EXECUTE (@strSQL);

	SET @strSQL = 'DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';
	EXECUTE (@strSQL);

--==========  Get Transaction Data For Opening Balance ==========

----------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------

-------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
            ISNULL((SELECT SUM(TrnCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.Location = @nFlag AND 
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND WFA2ZTRANSACTION.TrnCSGL = 0 AND  
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo AND A2ZACCOUNT.AccLocation = @nFlag AND 
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat;

---------- Debit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
            ISNULL((SELECT SUM(TrnDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.Location = @nFlag AND 
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND WFA2ZTRANSACTION.TrnCSGL = 0 AND  
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo AND A2ZACCOUNT.AccLocation = @nFlag AND
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat;

  
END






GO

