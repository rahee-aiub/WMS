using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public  class A2ZCURRENCYDTO
    {
        #region Propertise

        public int CurrencyCode { set; get; }
        public String CurrencyName { set; get; }
        #endregion


        public static int InsertInformation(A2ZCURRENCYDTO dto)
        {
            dto.CurrencyName = (dto.CurrencyName != null) ? dto.CurrencyName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCURRENCY(CurrencyCode,CurrencyName)values('" + dto.CurrencyCode + "','" + dto.CurrencyName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACWMS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZCURRENCYDTO GetInformation(int currency)
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCURRENCY WHERE CurrencyCode = " + currency, "A2ZACWMS");

            

            var p = new A2ZCURRENCYDTO();
            if (dt.Rows.Count > 0)
            {

                p.CurrencyCode = Converter.GetInteger(dt.Rows[0]["CurrencyCode"]);
                p.CurrencyName = Converter.GetString(dt.Rows[0]["CurrencyName"]);
                return p;
            }
            p.CurrencyCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZCURRENCYDTO dto)
        {
            dto.CurrencyName = (dto.CurrencyName != null) ? dto.CurrencyName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCURRENCY set CurrencyCode='" + dto.CurrencyCode + "',CurrencyName='" + dto.CurrencyName + "' where CurrencyCode='" + dto.CurrencyCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACWMS"));
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
