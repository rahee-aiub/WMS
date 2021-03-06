USE [A2ZACWMS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptWMSDxbStockDetailsReport]    Script Date: 1/9/2019 4:05:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE  [dbo].[Sp_rptWMSDxbStockDetailsReport] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSDxbStockDetailsReport '2019-01-01'

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
			Purchase MONEY DEFAULT 0,
			PurchaseReturn MONEY DEFAULT 0,
			TotalPurchase MONEY DEFAULT 0,
			Issue MONEY DEFAULT 0,
			IssueReturn MONEY DEFAULT 0,
			TotalIssue MONEY DEFAULT 0,
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
		SELECT AccPartyNo,AccKarat,AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccType = 12 AND AccLocation = 1;


	---------------------
	 DECLARE wfTable CURSOR FOR
     SELECT TrackingCode,Karat FROM #TEMPTABLE; 
	
	 OPEN wfTable;
     FETCH NEXT FROM wfTable INTO @TrackingCode,@Karat;
     WHILE @@FETCH_STATUS = 0 
	     BEGIN

				UPDATE #TEMPTABLE SET Purchase = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 1 AND  WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 1) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
				UPDATE #TEMPTABLE SET PurchaseReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 2 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 1) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;		
				UPDATE #TEMPTABLE SET Issue = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 11 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 1) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
				UPDATE #TEMPTABLE SET IssueReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 12 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @TrackingCode AND Location = 1) WHERE TrackingCode = @TrackingCode AND Karat = @Karat;
		 
	
			 FETCH NEXT FROM wfTable INTO @TrackingCode,@Karat;
          
	     END

     CLOSE wfTable; 
     DEALLOCATE wfTable;

		
		UPDATE #TEMPTABLE SET TotalPurchase = Purchase - PurchaseReturn;
		UPDATE #TEMPTABLE SET TotalIssue = Issue - IssueReturn;
		UPDATE #TEMPTABLE SET TotalBalance = Opening + TotalPurchase - TotalIssue;

		DELETE FROM #TEMPTABLE WHERE Opening = 0 AND Purchase = 0 AND PurchaseReturn = 0 AND TotalPurchase = 0 AND Issue = 0 AND IssueReturn = 0 AND TotalIssue = 0 AND TotalBalance = 0;

		UPDATE #TEMPTABLE SET TrackingCodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.TrackingCode = A2ZPARTYCODE.PartyCode);

		SELECT * FROM #TEMPTABLE;

		DROP TABLE #TEMPTABLE;

END


