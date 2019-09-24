USE [master]
GO

/****** Object:  StoredProcedure [dbo].[Sp_Restore]    Script Date: 11/16/2018 4:17:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_Restore](@DataItems VARCHAR(max),@txtFrom VARCHAR(max),@BackItems VARCHAR(max))
AS

BEGIN
	
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @strSQL2 NVARCHAR(MAX);
	DECLARE @strSQL3 NVARCHAR(MAX);
	

	SET @strSQL='ALTER DATABASE '  + @DataItems + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE ';
	EXECUTE (@strSQL);


	SET @strSQL2 ='RESTORE DATABASE ' + @DataItems + '  FROM DISK = ' + '''' + @txtFrom + @BackItems +  ''' WITH REPLACE'; 
	EXECUTE (@strSQL2);

	SET @strSQL= 'ALTER DATABASE ' + @DataItems + ' SET MULTI_USER ';
	EXECUTE (@strSQL3);
	

	PRINT @strSQL



END




GO

