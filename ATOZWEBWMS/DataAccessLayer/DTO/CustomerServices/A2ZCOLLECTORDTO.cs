using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZCOLLECTORDTO
    {
        #region Propertise
        public int BranchNo { set; get; }
        public int CollectorNo { set; get; }
        public String CollectorName { set; get; }
        public DateTime opendate { set; get; }
        public int NationalIdNo { set; get; }
        public String AddressL1 { set; get; }
        public String AddressL2 { set; get; }
        public String AddressL3 { set; get; }
        public String TelephoneNo { set; get; }
        public String MobileNo { set; get; }
        public String Fax { set; get; }
        public String email { set; get; }
        public int Division { set; get; }
        public int District { set; get; }
        public int UpZila { set; get; }
        public int Thana { set; get; }
        public Int16 Status { set; get; }
        public DateTime Statusdate { set; get; }
        public String StatusNote { set; get; }
        #endregion


        public static int InsertInformation(A2ZCOLLECTORDTO dto)
        {
            dto.CollectorName = (dto.CollectorName != null) ? dto.CollectorName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCOLLECTOR(BranchNo,ColNo,ColName,ColNatIdNo,ColAddL1,ColAddL2,ColAddL3,ColTel,ColMobile,ColFax,ColEmail,
                       ColDivi,ColDist,ColUpzila,ColThana)values('" + dto.BranchNo + "','" + dto.CollectorNo + "','" + dto.CollectorName 
                      + "','" + dto.NationalIdNo + "','" + dto.AddressL1 + "','" + dto.AddressL2 + "','" + dto.AddressL3 + "','" + dto.TelephoneNo
                      + "','" + dto.MobileNo + "','" + dto.Fax + "','" + dto.email + "','" + dto.Division + "','" + dto.District + "','" + dto.UpZila + "','" + dto.Thana 
                      + "')";
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

        public static A2ZCOLLECTORDTO GetInformation(int BranchNo,int ColNo)
        {
            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZCOLLECTOR WHERE  BranchNo='" + BranchNo + "' AND  ColNo='" + ColNo + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSCUBS");

            var p = new A2ZCOLLECTORDTO();
            if (dt.Rows.Count > 0)
            {

                p.CollectorNo = Converter.GetInteger(dt.Rows[0]["ColNo"]);
                p.CollectorName = Converter.GetString(dt.Rows[0]["ColName"]);
               
                p.NationalIdNo = Converter.GetInteger(dt.Rows[0]["ColNatIdNo"]);
                p.AddressL1 = Converter.GetString(dt.Rows[0]["ColAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["ColAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["ColAddL3"]);
                p.TelephoneNo = Converter.GetString(dt.Rows[0]["ColTel"]);
                p.MobileNo = Converter.GetString(dt.Rows[0]["ColMobile"]);
                p.Fax = Converter.GetString(dt.Rows[0]["ColFax"]);
                p.email = Converter.GetString(dt.Rows[0]["ColEmail"]);
                p.Division = Converter.GetInteger(dt.Rows[0]["ColDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["ColDist"]);
                p.UpZila = Converter.GetInteger(dt.Rows[0]["ColUpZila"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["ColThana"]);
                //p.Status = Converter.GetSmallInteger(dt.Rows[0]["ColStatus"]);
                //p.Statusdate = Converter.GetDateTime(dt.Rows[0]["ColDate"]);
                //p.StatusNote = Converter.GetString(dt.Rows[0]["ColNote"]);
                return p;
            }
            p.CollectorNo = 0;

            return p;

        }

        public static int UpdateInformation(A2ZCOLLECTORDTO dto)
        {
            dto.CollectorName = (dto.CollectorName != null) ? dto.CollectorName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCOLLECTOR set BranchNo='" + dto.BranchNo + "',ColNo='" + dto.CollectorNo + "',ColName='" + dto.CollectorName + "',ColNatIdNo='" + dto.NationalIdNo
                + "',ColAddL1='" + dto.AddressL1 + "',ColAddL2='" + dto.AddressL2 + "',ColAddL3='" + dto.AddressL3 + "',ColTel='" + dto.TelephoneNo
                + "',ColMobile='" + dto.MobileNo + "',ColFax='" + dto.Fax + "',ColEmail='" + dto.email + "',ColDivi='" + dto.Division
                + "',ColDist='" + dto.District + "',ColUpZila='" + dto.UpZila + "',ColThana='" + dto.Thana + "' where  BranchNo='" + dto.BranchNo + "' AND ColNo='" + dto.CollectorNo + "' "; 
               
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
