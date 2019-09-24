using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZLOANDTO
   {
       #region Propertise
       public int ID { get; set; }
       public int BranchNo { set; get; }
       public int LoanApplicationNo { set; get; }
       public DateTime LoanApplicationDate { set; get; }
       public int LoanAccountType { set; get; }
       public int LoanMemberNo { set; get; }
       public int MemType { set; get; }
       public Decimal LoanApplicationAmount { set; get; }
       public Decimal LoanInterestRate { set; get; }
       public Int16 LoanGracePeriod { set; get; }
       public Decimal LoanInstallmentAmount { set; get; }
       public Decimal LoanLastInstallmentAmount { set; get; }
       public int LoanNoInstallment { set; get; }
       public DateTime LoanFirstInstallmentdate { set; get; }
       public DateTime LoanExpDate { set; get; }
       public Int16 LoanInterestCalPeriod { set; get; }
       public Int16 LoanInterestCalMethod { set; get; }
       public Int16 LoanPurpose { set; get; }
       public Int16 LoanCategory { set; get; }
       public int LoanSuretyMemType { set; get; }
       public int LoanSuretyMemNo { set; get; }
       public Int16 LoanStatus { set; get; }
       public DateTime LoanStatDate { set; get; }
       public Int16 LoanProcFlag { set; get; }
       public DateTime LoanStatusdate { set; get; }
       public String LoanStatusNote { set; get; }

       public Int16 AccTypeMode { set; get; }
       public int FromCashCode { set; get; }
       public Decimal LoanTotGuarantorAmt { set; get; }
       public Int16 InputBy { set; get; }
       public Int16 ApprovBy { set; get; }
       public DateTime InputByDate { set; get; }      
       public DateTime ApprovByDate { set; get; }
       public int AccCorrAccType { set; get; }
       public Int64 AccCorrAccNo { set; get; }
       public Int16 AccAutoTrfFlag { set; get; }
       public Int16 AccDepositMode { set; get; }
       public Int16 AccWeeklyDays { set; get; }
       public Int16 LoanModule { set; get; }
       #endregion


       public static int InsertInformation(A2ZLOANDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZLOAN(BranchNo,LoanApplicationNo,LoanApplicationDate,AccType,MemType,MemNo,LoanApplicationAmt,LoanIntRate,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,LoanExpiryDate,LoanIntPeriod,LoanIntMethod,LoanPurpose,LoanProcFlag,LoanCategory,LoanSuretyMemType,LoanSuretyMemNo,AccTypeMode,FromCashCode,LoanTotGuarantorAmt,InputBy,InputByDate,ApprovBy,ApprovByDate,AccCorrAccType,AccCorrAccNo,AccAutoTrfFlag,AccDepositMode,AccWeeklyDays,LoanModule)values('" + dto.BranchNo + "','" + dto.LoanApplicationNo + "','" + dto.LoanApplicationDate + "','" + dto.LoanAccountType + "','" + dto.MemType + "','" + dto.LoanMemberNo + "','" + dto.LoanApplicationAmount + "','" + dto.LoanInterestRate + "','" + dto.LoanInstallmentAmount + "','" + dto.LoanLastInstallmentAmount + "','" + dto.LoanNoInstallment + "','" + dto.LoanExpDate + "','" + dto.LoanInterestCalPeriod + "','" + dto.LoanInterestCalMethod + "','" + dto.LoanPurpose + "','" + dto.LoanProcFlag + "','" + dto.LoanCategory + "','" + dto.LoanSuretyMemType + "','" + dto.LoanSuretyMemNo + "','" + dto.AccTypeMode + "','" + dto.FromCashCode + "','" + dto.LoanTotGuarantorAmt + "','" + dto.InputBy + "','" + dto.InputByDate + "','" + dto.ApprovBy + "','" + dto.ApprovByDate + "','" + dto.AccCorrAccType + "','" + dto.AccCorrAccNo + "','" + dto.AccAutoTrfFlag + "','" + dto.AccDepositMode + "','" + dto.AccWeeklyDays + "','" + dto.LoanModule + "')";
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

       public static A2ZLOANDTO GetInformation(int BranchNo,int AppNo)
       {

           var prm = new object[2];

           prm[0] = BranchNo;
           prm[1] = AppNo;


           DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoLoan", prm, "A2ZCSCUBS");
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo, LoanApplicationNo, LoanApplicationDate, AccType, MemNo, LoanApplicationAmt, LoanIntRate, LoanGrace, LoanInstlAmt, LoanLastInstlAmt, LoanNoInstl, LoanFirstInstlDt, LoanExpiryDate,LoanIntPeriod, LoanIntMethod, LoanPurpose, LoanCategory, LoanSuretyMemNo, LoanStatDate, LoanProcFlag,LoanExpiryDate,InputBy,ApprovBy,InputByDate,ApprovByDate FROM  dbo.A2ZLOAN WHERE LoanApplicationNo = " + AppNo, "A2ZCSMCUS");


           var p = new A2ZLOANDTO();
           if (dt.Rows.Count > 0)
           {
               p.ID = Converter.GetInteger(dt.Rows[0]["ID"]);
               p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
               p.LoanApplicationNo = Converter.GetInteger(dt.Rows[0]["LoanApplicationNo"]);

               p.LoanApplicationDate = Converter.GetDateTime(dt.Rows[0]["LoanApplicationDate"]);
               p.LoanAccountType = Converter.GetInteger(dt.Rows[0]["AccType"]);
               p.MemType = Converter.GetInteger(dt.Rows[0]["MemType"]);
               p.LoanMemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
               p.LoanApplicationAmount = Converter.GetDecimal(dt.Rows[0]["LoanApplicationAmt"]);
               p.LoanInterestRate = Converter.GetDecimal(dt.Rows[0]["LoanIntRate"]);
               //p.LoanGracePeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanGrace"]);
               p.LoanInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanInstlAmt"]);
               p.LoanLastInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanLastInstlAmt"]);
               p.LoanNoInstallment = Converter.GetInteger(dt.Rows[0]["LoanNoInstl"]);
               //p.LoanFirstInstallmentdate = Converter.GetDateTime(dt.Rows[0]["LoanFirstInstlDt"]);
               p.LoanInterestCalPeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntPeriod"]);
               p.LoanInterestCalMethod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntMethod"]);
               p.LoanPurpose = Converter.GetSmallInteger(dt.Rows[0]["LoanPurpose"]);
               p.LoanCategory = Converter.GetSmallInteger(dt.Rows[0]["LoanCategory"]);
               p.LoanSuretyMemType = Converter.GetInteger(dt.Rows[0]["LoanSuretyMemType"]);
               p.LoanSuretyMemNo = Converter.GetInteger(dt.Rows[0]["LoanSuretyMemNo"]);

               p.LoanStatus = Converter.GetSmallInteger(dt.Rows[0]["LoanStatus"]);

               p.LoanStatDate = Converter.GetDateTime(dt.Rows[0]["LoanStatDate"]);

               
               p.LoanProcFlag = Converter.GetSmallInteger(dt.Rows[0]["LoanProcFlag"]);
             
               p.LoanExpDate = Converter.GetDateTime(dt.Rows[0]["LoanExpiryDate"]);

               p.FromCashCode = Converter.GetInteger(dt.Rows[0]["FromCashCode"]);

               p.LoanTotGuarantorAmt = Converter.GetDecimal(dt.Rows[0]["LoanTotGuarantorAmt"]);

               //p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);

              // p.LoanStatusNote = Converter.GetString(dt.Rows[0]["LoanStatNote"]);

               p.InputBy = Converter.GetSmallInteger(dt.Rows[0]["InputBy"]);
               p.ApprovBy = Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);
               p.InputByDate = Converter.GetDateTime(dt.Rows[0]["InputByDate"]);
               p.ApprovByDate = Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);

               p.AccCorrAccType = Converter.GetInteger(dt.Rows[0]["AccCorrAccType"]);
               p.AccCorrAccNo = Converter.GetLong(dt.Rows[0]["AccCorrAccNo"]);
               p.AccAutoTrfFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
               p.AccDepositMode = Converter.GetSmallInteger(dt.Rows[0]["AccDepositMode"]);
               p.AccWeeklyDays = Converter.GetSmallInteger(dt.Rows[0]["AccWeeklyDays"]);

               p.AccWeeklyDays = Converter.GetSmallInteger(dt.Rows[0]["AccWeeklyDays"]);

               p.LoanModule = Converter.GetSmallInteger(dt.Rows[0]["LoanModule"]);
               return p;
           }
           p.LoanApplicationNo = 0;

           return p;

       }

       public static int UpdateInformation(A2ZLOANDTO dto)
       {
          
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZLOAN set BranchNo='" + dto.BranchNo + "',LoanApplicationNo='" + dto.LoanApplicationNo + "',LoanApplicationDate='" + dto.LoanApplicationDate + "',MemNo='" + dto.LoanMemberNo + "',MemType='" + dto.MemType + "', AccType='" + dto.LoanAccountType + "', AccTypeMode='" + dto.AccTypeMode + "',LoanApplicationAmt='" + dto.LoanApplicationAmount + "',LoanIntRate='" + dto.LoanInterestRate + "',LoanInstlAmt='" + dto.LoanInstallmentAmount + "',LoanLastInstlAmt='" + dto.LoanLastInstallmentAmount + "',LoanNoInstl='" + dto.LoanNoInstallment + "',LoanExpiryDate='" + dto.LoanExpDate + "',LoanIntPeriod='" + dto.LoanInterestCalPeriod + "',LoanIntMethod='" + dto.LoanInterestCalMethod + "',LoanPurpose='" + dto.LoanPurpose + "',LoanCategory='" + dto.LoanCategory + "',LoanSuretyMemType='" + dto.LoanSuretyMemType + "',LoanSuretyMemNo='" + dto.LoanSuretyMemNo + "',LoanTotGuarantorAmt='" + dto.LoanTotGuarantorAmt + "',InputBy='" + dto.InputBy + "',InputByDate='" + dto.InputByDate + "',ApprovBy='" + dto.ApprovBy + "',ApprovByDate='" + dto.ApprovByDate + "',AccCorrAccType='" + dto.AccCorrAccType + "',AccCorrAccNo='" + dto.AccCorrAccNo + "',AccAutoTrfFlag='" + dto.AccAutoTrfFlag + "',AccDepositMode='" + dto.AccDepositMode + "',AccWeeklyDays='" + dto.AccWeeklyDays + "' where BranchNo='" + dto.BranchNo + "' AND LoanApplicationNo ='" + dto.LoanApplicationNo + "'";
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

       public static int UpdateInformationBySP(A2ZLOANDTO dto)
       {

           int rowEffect = 0;

           var prm = new object[21];

           prm[0] = dto.ID;
           prm[1] = dto.InputBy;
           prm[2] = dto.AccTypeMode;
           prm[3] = dto.LoanApplicationAmount;
           prm[4] = dto.LoanInterestRate;
           prm[5] = dto.LoanInstallmentAmount;
           prm[6] = dto.LoanLastInstallmentAmount;
           prm[7] = dto.LoanNoInstallment;
           prm[8] = dto.LoanExpDate;
           prm[9] = dto.LoanInterestCalPeriod;
           prm[10] = dto.LoanInterestCalMethod;
           prm[11] = dto.LoanPurpose;
           prm[12] = dto.LoanCategory;
           prm[13] = dto.LoanSuretyMemType;
           prm[14] = dto.LoanSuretyMemNo;
           prm[15] = dto.LoanTotGuarantorAmt;
           prm[16] = dto.AccCorrAccType;
           prm[17] = dto.AccCorrAccNo;
           prm[18] = dto.AccAutoTrfFlag;
           prm[19] = dto.AccDepositMode;
           prm[20] = dto.AccWeeklyDays;

           int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSLoanDataUpdate", prm, "A2ZCSCUBS"));
           if (result == 0)
           {
               rowEffect = 1;
           }
           else 
           {
               rowEffect = 0;
           }

           //BLL.CommonManager.Instance.GetScalarValueBySp("Sp_CSLoanDataUpdate", prm, "A2ZCSCUBS");


           return rowEffect;
       }

   }
}
