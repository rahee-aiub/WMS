USE [master]
GO

/****** Object:  StoredProcedure [dbo].[Sp_BackUp]    Script Date: 11/16/2018 4:17:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_BackUp](@DataItems VARCHAR(max),@txtTo VARCHAR(max),@BackItems VARCHAR(max))
AS

--EXECUTE A2ZCSCUBS..Sp_ShrinkDatabase ;

BEGIN
	
	DECLARE @strSQL NVARCHAR(MAX);
 
    SET @strSQL ='BACKUP DATABASE ' + @DataItems + ' TO DISK = ' + '''' + @txtTo + @BackItems + '''';

 EXECUTE (@strSQL);	

	

END






GO

