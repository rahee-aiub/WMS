using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZVCHNOCTRLDTO
    {
        #region Propertise

        public int RecCode { set; get; }
        public int RecLastNo { set; get; }
        public int Record { set; get; }

        #endregion



        public static A2ZVCHNOCTRLDTO GetLastCollVchNo(int BranchNo, int Code)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCOLLECTOR WHERE BranchNo ='" + BranchNo + "' and  ColNo='" + Code + "'", "A2ZCSCUBS");

            var p = new A2ZVCHNOCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                p.RecCode = 1;
                int rowEffect = 0;
                string strQuery = "UPDATE A2ZCOLLECTOR set LastVoucherNo='" + p.RecLastNo + "' WHERE BranchNo ='" + BranchNo + "' and  ColNo='" + Code + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

                return p;
            }
            p.RecCode = 0;
            return p;

        }

        public static A2ZVCHNOCTRLDTO GetLastUserIdCSVchNo(int Code)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSYSIDS WHERE IdsNo ='" + Code + "'", "A2ZCSCUBS");

            var p = new A2ZVCHNOCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["CSLastVchNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                p.RecCode = 1;
                int rowEffect = 0;
                string strQuery = "UPDATE A2ZSYSIDS set CSLastVchNo='" + p.RecLastNo + "' WHERE IdsNo ='" + Code + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

                return p;
            }
            p.RecCode = 0;
            return p;

        }

        public static A2ZVCHNOCTRLDTO GetLastUserIdGLVchNo(int Code)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSYSIDS WHERE IdsNo ='" + Code + "'", "A2ZCSCUBS");

            var p = new A2ZVCHNOCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["GLLastVchNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                p.RecCode = 1;
                int rowEffect = 0;
                string strQuery = "UPDATE A2ZSYSIDS set GLLastVchNo='" + p.RecLastNo + "' WHERE IdsNo ='" + Code + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

                return p;
            }
            p.RecCode = 0;
            return p;

        }

        public static A2ZVCHNOCTRLDTO GetLastDefaultVchNo()
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAMETER", "A2ZCSCUBS");

            var p = new A2ZVCHNOCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                p.RecCode = 1;
                int rowEffect = 0;
                string strQuery = "UPDATE A2ZCSPARAMETER set LastVoucherNo='" + p.RecLastNo + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

                return p;
            }
            p.RecCode = 0;
            return p;

        }

        //public static A2ZVCHNOCTRLDTO ReadLastVoucherNo(int CashCode, Int16 RecType)
        //{
        //    DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZRECCTRLNO WHERE CtrlGLCashCode ='" + CashCode + "' and  CtrlRecType='" + RecType + "' ", "A2ZCSCUBS");


        //    var p = new A2ZVCHNOCTRLDTO();
        //    if (dt.Rows.Count > 0)
        //    {

        //        p.GLCashCode = Converter.GetInteger(dt.Rows[0]["CtrlGLCashCode"]);
        //        p.RecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);


        //        return p;
        //    }
        //    p.GLCashCode = 0;
        //    return p;

        //}


    }
}
