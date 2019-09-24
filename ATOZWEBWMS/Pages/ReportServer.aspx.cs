﻿using A2ZWEBWMS.WebSessionStore;
using System;
using System.Configuration;
using System.Drawing;
using System.Management;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DataAccessLayer.Utility;
using CrystalDecisions.CrystalReports.ViewerObjectModel;


namespace A2ZWEBWMS.Pages
{
    public partial class ReportServer : System.Web.UI.Page
    {
        string aspxPreviousPageKey = "AspxPreviousPage";
        private String AspxPreviousPage
        {
            get
            {
                //session should different key for different users
                //That is why I add SessionID in key, otherwise data become same for all users
                if (Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] != null)
                {
                    return Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] as String;
                }
                return null;
            }
            set
            {
                if (Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] != null)
                {
                    Page.Cache.Remove(aspxPreviousPageKey + Page.Session.SessionID);
                }
                Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                    {
                        AspxPreviousPage = Request.UrlReferrer.ToString();
                    }                    
                }
            }
            catch (Exception ex)
            {
                string notifyMsg = "?msg=" + ex.ToString() + System.Environment.NewLine + Request.UrlReferrer + "&PreviousMenu=A2ZERPModule.aspx";
                Server.Transfer("MessageDisplay.aspx" + notifyMsg);

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ReportServer Page Load Problem');</script>");
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {

            Response.Redirect(AspxPreviousPage, false);
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GenerateReport();                
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["ReportDocument"];
                CrystalReportViewer1.ReportSource = doc;
            }
        }
        protected void GenerateReport()
        {
            string paramName = string.Empty;
            try
            {                
                ReportDocument theReport = new ReportDocument();
                string reportFile = Converter.GetString(SessionStore.GetValue(Params.REPORT_FILE_NAME_KEY));

                if ((reportFile != string.Empty) & (reportFile != null))
                {
                    TableLogOnInfo myLog;
                    CrystalDecisions.CrystalReports.Engine.Table myTable;
                    reportFile = reportFile + ".rpt";
                    string reportPath = this.MapPath(".") + @"\Reports\" + reportFile;
                    theReport.Load(reportPath);
                    ConnectionInfo conn = new ConnectionInfo();
                    //conn.ServerName = ConfigurationManager.AppSettings["DBServer"].Trim();
                    conn.DatabaseName = Converter.GetString(SessionStore.GetValue(Params.REPORT_DATABASE_NAME_KEY));//ConfigurationManager.AppSettings["InitialDB"].Trim();
                    //conn.UserID = ConfigurationManager.AppSettings["DBUserName"].Trim();
                    //conn.Password = ConfigurationManager.AppSettings["DBPassword"].Trim();
                    conn.IntegratedSecurity = true;
                    int tableCount = theReport.Database.Tables.Count - 1;
                    for (int i = 0; i <= tableCount; i++)
                    {
                        myTable = theReport.Database.Tables[i];
                        myLog = myTable.LogOnInfo;
                        myLog.ConnectionInfo = conn;
                        myTable.ApplyLogOnInfo(myLog);
                        myTable.Location = myLog.TableName;

                    }
                    int reportobjectsCount = theReport.ReportDefinition.ReportObjects.Count - 2;
                    for (int i = 0; i <= reportobjectsCount; i++)
                    {
                        CrystalDecisions.CrystalReports.Engine.ReportObject rpt = theReport.ReportDefinition.ReportObjects[i];
                        if (rpt.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subrpt = (SubreportObject)rpt;
                            ReportDocument r = theReport.OpenSubreport(subrpt.SubreportName);
                            int subreportTableCount = r.Database.Tables.Count - 2;
                            for (int j = 0; j <= subreportTableCount; j++)
                            {
                                myTable = r.Database.Tables[j];
                                myLog = myTable.LogOnInfo;
                                myTable.ApplyLogOnInfo(myLog);
                                myTable.Location = myLog.TableName;
                            }
                        }
                    }
                    ParameterValues pList = new ParameterValues();
                    ParameterDiscreteValue pV = new ParameterDiscreteValue();
                    int parameterFieldsCount = theReport.DataDefinition.ParameterFields.Count - 1;
                    for (int i = 0; i <= parameterFieldsCount; i++)
                    {
                        paramName = theReport.DataDefinition.ParameterFields[i].Name;
                        //Report Parameters Fields 1
                        if (paramName == "@BRNO")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRNO_ID);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 2
                        else if (paramName == "@CONO")
                        {
                            pV.Value = SessionStore.GetValue(Params.CONO_ID);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 3
                        else if (paramName == "@CompanyName")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMPANY_NAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CompanyNameB")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMPANY_NAME_B);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 4
                        
                        else if (paramName == "@BranchName")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRANCH_NAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 5
                        else if (paramName == "@BranchAddress")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRANCH_ADDRESS);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@BranchAddressB")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRANCH_ADDRESS_B);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 6
                        else if (paramName == "@CommonName")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 6
                        else if (paramName == "@CommonName1")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME1);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 7
                        else if (paramName == "@CommonName2")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME2);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                          //Report Parameters Fields 8
                        else if (paramName == "@CommonName3")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME3);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 9
                        else if (paramName == "@CommonName4")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME4);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 10
                        else if (paramName == "@CommonName5")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME5);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                         //Report Parameters Fields 11
                        else if (paramName == "@CommonName6")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME6);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 12
                        else if (paramName == "@CommonName7")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME7);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 13
                        else if (paramName == "@CommonName8")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME8);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 14
                        else if (paramName == "@CommonName9")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME9);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 15
                        else if (paramName == "@CommonName10")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME10);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 15
                        else if (paramName == "@CommonName11")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME11);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 15
                        else if (paramName == "@CommonName12")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME12);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 15
                        else if (paramName == "@CommonName13")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME13);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 15
                        else if (paramName == "@CommonName14")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME14);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonName15")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME15);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 16
                        else if (paramName == "@CommonNo1")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO1);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 17
                        else if (paramName == "@CommonNo2")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO2);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 18
                        else if (paramName == "@CommonNo3")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO3);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 19
                        else if (paramName == "@CommonNo4")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO4);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 20
                        else if (paramName == "@CommonNo5")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO5);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 21
                        else if (paramName == "@CommonNo6")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO6);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 22
                        else if (paramName == "@CommonNo7")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO7);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 23
                        else if (paramName == "@CommonNo8")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO8);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 24
                        else if (paramName == "@CommonNo9")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO9);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 25
                        else if (paramName == "@CommonNo10")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO10);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }


                        else if (paramName == "@CommonNo11")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO11);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }


                       //Report Parameters Fields 32
                        else if (paramName == "@CommonNo12")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO12);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                          //Report Parameters Fields 33
                        else if (paramName == "@CommonNo13")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO13);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }


                       //Report Parameters Fields 34
                        else if (paramName == "@CommonNo14")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO14);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 35
                        else if (paramName == "@CommonNo15")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO15);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonNo16")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO16);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonNo17")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO17);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonNo18")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO18);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonNo19")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO19);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CommonNo20")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO20);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 26
                        else if (paramName == "@UserID")
                        {
                            pV.Value = SessionStore.GetValue(Params.USERNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@UserName")
                        {
                            pV.Value = SessionStore.GetValue(Params.USERNAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@BranchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CuNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.CUNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 27
                        else if (paramName == "@CuType")
                        {
                            pV.Value = SessionStore.GetValue(Params.CUPTYE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 28
                        else if (paramName == "@AccType")
                        {
                            pV.Value = SessionStore.GetValue(Params.ACCTYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 29
                        else if (paramName == "@AccNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.ACCNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }


                        //Report Parameters Fields 30
                        else if (paramName == "@MemType")
                        {
                            pV.Value = SessionStore.GetValue(Params.MEMTYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@MemNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.MEMNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@ColCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.COLCODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@RegNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.REGNO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 31
                        //Report Parameters Fields 36
                        else if (paramName == "@fDate")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_FDATE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 37
                        else if (paramName == "@tDate")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_TDATE);                            
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                         // Report Parameters Fields 38

                        else if (paramName == "@ProdCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.PROD_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 39
                        else if (paramName == "@BatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 40
                        else if (paramName == "@VchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.VOUCHER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 41
                        else if (paramName == "@fBatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.FIRST_BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 42
                        else if (paramName == "@tBatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.TO_BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 43
                        else if (paramName == "@TrnasferType")
                        {
                            pV.Value = SessionStore.GetValue(Params.TRANSFER_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                         // Report Parameters Fields 45
                        else if (paramName == "@TrnSlNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.TRNSL_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 46
                        else if (paramName == "@LedgerType")
                        {
                            pV.Value = SessionStore.GetValue(Params.LEDGER_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 47
                        else if (paramName == "@LedgerNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.LEDGER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                          //Report Parameters Fields 48
                        else if (paramName == "@ReqNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.REQ_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 49
                        else if (paramName == "@ProdCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.PROD_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                         //Report Parameters Fields 50
                        else if (paramName == "@BatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 51
                        else if (paramName == "@GRNNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.GRN_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 52
                        else if (paramName == "@nItemType")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 53
                        else if (paramName == "@nItemSource")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_SOURCE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //......................................nimara.................................................
                        //Report Parameters Fields 54
                        else if (paramName == "@SuppCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.SUPP_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //.......................................................................................
                        //Report Parameters Fields 55
                        else if (paramName == "@ManuCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MANU_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //............................................................................................
                        //Report Parameters Fields 56
                        else if (paramName == "@OrdNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.ORDER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 57
                        else if (paramName == "@ItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 58
                        else if (paramName == "@fItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.F_ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 59
                        else if (paramName == "@tItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.T_ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 60
                        else if (paramName == "@ChalanNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.CHALLAN_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 61
                        else if (paramName == "@EmpNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.EMP_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 62
                        else if (paramName == "@PPICNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.PPIC_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@AccountCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.ACCOUNTCODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@nFlag")
                        {
                            pV.Value = SessionStore.GetValue(Params.NFLAG);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@LPurpose")
                        {
                            pV.Value = SessionStore.GetValue(Params.LPURPOSE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@KeyCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.KEYCODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@Currency")
                        {
                            pV.Value = SessionStore.GetValue(Params.CURRENCY);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
        //......................................Multiuser Store Procedure...........................
                        else if (paramName == "@AccClass")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_ACC_CLASS);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@Func99")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_FUNC_99);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@FcashCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_F_CASH_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnType")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRN_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@teller")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TELLER);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnMode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRN_MODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnNature")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRN_NATURE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnModule")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRN_MODULE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TranSw")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRAN_SW);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@UserId")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_USER_ID);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CashCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_CASH_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CSGLTransaction")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_CS_GL_TRN);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@AutoTransaction")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_AUTO_TRN);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TransactionAmount")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRAN_AMT);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@AccStatFlag")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_ACC_STAT_FLAG);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@BalanceFlag")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_BALANCE_FLAG);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@fBalance")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_F_BALANCE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@tBalance")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_T_BALANCE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@CodeType")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_CODE_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_TRN_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@ContractInt")
                        {
                            pV.Value = SessionStore.GetValue(Params.MU_CONTRACT_INT);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //......................................End of Multiuser Store Procedure....................
                        //............................................................................................
                        else
                        {
                            //This else for SP parameters 63
                            paramName = theReport.DataDefinition.ParameterFields[i].Name;
                            string paramValue = Converter.GetString(SessionStore.GetValue(Params.WHERE_CLAUSE));
                            pV.Value = paramValue;
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                    }

                    this.CrystalReportViewer1.ReportSource = theReport;
                    this.CrystalReportViewer1.RefreshReport();

                    Session["ReportDocument"] = theReport;

                }
                else
                {
                    //TODO
                }
            }
            catch (Exception ex)
            {
                //string notifyMsg = "?msg=" + ex.ToString() + System.Environment.NewLine + "Please Check Value = " + paramName + System.Environment.NewLine + Request.UrlReferrer + "&PreviousMenu=A2ZERPModule.aspx";
                //Server.Transfer("MessageDisplay.aspx" + notifyMsg);

                throw ex;

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GenerateReport Problem');</script>");
            }
        }


    }
}
