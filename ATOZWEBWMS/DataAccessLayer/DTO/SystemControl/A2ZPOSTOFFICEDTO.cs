using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZPOSTOFFICEDTO
    {
        #region Propertise

       public int PostOfficeCode { set; get; }
       public String PostOfficeName { set; get; }
        #endregion


       public static int InsertInformation(A2ZPOSTOFFICEDTO dto)
        {
            dto.PostOfficeName = (dto.PostOfficeName != null) ? dto.PostOfficeName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZPOSTOFFICE(PostOfficeCode,PostOfficeName)values('" + dto.PostOfficeCode + "','" + dto.PostOfficeName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

       public static A2ZPOSTOFFICEDTO GetInformation(int POfficecode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZPOSTOFFICE WHERE PostOfficeCode = " + POfficecode, "A2ZHKCUBS");


            var p = new A2ZPOSTOFFICEDTO();
            if (dt.Rows.Count > 0)
            {

                p.PostOfficeCode = Converter.GetInteger(dt.Rows[0]["PostOfficeCode"]);
                p.PostOfficeName = Converter.GetString(dt.Rows[0]["PostOfficeName"]);
                return p;
            }
            p.PostOfficeCode = 0;

            return p;

        }
       public static int UpdateInformation(A2ZPOSTOFFICEDTO dto)
        {
            dto.PostOfficeName = (dto.PostOfficeName != null) ? dto.PostOfficeName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZPOSTOFFICE set PostOfficeCode='" + dto.PostOfficeCode + "',PostOfficeName='" + dto.PostOfficeName + "' where PostOfficeCode='" + dto.PostOfficeCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));
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
