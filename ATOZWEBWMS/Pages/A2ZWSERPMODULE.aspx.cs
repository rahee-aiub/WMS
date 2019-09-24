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
    public partial class A2ZWSERPMODULE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextBox username = new TextBox();

                DivParty.Visible = false;
                DivPurchase.Visible = false;
                DivTransit.Visible = false;
                DivReceive.Visible = false;
                DivSale.Visible = false;
                DivReports.Visible = false;

                lblCompanyName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.COMPANY_NAME));


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                lblIDName.Text = "Login as : " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());



            }
        }

        


        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("a2zLogin.aspx", false);
        }

        

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERP.aspx", false);
        }

        protected void btnParty_Click(object sender, EventArgs e)
        {
            DivParty.Visible = true;

            btnParty.Visible = false;
            btnPurchase.Visible = false;
            btnTransit.Visible = false;
            btnReceive.Visible = false;
            btnSale.Visible = false;
            btnReports.Visible = false;
            btnSpecial.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivParty.Attributes.CssStyle.Add("opacity", "0.7");
            DivParty.Attributes.CssStyle.Add("opacity", "0.7");



            DivParty.Style.Add("Top", "150px");
            DivParty.Style.Add("left", "36px");
            DivParty.Style.Add("position", "fixed");
            DivParty.Attributes.CssStyle.Add("opacity", "200");
            DivParty.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnPartyBack_Click(object sender, EventArgs e)
        {

            DivParty.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            DivPurchase.Visible = true;

            btnParty.Visible = false;
            btnPurchase.Visible = false;
            btnTransit.Visible = false;
            btnReceive.Visible = false;
            btnSale.Visible = false;
            btnReports.Visible = false;
            btnSpecial.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;

            DivPurchase.Attributes.CssStyle.Add("opacity", "0.7");
            DivPurchase.Attributes.CssStyle.Add("opacity", "0.7");



            DivPurchase.Style.Add("Top", "150px");
            DivPurchase.Style.Add("left", "36px");
            DivPurchase.Style.Add("position", "fixed");
            DivPurchase.Attributes.CssStyle.Add("opacity", "200");
            DivPurchase.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnPurchaseBack_Click(object sender, EventArgs e)
        {

            DivPurchase.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }

        protected void btnTransit_Click(object sender, EventArgs e)
        {
            DivTransit.Visible = true;

            btnParty.Visible = false;
            btnPurchase.Visible = false;
            btnTransit.Visible = false;
            btnReceive.Visible = false;
            btnSale.Visible = false;
            btnReports.Visible = false;
            btnSpecial.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivTransit.Attributes.CssStyle.Add("opacity", "0.7");
            DivTransit.Attributes.CssStyle.Add("opacity", "0.7");



            DivTransit.Style.Add("Top", "150px");
            DivTransit.Style.Add("left", "36px");
            DivTransit.Style.Add("position", "fixed");
            DivTransit.Attributes.CssStyle.Add("opacity", "200");
            DivTransit.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnTransitBack_Click(object sender, EventArgs e)
        {

            DivTransit.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldReceive.aspx", false);


            //DivReceive.Visible = true;

            //btnParty.Visible = false;
            //btnPurchase.Visible = false;
            //btnTransit.Visible = false;
            //btnReceive.Visible = false;
            //btnSale.Visible = false;
            //btnReports.Visible = false;
            //btnSpecial.Visible = false;
            //btnBack.Visible = false;
            //btnLogOut.Visible = false;


            //DivReceive.Attributes.CssStyle.Add("opacity", "0.7");
            //DivReceive.Attributes.CssStyle.Add("opacity", "0.7");



            //DivReceive.Style.Add("Top", "150px");
            //DivReceive.Style.Add("left", "36px");
            //DivReceive.Style.Add("position", "fixed");
            //DivReceive.Attributes.CssStyle.Add("opacity", "200");
            //DivReceive.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnReceiveBack_Click(object sender, EventArgs e)
        {




            DivReceive.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }
        protected void btnSale_Click(object sender, EventArgs e)
        {
            DivSale.Visible = true;

            btnParty.Visible = false;
            btnPurchase.Visible = false;
            btnTransit.Visible = false;
            btnReceive.Visible = false;
            btnSale.Visible = false;
            btnReports.Visible = false;
            btnSpecial.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivSale.Attributes.CssStyle.Add("opacity", "0.7");
            DivSale.Attributes.CssStyle.Add("opacity", "0.7");



            DivSale.Style.Add("Top", "150px");
            DivSale.Style.Add("left", "36px");
            DivSale.Style.Add("position", "fixed");
            DivSale.Attributes.CssStyle.Add("opacity", "200");
            DivSale.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnSaleBack_Click(object sender, EventArgs e)
        {

            DivSale.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }
        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx", false);


            //DivReports.Visible = true;

            //btnParty.Visible = false;
            //btnPurchase.Visible = false;
            //btnTransit.Visible = false;
            //btnReceive.Visible = false;
            //btnSale.Visible = false;
            //btnReports.Visible = false;
            //btnSpecial.Visible = false;
            //btnBack.Visible = false;
            //btnLogOut.Visible = false;


            //DivReports.Attributes.CssStyle.Add("opacity", "0.7");
            //DivReports.Attributes.CssStyle.Add("opacity", "0.7");



            //DivReports.Style.Add("Top", "150px");
            //DivReports.Style.Add("left", "36px");
            //DivReports.Style.Add("position", "fixed");
            //DivReports.Attributes.CssStyle.Add("opacity", "200");
            //DivReports.Attributes.CssStyle.Add("z-index", "200");

        }

        protected void btnReportsBack_Click(object sender, EventArgs e)
        {

            DivReports.Visible = false;

            btnParty.Visible = true;
            btnPurchase.Visible = true;
            btnTransit.Visible = true;
            btnReceive.Visible = true;
            btnSale.Visible = true;
            btnReports.Visible = true;
            btnSpecial.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }
        protected void btnNewParty_Click(object sender, EventArgs e)
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

        protected void btnGPurchaseReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldPurchaseReturn.aspx", false);
            
        }

        protected void btnSendTransit_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldTransit.aspx", false);
            
        }

        protected void btnTransitReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldTransitReturn.aspx", false);

        }

        protected void btnReceiveCarrier_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiveReports.aspx", false);

        }

        protected void btnReceiveDirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiveDirect.aspx", false);

        }
        protected void btnSaleParty_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldSale.aspx", false);

        }

        protected void btnSaleReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoldSaleReturn.aspx", false);

        }

        protected void btnPurchaseReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseReports.aspx", false);

        }

        protected void btnPurchaseReturnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleToPartyReturn.aspx", false);

        }

        protected void btnTransitReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransitReports.aspx", false);

        }

        protected void btnTransitReceiveReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleToPartyReturn.aspx", false);

        }

        protected void btnSalesReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleReports.aspx", false);

        }

        protected void btnSalesReturnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleToPartyReturn.aspx", false);

        }

        
        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZDailyReverseTransaction.aspx", false);
        }

        
    }
}