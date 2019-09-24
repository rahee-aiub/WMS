<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="ATOZWEBWMS.Pages.Reports" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>WMS Reports</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../PartyOpenPageDesign/css/main.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/css/util.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/fonts/iconic/css/material-design-iconic-font.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/fonts/iconic/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/fonts/Linearicons-Free-v1.0.0/icon-font.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/animate/animate.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/animsition/css/animsition.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/animsition/css/animsition.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap-grid.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap-reboot.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap-reboot.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/css-hamburgers/hamburgers.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/perfect-scrollbar/perfect-scrollbar.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/select2/select2.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/vendor/select2/select2.min.css" rel="stylesheet" />
    <script src="../PartyOpenPageDesign/vendor/daterangepicker/daterangepicker.js"></script>
    <script src="../PartyOpenPageDesign/js/main.js"></script>
    <script src="../PartyOpenPageDesign/vendor/animsition/js/animsition.js"></script>
    <script src="../PartyOpenPageDesign/vendor/animsition/js/animsition.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/bootstrap/js/bootstrap.js"></script>
    <script src="../PartyOpenPageDesign/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/bootstrap/js/popper.js"></script>
    <script src="../PartyOpenPageDesign/vendor/bootstrap/js/popper.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/bootstrap/js/tooltip.js"></script>
    <script src="../PartyOpenPageDesign/vendor/countdowntime/countdowntime.js"></script>
    <script src="../PartyOpenPageDesign/vendor/daterangepicker/moment.js"></script>
    <script src="../PartyOpenPageDesign/vendor/daterangepicker/moment.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/jquery/jquery-3.2.1.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="../PartyOpenPageDesign/vendor/select2/select2.js"></script>
    <script src="../PartyOpenPageDesign/vendor/select2/select2.min.js"></script>
    <style type="text/css">
        body {
            background: #333 url(../images/bg.jpg) repeat top left;
            font-family: Arial;
        }
    </style>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" type="text/css" media="all" />
    <link rel="stylesheet" href="http://static.jquery.com/ui/css/demo-docs-theme/ui.theme.css" type="text/css" media="all" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js" type="text/javascript"></script>


    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtFromDate.ClientID %>").datepicker();
            $("#<%= txtToDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtFromDate.ClientID %>").datepicker();
                $("#<%= txtToDate.ClientID %>").datepicker();

            });

        });

    </script>


    <script language="javascript" type="text/javascript">
        $(function () {
           
            $("#<%= ddlReportsDetails1.ClientID %>").chosen();
            $("#<%= ddlReportsDetails2.ClientID %>").chosen();
            $("#<%= ddlReportsDetails3.ClientID %>").chosen();


            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
               
                $("#<%= ddlReportsDetails1.ClientID %>").chosen();
                $("#<%= ddlReportsDetails2.ClientID %>").chosen();
                $("#<%= ddlReportsDetails3.ClientID %>").chosen();
                


            });

        });


        $(function () {
            $("#<%= ddlParty.ClientID %>").chosen();


            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlParty.ClientID %>").chosen();


            });

        });

    </script>

    <script language="javascript" type="text/javascript">
        $(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $(".youpii").chosen();
            });
        });


    </script>


</head>
<body>


    <div class="container-contact100">
        <div class="wrap-contact100">
            <form class="contact100-form validate-form" runat="server">
                <asp:Label ID="lblIDName" class="label-input100" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblProcessDate" class="label-input100" runat="server" Text=""></asp:Label>
                <span class="contact100-form-title">Reports
                </span>

                <label class="label-input100" for="email">Select Reports</label>


                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:RadioButton ID="rbtReports01" runat="server" GroupName="GLRpt30OptPrm" Text="Transit Reports" AutoPostBack="True" style="font-weight: 200" Font-Italic="True" Checked="True" OnCheckedChanged="rbtReports01_CheckedChanged"  />
                    </div>
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:RadioButton ID="rbtReports02" runat="server" GroupName="GLRpt30OptPrm" Text="Stock Reports" AutoPostBack="True" style="font-weight: 200" Font-Italic="True" OnCheckedChanged="rbtReports02_CheckedChanged"  />
                    </div>
                     &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:RadioButton ID="rbtReports03" runat="server" GroupName="GLRpt30OptPrm" Text="Transaction Reports" AutoPostBack="True" style="font-weight: 200" Font-Italic="True" OnCheckedChanged="rbtReports03_CheckedChanged"  />
                    </div>
                </div>





                <div class="wrap-input100 validate-input">
                   

                    <asp:DropDownList ID="ddlReportsDetails1" runat="server" class="input100" Width="350px" Height="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportsDetails1_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>

                        <asp:ListItem Value="3">Transit Party Statement - Code</asp:ListItem>
                        <asp:ListItem Value="4">Transit Party Statement - Name</asp:ListItem>
                        <asp:ListItem Value="5">Transit Party Statement - Details</asp:ListItem> 
                        <asp:ListItem Value="7">Transit Stock Details Report</asp:ListItem> 
                        <asp:ListItem Value="9">Transit Issue Details</asp:ListItem>
                        <asp:ListItem Value="10">Transit Received Details</asp:ListItem>
                        <asp:ListItem Value="16">Transit Details Sales Party Wise</asp:ListItem>
                        <asp:ListItem Value="17">Transit Summary Sales Party Wise</asp:ListItem>
                                              
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlReportsDetails2" runat="server" class="input100" Width="350px" Height="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportsDetails2_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="6">DXB Stock Details Report</asp:ListItem>
                        <asp:ListItem Value="8">Dhaka Stock Details Report</asp:ListItem>
                        <asp:ListItem Value="12">Stock Balance Report</asp:ListItem>
                        <asp:ListItem Value="13">Stock Balance Detail</asp:ListItem>
                        <asp:ListItem Value="14">Stock Balance Detail Party Wise</asp:ListItem>
                        <asp:ListItem Value="15">Total Stock Summary</asp:ListItem>
  
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlReportsDetails3" runat="server" class="input100" Width="350px" Height="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportsDetails3_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>

                        <asp:ListItem Value="1">Purchase & Issue Report</asp:ListItem> 
                        <asp:ListItem Value="2">Party Purchase Details</asp:ListItem> 
                        <asp:ListItem Value="11">Party Sales Details Report</asp:ListItem>
                        <asp:ListItem Value="99">Daily Transaction List</asp:ListItem>
                    </asp:DropDownList>

                    <script type="text/javascript" src="../scripts/chosen.jquery.js"></script>
                    <span class="focus-input100"></span>

                    </div>
                

                <label id="lblParty" runat="server" class="label-input100" for="email">Party Name</label>
                <div class="wrap-input100 validate-input">
                    <asp:CheckBox ID="chkAllParty" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chkAllParty_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    <asp:DropDownList ID="ddlParty" runat="server" class="input100" Height="50px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>



                    </asp:DropDownList>
                    <script type="text/javascript" src="../scripts/chosen.jquery.js"></script>
                    <span class="focus-input100"></span>
                </div>


                <label id="lblfromDate" runat="server" class="label-input100" for="Date">From Date</label>
                <div class="wrap-input100 rs1 validate-input">
                    <asp:TextBox ID="txtFromDate" runat="server" class="input100" placeholder="From Date"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>
                <label id="lbltoDate" runat="server" class="label-input100" for="Date">To Date</label>
                <div class="wrap-input100 rs1 validate-input">
                    <asp:TextBox ID="txtToDate" runat="server" class="input100" placeholder="To Date"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>

                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" class="contact100-form-btn green" OnClick="btnSubmit_Click"></asp:Button>
                    </div>
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnExit" runat="server" Text="Exit" class="contact100-form-btn red" OnClick="btnExit_Click"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
    </div>



    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlProgFlag" runat="server" Visible="False"></asp:Label>

     <asp:Label ID="CtrlReportSelect" runat="server" Visible="False"></asp:Label>





    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-23581568-13"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-23581568-13');
    </script>

</body>
</html>
