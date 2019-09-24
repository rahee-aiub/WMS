using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.SystemControl
{
    public class A2ZVILLAGEDTO
    {
        #region Propertise

        public int VillageCode { set; get; }
        public String VillageName { set; get; }
        public int PostOfficeCode { set; get; }
        public int DivisionCode { set; get; }
        public int DistrictCode { set; get; }
        public int UpzilaCode { set; get; }
        public int ThanaCode { set; get; }

        #endregion


        public static int InsertInformation(A2ZVILLAGEDTO dto)
        {
            dto.VillageName = (dto.VillageName != null) ? dto.VillageName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZVILLAGE(VillageCode,VillageName,PostOfficeCode,DiviCode,DistCode,UpzilaCode,ThanaCode)values('" + dto.VillageCode + "','" + dto.VillageName + "','" + dto.PostOfficeCode + "','" + dto.DivisionCode + "','" + dto.DistrictCode + "','" + dto.UpzilaCode + "','" + dto.ThanaCode + "')";
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

        public static A2ZVILLAGEDTO GetInformation(int VillageCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZVILLAGE WHERE VillageCode = " + VillageCode, "A2ZHKCUBS");


            var p = new A2ZVILLAGEDTO();
            if (dt.Rows.Count > 0)
            {

                p.VillageCode = Converter.GetInteger(dt.Rows[0]["VillageCode"]);
                p.VillageName = Converter.GetString(dt.Rows[0]["VillageName"]);

                p.PostOfficeCode = Converter.GetInteger(dt.Rows[0]["PostOfficeCode"]);
                p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
                p.DistrictCode = Converter.GetInteger(dt.Rows[0]["DistCode"]);
                p.UpzilaCode = Converter.GetInteger(dt.Rows[0]["UpzilaCode"]);
                p.ThanaCode = Converter.GetInteger(dt.Rows[0]["ThanaCode"]);
                return p;
            }
            p.VillageCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZVILLAGEDTO dto)
        {
            dto.VillageName = (dto.VillageName != null) ? dto.VillageName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZVILLAGE set VillageCode='" + dto.VillageCode + "',VillageName='" + dto.VillageName + "',PostOfficeCode='" + dto.PostOfficeCode + "',DiviCode='" + dto.DivisionCode + "',DistCode='" + dto.DistrictCode + "',UpzilaCode='" + dto.UpzilaCode + "',ThanaCode='" + dto.ThanaCode + "' where VillageCode='" + dto.VillageCode + "'";
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
