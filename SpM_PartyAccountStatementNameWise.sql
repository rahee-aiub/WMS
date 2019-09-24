USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_PartyAccountStatementNameWise]    Script Date: 01/17/2019 2:17:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









ALTER PROCEDURE [dbo].[SpM_PartyAccountStatementNameWise](@AccNo BIGINT,@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--- @nFlag = 0 = Normal CS Account Statement
---EXECUTE SpM_PartyAccountStatementNameWise 120001,'2018-11-10','2018-11-11',2



BEGIN	
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @xDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

    DECLARE @opBalance18 MONEY;
    DECLARE @opBalance21 MONEY;
	DECLARE @opBalance22 MONEY;
	

	DECLARE @debitAmt18 MONEY;
	DECLARE @creditAmt18 MONEY;

	DECLARE @debitAmt21 MONEY;
	DECLARE @creditAmt21 MONEY;

	DECLARE @debitAmt22 MONEY;
	DECLARE @creditAmt22 MONEY;
	

    DECLARE @pYear INT;
	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @atyClass INT;

    DECLARE @ReadFlag INT;
    DECLARE @processDate SMALLDATETIME;

	DECLARE @ChkTrnDate SMALLDATETIME;
	DECLARE @ChkCode18   INT;
	DECLARE @ChkCode21   INT;
	DECLARE @ChkCode22   INT;

    DECLARE @tmm INT;
    DECLARE @tdd INT;
    DECLARE @xFlag INT;
    DECLARE @yFlag INT;
    
    DECLARE @xShow INT;

	DECLARE @KeyNo INT;

	DECLARE @AccType  INT;
	DECLARE @AccountNo VARCHAR(10);
	DECLARE @AccCurrency INT;

	DECLARE @RecFlag  INT;
	DECLARE @Id  INT;
	DECLARE @wTrnDate  SMALLDATETIME;
	DECLARE @TrnDate  SMALLDATETIME;
	DECLARE @RefTrnKeyNo  INT;
	DECLARE @AccKarat  INT;
	DECLARE @TrnDrCr  INT;
	DECLARE @TrnDebitAmt MONEY;
	DECLARE @TrnCreditAmt MONEY;

	DECLARE @Code18 INT;
	DECLARE @Code21 INT;
	DECLARE @Code22 INT;

	DECLARE @Write18 INT;
	DECLARE @Id18 INT;

	DECLARE @Write21 INT;
	DECLARE @Id21 INT;

	DECLARE @Write22 INT;
	DECLARE @Id22 INT;

	SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;

	SELECT * INTO #WFPARTYSTATEMENT FROM WFPARTYSTATEMENT WHERE ID = 0;
	TRUNCATE TABLE #WFPARTYSTATEMENT;
    


    SET @processDate = (SELECT ProcessDate FROM A2ZACWMS..A2ZCSPARAMETER);

    SET @pYear = YEAR(@processDate);
    SET @fYear = LEFT(@fDate,4);
    SET @xFlag = 2;  
    IF @pYear <> @fYear
        BEGIN
           SET @xFlag = 1;
        END

    IF @fDate = @processDate
       BEGIN
            SET @ReadFlag = 0;
            SET @xFlag = 0;  
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END

	   


	     UPDATE A2ZACCOUNT SET AccOpBal = 0 where AccNo = @AccNo;

		 IF @ReadFlag = 0
            BEGIN
                 UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccTodaysOpBalance           
                 FROM A2ZACCOUNT WHERE AccNo = @AccNo AND AccLocation = @nFlag;    
            END  

          

         IF @ReadFlag = 1
            BEGIN
                 EXECUTE SpM_CSGenerateTransitPartyOpBalSingle @AccNo, @fDate, @tDate, @nFlag;
            END


         SET @opBalance18 = (SELECT TOP(1) ISNULL((AccOpBal),0) FROM A2ZACCOUNT WHERE AccNo = @AccNo AND AccKarat = 18 AND AccLocation = @nFlag);
	     SET @opBalance21 = (SELECT TOP(1) ISNULL((AccOpBal),0) FROM A2ZACCOUNT WHERE AccNo = @AccNo AND AccKarat = 21 AND AccLocation = @nFlag);
		 SET @opBalance22 = (SELECT TOP(1) ISNULL((AccOpBal),0) FROM A2ZACCOUNT WHERE AccNo = @AccNo AND AccKarat = 22 AND AccLocation = @nFlag);
	     

	     SET @debitAmt18 = 0;
	     SET @creditAmt18 = 0;

		 SET @debitAmt21 = 0;
	     SET @creditAmt21 = 0;

		 SET @debitAmt22 = 0;
	     SET @creditAmt22 = 0;
	
	
	     IF @opBalance18 > 0
	        BEGIN
			    SET @creditAmt18 = ABS(@opBalance18);
	        END
	     ELSE
		     BEGIN
			     SET @debitAmt18 = ABS(@opBalance18);
		     END

         IF @opBalance21 > 0
	        BEGIN
			    SET @creditAmt21 = ABS(@opBalance21);
	        END
	     ELSE
		     BEGIN
			     SET @debitAmt21 = ABS(@opBalance21);
		     END

         IF @opBalance22 > 0
	        BEGIN
			    SET @creditAmt22 = ABS(@opBalance22);
	        END
	     ELSE
		     BEGIN
			     SET @debitAmt22 = ABS(@opBalance22);
		     END


         INSERT INTO #WFPARTYSTATEMENT (TrnDate,Code18,Name18,TrnDebit18,TrnCredit18,Code21,Name21,TrnDebit21,TrnCredit21,Code22,Name22,TrnDebit22,TrnCredit22)
	     VALUES (@fDate,0,'- O/B -',ISNULL(@debitAmt18,0),ISNULL(@creditAmt18,0),0,'- O/B -',ISNULL(@debitAmt21,0),ISNULL(@creditAmt21,0),0,'- O/B -',ISNULL(@debitAmt22,0),ISNULL(@creditAmt22,0))

		 

		 --=========== Get Transaction Data For Account Statement ================
	     SET @fYear = LEFT(@fDate,4);
	     SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear
	    WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (TrnDate,VchNo,TrnKeyNo,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,TrnKeyNo,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +	
				' FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +	
                ' WHERE FuncOpt <> 1 AND AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND Location = ' + CAST(@nFlag AS VARCHAR(2)) +
       	        ' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


				 
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 


		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (TrnDate,VchNo,TrnKeyNo,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,TrnKeyNo,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +		
				' FROM A2ZACWMS..A2ZTRANSACTION ' + 
        ' WHERE  FuncOpt <> 1 AND AccNo = ' + CAST(@AccNo AS VARCHAR(6)) + ' AND Location = ' + CAST(@nFlag AS VARCHAR(2)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


		EXECUTE (@strSQL);

		
--=========== End of Get Transaction Data For Account Statement ================


     INSERT INTO #WFPARTYSTATEMENT (TrnDate,Code18,RecFlag)
	 SELECT
	 TrnDate,RefTrnKeyNo,0
	 FROM #WFA2ZTRANSACTION WHERE AccKarat = 18 GROUP BY TrnDate,RefTrnKeyNo;

	 INSERT INTO #WFPARTYSTATEMENT (TrnDate,Code21,RecFlag)
	 SELECT
	 TrnDate,RefTrnKeyNo,0
	 FROM #WFA2ZTRANSACTION WHERE AccKarat = 21 GROUP BY TrnDate,RefTrnKeyNo;

	 INSERT INTO #WFPARTYSTATEMENT (TrnDate,Code22,RecFlag)
	 SELECT
	 TrnDate,RefTrnKeyNo,0
	 FROM #WFA2ZTRANSACTION WHERE AccKarat = 22 GROUP BY TrnDate,RefTrnKeyNo;


	 ---------------------------------------------------------------

	 SET @Write18 = 0;
	 SET @Write21 = 0;
	 SET @Write22 = 0;

---------------------------------------------------------------------------------

     DECLARE TrnTable CURSOR FOR
     SELECT Id,TrnDate,Code18,Code21,Code22,RecFlag
     FROM #WFPARTYSTATEMENT WHERE RecFlag = 0;

     OPEN TrnTable;
     FETCH NEXT FROM TrnTable INTO
     @Id,@TrnDate,@Code18,@Code21,@Code22,@RecFlag;

     WHILE @@FETCH_STATUS = 0 
	     BEGIN
			  IF (@Code18 = 0 OR @Code18 IS NULL) AND @Write18 = 0 
				 BEGIN 
					 SET @Write18 = 1;
					 SET @Id18 = @Id;
                 END
              
			  IF (@Code21 = 0 OR @Code21 IS NULL) AND @Write21 = 0 
				 BEGIN 
					 SET @Write21 = 1;
					 SET @Id21 = @Id;
                 END

              IF (@Code22 = 0 OR @Code22 IS NULL) AND @Write22 = 0 
				 BEGIN 
					 SET @Write22 = 1;
					 SET @Id22 = @Id;
                 END
				            		

              IF @Code18 <> 0 AND @Write18 = 1 
				 BEGIN 
				     UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.Code18 = @Code18 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id18; 

					 UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.RecFlag = 1 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id; 

					 SET @Write18 = 0;
					 SET @Id18 = 0;
                 END
              
			  IF @Code21 <> 0 AND @Write21 = 1 
				 BEGIN 
				     UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.Code21 = @Code21 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id21; 

					 UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.RecFlag = 1 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id; 

					 SET @Write21 = 0;
					 SET @Id21 = 0;
                 END

              IF @Code22 <> 0 AND @Write22 = 1 
				 BEGIN 
				     UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.Code22 = @Code22 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id22; 

					 UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.RecFlag = 1 
	                 WHERE #WFPARTYSTATEMENT.Id = @Id; 

					 SET @Write22 = 0;
					 SET @Id22 = 0;
                 END

		
     FETCH NEXT FROM TrnTable INTO
         @Id,@TrnDate,@Code18,@Code21,@Code22,@RecFlag;

	     END

CLOSE TrnTable; 
DEALLOCATE TrnTable;
    

	DELETE FROM #WFPARTYSTATEMENT  WHERE RecFlag = 1;


	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnDebit18 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18 AND #WFA2ZTRANSACTION.FuncOpt <> 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18 AND #WFA2ZTRANSACTION.FuncOpt <> 12;


	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit18 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18; 

	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit18 = TrnCredit18 - (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18 AND #WFA2ZTRANSACTION.FuncOpt = 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code18 AND
	#WFA2ZTRANSACTION.AccKarat = 18 AND #WFA2ZTRANSACTION.FuncOpt = 12; 



	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnDebit21 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21 AND #WFA2ZTRANSACTION.FuncOpt <> 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21 AND #WFA2ZTRANSACTION.FuncOpt <> 12;


	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit21 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21; 

	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit21 = TrnCredit21 - (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21 AND #WFA2ZTRANSACTION.FuncOpt = 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code21 AND
	#WFA2ZTRANSACTION.AccKarat = 21 AND #WFA2ZTRANSACTION.FuncOpt = 12; 


	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnDebit22 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22 AND #WFA2ZTRANSACTION.FuncOpt <> 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22 AND #WFA2ZTRANSACTION.FuncOpt <> 12;


	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit22 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22; 

	UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnCredit22 = TrnCredit22 - (SELECT ISNULL(SUM(TrnDebitAmt),0) 
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22 AND #WFA2ZTRANSACTION.FuncOpt = 12)
	FROM #WFA2ZTRANSACTION,#WFPARTYSTATEMENT
	WHERE #WFA2ZTRANSACTION.TrnDate = #WFPARTYSTATEMENT.TrnDate AND 
	#WFA2ZTRANSACTION.RefTrnKeyNo = #WFPARTYSTATEMENT.Code22 AND
	#WFA2ZTRANSACTION.AccKarat = 22 AND #WFA2ZTRANSACTION.FuncOpt = 12; 

	

	--TRUNCATE TABLE #WFA2ZTRANSACTION;


	 	
  UPDATE #WFPARTYSTATEMENT SET Name18 = (SELECT PartyName FROM A2ZPARTYCODE 
  WHERE #WFPARTYSTATEMENT.Code18 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0)
  FROM #WFPARTYSTATEMENT,A2ZPARTYCODE
  WHERE #WFPARTYSTATEMENT.Code18 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0;

  UPDATE #WFPARTYSTATEMENT SET Name21 = (SELECT PartyName FROM A2ZPARTYCODE 
  WHERE #WFPARTYSTATEMENT.Code21 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0)
  FROM #WFPARTYSTATEMENT,A2ZPARTYCODE
  WHERE #WFPARTYSTATEMENT.Code21 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0;


  UPDATE #WFPARTYSTATEMENT SET Name22 = (SELECT PartyName FROM A2ZPARTYCODE 
  WHERE #WFPARTYSTATEMENT.Code22 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0)
  FROM #WFPARTYSTATEMENT,A2ZPARTYCODE
  WHERE #WFPARTYSTATEMENT.Code22 = A2ZPARTYCODE.PartyCode AND #WFPARTYSTATEMENT.RecFlag = 0;


 
  
  SELECT * FROM #WFPARTYSTATEMENT

  


END





GO

