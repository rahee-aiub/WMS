using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Drawing;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using System.Text;
using A2ZWEBWMS.WebSessionStore;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.CustomerServices;


namespace A2ZWEBWMS.Pages
{
    public partial class OLDA2ZWSERPMODULE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextBox username = new TextBox();



                lblCompanyName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.COMPANY_NAME));


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                lblIDName.Text = "Login as : " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());



            }
        }

        protected void btnOpenNewParty_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpenNewParty.aspx", false);
        }

        protected void btnEditParty_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditParty.aspx", false);
        }

        protected void btnGoldPurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldPurchase.aspx", false);

        }

        protected void btnPurchaseReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldPurchaseReturn.aspx", false);            
        }

        protected void btnSendToTransit_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransitSend.aspx", false);
        }

        protected void btnTransitReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransitSendReturn.aspx", false);
        }

        protected void btnRcvFromCarrier_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiveFromCarrier.aspx", false);
        }       

        protected void btnSaletoParty_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleToParty.aspx", false);
        }

        protected void btnSaleReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleToPartyReturn.aspx", false);
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("a2zLogin.aspx", false);
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx", false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERP.aspx", false);
        }




    }
}