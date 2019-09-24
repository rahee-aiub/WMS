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
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public int AccPartyNo { set; get; }
        public DateTime Opendate { set; get; }
        public int AccCurrency { set; get; }
        public int AccStatus { set; get; }
        public decimal AccBalance { set; get; }
        public decimal AccPrincipal { set; get; }
        public decimal AccInterest { set; get; }
        public DateTime LastTrnDate { set; get; }
        public decimal LoanAmount { set; get; }
        public DateTime AccLoanEffectingDate { set; get; }
        public DateTime AccLoanExpiryDate { set; get; }
        public int AccCalculateDays { set; get; }
        public decimal AccIntRate { set; get; }
        public decimal AccOpBal { set; get; }
        public Int16 a { set; get; }
          
        #endregion


        

        public static A2ZACCOUNTDTO GetInfoAccNo(Int64 AccountNo)
        {

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm1 = new object[3];    
            prm1[0] = AccountNo;
            prm1[1] = Converter.GetDateToYYYYMMDD(date1); 
            prm1[2] = 0;

            string input = Converter.GetString(AccountNo);
            string AccType = input.Substring(0, 2);

            
            DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_GenerateSingleAccountBalance", prm1, "A2ZACWMS");
            
            //}
            //else if (AccType == "16") 
            //{
            //    DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_GenerateSingleCashBalance", prm1, "A2ZACWMS");
            //}
            //else
            //{
            //    DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_GenerateSinglePartyBalance", prm1, "A2ZACWMS");
            //}


           


            var prm = new object[1];    
            prm[0] = AccountNo;
                       
            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetInfoAccountNo", prm, "A2ZACWMS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["Id"]);
               
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.AccPartyNo = Converter.GetInteger(dt.Rows[0]["AccPartyNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.AccCurrency = Converter.GetInteger(dt.Rows[0]["AccCurrency"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccInterest = Converter.GetDecimal(dt.Rows[0]["AccInterest"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanAmt"]);

                p.AccLoanEffectingDate = Converter.GetDateTime(dt.Rows[0]["AccLoanEffectingDate"]);
                p.AccLoanExpiryDate = Converter.GetDateTime(dt.Rows[0]["AccLoanExpiryDate"]);
                p.AccCalculateDays = Converter.GetInteger(dt.Rows[0]["AccCalculateDays"]);

                p.AccIntRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);

                                          
                p.AccOpBal = Converter.GetDecimal(dt.Rows[0]["AccOpBal"]);


                p.a = Converter.GetSmallInteger(1);

                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
                p.a = 0;
              
            }


            return p;

        }


     
    }
}
