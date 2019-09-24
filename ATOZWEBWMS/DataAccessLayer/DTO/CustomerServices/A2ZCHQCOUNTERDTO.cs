using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO
{
  public  class A2ZCHQCOUNTERDTO
    {
        #region Propertise
        public int BranchNo { set; get; } 
        public Int16 MemType { set; get; } 
        public int MemNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public String ChqPrefix { set; get; }
        public int ChqSerialNo { set; get; }
        public Int16 ChqPage { set; get; }
        public Int16 ChqPStatus { set; get; }
        public DateTime ChqPDate { set; get; }
        public Int16 ChqBStatus { set; get; }
        public DateTime ChqBStatusDate { set; get; }
        public DateTime ChqBIssueDate { set; get; }
        #endregion

        public static int InsertInformation(A2ZCHQCOUNTERDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCHQCOUNTER(BranchNo,MemType,MemNo,AccType,AccNo,ChqeFx,ChqSlNo,ChqbPage,ChqPStat,ChqPDt,ChqBStat,ChqBStatDt,ChqBIssDt)values('" + dto.BranchNo + "','" + dto.MemType + "','" + dto.MemNo + "','" + dto.AccType + "','" + dto.AccNo + "','" + dto.ChqPrefix + "','" + dto.ChqSerialNo + "','" + dto.ChqPage + "','" + dto.ChqPStatus + "','" + dto.ChqPDate + "','" + dto.ChqBStatus + "','" + dto.ChqBStatusDate + "','" + dto.ChqBIssueDate + "')";
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
        public static int UpdateInformation(A2ZCHQCOUNTERDTO dto)
        {
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCHQCOUNTER set ChqPStat='" + dto.ChqPStatus + "',ChqBStat='" + dto.ChqBStatus + "' where BranchNo='" + dto.BranchNo + "' and MemType='" + dto.MemType + "' and MemNo='" + dto.MemNo + "' and AccType='" + dto.AccType + "' and AccNo='" + dto.AccNo + "'";
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
