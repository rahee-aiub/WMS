USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_MoveDailyTransaction]    Script Date: 01/15/2019 2:17:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










ALTER PROCEDURE  [dbo].[Sp_MoveDailyTransaction] (@fYY VARCHAR(4))

AS
--EXECUTE Sp_MoveDailyTransaction 2018

BEGIN


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

DECLARE @branchNo INT;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

-------   Generate Profit/Loss Transaction for P/L Code =========
	--SET @PLCode = (SELECT PLGLCode FROM A2ZGLCUBS..A2ZGLPARAMETER);
	SET @processDate = (SELECT ProcessDate FROM A2ZACWMS..A2ZCSPARAMETER);


    

	SET @strSQL = 'INSERT INTO A2ZACWMST' + CAST(@fYY AS VARCHAR(4)) + '..A2ZTRANSACTION (TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZACWMS..A2ZTRANSACTION ';

	EXECUTE (@strSQL);


----------  Create CS Opening Account Balance

        TRUNCATE TABLE WFA2ZTRANSACTION;


		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZACWMS..A2ZTRANSACTION ';

    
		
		EXECUTE (@strSQL);

        UPDATE A2ZACCOUNT SET AccOpBal = 0;

		


---------- Credit Op Non Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + 
            ISNULL((SELECT SUM(TrnCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation  AND
			WFA2ZTRANSACTION.AccType <> 13),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = WFA2ZTRANSACTION.Location  AND
			A2ZACCOUNT.AccType <> 13;
					
---------- Debit Op Non Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) - 
            ISNULL((SELECT SUM(TrnDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			WFA2ZTRANSACTION.AccType <> 13),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccType <> 13;

---------- Credit Op Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + 
            ISNULL((SELECT SUM(TrnCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			WFA2ZTRANSACTION.RefAccNo = A2ZACCOUNT.AccTrackingCode AND
			WFA2ZTRANSACTION.AccType = 13),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccType = 13;
					
---------- Debit Op Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) - 
            ISNULL((SELECT SUM(TrnDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND
			WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			WFA2ZTRANSACTION.RefAccNo = A2ZACCOUNT.AccTrackingCode AND
			WFA2ZTRANSACTION.AccType = 13),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccType = 13;


---------------------------------------------------------------------------------------------------
	   					             
-------------------------------------Party Balance---------------------------------------------		   
           UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = ISNULL(A2ZACCOUNT.AccTodaysOpBalance,0) + ISNULL(A2ZACCOUNT.AccOpBal,0);			                      
		  
		 

TRUNCATE TABLE A2ZACWMS..A2ZTRANSACTION;

COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH


END;









GO

