USE [A2ZACWMS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_PartyAccountStatementDetails]    Script Date: 1/17/2019 2:41:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SpM_PartyAccountStatementDetails](@AccNo BIGINT,@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--- @nFlag = 0 = Normal CS Account Statement
---EXECUTE SpM_PartyAccountStatementDetails 130002,'2018-11-11','2018-11-11',2



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



	DECLARE @TrnDebit18  MONEY;
	DECLARE @TrnCredit18 MONEY;

	DECLARE @TrnDebit21  MONEY;
	DECLARE @TrnCredit21 MONEY;

	DECLARE @TrnDebit22  MONEY;
	DECLARE @TrnCredit22 MONEY;
	

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

	DECLARE @VchNo nvarchar(20);

	
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


        

         INSERT INTO #WFPARTYSTATEMENT (TrnDate,VchNo,AccNo,TrnDebit18,TrnCredit18,TrnDebit21,TrnCredit21,TrnDebit22,TrnCredit22)
	     VALUES (@fDate,'- O/B -',0,ISNULL(@debitAmt18,0),ISNULL(@creditAmt18,0),ISNULL(@debitAmt21,0),ISNULL(@creditAmt21,0),ISNULL(@debitAmt22,0),ISNULL(@creditAmt22,0))

		 

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
                ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(6)) +
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
        ' WHERE  AccNo = ' + CAST(@AccNo AS VARCHAR(6)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


		EXECUTE (@strSQL);


			       
--=========== End of Get Transaction Data For Account Statement ================

      INSERT INTO #WFPARTYSTATEMENT (TrnDate,VchNo,AccNo)
	  SELECT
	  TrnDate,VchNo,AccNo
	  FROM #WFA2ZTRANSACTION GROUP BY TrnDate,VchNo,AccNo;

-----------------------------------------------------------------------------------

     DECLARE TrnTable CURSOR FOR
     SELECT TrnDate,VchNo
     FROM #WFPARTYSTATEMENT WHERE AccNo <> 0;

     OPEN TrnTable;
     FETCH NEXT FROM TrnTable INTO
     @TrnDate,@VchNo;

     WHILE @@FETCH_STATUS = 0 
	     BEGIN

		     SET @TrnDebit18 = 0;
	         SET @TrnCredit18 = 0;

	         SET @TrnDebit21  = 0;
	         SET @TrnCredit21 = 0;

	         SET @TrnDebit22  = 0;
	         SET @TrnCredit22 = 0;


		     SET @TrnDebit18 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 18);

			 SET @TrnCredit18 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 18);

			 
			 SET @TrnDebit21 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 21);

			 SET @TrnCredit21 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 21);

			 
			 SET @TrnDebit22 = (SELECT ISNULL(SUM(TrnDebitAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 22);

			 SET @TrnCredit22 = (SELECT ISNULL(SUM(TrnCreditAmt),0) 
			 FROM #WFA2ZTRANSACTION WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccKarat = 22);

			 
			 UPDATE #WFPARTYSTATEMENT SET #WFPARTYSTATEMENT.TrnDebit18 = @TrnDebit18,
			                              #WFPARTYSTATEMENT.TrnCredit18 = @TrnCredit18, 
										  #WFPARTYSTATEMENT.TrnDebit21 = @TrnDebit21,
			                              #WFPARTYSTATEMENT.TrnCredit21 = @TrnCredit21, 
										  #WFPARTYSTATEMENT.TrnDebit22 = @TrnDebit22,
			                              #WFPARTYSTATEMENT.TrnCredit22 = @TrnCredit22
	         WHERE #WFPARTYSTATEMENT.TrnDate = @TrnDate AND VchNo = @VchNo; 

		  		
     FETCH NEXT FROM TrnTable INTO
         @TrnDate,@VchNo;

	     END

CLOSE TrnTable; 
DEALLOCATE TrnTable;
    

  TRUNCATE TABLE #WFA2ZTRANSACTION;

  SELECT * FROM #WFPARTYSTATEMENT

 

END





