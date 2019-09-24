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
    public partial class A2ZDailyReverseTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcessDate.Text = date;
            }
        }

        

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }

       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime opdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            gvDetailInfo.Visible = true;

            var prm = new object[2];


            prm[0] = txtVoucherNo.Text;
            prm[1] = lblID.Text;



            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GetRevTransaction", prm, "A2ZACWMS"));
            if (result == 0)
            {
                string qry = "SELECT Id,AccNo,FuncOpt,PayType FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACWMS");
                if (dt.Rows.Count > 0)
                {
                   
                    txtVoucherNo.ReadOnly = true;
                    
                    gvDetailInfo.Visible = true;
                    gvPreview();
                }
                else
                {
                    
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher not Found');", true);
                    return;
                }
            }
            else
            {
               
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher not Found');", true);
                return;
            }

        }

        private void gvPreview()
        {
            string Qry = "SELECT FuncOptDesc,TrnDesc,TrnDebitAmt,TrnCreditAmt FROM WF_REVERSETRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnFlag = 0";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZACWMS");
        }

        protected void btnReverse_Click(object sender, EventArgs e)
        {

            DateTime opdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var prm = new object[2];


            prm[0] = txtVoucherNo.Text;
            prm[1] = lblID.Text;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_DeleteTransaction", prm, "A2ZACWMS"));
            if (result == 0)
            {

                gvDetailInfo.Visible = false;
               
                txtVoucherNo.ReadOnly = false;
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.ReadOnly = false;
                txtVoucherNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Reverse Successfully Completed');", true);
                return;
            }



        }


      
    }
}