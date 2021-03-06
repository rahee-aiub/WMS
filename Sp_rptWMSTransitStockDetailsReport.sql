USE [A2ZACWMS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptWMSTransitStockDetailsReport]    Script Date: 2019-01-08 10:51:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE  [dbo].[Sp_rptWMSTransitStockDetailsReport] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSTransitStockDetailsReport '2019-01-01'

*/

DECLARE @ProcDate SMALLDATETIME;
DECLARE @TrackingCode INT;
DECLARE @Karat INT;
DECLARE @PartyNo INT;


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
			PartyNo INT,
			PartyName VARCHAR(50),	
			TrackingCode INT,			
			TrackingCodeName VARCHAR(50),			
			Karat INT,
			Opening MONEY Default 0,
			Issue MONEY Default 0,
			IssueReturn MONEY Default 0,
			TotalIssue MONEY Default 0,
			Received MONEY Default 0,
			ReceivedReturn MONEY Default 0,
			TotalReceived MONEY Default 0,			
			TotalBalance MONEY Default 0,
		
		)

		TRUNCATE TABLE WFA2ZTRANSACTION;


		INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
		SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate = @tDate AND FuncOpt IN (11,12,21,22);


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnDate = ' + '''' +@tDate + '''' + ' AND FuncOpt IN (11,12,21,22)' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 
		



		INSERT INTO #TEMPTABLE(PartyNo,TrackingCode,Karat,Opening)
		SELECT AccPartyNo,AccTrackingCode,AccKarat,AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccType = 13 AND AccLocation = 2;


	---------------------
	 DECLARE wfTable CURSOR FOR
     SELECT PartyNo,TrackingCode,Karat FROM #TEMPTABLE; 
	
	 OPEN wfTable;
     FETCH NEXT FROM wfTable INTO @PartyNo,@TrackingCode,@Karat;
     WHILE @@FETCH_STATUS = 0 
	     BEGIN
		 		
				UPDATE #TEMPTABLE SET Issue = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 11 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @PartyNo AND WFA2ZTRANSACTION.RefAccNo = @TrackingCode AND Location = 2) WHERE PartyNo = @PartyNo AND Karat = @Karat AND TrackingCode = @TrackingCode;
				UPDATE #TEMPTABLE SET IssueReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 12 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @PartyNo AND WFA2ZTRANSACTION.RefAccNo = @TrackingCode AND Location = 2) WHERE PartyNo = @PartyNo AND Karat = @Karat AND TrackingCode = @TrackingCode;
		 
				UPDATE #TEMPTABLE SET Received = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 21 AND  WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @PartyNo AND WFA2ZTRANSACTION.RefAccNo = @TrackingCode AND Location = 2) WHERE PartyNo = @PartyNo AND Karat = @Karat AND TrackingCode = @TrackingCode;
				UPDATE #TEMPTABLE SET ReceivedReturn = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE FuncOpt = 22 AND WFA2ZTRANSACTION.AccKarat = @Karat AND WFA2ZTRANSACTION.AccNo = @PartyNo AND WFA2ZTRANSACTION.RefAccNo = @TrackingCode AND Location = 2) WHERE PartyNo = @PartyNo AND Karat = @Karat AND TrackingCode = @TrackingCode;		
		
			 FETCH NEXT FROM wfTable INTO @PartyNo,@TrackingCode,@Karat;
          
	     END

     CLOSE wfTable; 
     DEALLOCATE wfTable;


		UPDATE #TEMPTABLE SET TotalIssue = Issue - IssueReturn;
		UPDATE #TEMPTABLE SET TotalReceived = Received - ReceivedReturn;
		
		UPDATE #TEMPTABLE SET TotalBalance = Opening + TotalIssue - TotalReceived;

		DELETE FROM #TEMPTABLE WHERE Opening = 0 AND Received = 0 AND ReceivedReturn = 0 AND TotalReceived = 0 AND Issue = 0 AND IssueReturn = 0 AND TotalIssue = 0 AND TotalBalance = 0;

		UPDATE #TEMPTABLE SET PartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.PartyNo = A2ZPARTYCODE.PartyCode);
		UPDATE #TEMPTABLE SET TrackingCodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.TrackingCode = A2ZPARTYCODE.PartyCode);

	
		SELECT * FROM #TEMPTABLE;

		DROP TABLE #TEMPTABLE;

END




