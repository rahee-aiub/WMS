USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_GenerateAccountBalance]    Script Date: 01/12/2019 12:28:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[SpM_GenerateAccountBalance]
AS
/*
EXECUTE SpM_GenerateAccountBalance


*/

BEGIN

	UPDATE A2ZACCOUNT SET AccBalance = 0,AccOpBal = 0; 
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @Balance  money;

    DECLARE @fYear INT;
	DECLARE @tYear INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	
	DECLARE @nCount INT;


	DECLARE @nYear INT;	


----------------------------------------------------------------------------
    SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;


    SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +		
				' FROM A2ZACWMS..A2ZTRANSACTION '; 
       
		EXECUTE (@strSQL);     


---------- Credit Op Non Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + 
            ISNULL((SELECT SUM(TrnCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			#WFA2ZTRANSACTION.TrnProcStat = 0 AND
			#WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			#WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation  AND
			#WFA2ZTRANSACTION.AccType <> 13),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = #WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = #WFA2ZTRANSACTION.Location  AND
			A2ZACCOUNT.AccType <> 13;
					
---------- Debit Op Non Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) - 
            ISNULL((SELECT SUM(TrnDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			#WFA2ZTRANSACTION.TrnProcStat = 0 AND
			#WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			#WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			#WFA2ZTRANSACTION.AccType <> 13),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = #WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = #WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccType <> 13;

---------- Credit Op Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + 
            ISNULL((SELECT SUM(TrnCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			#WFA2ZTRANSACTION.TrnProcStat = 0 AND
			#WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			#WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			#WFA2ZTRANSACTION.RefAccNo = A2ZACCOUNT.AccTrackingCode AND
			#WFA2ZTRANSACTION.AccType = 13),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = #WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = #WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccTrackingCode = #WFA2ZTRANSACTION.RefAccNo AND
			A2ZACCOUNT.AccType = 13;
					
---------- Debit Op Transit Party Balance
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) - 
            ISNULL((SELECT SUM(TrnDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			#WFA2ZTRANSACTION.TrnProcStat = 0 AND
			#WFA2ZTRANSACTION.AccKarat = A2ZACCOUNT.AccKarat AND
			#WFA2ZTRANSACTION.Location = A2ZACCOUNT.AccLocation AND
			#WFA2ZTRANSACTION.RefAccNo = A2ZACCOUNT.AccTrackingCode AND
			#WFA2ZTRANSACTION.AccType = 13),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo AND
			A2ZACCOUNT.AccKarat = #WFA2ZTRANSACTION.AccKarat AND
			A2ZACCOUNT.AccLocation = #WFA2ZTRANSACTION.Location AND
			A2ZACCOUNT.AccTrackingCode = #WFA2ZTRANSACTION.RefAccNo AND
			A2ZACCOUNT.AccType = 13;

---------------------------------- Party Balance --------------------------------------------------

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccTodaysOpBalance,0) + ISNULL(A2ZACCOUNT.AccOpBal,0);
			
						            
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance   = A2ZACCOUNT.AccOpBal;
			

			

END




















GO

