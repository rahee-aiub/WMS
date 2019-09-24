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
    public partial class StartUp2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                TextBox username = new TextBox();

              

                lblCompanyName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.COMPANY_NAME));


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                lblIDName.Text ="Login as : " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

             
              
            }
        }


      

    }
}