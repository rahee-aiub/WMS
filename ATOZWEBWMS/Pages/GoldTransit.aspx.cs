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
    public partial class GoldTransit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DivWeightView.Visible = false;

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

                TransitPartyDropdown();
                TrackingPartyDropdown();
                //KaratDropdown();

                CalculateBalance();

                TruncateWF();


            }

        }

        private void CalculateBalance()
        {
            int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_GenerateAccountBalance]", "A2ZACWMS"));

        }
        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZTRANSACTION WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACWMS"));
        }
        private void TransitPartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlTransitPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTransitPartyName, "A2ZACWMS");
        }


        private void TrackingPartyDropdown()
        {
            string sqlquery = "SELECT A2ZPARTYCODE.PartyCode,A2ZPARTYCODE.PartyName FROM A2ZACCOUNT inner join A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=A2ZACCOUNT.AccPartyNo Where AccType = 12 AND AccLocation = 1 AND AccBalance > 0";
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
                if (ddlTransitPartyName.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Transit Party');", true);
                    return;
                }


                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "GT" + getDTO.RecLastNo.ToString("000000");

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

            if (ddlTransitPartyName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Transit Code');", true);
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
                prm[1] = ddlTransitPartyName.SelectedValue; // TrnKeyNo
                prm[2] = ddlCode.SelectedValue; // RefTrnKeyNo
                prm[3] = ddlKarat.SelectedValue; // AccKarat
                prm[4] = "2";  // Location
                prm[5] = "11"; // FuncOpt
                prm[6] = "Gold Transit"; //FuncOptDesc
                prm[7] = "0"; // PayType
                prm[8] = "1"; // TrnType
                prm[9] = "1"; // TrnDrCr  
                prm[10] = txtGrossWt.Text; // TrnAmount 
                prm[11] = "0"; // TrnFlag
                prm[12] = "0"; // TrnProcStat
                prm[13] = lblID.Text; // UserID

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfGoldTransit]", prm, "A2ZACWMS"));

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

                int NId2 = NId1 + 1;

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));

                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + NId1 + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));

                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + NId2 + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));


                gvDetailsInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnWtBalance_Click(object sender, EventArgs e)
        {
            DivWeightView.Style.Add("Top", "220px");
            DivWeightView.Style.Add("left", "350px");
            DivWeightView.Style.Add("position", "fixed");

            //divMain.Attributes.CssStyle.Add("opacity", "0.5");

            DivWeightView.Attributes.CssStyle.Add("opacity", "400");
            DivWeightView.Attributes.CssStyle.Add("z-index", "400");

            DivWeightView.Visible = true;
            //btnBack.Visible = true;

            //lblWeightLocation.Text = ddlLocation.SelectedItem.Text;

            BtnAddItem.Enabled = false;

            gvItemStockDetailsInfo();
        }

        private void gvItemStockDetailsInfo()
        {
            string sqlquery = "SELECT A2ZACCOUNT.Id,A2ZPARTYCODE.PartyName,A2ZACCOUNT.AccKarat,A2ZACCOUNT.AccBalance FROM A2ZACCOUNT inner join A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=A2ZACCOUNT.AccPartyNo Where AccType = 12 AND AccLocation = 1 AND AccBalance > 0";
            gvItemStockDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemStockDetails, "A2ZACWMS");
        }

        protected void btnBackStock_Click(object sender, EventArgs e)
        {
            DivWeightView.Visible = false;
            //btnBackStock.Visible = false;
            BtnAddItem.Enabled = true;

            //divMain.Attributes.CssStyle.Add("opacity", "300");
        }

        private void BalanceWeight()
        {
            string qry = "SELECT AccBalance FROM A2ZACCOUNT WHERE AccLocation = 1 AND AccType = 12 AND AccNo = " + ddlCode.SelectedValue + " AND AccKarat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACWMS");
            if (dt.Rows.Count > 0)
            {
                lblAccBalance.Text = Converter.GetString(dt.Rows[0]["AccBalance"]);       
            }
        }


        protected void TotalWeight()
        {
            BalanceWeight();

            Decimal sumAmt = 0;
           

            for (int i = 0; i < gvDetails.Rows.Count; ++i)
            {
                Label keyno = (Label)gvDetails.Rows[i].Cells[1].FindControl("lblTrnKeyNo");
                Label karat = (Label)gvDetails.Rows[i].Cells[3].FindControl("lblAccKarat");
                Label amount = (Label)gvDetails.Rows[i].Cells[4].FindControl("lblTrnAmount");

                if (amount.Text != string.Empty && keyno.Text == ddlCode.SelectedValue && karat.Text == ddlKarat.SelectedValue)
                {
                    sumAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", amount.Text));
                }
            }


            decimal balance = Converter.GetDecimal(lblAccBalance.Text);

            decimal netbalance = (balance - sumAmt);

            lblAccBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", netbalance));
        }


        protected void txtGrossWt_TextChanged(object sender, EventArgs e)
        {
            TotalWeight();

            decimal InputWeight = Converter.GetDecimal(txtGrossWt.Text);
            decimal BalanceWeight = Converter.GetDecimal(lblAccBalance.Text);

            if (InputWeight > BalanceWeight)
            {
                txtGrossWt.Text = string.Empty;
                txtGrossWt.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Weight Not Avaiable');", true);
                return;
            }

        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string qry2 = "SELECT SUM(TrnAmount) AS TrnAmount FROM WFA2ZTRANSACTION WHERE TrnKeyNo = '" + ddlTransitPartyName.SelectedValue + "'";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZACWMS");
            if (dt2.Rows.Count > 0)
            {
                lblTotalValue.Text = Converter.GetString(dt2.Rows[0]["TrnAmount"]);
            }
        }

    }
}