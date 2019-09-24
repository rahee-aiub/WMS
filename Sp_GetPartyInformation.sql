USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_GetPartyInformation]    Script Date: 02/17/2019 3:10:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











ALTER PROCEDURE  [dbo].[Sp_GetPartyInformation](@PartyCode INT)

AS
BEGIN

 /*

 EXECUTE Sp_GetPartyInformation 100001

 */


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		SELECT GroupCode, GroupName, PartyCode, PartyName, PartyAddresss, PartyMobileNo, PartyEmail,PartyPurchaseCode,PartySalesCode
		FROM A2ZPARTYCODE WHERE PartyCode = @PartyCode;



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

