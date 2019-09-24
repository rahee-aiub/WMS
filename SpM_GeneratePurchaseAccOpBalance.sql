USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_GeneratePurchaseAccOpBalance]    Script Date: 03/19/2019 1:43:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[SpM_GeneratePurchaseAccOpBalance]
AS
/*
EXECUTE SpM_GeneratePurchaseAccOpBalance


*/

BEGIN

	UPDATE A2ZACCOUNT SET AccBalance = 0,AccOpBal = 0 WHERE AccType = 14; 
	
	DECLARE @PartyAccNo bigint;
	DECLARE @PartyPurchaseCode int;

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



	DECLARE partyTable CURSOR FOR
    SELECT PartyAccNo,PartyPurchaseCode
    FROM A2ZPARTYCODE WHERE PartyPurchaseCode <> 0;

    OPEN partyTable;
    FETCH NEXT FROM partyTable INTO
    @PartyAccNo,@PartyPurchaseCode;

    WHILE @@FETCH_STATUS = 0 
	    BEGIN

		     UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + (SELECT AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccNo = @PartyAccNo AND AccLocation = 1 AND AccKarat = 22)
			 WHERE AccPartyNo = @PartyPurchaseCode AND AccLocation = 1 AND AccKarat = 22

             UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + (SELECT AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccNo = @PartyAccNo AND AccLocation = 1 AND AccKarat = 21)
			 WHERE AccPartyNo = @PartyPurchaseCode AND AccLocation = 1 AND AccKarat = 21

			 UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccOpBal,0) + (SELECT AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccNo = @PartyAccNo AND AccLocation = 1 AND AccKarat = 18)
			 WHERE AccPartyNo = @PartyPurchaseCode AND AccLocation = 1 AND AccKarat = 18


		     FETCH NEXT FROM partyTable INTO
             @PartyAccNo,@PartyPurchaseCode;        
	    END

CLOSE partyTable; 
DEALLOCATE partyTable;


----------------------------------------------------------------------------
       
			
						            
  UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance   = A2ZACCOUNT.AccOpBal WHERE AccType = 14;
	

			

END























GO

