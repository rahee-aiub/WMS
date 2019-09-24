using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class GoldSaleReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
              
                if (lblID.Text == string.Empty || lblID.Text == "0")
                {
                    Response.Redirect("A2ZLogin.aspx");
                }

                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcessDate.Text = date;

                SalePartyDropdown();
                TrackingPartyDropdown();
                //KaratDropdown();

                TruncateWF();

               
            }

        }

        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZTRANSACTION WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACWMS"));
        }
        private void SalePartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 20 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlSalePartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSalePartyName, "A2ZACWMS");
        }


        private void TrackingPartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 12 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZACWMS");
        }

        //private void KaratDropdown()
        //{
        //    string sqlquery = "SELECT DISTINCT KaratCode,KaratName from A2ZKARAT ORDER BY KaratCode desc";
        //    ddlKarat = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlKarat, "A2ZACWMS");
        //}
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }

     

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSalePartyName.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Sale Party');", true);
                    return;
                }

               
                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "GSR" + getDTO.RecLastNo.ToString("000000");

                var prm = new object[2];

                prm[0] = CtrlVoucherNo.Text;
                prm[1] = lblID.Text;
              
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateGoldTransaction", prm, "A2ZACWMS"));
                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                
                //throw ex;
            }
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {

            if (ddlSalePartyName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Sale Code');", true);
                return;
            }

            if (ddlCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Code');", true);
                return;
            }

            if (ddlKarat.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Karat');", true);
                return;
            }


            if (txtGrossWt.Text == string.Empty || Converter.GetDecimal(txtGrossWt.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Weight');", true);
                return;
            }


            try
            {
                var prm = new object[14];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcessDate.Text); // TrnDate
                prm[1] = ddlSalePartyName.SelectedValue; // TrnKeyNo
                prm[2] = ddlCode.SelectedValue; // RefTrnKeyNo
                prm[3] = ddlKarat.SelectedValue; // AccKarat
                prm[4] = "3";  // Location
                prm[5] = "42"; // FuncOpt
                prm[6] = "Gold Sale Return"; //FuncOptDesc
                prm[7] = "0"; // PayType
                prm[8] = "1"; // TrnType
                prm[9] = "0"; // TrnDrCr  
                prm[10] = txtGrossWt.Text; // TrnAmount 
                prm[11] = "0"; // TrnFlag
                prm[12] = "0"; // TrnProcStat
                prm[13] = lblID.Text; // UserID

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfGoldSale]", prm, "A2ZACWMS"));

                if (result == 0)
                {
                    ClearInfo();
                    gvDetailsInfo();
                    
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void ClearInfo()
        {
            ddlCode.SelectedIndex = 0;
            ddlKarat.SelectedIndex = 0;
            txtGrossWt.Text = string.Empty;
        }
        private void gvDetailsInfo()
        {

            string sqlquery = @"SELECT WFA2ZTRANSACTION.Id,WFA2ZTRANSACTION.RefTrnKeyNo,A2ZPARTYCODE.PartyName,WFA2ZTRANSACTION.AccKarat,WFA2ZTRANSACTION.TrnAmount FROM WFA2ZTRANSACTION inner join A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=WFA2ZTRANSACTION.RefTrnKeyNo WHERE WFA2ZTRANSACTION.UserId = " + lblID.Text + " AND WFA2ZTRANSACTION.TrnFlag = 0 ORDER BY WFA2ZTRANSACTION.Id asc";
                        
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZACWMS");
        }
        protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label IdNo = (Label)gvDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                int NId1 = Id + 1;

               
                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));

                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + NId1 + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));

              
                
                gvDetailsInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string qry2 = "SELECT SUM(TrnAmount) AS TrnAmount FROM WFA2ZTRANSACTION WHERE TrnKeyNo = '" + ddlSalePartyName.SelectedValue + "'";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZACWMS");
            if (dt2.Rows.Count > 0)
            {
                lblTotalValue.Text = Converter.GetString(dt2.Rows[0]["TrnAmount"]);
            }
        }

        
    }
}