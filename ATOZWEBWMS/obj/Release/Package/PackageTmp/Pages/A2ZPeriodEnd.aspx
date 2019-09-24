<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZPeriodEnd.aspx.cs" Inherits="ATOZWEBWMS.Pages.A2ZPeriodEnd" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Open New Party</title>
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
</head>
<body>


    <div class="container-contact100">
        <div class="wrap-contact100">
            <form class="contact100-form validate-form" runat="server">
                <asp:Label ID="lblIDName" class="label-input100" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblProcessDate" class="label-input100" runat="server" Text=""></asp:Label>
                <span class="contact100-form-title">Day End Process
                </span>

                
                
                <div class="wrap-input100 validate-input">
                    <label class="label-input100" for="first-name">Today's Date</label>
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="false" class="input100" placeholder="User Name"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>

                <label class="label-input100" for="first-name">Please Input 'END OF DAY'</label>
                <div class="wrap-input100 validate-input">
                   
                    <asp:TextBox ID="txtDayEnd" runat="server" class="input100" MaxLength="10" placeholder="Enf of Day"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>
                


               
                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnProcess" runat="server" Text="End of Day" class="contact100-form-btn green" OnClick="btnProcess_Click"></asp:Button>
                    </div>
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnExit" runat="server" Text="Exit" class="contact100-form-btn red" OnClick="btnExit_Click"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
    </div>


    <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblNewLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnNewAccNo" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="CtrlBackUpStat" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblNewYear" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblEndOfMonth" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblYearEnd" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblUserID" runat="server" Visible="False"></asp:Label>


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
