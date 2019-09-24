using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
//using DataAccessLayer.Conn;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCOUNTDTO
    {
        #region Propertise
        public Int32 Id { set; get; }
        public int  BranchNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public Int16 MemType { set; get; }
        public int MemberNo { set; get; }
        public String MemberName { set; get; }
        public DateTime Opendate { set; get; }
        public decimal DepositAmount { set; get; }
        public decimal TotDepositAmount { set; get; }
        public decimal FixedDepositAmount { set; get; }
        public decimal FixedMthInt { set; get; }
        public Int16 Period { set; get; }
        public Int16 WithDrawalAC { set; get; }
        public Int16 InterestCalculation { set; get; }
        public DateTime MatruityDate { set; get; }
        public decimal MatruityAmount { set; get; }
        public Int16 InterestWithdrawal { set; get; }
        public Int16 AutoRenewal { set; get; }
        public decimal LoanAmount { set; get; }
        public Int16 NoInstallment { set; get; }
        public decimal MonthlyInstallment { set; get; }
        public decimal LastInstallment { set; get; }
        public decimal InterestRate { set; get; }
        public Int16 ContractInt { set; get; }
        public Int16 GracePeriod { set; get; }
        public int LoaneeACType { set; get; }
        public int LoaneeMemberNo { set; get; }
        
        public String SpInstruction { set; get; }
        public int CorrAccType { set; get; }
        public Int64 CorrAccNo { set; get; }
        public Int16 AccAutoTrfFlag { set; get; }
        public String OldAccountNo { set; get; }
        public Int16 AccAtyClass { set; get; }
        public int AccStatus { set; get; }
        public String AccStatusNote { set; get; }
        public DateTime AccStatusDate { set; get; }
        public Int16 a { set; get; }
        public int MemAccTypeClass { set; get; }
        public decimal AccBalance { set; get; }

        public decimal AccHoldBalanceDr { set; get; }
        public decimal AccHoldBalanceCr { set; get; }
        public decimal AccOpBal { set; get; }
        public decimal AccLienAmt { set; get; }
        public decimal AccProvBalance { set; get; }
        public decimal AccPrincipal { set; get; }
        public decimal AccRenwlAmt { set; get; }
        public DateTime AccRenwlDate { set; get; }
        public decimal AccOrgAmt { set; get; }
        public DateTime LastTrnDate { set; get; }
        public int AccLoanAppNo { set; get; }
        public DateTime AccLoanAppDate { set; get; }
        public DateTime LoanSancDate { set; get; }
        public DateTime AccDisbDate { set; get; }
        public decimal AccDisbAmt { set; get; }
        public DateTime AccLoanExpiryDate { set; get; }
        public decimal AccDueIntAmt { set; get; }
        public String AccCertNo { set; get; }
        public Int16 AccNoBenefit { set; get; }
        public DateTime AccBenefitDate { set; get; }
        public DateTime AccAnniDate { set; get; }
        public DateTime AccEffectDate { set; get; }
        public String OpenNulldate { set; get; }
        public String MatruityNullDate { set; get; }
        public String AccStatusNullDate { set; get; }
        public String AccRenwlNullDate { set; get; }
        public String LastTrnNullDate { set; get; }
        public String AccBenefitNullDate { set; get; }
        public String AccAnniNullDate { set; get; }
        public String AccSancNullDate { set; get; }
        public String AccDisbNullDate { set; get; }
        public String AccExpiryNullDate { set; get; }
        public int UserId { set; get; }
        public Int16 AccSMSFlag { set; get; }


       
        #endregion


        public static int InsertInformation(A2ZACCOUNTDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@Opendate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            
            if (dto.OpenNulldate == "")
            {

                param1 = new SqlParameter("@Opendate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@Opendate", dto.Opendate);
            }
            
            if (dto.MatruityNullDate == "")
            {

                param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@MatruityDate", dto.MatruityDate);
            }

            if (dto.AccBenefitNullDate == "")
            {

                param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@AccBenefitDate", dto.AccBenefitDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSCUBS"), "Sp_CSAccountDataInsert", new object[] { dto.BranchNo, dto.AccType, dto.AccNo, dto.MemType, dto.MemberNo, param1, dto.DepositAmount, dto.Period, dto.WithDrawalAC, dto.InterestCalculation, param2, dto.MatruityAmount, dto.InterestWithdrawal, dto.AutoRenewal, dto.LoanAmount, dto.NoInstallment, dto.MonthlyInstallment, dto.LastInstallment, dto.InterestRate, dto.ContractInt, dto.GracePeriod, dto.LoaneeACType, dto.LoaneeMemberNo, dto.SpInstruction, dto.CorrAccType, dto.CorrAccNo, dto.AccAutoTrfFlag, dto.OldAccountNo, dto.FixedDepositAmount, dto.FixedMthInt, dto.AccStatus, dto.AccAtyClass, param3, dto.AccSMSFlag });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }


        //public static int Insert(A2ZACCOUNTDTO dto)
        //{
        //    int rowEffect = 0;
        //    string strQuery = @"INSERT into A2ZACCOUNT(AccType, AccNo,CuType,CuNo,MemNo,AccStatus,AccAtyClass,AccOpenDate) values('" + dto.AccType + "','" +
        //               dto.AccNo + "','" + dto.CuType + "','" + dto.CuNo + "','" + dto.MemberNo + "','" + dto.AccStatus + "','" + dto.AccAtyClass + "','" + dto.Opendate + "')";
        //    rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

        //    if (rowEffect == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}


        public static A2ZACCOUNTDTO GetInformation(int BranchNo, int AccType, Int64 AccountNo, Int16 MemType, int MemberNo)
        {

            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = AccType;
            prm[2] = AccountNo;
            prm[3] = MemType;
            prm[4] = MemberNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccount", prm, "A2ZCSCUBS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
               
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
                p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccNoBenefit = Converter.GetSmallInteger(dt.Rows[0]["AccNoBenefit"]);
                p.AccSMSFlag = Converter.GetSmallInteger(dt.Rows[0]["AccSMSFlag"]);

                
                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
               
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }

        public static A2ZACCOUNTDTO GetInfo(int BranchNo, int AccType, Int16 MemType, int MemberNo)
        {

            var prm = new object[4];

            prm[0] = BranchNo;
            prm[1] = AccType;
            prm[2] = MemType;  
            prm[3] = MemberNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAcc", prm, "A2ZCSCUBS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
              
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
                p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccAnniDate = Converter.GetDateTime(dt.Rows[0]["AccAnniDate"]);
                p.AccNoBenefit = Converter.GetSmallInteger(dt.Rows[0]["AccNoBenefit"]);


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
                
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }

        public static A2ZACCOUNTDTO GetInfoAccNo(Int64 AccountNo)
        {

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm1 = new object[3];    
            prm1[0] = AccountNo;
            prm1[1] = Converter.GetDateToYYYYMMDD(date1); 
            prm1[2] = 0;

            DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_CSGenerateSingleAccountBalance", prm1, "A2ZCSCUBS");


            var prm = new object[1];    
            prm[0] = AccountNo;
                       
            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccountNo", prm, "A2ZCSCUBS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");



            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["Id"]);
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
               
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);

                p.AccLoanAppNo = Converter.GetInteger(dt.Rows[0]["AccLoanAppNo"]);
                p.AccLoanAppDate = Converter.GetDateTime(dt.Rows[0]["AccLoanAppDate"]);

                p.LoanSancDate = Converter.GetDateTime(dt.Rows[0]["AccLoanSancDate"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
                p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);

                p.AccHoldBalanceDr = Converter.GetDecimal(dt.Rows[0]["AccHoldBalanceDr"]);
                p.AccHoldBalanceCr = Converter.GetDecimal(dt.Rows[0]["AccHoldBalanceCr"]);

                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbDate = Converter.GetDateTime(dt.Rows[0]["AccDisbDate"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.AccLoanExpiryDate = Converter.GetDateTime(dt.Rows[0]["AccLoanExpiryDate"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccNoBenefit = Converter.GetSmallInteger(dt.Rows[0]["AccNoBenefit"]);
                p.AccOpBal = Converter.GetDecimal(dt.Rows[0]["AccOpBal"]);
                p.AccEffectDate = Converter.GetDateTime(dt.Rows[0]["AccEffectDate"]);

                p.AccAnniDate = Converter.GetDateTime(dt.Rows[0]["AccAnniDate"]);
                p.AccSMSFlag = Converter.GetSmallInteger(dt.Rows[0]["AccSMSFlag"]);


                p.AccBalance = p.AccOpBal;


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
              
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }


        public static A2ZACCOUNTDTO GetInfoRevAccNo(Int64 AccountNo)
        {

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm1 = new object[3];
            prm1[0] = AccountNo;
            prm1[1] = Converter.GetDateToYYYYMMDD(date1);
            prm1[2] = 0;

            DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_CSGenerateSingleAccountRevBalance", prm1, "A2ZCSCUBS");


            var prm = new object[1];
            prm[0] = AccountNo;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccountNo", prm, "A2ZCSCUBS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");



            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["Id"]);
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);

                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);

                p.AccLoanAppNo = Converter.GetInteger(dt.Rows[0]["AccLoanAppNo"]);
                p.AccLoanAppDate = Converter.GetDateTime(dt.Rows[0]["AccLoanAppDate"]);

                p.LoanSancDate = Converter.GetDateTime(dt.Rows[0]["AccLoanSancDate"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
                p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbDate = Converter.GetDateTime(dt.Rows[0]["AccDisbDate"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.AccLoanExpiryDate = Converter.GetDateTime(dt.Rows[0]["AccLoanExpiryDate"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccNoBenefit = Converter.GetSmallInteger(dt.Rows[0]["AccNoBenefit"]);
                p.AccOpBal = Converter.GetDecimal(dt.Rows[0]["AccOpBal"]);
                p.AccEffectDate = Converter.GetDateTime(dt.Rows[0]["AccEffectDate"]);

                p.AccAnniDate = Converter.GetDateTime(dt.Rows[0]["AccAnniDate"]);
                p.AccSMSFlag = Converter.GetSmallInteger(dt.Rows[0]["AccSMSFlag"]);


                p.AccBalance = p.AccOpBal;


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;

                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }


        public static A2ZACCOUNTDTO GetInfoAccNoByApplicationNo(int BranchNo, int AppNo)
        {

            //A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            //DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            //string date1 = dt2.ToString("dd/MM/yyyy");

            //var prm1 = new object[3];
            //prm1[0] = AccountNo;
            //prm1[1] = Converter.GetDateToYYYYMMDD(date1);
            //prm1[2] = 0;

            //DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_CSGenerateSingleAccountBalance", prm1, "A2ZCSCUBS");


            var prm = new object[2];
            prm[0] = BranchNo;
            prm[1] = AppNo;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccNoByApplicationNo", prm, "A2ZCSCUBS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["Id"]);
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);

                p.AccLoanAppNo = Converter.GetInteger(dt.Rows[0]["AccLoanAppNo"]);
                p.AccLoanAppDate = Converter.GetDateTime(dt.Rows[0]["AccLoanAppDate"]);
                
                p.LoanSancDate = Converter.GetDateTime(dt.Rows[0]["AccLoanSancDate"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
                p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbDate = Converter.GetDateTime(dt.Rows[0]["AccDisbDate"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.AccLoanExpiryDate = Converter.GetDateTime(dt.Rows[0]["AccLoanExpiryDate"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccNoBenefit = Converter.GetSmallInteger(dt.Rows[0]["AccNoBenefit"]);
                p.AccOpBal = Converter.GetDecimal(dt.Rows[0]["AccOpBal"]);
                p.AccEffectDate = Converter.GetDateTime(dt.Rows[0]["AccEffectDate"]);



                p.AccBalance = p.AccOpBal;


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;

                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }



        public static A2ZACCOUNTDTO GetInforbyAccType(int BrNo, Int16 MemType, int memNo, int AType)
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE CuType = 0 AND CuNo = 0 AND MemNo = '" + memNo + "' AND AccType = '" + AType + "'", "A2ZCSCUBS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
               
                return p;
            }
            p.AccNo = 0;

            return p;

        }
        public static int UpdateInformation(A2ZACCOUNTDTO dto)
        {

            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@Opendate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@AccAnniDate", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@AccRenwlDate", DBNull.Value);
            
            if (dto.OpenNulldate == "")
            {

                param1 = new SqlParameter("@Opendate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@Opendate", dto.Opendate);
            }
            
            if (dto.MatruityNullDate == "")
            {

                param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@MatruityDate", dto.MatruityDate);
            }         
            
            if (dto.AccBenefitNullDate == "")
            {

                param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@AccBenefitDate", dto.AccBenefitDate);
            }

            if (dto.AccAnniNullDate == "")
            {

                param4 = new SqlParameter("@AccAnniDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@AccAnniDate", dto.AccAnniDate);
            }

            if (dto.AccRenwlNullDate == "")
            {

                param5 = new SqlParameter("@AccRenwlDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@AccRenwlDate", dto.AccRenwlDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSCUBS"), "Sp_CSAccountDataUpdate", new object[] { dto.UserId, dto.Id, dto.BranchNo, dto.AccType, dto.AccNo, dto.MemType, dto.MemberNo, param1, dto.DepositAmount, dto.Period, dto.WithDrawalAC, dto.InterestCalculation, param2, dto.MatruityAmount, dto.InterestWithdrawal, dto.AutoRenewal, dto.LoanAmount, dto.NoInstallment, dto.MonthlyInstallment, dto.LastInstallment, dto.InterestRate, dto.ContractInt, dto.GracePeriod, dto.LoaneeACType, dto.LoaneeMemberNo, dto.AccCertNo, dto.SpInstruction, dto.CorrAccType, dto.CorrAccNo, dto.AccAutoTrfFlag, dto.OldAccountNo, dto.FixedDepositAmount, dto.FixedMthInt, dto.AccAtyClass, param3, param4, param5, dto.AccOrgAmt, dto.AccPrincipal, dto.AccRenwlAmt, dto.AccNoBenefit, dto.TotDepositAmount, dto.AccSMSFlag });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }


        public static int UpdateLoanInformation(A2ZACCOUNTDTO dto)
        {

            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@Opendate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@LoanSancDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@AccDisbDate", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@AccLoanExpiryDate", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@LastTrnDate", DBNull.Value);
            

            if (dto.OpenNulldate == "")
            {

                param1 = new SqlParameter("@Opendate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@Opendate", dto.Opendate);
            }

            if (dto.AccSancNullDate == "")
            {

                param2 = new SqlParameter("@LoanSancDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@LoanSancDate", dto.LoanSancDate);
            }

            if (dto.AccDisbNullDate == "")
            {

                param3 = new SqlParameter("@AccDisbDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@AccDisbDate", dto.AccDisbDate);
            }


            if (dto.AccExpiryNullDate == "")
            {

                param4 = new SqlParameter("@AccLoanExpiryDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@AccLoanExpiryDate", dto.AccLoanExpiryDate);
            }

            if (dto.LastTrnNullDate == "")
            {

                param5 = new SqlParameter("@LastTrnDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@LastTrnDate", dto.LastTrnDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSCUBS"), "Sp_CSLoanAccountDataUpdate", new object[] { dto.Id, dto.BranchNo, dto.AccType, dto.AccNo, dto.MemType, dto.MemberNo, param1, param2, param3, param4, param5, dto.LoanAmount, dto.AccDisbAmt, dto.InterestRate, dto.NoInstallment, dto.MonthlyInstallment, dto.LastInstallment, dto.CorrAccType, dto.CorrAccNo, dto.AccAutoTrfFlag, dto.OldAccountNo, dto.UserId, dto.AccSMSFlag });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        public static int Update1(A2ZACCOUNTDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCOUNT SET AccAtyClass ='" + dto.AccAtyClass + "', MemType ='" + dto.MemType + "' where  Id='" + dto.Id + "' ";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}
