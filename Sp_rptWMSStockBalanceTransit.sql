USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSStockBalanceTransit]    Script Date: 01/12/2019 11:33:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








ALTER PROCEDURE  [dbo].[Sp_rptWMSStockBalanceTransit] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSStockBalanceTransit '2019-01-02'

*/

DECLARE @ProcDate SMALLDATETIME;
DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;


SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

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

	   		TRUNCATE TABLE WFA2ZTRANSACTION;

			 INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
				  SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE FuncOpt IN(11,12,21) AND TrnDate = @tDate;


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (11,12,21) AND TrnDate = ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 
		


	   CREATE TABLE #Table
	   (
			PartyName VARCHAR(50),
			AccType INT,
			AccNo INT,
			AccKarat INT,
			TrackingAccNo INT,	
			TrackingPartyName VARCHAR(10),		
			AccLocation INT,
			OpBalance MONEY,
			Received MONEY,
			Issue MONEY,
			Balance MONEY
	   )


			INSERT INTO #Table	(AccType,AccNo,TrackingAccNo,Acckarat,AccLocation,OpBalance,PartyName)
			SELECT  A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo,A2ZACCOUNT.AccTrackingCode,A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, SUM(A2ZACCOUNT.AccBalance), A2ZPARTYCODE.PartyName
			FROM    A2ZACCOUNT LEFT OUTER JOIN
			A2ZPARTYCODE ON A2ZACCOUNT.AccType = A2ZPARTYCODE.GroupCode AND A2ZACCOUNT.AccNo = A2ZPARTYCODE.PartyAccNo
			GROUP BY A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccLocation, A2ZPARTYCODE.PartyName,A2ZACCOUNT.AccTrackingCode,A2ZACCOUNT.AccKarat
			HAVING  (A2ZACCOUNT.AccType = 13) AND (A2ZACCOUNT.AccLocation = 2)

		
			UPDATE #Table SET Received = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 21 AND WFA2ZTRANSACTION.TrnDate = @tDate  AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.AccKarat = #Table.AccKarat AND WFA2ZTRANSACTION.Location = 2 AND WFA2ZTRANSACTION.RefTrnKeyNo = #Table.TrackingAccNo);
			
			UPDATE #Table SET Issue = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 11 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location=2 AND WFA2ZTRANSACTION.AccKarat = #Table.AccKarat AND WFA2ZTRANSACTION.RefTrnKeyNo = #Table.TrackingAccNo);
			UPDATE #Table SET Issue = #Table.Issue - (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 12 AND WFA2ZTRANSACTION.TrnDate = @tDate  AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location=2 AND WFA2ZTRANSACTION.AccKarat = #Table.AccKarat AND WFA2ZTRANSACTION.RefTrnKeyNo = #Table.TrackingAccNo);

			UPDATE #Table SET Balance = OpBalance + Issue - Received;

			DELETE FROM #Table WHERE OpBalance = 0 AND Received=0 AND Issue=0;
			
			UPDATE #Table SET TrackingPartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #Table.TrackingAccNo = A2ZPARTYCODE.PartyAccNo);


	SELECT PartyName,AccKarat,TrackingPartyName,OpBalance,Issue,Received,Balance FROM #Table;
	DROP TABLE #Table;

END














GO

