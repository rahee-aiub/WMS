USE [A2ZACWMS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptWMSDhkStockDetailsReport]    Script Date: 1/9/2019 4:17:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE  [dbo].[Sp_rptWMSDhkStockDetailsReport] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSDhkStockDetailsReport '2019-01-01'

*/

DECLARE @ProcDate SMALLDATETIME;
DECLARE @TrackingCode INT;
DECLARE @Karat INT;

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;

SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

IF YEAR(@ProcDate) = YEAR(@tDate) AND 
	   MONTH(@ProcDate) = MONTH(@tDate) AND 
	   DAY(@ProcDate) = DAY(@tDate) 

	   BEGIN		  
	        EXECUTE SpM_GenerateAccountBalance;
			
	   END

    ELSE
	   BEGIN
			EXECUTE SpM_GenerateAccountBalancePrevious @tDate ;		
			
	   END


	   CREATE TABLE #TEMPTABLE
		(		
			TrackingCode INT,
			TrackingCodeName VARCHAR(50),
			Karat INT,
			Opening MONEY DEFAULT 0,
			Received MONEY DEFAULT 0,
			ReceivedReturn MONEY DEFAULT 0,
			TotalReceived MONEY DEFAULT 0,
			Sale MONEY DEFAULT 0,
			SaleReturn MONEY DEFAULT 0,
			TotalSale MONEY DEFAULT 0,
			TotalBalance MONEY DEFAULT 0,
		
		)

		TRUNCATE TABLE WFA2ZTRANSACTION;


		INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
		SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate = @tDate;


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnDate = ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 
		



		INSERT INTO #TEMPTABLE(TrackingCode,Karat,Opening)
		SELECT AccPartyNo,AccKarat,AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccType = 12 AND AccLocation = 3;


	---------------------
	 DECLARE wfTable CURSOR FOR
     SELECT TrackingCode,Karat FROM #TEMPTABLE; 
	
	 OPEN wfTable;
     FETCH NEXT FROM wfTable INTO @TrackingCode,@Karat;
     WHILE @@FETCH_STATUS = 0 
	     BEGIN

				UPDATE #TEMPTABLE SET Received = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 21 AND  WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 3) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
				UPDATE #TEMPTABLE SET ReceivedReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 22 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 3) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;		
				UPDATE #TEMPTABLE SET Sale = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 41 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 3) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
				UPDATE #TEMPTABLE SET SaleReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 42 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 3) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
		 
	
			 FETCH NEXT FROM wfTable INTO @TrackingCode,@Karat;
          
	     END

     CLOSE wfTable; 
     DEALLOCATE wfTable;

		
		UPDATE #TEMPTABLE SET TotalReceived = Received - ReceivedReturn;
		UPDATE #TEMPTABLE SET TotalSale = Sale - SaleReturn;
		UPDATE #TEMPTABLE SET TotalBalance = Opening + TotalReceived - TotalSale;

		DELETE FROM #TEMPTABLE WHERE Opening = 0 AND Received = 0 AND ReceivedReturn = 0 AND TotalReceived = 0 AND Sale = 0 AND SaleReturn = 0 AND TotalSale = 0 AND TotalBalance = 0;

		UPDATE #TEMPTABLE SET TrackingCodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.TrackingCode = A2ZPARTYCODE.PartyCode);

		SELECT * FROM #TEMPTABLE;

		DROP TABLE #TEMPTABLE;

END



