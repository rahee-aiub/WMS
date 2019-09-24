using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZCOLLGROUPDTO
    {
        #region Propertise
        public int BranchNo { set; get; }
        public int RegNo { set; get; }
        public int SubRegNo { set; get; }
        public int ColNo { set; get; }
        
        
                
        #endregion


        public static int InsertInformation(A2ZCOLLGROUPDTO dto)
        {
          
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCOLLGROUP(BranchNo,REGNO,SUBREGNO,COLNO)values('" + dto.BranchNo + "','" + dto.RegNo + "','" + dto.SubRegNo + "','" + dto.ColNo + "')";
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

        public static A2ZCOLLGROUPDTO GetInformation(int BranchNo,int RegNo, int SubRegNo)
        {
            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZCOLLGROUP WHERE  BranchNo='" + BranchNo + "' AND  RegNo='" + RegNo + "' AND  SubRegNo='" + SubRegNo + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSCUBS");



            var p = new A2ZCOLLGROUPDTO();
            if (dt.Rows.Count > 0)
            {

                p.RegNo = Converter.GetInteger(dt.Rows[0]["RegNo"]);
                p.SubRegNo = Converter.GetInteger(dt.Rows[0]["SubRegNo"]);

                p.ColNo = Converter.GetInteger(dt.Rows[0]["ColNo"]);

                               
                return p;
            }
            p.RegNo = 0;

            return p;

        }

        public static int UpdateInformation(A2ZCOLLGROUPDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCOLLGROUP set BranchNo='" + dto.BranchNo + "', RegNo='" + dto.RegNo + "',SubRegNo='" + dto.SubRegNo + "',ColNo='" + dto.ColNo + "'"; 
                
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
