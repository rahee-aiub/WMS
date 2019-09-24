USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSTransitReport]    Script Date: 03/18/2019 9:19:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE  [dbo].[Sp_rptWMSTransitReport](@fDate VARCHAR(10), @tDate VARCHAR(10))

AS
BEGIN

DECLARE @processDate SMALLDATETIME;
DECLARE @strSQL NVARCHAR(MAX);	
DECLARE @nCount INT;
DECLARE @fYear INT;
DECLARE @tYear INT;

SET @nCount = LEFT(@fDate,4);

DECLARE @TransitPartyCode INT;
DECLARE @Karat INT;
DECLARE @Code INT;


DECLARE @GrossWt MONEY;
DECLARE @NetGrossWt MONEY;

/*

 EXECUTE Sp_rptWMSTransitReport '2019-03-16','2019-03-16'

*/


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		SET @processDate = (SELECT ProcessDate FROM A2ZACWMS..A2ZCSPARAMETER);


		
		----------------------------------------------------------------------------------

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


        CREATE TABLE #Table(
			TransitPartyCode INT,
			TransitPartyName VARCHAR(100),
			Code INT,
			CodeDesc VARCHAR(10),
			Karat INT,
			GrossWt MONEY,
			GrossReturnWt MONEY
			
			
		)

		    INSERT INTO #Table(TransitPartyCode,Code,Karat,GrossWt)
			SELECT RefTrnKeyNo,AccNo,AccKarat,SUM(TrnCreditAmt)-SUM(TrnDebitAmt) AS TotalTransit 
			FROM WFA2ZTRANSACTION 
			GROUP BY RefTrnKeyNo,AccNo,AccKarat;

			UPDATE #Table SET TransitPartyName = (SELECT PartyName FROM A2ZPARTYCODE WHERE #Table.TransitPartyCode = A2ZPARTYCODE.PartyCode);
		    UPDATE #Table SET CodeDesc = (SELECT PartyName FROM A2ZPARTYCODE WHERE #Table.Code = A2ZPARTYCODE.PartyCode);


		    SELECT TransitPartyCode,TransitPartyName,Code,CodeDesc,Karat,GrossWt FROM #Table;
		    DROP TABLE #Table;


				

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

