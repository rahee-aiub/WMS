USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSStockBalanceDHK]    Script Date: 01/12/2019 11:32:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








ALTER PROCEDURE  [dbo].[Sp_rptWMSStockBalanceDHK] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSStockBalanceDHK '2019-01-06'

*/

DECLARE @ProcDate SMALLDATETIME;

SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;



IF YEAR(@ProcDate) = YEAR(@tDate) AND 
	   MONTH(@ProcDate) = MONTH(@tDate) AND 
	   DAY(@ProcDate) = DAY(@tDate) 

	   BEGIN		  
	        EXECUTE SpM_GenerateAccountOpBalance;
			
	   END

    ELSE
	   BEGIN
			EXECUTE SpM_GenerateAccountOpBalancePrevious @tDate ;		
			
	   END

	   CREATE TABLE #Table
	   (
			AccType INT,
			AccNo INT,
			AccKarat INT,
			AccLocation INT,
			OpBalance MONEY,
			Received MONEY,
			Sale MONEY,
			PartyName VARCHAR(50),
			Balance MONEY
	   )


			INSERT INTO #Table	(AccType,AccNo,AccKarat,AccLocation,OpBalance,PartyName)
			SELECT  A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZACCOUNT.AccBalance, A2ZPARTYCODE.PartyName
			FROM    A2ZACCOUNT LEFT OUTER JOIN
			A2ZPARTYCODE ON  A2ZACCOUNT.AccNo = A2ZPARTYCODE.PartyAccNo
			GROUP BY A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZPARTYCODE.PartyName,A2ZACCOUNT.AccBalance
			HAVING  (A2ZACCOUNT.AccType = 12) AND (A2ZACCOUNT.AccLocation = 3)


		TRUNCATE TABLE WFA2ZTRANSACTION;

			 INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
				  SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE FuncOpt IN(21,41,42) AND TrnDate = @tDate;


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (21,41,42) AND TrnDate = ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 
		


			UPDATE #Table SET Received = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 21 AND WFA2ZTRANSACTION.TrnDate = @tDate  AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.AccKarat = #Table.AccKarat AND WFA2ZTRANSACTION.Location = 2);		
			UPDATE #Table SET Sale = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 41 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location = 3);
			UPDATE #Table SET Sale = #Table.Sale - (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 42 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location = 3);
			UPDATE #Table SET Balance = OpBalance + Received - Sale;

			DELETE FROM #Table WHERE OpBalance = 0 AND Received=0 AND Sale=0;

	SELECT PartyName,AccKarat,OpBalance,Received,Sale,Balance FROM #Table;
	DROP TABLE #Table;

END














GO

