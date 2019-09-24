using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace A2ZWEBWMS.Pages
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);

                var login = Session["login"];

                Response.Redirect("A2ZLogin.aspx?value=" + login, false);

                Session.RemoveAll();  


                //SessionStore.RemoveFromCustomStore(Params.SYS_USER_ID);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}