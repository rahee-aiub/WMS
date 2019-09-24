<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveFromCarrier.aspx.cs" Inherits="ATOZWEBWMS.Pages.ReceiveFromCarrier" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Gold Receive</title>
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
    <script src="../scripts/validation.js"></script>


    <style type="text/css">
        body {
            background: #333 url(../images/bg.jpg) repeat top left;
            font-family: Arial;
        }
    </style>

     <script language="javascript" type="text/javascript">
         function ValidationBeforeSave() {
             return confirm('Are You Sure You Want to Update Information?');
         }


    </script>

     <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet"  type="text/css" />
    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlLocation.ClientID %>").chosen();
            $("#<%= ddlPartyName.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlLocation.ClientID %>").chosen();
                $("#<%= ddlPartyName.ClientID %>").chosen();

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

                <span class="contact100-form-title">Gold Receive From Carrier
                </span>
                <label class="label-input100" for="first-name">Location</label>
                <div class="wrap-input100 validate-input">
                    <asp:DropDownList ID="ddlLocation" runat="server" Enabled="false" class="input100" Height="50px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        
                    </asp:DropDownList>

                    <span class="focus-input100"></span>
                </div>
                <label class="label-input100" for="first-name">Carrier Name</label>

                <div class="wrap-input100 validate-input">
                    <asp:DropDownList ID="ddlPartyName" runat="server" class="input100" Height="50px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                         <script type="text/javascript" src="../scripts/chosen.jquery.js"></script>

                    <span class="focus-input100"></span>
                </div>

                <label class="label-input100" for="email">Gold Weight</label>
                <div class="wrap-input100 validate-input">
                    <asp:TextBox ID="txtGoldWeight" runat="server" class="input100" placeholder="Gold Weight in GRM" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>

                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="contact100-form-btn green" OnClientClick="return ValidationBeforeSave()" OnClick="btnSubmit_Click"></asp:Button>
                    </div>
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnExit" runat="server" Text="Exit" class="contact100-form-btn red" OnClick="btnExit_Click"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
    </div>

       <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label>




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
