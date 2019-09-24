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
    public class A2ZPARTYDTO
    {
        #region Propertise
        public int ID { get; set; }
        public int GroupCode { set; get; }
        public string GroupName { set; get; }
        public int PartyCode { set; get; }
        public string PartyName { set; get; }
        public string PartyAddresss { set; get; }     
        public string PartyMobileNo { set; get; }
        public string PartyEmail { set; get; }
        public int PartyPurchaseCode { set; get; }
        public int PartySalesCode { set; get; }
    

        #endregion


        public static int InsertInformation(A2ZPARTYDTO dto)
        {
            
            int rowEffect = 0;

            var prm = new object[9];

            prm[0] = dto.GroupCode;
            prm[1] = dto.GroupName;
            prm[2] = dto.PartyCode;
            prm[3] = dto.PartyName;            
            prm[4] = dto.PartyAddresss;         
            prm[5] = dto.PartyMobileNo;
            prm[6] = dto.PartyEmail;
            prm[7] = dto.PartyPurchaseCode;
            prm[8] = dto.PartySalesCode;
           
          
           
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_InsertParty", prm, "A2ZACWMS"));
            if (result == 0)
            {
                rowEffect = 1;
            }
            else
            {
                rowEffect = 0;
            }

            return rowEffect;
        }

        public static A2ZPARTYDTO GetAllLoanParty()
        {



            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySp("Sp_GetPartyInfo", "A2ZACWMS");

            var p = new A2ZPARTYDTO();
            if (dt.Rows.Count > 0)
            {
               
                p.PartyName = Converter.GetString(dt.Rows[0]["PartyName"]);
                p.PartyAddresss = Converter.GetString(dt.Rows[0]["PartyAddresss"]);
                p.PartyMobileNo = Converter.GetString(dt.Rows[0]["PartyMobileNo"]);
                p.PartyEmail = Converter.GetString(dt.Rows[0]["PartyEmail"]);

                return p;
            }

            return p;

        }


        public static A2ZPARTYDTO GetPartyInformation(int PartyCode)
        {

            
            var prm = new object[1];
            
            prm[0] = PartyCode;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetPartyInformation", prm, "A2ZACWMS");

            var p = new A2ZPARTYDTO();
            if (dt.Rows.Count > 0)
            {
                p.GroupCode = Converter.GetInteger(dt.Rows[0]["GroupCode"]);
                p.GroupName = Converter.GetString(dt.Rows[0]["GroupName"]);
                p.PartyCode = Converter.GetInteger(dt.Rows[0]["PartyCode"]);
                p.PartyName = Converter.GetString(dt.Rows[0]["PartyName"]);
                p.PartyAddresss = Converter.GetString(dt.Rows[0]["PartyAddresss"]);
                p.PartyMobileNo = Converter.GetString(dt.Rows[0]["PartyMobileNo"]);
                p.PartyEmail = Converter.GetString(dt.Rows[0]["PartyEmail"]);

                p.PartyPurchaseCode = Converter.GetInteger(dt.Rows[0]["PartyPurchaseCode"]);
                p.PartySalesCode = Converter.GetInteger(dt.Rows[0]["PartySalesCode"]);

                return p;
            }
            else 
            {
                p.PartyCode = 0;
            }

            return p;

        }


        public static A2ZPARTYDTO GetAccNoInformation(Int64 AccNo)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZPARTYCODE WHERE  PartyAccNo='" + AccNo + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACWMS");

            var p = new A2ZPARTYDTO();
            if (dt.Rows.Count > 0)
            {
                p.PartyCode = Converter.GetInteger(dt.Rows[0]["PartyCode"]);
                


                return p;
            }
            p.PartyCode = 0;
            return p;

        }

       
        public static int UpdateParty(A2ZPARTYDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.GroupCode;
            prm[1] = dto.GroupName;
            prm[2] = dto.PartyCode;
            prm[3] = dto.PartyName;
            prm[4] = dto.PartyAddresss;
            prm[5] = dto.PartyMobileNo;
            prm[6] = dto.PartyEmail;
           
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdatePartyInfo", prm, "A2ZACWMS"));
            if (result == 0)
            {
                rowEffect = 1;
            }
            else
            {
                rowEffect = 0;
            }

            return rowEffect;
        }

    

    }
}
