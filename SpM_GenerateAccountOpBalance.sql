USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_GenerateAccountOpBalance]    Script Date: 01/12/2019 11:33:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SpM_GenerateAccountOpBalance]
AS
/*
EXECUTE SpM_GenerateAccountOpBalance


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
   

---------------------------------- Party Balance --------------------------------------------------

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ISNULL(A2ZACCOUNT.AccTodaysOpBalance,0);
			
						            
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance   = A2ZACCOUNT.AccOpBal;
			

			

END





















GO

