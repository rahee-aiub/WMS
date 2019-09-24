USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSpartySaleDetailsReport]    Script Date: 2019-01-09 11:39:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE  [dbo].[Sp_rptWMSpartySaleDetailsReport] (@fDate VARCHAR(10),@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSpartySaleDetailsReport '2019-01-01','2019-01-01'

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
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate BETWEEN @fDate AND @tDate AND FuncOpt IN (41,42) AND AccType = 12;


		SET @fYear = LEFT(@fDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (41,42) AND AccType = 12 AND TrnDate BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' +@tDate + '''' + '' ;
			
            
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
				Sale MONEY DEFAULT 0,
				SaleReturn MONEY DEFAULT 0,
				TotalSale MONEY DEFAULT 0,	
			)



			INSERT INTO #TEMPTABLE(AccKarat,Code,PartyCode,Sale,SaleReturn,TotalSale)
			SELECT AccKarat,TrnKeyNo,RefAccNo,SUM(TrnDebitAmt)AS Sale,SUM(TrnCreditAmt) AS SaleReturn,SUM(TrnDebitAmt)-SUM(TrnCreditAmt) AS TotalSale 
			FROM WFA2ZTRANSACTION 
			GROUP BY AccKarat,TrnKeyNo,RefAccNo;

			UPDATE #TEMPTABLE SET CodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.Code = A2ZPARTYCODE.PartyCode);
			UPDATE #TEMPTABLE SET PartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.PartyCode = A2ZPARTYCODE.PartyCode);


			SELECT * FROM #TEMPTABLE;

			DROP TABLE #TEMPTABLE;
END




GO


