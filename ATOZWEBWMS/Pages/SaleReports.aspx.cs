using A2ZWEBWMS.WebSessionStore;
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
    public partial class SaleReports : System.Web.UI.Page
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
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                txtFromDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
                txtToDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
            }

        }



        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }



    }
}