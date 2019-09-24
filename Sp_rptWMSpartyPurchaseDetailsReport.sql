USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSpartyPurchaseDetailsReport]    Script Date: 2019-01-09 9:33:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_rptWMSpartyPurchaseDetailsReport] (@fDate VARCHAR(10),@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSpartyPurchaseDetailsReport '2019-01-01','2019-01-01'

*/



DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;



		TRUNCATE TABLE WFA2ZTRANSACTION;


		INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
		SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate BETWEEN @fDate AND @tDate AND FuncOpt IN (1,2) AND AccType = 12;


		SET @fYear = LEFT(@fDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (1,2) AND AccType = 12 AND TrnDate BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 
		

	   CREATE TABLE #TEMPTABLE
			(		
				AccKarat INT,
				Code INT,
				CodeName VARCHAR(5),
				PartyCode INT,
				PartyName VARCHAR(50),
				Purchase MONEY DEFAULT 0,
				PurchaseReturn MONEY DEFAULT 0,
				TotalPurchase MONEY DEFAULT 0,	
			)



			INSERT INTO #TEMPTABLE(AccKarat,Code,PartyCode,Purchase,PurchaseReturn,TotalPurchase)
			SELECT AccKarat,TrnKeyNo,RefAccNo,SUM(TrnCreditAmt)AS Purchase,SUM(TrnDebitAmt) AS PurchaseReturn,SUM(TrnCreditAmt)-SUM(TrnDebitAmt) AS TotalPurchase 
			FROM WFA2ZTRANSACTION 
			GROUP BY AccKarat,TrnKeyNo,RefAccNo;

			UPDATE #TEMPTABLE SET CodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.Code = A2ZPARTYCODE.PartyCode);
			UPDATE #TEMPTABLE SET PartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.PartyCode = A2ZPARTYCODE.PartyCode);


			SELECT * FROM #TEMPTABLE;

			DROP TABLE #TEMPTABLE;
END



GO


