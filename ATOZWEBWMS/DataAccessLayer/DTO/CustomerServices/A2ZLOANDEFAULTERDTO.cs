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
  public  class A2ZLOANDEFAULTERDTO
    {
        #region Propertise
        public Int32 ID { get; set; }

        public int BranchNo { set; get; }
        public DateTime TrnDate { set; get; }
        public Int16 MemType { set; get; }
        public int MemNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public decimal CalPrincAmt { set; get; }
        public decimal CalIntAmt { set; get; }
        public decimal UptoDuePrincAmt { set; get; }
        public decimal UptoDueIntAmt { set; get; }
        public decimal PayablePrincAmt { set; get; }
        public decimal PayableIntAmt { set; get; }
        public decimal PayablePenalAmt { set; get; }
        public decimal PaidPrincAmt { set; get; }
        public decimal PaidIntAmt { set; get; }
        public decimal PaidPenalAmt { set; get; }
        public decimal CurrDuePrincAmt { set; get; }
        public decimal CurrDueIntAmt { set; get; }
        public String Remarks { set; get; }

        public int UserId { get; set; }
        public DateTime UptoPaidDate { set; get; }
        
        #endregion



        public static A2ZLOANDEFAULTERDTO GetLoanInformation(int BranchNo, DateTime TrnDate, Int16 MemType, int MemNo, int AccType, Int64 AccNo)
        {
            
            var prm = new object[6];

            prm[0] = BranchNo;
            prm[1] = TrnDate;
            prm[2] = MemType;   
            prm[3] = MemNo;
            prm[4] = AccType;
            prm[5] = AccNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoLoanDefaulter", prm, "A2ZCSCUBS");
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDESIGNATION WHERE DesigCode = " + Designcode, "A2ZCSMCUS");


            var p = new A2ZLOANDEFAULTERDTO();
            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetInteger(dt.Rows[0]["ID"]);

                p.TrnDate = Converter.GetDateTime(dt.Rows[0]["TrnDate"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
                
                p.MemNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.AccType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.CalPrincAmt = Converter.GetDecimal(dt.Rows[0]["CalPrincAmt"]);
                p.CalIntAmt = Converter.GetDecimal(dt.Rows[0]["CalIntAmt"]);
                p.UptoDuePrincAmt = Converter.GetDecimal(dt.Rows[0]["UptoDuePrincAmt"]);
                p.UptoDueIntAmt = Converter.GetDecimal(dt.Rows[0]["UptoDueIntAmt"]);
                p.PayablePrincAmt = Converter.GetDecimal(dt.Rows[0]["PayablePrincAmt"]);
                p.PayableIntAmt = Converter.GetDecimal(dt.Rows[0]["PayableIntAmt"]);
                p.PayablePenalAmt = Converter.GetDecimal(dt.Rows[0]["PayablePenalAmt"]);
                p.PaidPrincAmt = Converter.GetDecimal(dt.Rows[0]["PaidPrincAmt"]);
                p.PaidIntAmt = Converter.GetDecimal(dt.Rows[0]["PaidIntAmt"]);
                p.PaidPenalAmt = Converter.GetDecimal(dt.Rows[0]["PaidPenalAmt"]);
                p.CurrDuePrincAmt = Converter.GetDecimal(dt.Rows[0]["CurrDuePrincAmt"]);
                p.CurrDueIntAmt = Converter.GetDecimal(dt.Rows[0]["CurrDueIntAmt"]);
                p.UptoPaidDate = Converter.GetDateTime(dt.Rows[0]["UptoPaidDate"]);

                return p;
            }
            p.MemType = 0;

            return p;

        }


        public static int UpdateInformation01(A2ZLOANDEFAULTERDTO dto)
        {

            var prm = new object[11];

            prm[0] = dto.BranchNo;
            prm[1] = dto.AccType;
            prm[2] = dto.AccNo;
            prm[3] = dto.MemType;
            prm[4] = dto.MemNo;
            prm[5] = dto.PayablePrincAmt;
            prm[6] = dto.PayableIntAmt;
            prm[7] = dto.CurrDuePrincAmt;
            prm[8] = dto.CurrDueIntAmt;
            prm[9] = dto.UserId;
            prm[10] = dto.TrnDate;

            BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSLoanAccountDefaulterDataUpdate", prm, "A2ZCSCUBS");

            return 0;

            //int rowEffect = 0;
            //string strQuery = "UPDATE A2ZLOANDEFAULTER set PayablePrincAmt='" + dto.PayablePrincAmt + "',PayableIntAmt='" + dto.PayableIntAmt + "', CurrDuePrincAmt='" + dto.CurrDuePrincAmt + "',CurrDueIntAmt='" + dto.CurrDueIntAmt + "' where BranchNo='" + dto.BranchNo + "' and TrnDate='" + dto.TrnDate + "' and MemType='" + dto.MemType + "' and MemNo='" + dto.MemNo + "' and AccType='" + dto.AccType + "' and AccNo='" + dto.AccNo + "'";
            //rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));
            //if (rowEffect == 0)
            //{
            //    return 0;
            //}
            //else
            //{
            //    return 1;
            //}
        }


    }
}
