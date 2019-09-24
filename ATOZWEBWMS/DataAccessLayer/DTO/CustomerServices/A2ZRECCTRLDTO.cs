using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZRECCTRLDTO
    {
        #region Propertise

        public int BranchNo { set; get; }
        public Int16 CtrlRecType { set; get; }
        public int CtrlRecLastNo { set; get; }

        public int GLCashCode { set; get; }

        public int RecLastVchNo { set; get; }

        #endregion

        public static A2ZRECCTRLDTO UpdateLastRecords(int BranchNo, Int16 RecType,int MemNo)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE  BranchNo='" + BranchNo + "' AND  CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSCUBS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.CtrlRecLastNo = p.CtrlRecLastNo + 1;

                int rowEffect = 0;
                string str1Query = "UPDATE A2ZRECCTRL set CtrlRecLastNo='" + MemNo + "' where BranchNo='" + BranchNo + "' AND CtrlRecType='" + p.CtrlRecType + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSCUBS"));

                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }

        public static A2ZRECCTRLDTO GetLastRecords(int BranchNo, Int16 RecType)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE  BranchNo='" + BranchNo + "' AND  CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSCUBS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.CtrlRecLastNo = p.CtrlRecLastNo + 1;

                int rowEffect = 0;
                string str1Query = "UPDATE A2ZRECCTRL set CtrlRecLastNo='" + p.CtrlRecLastNo + "' where BranchNo='" + BranchNo + "' AND CtrlRecType='" + p.CtrlRecType + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSCUBS"));

                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }
        public static A2ZRECCTRLDTO ReadLastRecords(int BranchNo, Int16 RecType)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE  BranchNo='" + BranchNo + "' AND  CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSCUBS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);


                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }
        //---------------------------  VOUCHER'S INFORMATIONS ------------------------
        public static A2ZRECCTRLDTO GetLastVoucherNo(int CashCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCGLMST WHERE GLAccNo = " + CashCode, "A2ZGLCUBS");


            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                p.RecLastVchNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.RecLastVchNo = p.RecLastVchNo + 1;

                int rowEffect = 0;
                string strQuery = "UPDATE A2ZCGLMST set LastVoucherNo='" + p.RecLastVchNo + "' where GLAccNo='" + p.GLCashCode + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLCUBS"));

                return p;
            }
            p.GLCashCode = 0;
            return p;

        }

        public static A2ZRECCTRLDTO ReadLastVoucherNo(int CashCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCGLMST WHERE GLAccNo = " + CashCode, "A2ZGLCUBS");


            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                p.RecLastVchNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);


                return p;
            }
            p.GLCashCode = 0;
            return p;

        }
    }
}
