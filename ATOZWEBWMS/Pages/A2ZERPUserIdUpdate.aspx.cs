using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO;
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
    public partial class A2ZERPUserIdUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

            }
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx");
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery;
                int rowEffiect;

                A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                dto.IdsNo = Converter.GetInteger(txtUserId.Text);
                dto.IdsName = Converter.GetString(txtUserName.Text);
                dto.SODflag = Converter.GetBoolean(ChkSODflag.Checked);

                int noOfRowEffected = A2ZSYSIDSDTO.Update(dto);


                sqlQuery = @"DELETE  FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO = '" + txtUserId.Text + "' ";
                rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKWMS"));

                if (ChkWSmoduleflag.Checked == true)
                {
                    sqlQuery = "INSERT INTO A2ZSYSMODULECTRL (IdsNo,ModuleNo,ModuleName) VALUES ('" + txtUserId.Text + "',1,'Weight Service')";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKWMS"));
                }

                if (ChkHKmoduleflag.Checked == true)
                {
                    sqlQuery = "INSERT INTO A2ZSYSMODULECTRL (IdsNo,ModuleNo,ModuleName) VALUES ('" + txtUserId.Text + "',2,'House Keeping')";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKWMS"));
                }


                Response.Redirect(Request.RawUrl);


            }
            catch (Exception ex)
            {

                //throw ex;
            }
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            A2ZSYSIDSDTO dto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtUserId.Text), "A2ZHKWMS");

            if (dto.IdsNo != 0)
            {
                txtUserId.Text = Converter.GetString(dto.IdsNo);
                txtUserName.Text = Converter.GetString(dto.IdsName);
                ChkSODflag.Checked = Converter.GetBoolean(dto.SODflag);


                string sqlQuery = "SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO = '" + txtUserId.Text + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKWMS");

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {
                        int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString());
                        if (moduleNo == 1)
                        {
                            ChkWSmoduleflag.Checked = true;
                        }

                        if (moduleNo == 2)
                        {
                            ChkHKmoduleflag.Checked = true;
                        }
                    }
                }
            }
            else
            {
                txtUserId.Text = string.Empty;
                txtUserId.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Id. Not Found');", true);
                return;
            }
        }



    }
}