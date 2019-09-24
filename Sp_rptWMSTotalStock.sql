USE [A2ZACWMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptWMSTotalStock]    Script Date: 03/20/2019 3:19:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE  [dbo].[Sp_rptWMSTotalStock] (@tDate VARCHAR(10))
AS
BEGIN


/* 

 EXECUTE Sp_rptWMSTotalStock '2019-03-14'

*/

DECLARE @ProcDate SMALLDATETIME;

SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;
DECLARE @TotalSale MONEY;

DECLARE @DhakaStockOpenning MONEY;
DECLARE @DXBStockOpenning MONEY;

DECLARE @RepHead2  INT;
DECLARE @RepSubHead2  INT;
DECLARE @Location2 INT;
DECLARE @LocationDesc2 NVARCHAR(50);
DECLARE @ItemGroupCode2 INT;
DECLARE @ItemGroupName2 NVARCHAR(50);

DECLARE @Karat2   INT;
DECLARE @Purity2  MONEY;
DECLARE @GrossWt2 MONEY;
DECLARE @StoneWt2 MONEY;
DECLARE @NetWt2 MONEY;
DECLARE @PureWt2 MONEY;
DECLARE @MakingValue2 MONEY;
DECLARE @StoneMakingValue2 MONEY;
DECLARE @TotalValue2 MONEY;
DECLARE @CarringValue2 MONEY;


DECLARE @RepHead3  INT;
DECLARE @RepSubHead3  INT;
DECLARE @Location3 INT;
DECLARE @LocationDesc3 NVARCHAR(50);
DECLARE @ItemGroupCode3 INT;
DECLARE @ItemGroupName3 NVARCHAR(50);
DECLARE @Karat3   INT;
DECLARE @Purity3  MONEY;
DECLARE @GrossWt3 MONEY;
DECLARE @StoneWt3 MONEY;
DECLARE @NetWt3 MONEY;
DECLARE @PureWt3 MONEY;
DECLARE @MakingValue3 MONEY;
DECLARE @StoneMakingValue3 MONEY;
DECLARE @TotalValue3 MONEY;
DECLARE @CarringValue3 MONEY;


DECLARE @GrossWt4 MONEY;
DECLARE @StoneWt4 MONEY;
DECLARE @NetWt4 MONEY;
DECLARE @PureWt4 MONEY;
DECLARE @MakingValue4 MONEY;
DECLARE @StoneMakingValue4 MONEY;
DECLARE @TotalValue4 MONEY;
DECLARE @CarringValue4 MONEY;


DECLARE @processDate SMALLDATETIME;
DECLARE @PRvDate SMALLDATETIME;


DECLARE @AllStockOpening  MONEY;
DECLARE @DubaiStockOpening  MONEY;
DECLARE @DubaiParchase  MONEY;
DECLARE @DubaiStockOut  MONEY;
DECLARE @DubaiStockClosing MONEY;
DECLARE @DhakaStockOpening  MONEY;
DECLARE @DhakaStockIn  MONEY;
DECLARE @DhakaSaleDetails  MONEY;
DECLARE @DhakaStockClosing  MONEY;
DECLARE @DhakaStockAfterSales  MONEY;
DECLARE @AllStockClosing  MONEY;
DECLARE @TransitReceiveDetails  MONEY;
DECLARE @TransitStockOpening  MONEY;

DECLARE @TransitIssueReceive  MONEY;
DECLARE @TotalTransitOut  MONEY;
DECLARE @TransitStockClosing  MONEY;



DECLARE @RecLineNo INT;
DECLARE @LineSL INT;

DECLARE @WriteFlag INT;

DECLARE @PartName NVARCHAR(100);
DECLARE @GrossWt  MONEY;


		TRUNCATE TABLE WFA2ZTRANSACTION;


		INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch)
		SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, 
                         TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, 
                         TrnSwitch FROM A2ZTRANSACTION WHERE TrnDate BETWEEN @tDate AND @tDate;


		SET @fYear = LEFT(@tDate,4);
		SET @tYear = LEFT(@tDate,4);

		SET @nCount = @fYear;

		

		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION(TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate,TrnSwitch) ' +
					'SELECT TrnDate,VchNo,TrnKeyNo,AccType,AccNo,RefTrnKeyNo,RefAccType,RefAccNo,AccKarat,Location,FuncOpt,FuncOptDesc,PayType,TrnType, ' +
					'TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnAmount,TrnDesc,TrnVchType,TrnChqNo,TrnCSGL,TrnFlag,TrnProcStat, TrnSysUser, UserID, VerifyUserID, CreateDate, ' +
					'TrnSwitch FROM A2ZACWMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnDate BETWEEN ' + '''' +@tDate + '''' + ' AND ' + '''' +@tDate + '''' + '' ;
			
            
				EXECUTE (@strSQL);            

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 


   TRUNCATE TABLE WFTOTALSTOCKDETAILS;
   SET @processDate = (SELECT ProcessDate FROM A2ZACWMS..A2ZCSPARAMETER);
   --SET @PRvDate = (SELECT TOP 1 TrnDate FROM A2ZEXCHANGERATE WHERE CurrencyCode = 99 AND TrnDate < @fDate ORDER BY TrnDate DESC);


	CREATE TABLE #TempStockDetails(
			LNSRL_1 INT,
			SORT_SRL2 INT,
			SORT_SL3 INT,
			
			Head1 NVARCHAR(100),
			
			Head2 NVARCHAR(100),
			Details1 NVARCHAR(100),
			DetailsAmt1 MONEY,
			HeadAmt1 MONEY,
			Head2Amt1 MONEY,
			Head3 NVARCHAR(100),
			Head4 NVARCHAR(100),
			Details2 NVARCHAR(100),
			DetailsAmt2 MONEY,
			HeadAmt2 MONEY,
			Head2Amt2 MONEY,
			SL INT,
			LineSL INT
			);


		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 1,1,0,'Total Stock Opening',1;
		
		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head3,SL) 
		SELECT 2,2,0,'DXB Stock Opening','Dhaka Stock Opening',2;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head3,SL) 
		SELECT 3,3,0,'Purchase Details','Transit Receive Details',3;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head2,Head4,SL) 
		SELECT 9,3,5,'Total Purchase in DXB','Total Received',5;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head3,SL) 
		SELECT 3,4,0,'Issue Details','Sales Details',6;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head2,Head4,SL) 
		SELECT 9,4,5,'Total Issue','Total Sales After Return',9;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 9,5,0,'DXB Stock Closing',10;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 10,6,0,'Transit Stock Opeing',11;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 10,7,0,'Transit Issue Receive',12;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 10,8,0,'Total Transit Out',13;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 10,9,0,'Transit Stock Closing Details',14;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head3,SL) 
		SELECT 11,10,0,'Transit Stock Closing','Dhaka Stock Closing',15;

		INSERT INTO #TempStockDetails (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,SL) 
		SELECT 99,10,0,'Total Stock Closing',16;






IF YEAR(@ProcDate) = YEAR(@tDate) AND 
	   MONTH(@ProcDate) = MONTH(@tDate) AND 
	   DAY(@ProcDate) = DAY(@tDate) 

	   BEGIN		  
	        EXECUTE SpM_GenerateAccountOpBalance;
			
	   END

    ELSE
	   BEGIN
			EXECUTE SpM_GenerateAccountOpBalancePrevious @tDate ;		
			
	   END

	   CREATE TABLE #Table
	   (
			AccType INT,
			AccNo INT,
			AccKarat INT,
			AccLocation INT,
			OpBalance MONEY,
			Received MONEY,
			Sale MONEY,
			PartyName VARCHAR(50),
			Balance MONEY
	   )


			INSERT INTO #Table	(AccType,AccNo,AccKarat,AccLocation,OpBalance,PartyName)
			SELECT  A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZACCOUNT.AccBalance, A2ZPARTYCODE.PartyName
			FROM    A2ZACCOUNT LEFT OUTER JOIN
			A2ZPARTYCODE ON  A2ZACCOUNT.AccNo = A2ZPARTYCODE.PartyAccNo
			GROUP BY A2ZACCOUNT.AccType, A2ZACCOUNT.AccNo, A2ZACCOUNT.AccKarat, A2ZACCOUNT.AccLocation, A2ZPARTYCODE.PartyName,A2ZACCOUNT.AccBalance
			HAVING  (A2ZACCOUNT.AccType IN(12,13,20)) AND (A2ZACCOUNT.AccLocation IN (3,2,1))


		

			UPDATE #Table SET Received = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 21 AND WFA2ZTRANSACTION.TrnDate = @tDate  AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.AccKarat = #Table.AccKarat AND WFA2ZTRANSACTION.Location = 3 AND AccType = 12);		
			UPDATE #Table SET Sale = (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 41 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location = 3);
			UPDATE #Table SET Sale = #Table.Sale - (SELECT ISNULL(SUM(TrnAmount),0) FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.FuncOpt = 42 AND WFA2ZTRANSACTION.TrnDate = @tDate AND WFA2ZTRANSACTION.AccKarat=#Table.AccKarat AND WFA2ZTRANSACTION.AccNo = #Table.AccNo AND WFA2ZTRANSACTION.Location = 3);
			UPDATE #Table SET Balance = OpBalance + Received - Sale;

			DELETE FROM #Table WHERE OpBalance = 0 AND Received=0 AND Sale=0;

			 CREATE TABLE #Table1
			   (
					AccType INT,
					AccNo INT,
					AccKarat INT,
					AccLocation INT,
					OpBalance MONEY,
					Received MONEY,
					Sale MONEY,
					PartyName VARCHAR(50),
					Balance MONEY
				)


	

	
-------------------------------Stock Opening--------------------------------------
	SET @DhakaStockOpening = (SELECT SUM(OpBalance) FROM #Table where AccType = 12 AND AccLocation=3);
	SET @DubaiStockOpening = (SELECT SUM(OpBalance) FROM #Table where AccType = 12 AND AccLocation=1);
	SET @TransitStockOpening = (SELECT SUM(OpBalance) FROM #Table where AccType = 12 AND AccLocation=2);
	SET @AllStockOpening = @DhakaStockOpening+@DubaiStockOpening;


	UPDATE #TempStockDetails SET HeadAmt1 = @AllStockOpening WHERE LNSRL_1 = 1 AND SORT_SRL2 = 1 AND SORT_SL3 = 0;
	UPDATE #TempStockDetails SET HeadAmt1 = @DubaiStockOpening WHERE LNSRL_1 = 2 AND SORT_SRL2 = 2 AND SORT_SL3 = 0;
	UPDATE #TempStockDetails SET HeadAmt2 = @DhakaStockOpening WHERE LNSRL_1 = 2 AND SORT_SRL2 = 2 AND SORT_SL3 = 0;
	UPDATE #TempStockDetails SET HeadAmt1 = @TransitStockOpening WHERE LNSRL_1 = 10 AND SORT_SRL2 = 6 AND SORT_SL3 = 0;


-------------------------------Dubai Purchase Total--------------------------------------

		INSERT INTO #TempStockDetails(LNSRL_1,SORT_SRL2,SORT_SL3,Details1,DetailsAmt1,SL) 
		SELECT DISTINCT 6,3,2,A2ZPARTYCODE.PartyName,SUM(WFA2ZTRANSACTION.TrnDebitAmt),4
		FROM WFA2ZTRANSACTION LEFT OUTER JOIN A2ZPARTYCODE ON A2ZPARTYCODE.PartyAccNo = WFA2ZTRANSACTION.TrnKeyNo
		WHERE (FuncOpt = 1 AND Location = 1 AND TrnDrCr = 0 AND AccType = 14)
		GROUP BY A2ZPARTYCODE.PartyName;

	

-------------------------------Dubai Purchase Total--------------------------------------
	
	SET @DubaiParchase = (SELECT SUM(DetailsAmt1) FROM #TempStockDetails WHERE LNSRL_1 = 6 AND SORT_SRL2 = 3 AND SORT_SL3 =2);
	UPDATE #TempStockDetails SET Head2Amt1 = @DubaiParchase WHERE LNSRL_1 = 9 AND SORT_SRL2 = 3 AND SORT_SL3 = 5;


-------------------------------Transit Stock Closing Details--------------------------------------

    SET @RecLineNo = 0;

    DECLARE GoldTable CURSOR FOR
    SELECT DISTINCT A2ZPARTYCODE.PartyName,SUM(WFA2ZTRANSACTION.TrnCreditAmt) AS GrossWt
	FROM WFA2ZTRANSACTION LEFT OUTER JOIN A2ZPARTYCODE ON A2ZPARTYCODE.PartyAccNo = WFA2ZTRANSACTION.TrnKeyNo
	WHERE (FuncOpt = 11) AND TrnDate = @tDate AND Location = 2 AND AccType = 13
	GROUP BY A2ZPARTYCODE.PartyName;

    OPEN GoldTable;
    FETCH NEXT FROM GoldTable INTO
    @PartName,@GrossWt;

    WHILE @@FETCH_STATUS = 0 
	   BEGIN

	        SET @RecLineNo = @RecLineNo + 1;

	        INSERT INTO #TempStockDetails(LNSRL_1,SORT_SRL2,SORT_SL3,Details1,DetailsAmt1,SL,LineSL) 
			VALUES (10,9,0,@PartName,@GrossWt,14,@RecLineNo)


	        FETCH NEXT FROM GoldTable INTO
             @PartName,@GrossWt;        
	END

    CLOSE GoldTable; 
    DEALLOCATE GoldTable;

-------------------------------Dubai Stock Out And Dhaka Stock In--------------------------------------
	
	SET @DubaiStockOut = (SELECT SUM(ABS(TrnCreditAmt)) FROM WFA2ZTRANSACTION WHERE TrnDate = @tDate AND Location = 2 AND FuncOpt = 11 AND AccType = 13 AND TrnDrCr = 1);
	SET @DhakaStockIn = @DubaiStockOut;
	UPDATE #TempStockDetails SET Head2Amt1 = @DubaiStockOut WHERE LNSRL_1 = 9 AND SORT_SRL2 = 4 AND SORT_SL3 = 5;
	UPDATE #TempStockDetails SET Head2Amt2 = @DubaiStockOut WHERE LNSRL_1 = 9 AND SORT_SRL2 = 3 AND SORT_SL3 = 5;
	UPDATE #TempStockDetails SET HeadAmt1 = @DubaiStockOut WHERE LNSRL_1 = 10 AND SORT_SRL2 = 7 AND SORT_SL3 = 0;


/* 

 EXECUTE Sp_rptWMSTotalStock '2019-02-23'

*/
-------------------------------Transit Receive Details--------------------------------------
   
    SET @RecLineNo = 0;

    DECLARE GoldTable CURSOR FOR
    SELECT DISTINCT A2ZPARTYCODE.PartyName,SUM(WFA2ZTRANSACTION.TrnDebitAmt) AS GrossWt
	FROM WFA2ZTRANSACTION LEFT OUTER JOIN A2ZPARTYCODE ON A2ZPARTYCODE.PartyAccNo = WFA2ZTRANSACTION.TrnKeyNo
	WHERE (FuncOpt = 21) AND TrnDrCr = 0 AND AccType = 13 AND TrnDate = @tDate AND Location = 2
	GROUP BY A2ZPARTYCODE.PartyName;

    OPEN GoldTable;
    FETCH NEXT FROM GoldTable INTO
    @PartName,@GrossWt;

    WHILE @@FETCH_STATUS = 0 
	   BEGIN

	       SET @RecLineNo = @RecLineNo + 1;

		   SET @WriteFlag = 0;

	       DECLARE TempTable CURSOR FOR
           SELECT DISTINCT LineSL
	       FROM #TempStockDetails
	       WHERE SL = 4 AND LineSL = @RecLineNo;

           OPEN TempTable;
           FETCH NEXT FROM TempTable INTO
           @LineSL;

           WHILE @@FETCH_STATUS = 0 
	           BEGIN

			        SET @WriteFlag = 1;

			        UPDATE #TempStockDetails SET Details2 = @PartName,DetailsAmt2 = @GrossWt			
					WHERE SL = 4 AND LineSL = @LineSL;

			        FETCH NEXT FROM TempTable INTO
                    @LineSL;        
	           END

           CLOSE TempTable; 
           DEALLOCATE TempTable;


		   IF @WriteFlag = 0
		      BEGIN
			       INSERT INTO #TempStockDetails(LNSRL_1,SORT_SRL2,SORT_SL3,Details2,DetailsAmt2,SL,LineSL) 
		           VALUES (6,3,2,@PartName,@GrossWt,4,@RecLineNo)
			  END
     

	FETCH NEXT FROM GoldTable INTO
    @PartName,@GrossWt;        
	END

    CLOSE GoldTable; 
    DEALLOCATE GoldTable;


-------------------------------Transit Receive Total--------------------------------------

	SET @TransitReceiveDetails = (SELECT SUM(TrnDebitAmt) FROM WFA2ZTRANSACTION WHERE (FuncOpt = 21) AND TrnDrCr = 0 AND AccType = 13 AND TrnDate = @tDate AND Location = 2);	
	UPDATE #TempStockDetails SET Head2Amt2 = @TransitReceiveDetails WHERE LNSRL_1 = 9 AND SORT_SRL2 = 3 AND SORT_SL3 = 5;


-------------------------------Total Transit Out--------------------------------------

	SET @TotalTransitOut = (SELECT SUM(TrnDebitAmt) FROM WFA2ZTRANSACTION WHERE (FuncOpt = 21) AND TrnDrCr = 0 AND AccType = 13 AND TrnDate = @tDate AND Location = 2);	
	UPDATE #TempStockDetails SET HeadAmt1 = @TotalTransitOut WHERE LNSRL_1 = 10 AND SORT_SRL2 = 8 AND SORT_SL3 = 0;




-------------------------------Dhaka Sales Details--------------------------------------
   
    SET @RecLineNo = 0;

    DECLARE GoldTable CURSOR FOR
    SELECT DISTINCT A2ZPARTYCODE.PartyName,SUM(WFA2ZTRANSACTION.TrnCreditAmt) AS GrossWt
	FROM WFA2ZTRANSACTION LEFT OUTER JOIN A2ZPARTYCODE ON A2ZPARTYCODE.PartyAccNo = WFA2ZTRANSACTION.TrnKeyNo
	WHERE (FuncOpt = 41) AND TrnDrCr = 1 AND AccType = 20 AND TrnDate = @tDate AND Location = 3
	GROUP BY A2ZPARTYCODE.PartyName;

    OPEN GoldTable;
    FETCH NEXT FROM GoldTable INTO
    @PartName,@GrossWt;

    WHILE @@FETCH_STATUS = 0 
	   BEGIN

	       SET @RecLineNo = @RecLineNo + 1;

		   SET @WriteFlag = 0;

	       DECLARE TempTable CURSOR FOR
           SELECT DISTINCT LineSL
	       FROM #TempStockDetails
	       WHERE SL = 7 AND LineSL = @RecLineNo;

           OPEN TempTable;
           FETCH NEXT FROM TempTable INTO
           @LineSL;

           WHILE @@FETCH_STATUS = 0 
	           BEGIN

			        SET @WriteFlag = 1;

			        UPDATE #TempStockDetails SET Details2 = @PartName,DetailsAmt2 = @GrossWt			
					WHERE SL = 7 AND LineSL = @LineSL;

			        FETCH NEXT FROM TempTable INTO
                    @LineSL;        
	           END

           CLOSE TempTable; 
           DEALLOCATE TempTable;


		   IF @WriteFlag = 0
		      BEGIN
			       INSERT INTO #TempStockDetails(LNSRL_1,SORT_SRL2,SORT_SL3,Details2,DetailsAmt2,SL,LineSL) 
		           VALUES (6,4,2,@PartName,@GrossWt,7,@RecLineNo)
			  END
     

	FETCH NEXT FROM GoldTable INTO
    @PartName,@GrossWt;        
	END

    CLOSE GoldTable; 
    DEALLOCATE GoldTable;


-------------------------------Dhaka Sales Total--------------------------------------

	SET @DhakaSaleDetails = (SELECT SUM(TrnCreditAmt) FROM WFA2ZTRANSACTION WHERE (FuncOpt = 41) AND TrnDrCr = 1 AND AccType = 20 AND TrnDate = @tDate AND Location = 3);	
	UPDATE #TempStockDetails SET Head2Amt2 = @DhakaSaleDetails WHERE LNSRL_1 = 9 AND SORT_SRL2 = 4 AND SORT_SL3 = 5;


-------------------------------Dubai Stock Closing Total--------------------------------------

	SET @DubaiStockClosing = ISNULL(@DubaiStockOpening,0) + 	ISNULL(@DubaiParchase,0) - ISNULL(@DubaiStockOut,0);
	UPDATE #TempStockDetails SET HeadAmt1 = @DubaiStockClosing WHERE LNSRL_1 = 9 AND SORT_SRL2 = 5 AND SORT_SL3 = 0;



-------------------------------Dhaka Stock Closing Total--------------------------------------

	SET @DhakaStockClosing = ISNULL(@DhakaStockIn,0) + ISNULL(@DhakaStockOpening,0) - ISNULL(@DhakaSaleDetails,0);
	UPDATE #TempStockDetails SET HeadAmt2 = @DhakaStockClosing WHERE LNSRL_1 = 11 AND SORT_SRL2 = 10 AND SORT_SL3 = 0;

-------------------------------Transit Stock Closing--------------------------------------


	SET @TransitStockClosing = @TransitStockOpening + @DubaiStockOut - @TotalTransitOut;
	UPDATE #TempStockDetails SET HeadAmt1 = @TransitStockClosing WHERE LNSRL_1 = 11 AND SORT_SRL2 = 10 AND SORT_SL3 = 0;

	/* 

 EXECUTE Sp_rptWMSTotalStock '2019-02-23'

*/
-------------------------------All Stock Closing Total--------------------------------------
	
	SET @AllStockClosing = ISNULL(@DhakaStockClosing,0) + ISNULL(@DubaiStockClosing,0);
	UPDATE #TempStockDetails SET HeadAmt1 = @AllStockClosing WHERE LNSRL_1 = 99 AND SORT_SRL2 = 10 AND SORT_SL3 = 0;

  INSERT INTO WFTOTALSTOCKDETAILS (LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head2,Details1,DetailsAmt1,HeadAmt1,Head2Amt1,Head3,Head4,Details2,DetailsAmt2,HeadAmt2,Head2Amt2,SL,LineSL)
  SELECT LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head2,Details1,SUM(DetailsAmt1),HeadAmt1,Head2Amt1,Head3,Head4,Details2,SUM(DetailsAmt2),HeadAmt2,Head2Amt2,SL,LineSL FROM #TempStockDetails 
  GROUP BY LNSRL_1,SORT_SRL2,SORT_SL3,Head1,Head2,Details1,HeadAmt1,Head2Amt1,Head3,Head4,Details2,HeadAmt2,Head2Amt2,SL,LineSL
  ORDER BY SL,LineSL ASC;

  SELECT * FROM WFTOTALSTOCKDETAILS order by SL,LineSL ASC;




  DROP TABLE #TempStockDetails;;

	DROP TABLE #Table;
	DROP TABLE #Table1;

END



















GO

