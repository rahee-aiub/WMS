using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class ReceiveFromCarrier : System.Web.UI.Page
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

                LocationDropdown();
                PartyDropdown();

                ddlLocation.SelectedIndex = 1;
            }

        }

        private void LocationDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND RIGHT(PartyCode,1) = 1 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlLocation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLocation, "A2ZACWMS");
        }
        private void PartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACWMS");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }

      

     

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "RFC" + getDTO.RecLastNo.ToString("000000");

                //lblDescription.Text = "Gw:" + lblGrossWt.Text + ",St:" + lblStoneWt.Text + ",Nw:" + lblNetWt.Text + ",Pt:" + lblPureWt.Text + ",Sv:" + lblStoneValue.Text + ",Mc:" + lblMaking.Text;


                var prm = new object[10];

                prm[0] = CtrlVoucherNo.Text;
                prm[1] = ddlPartyName.SelectedValue;
                prm[2] = txtGoldWeight.Text;
                prm[3] = Converter.GetDateToYYYYMMDD(lblProcessDate.Text);
                prm[4] = "21";
                prm[5] = "Receive From Carrier";
                prm[6] = lblID.Text;
                prm[7] = ddlLocation.SelectedValue;
                prm[8] = "1";
                prm[9] = "13";


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GoldTransaction", prm, "A2ZACWMS"));
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
    }
}