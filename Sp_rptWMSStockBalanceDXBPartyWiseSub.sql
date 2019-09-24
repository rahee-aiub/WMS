USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSStockBalanceDXBPartyWiseSub]    Script Date: 03/19/2019 4:05:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE  [dbo].[Sp_rptWMSStockBalanceDXBPartyWiseSub] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSStockBalanceDXBPartyWiseSub '2019-03-16'

*/

DECLARE @PartyAccNo bigint;
DECLARE @PartyPurchaseCode int;

DECLARE @ProcDate SMALLDATETIME;
DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;

DECLARE @Purchase MONEY;

SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

IF YEAR(@ProcDate) = YEAR(@tDate) AND 
	   MONTH(@ProcDate) = MONTH(@tDate) AND 
	   DAY(@ProcDate) = DAY(@tDate) 

	   BEGIN		  
	        EXECUTE SpM_GeneratePurchaseAccOpBalance;
			
	   END

    ELSE
	   BEGIN
			EXECUTE SpM_GeneratePurchaseAccOpBalancePrevious @tDate ;		
			
	   END

	   	TRUNCATE TABLE WFA2ZTRANSACTION;

			 INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
				  SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE FuncOpt IN(1,2,11,12) AND TrnDate = @tDate;


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE FuncOpt IN (1,2,11,12) AND TrnDate = ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 

	   CREATE TABLE #Table
	   (
			AccType INT,
			AccNo INT,
			AccKarat INT,
			AccLocation INT,
			OpBalance MONEY,
			Purchase MONEY,
			Issue MONEY,
			PartyName VARCHAR(50),
			Balance MONEY
	   )


			INSERT INTO #Table	(AccType,AccNo,AccKarat,AccLocation,OpBalance,PartyName,Purchase,Issue)
			SELECT  A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZACCOUNT.AccBalance, A2ZPARTYCODE.PartyName,0,0
			FROM    A2ZACCOUNT LEFT OUTER JOIN
			A2ZPARTYCODE ON  A2ZACCOUNT.AccNo = A2ZPARTYCODE.PartyAccNo
			GROUP BY A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZPARTYCODE.PartyName,A2ZACCOUNT.AccBalance
			HAVING  (A2ZACCOUNT.AccType = 14) AND (A2ZACCOUNT.AccLocation = 1)

									
			DECLARE partyTable CURSOR FOR
            SELECT PartyAccNo,PartyPurchaseCode
            FROM A2ZPARTYCODE WHERE PartyPurchaseCode <> 0;

            OPEN partyTable;
            FETCH NEXT FROM partyTable INTO
            @PartyAccNo,@PartyPurchaseCode;

			print @PartyAccNo;
			print @PartyPurchaseCode;

            WHILE @@FETCH_STATUS = 0 
	            BEGIN
					
				    UPDATE #Table SET Purchase = #Table.Purchase + (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 1 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo)
					FROM WFA2ZTRANSACTION, #Table
					WHERE WFA2ZTRANSACTION.FuncOpt = 1 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND #Table.AccKarat = WFA2ZTRANSACTION.AccKarat AND #Table.AccNo = @PartyPurchaseCode;

					UPDATE #Table SET Purchase = #Table.Purchase - (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 2 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo)
					FROM WFA2ZTRANSACTION, #Table
					WHERE WFA2ZTRANSACTION.FuncOpt = 2 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND #Table.AccNo = @PartyPurchaseCode;
		
		            UPDATE #Table SET Issue = #Table.Issue + (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 11 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND WFA2ZTRANSACTION.Location=2)
					FROM WFA2ZTRANSACTION, #Table
					WHERE WFA2ZTRANSACTION.FuncOpt = 11 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND #Table.AccNo = @PartyPurchaseCode AND WFA2ZTRANSACTION.Location=2;

					UPDATE #Table SET Issue = #Table.Issue - (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 12 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND WFA2ZTRANSACTION.Location=2)
					FROM WFA2ZTRANSACTION, #Table
					WHERE WFA2ZTRANSACTION.FuncOpt = 12 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = @PartyAccNo AND #Table.AccNo = @PartyPurchaseCode AND WFA2ZTRANSACTION.Location=2;


		            FETCH NEXT FROM partyTable INTO
                    @PartyAccNo,@PartyPurchaseCode;        
	            END

            CLOSE partyTable; 
            DEALLOCATE partyTable;

			
			UPDATE #Table SET Balance = OpBalance + Purchase - Issue;

			DELETE FROM #Table WHERE OpBalance = 0 AND Purchase=0 AND Issue=0;

	SELECT PartyName,SUM(OpBalance) AS OpBalance ,SUM(Purchase) AS Purchase,SUM(Issue) AS Issue,SUM(Balance) AS Balance FROM #Table GROUP BY PartyName;

	DROP TABLE #Table;

END


















GO

