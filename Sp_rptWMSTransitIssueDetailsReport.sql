USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSTransitIssueDetailsReport]    Script Date: 2019-01-09 11:43:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_rptWMSTransitIssueDetailsReport] (@fDate VARCHAR(10),@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSTransitIssueDetailsReport '2019-01-01','2019-01-01'

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
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate BETWEEN @fDate AND @tDate AND FuncOpt IN (11,12) AND AccType = 12 AND Location = 2;


		SET @fYear = LEFT(@fDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (11,12) AND AccType = 12 AND Location = 2 AND TrnDate BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' +@tDate + '''' + '' ;
			
            
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
				Transit MONEY DEFAULT 0,
				TransitReturn MONEY DEFAULT 0,
				TotalTransit MONEY DEFAULT 0,	
			)



			INSERT INTO #TEMPTABLE(AccKarat,Code,PartyCode,Transit,TransitReturn,TotalTransit)
			SELECT AccKarat,TrnKeyNo,RefAccNo,SUM(TrnCreditAmt)AS Transit,SUM(TrnDebitAmt) AS TransitReturn,SUM(TrnCreditAmt)-SUM(TrnDebitAmt) AS TotalTransit 
			FROM WFA2ZTRANSACTION 
			GROUP BY AccKarat,TrnKeyNo,RefAccNo;

			UPDATE #TEMPTABLE SET CodeName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.Code = A2ZPARTYCODE.PartyCode);
			UPDATE #TEMPTABLE SET PartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #TEMPTABLE.PartyCode = A2ZPARTYCODE.PartyCode);


			SELECT * FROM #TEMPTABLE;

			DROP TABLE #TEMPTABLE;
END





GO


