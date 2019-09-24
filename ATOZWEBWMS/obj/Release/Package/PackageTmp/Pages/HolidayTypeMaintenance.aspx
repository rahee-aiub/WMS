<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayTypeMaintenance.aspx.cs" Inherits="ATOZWEBWMS.Pages.HolidayTypeMaintenance" %>

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

    
       <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet"  type="text/css" />
    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlHolType.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlHolType.ClientID %>").chosen();

            });

        });

    </script>

    <script lang="javascript" type="text/javascript">
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
                <span class="contact100-form-title">Holiday Type Maintenance
                </span>
                <label class="label-input100" for="email">Holiday Type</label>
                <div class="wrap-input100 validate-input">
                    <asp:TextBox ID="txtHolType" runat="server" class="input100" placeholder="Party name" OnTextChanged="txtHolType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlHolType" runat="server" class="input100" Height="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlHolType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                    <script type="text/javascript" src="../scripts/chosen.jquery.js"></script>
                    <span class="focus-input100"></span>
                </div>
                <label class="label-input100" for="first-name">Description</label>

                <div class="wrap-input100 validate-input">
                    <asp:TextBox ID="txtDescription" runat="server" class="input100" placeholder="Party name"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>

                

                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="contact100-form-btn green" OnClick="btnSubmit_Click"></asp:Button>
                    </div>
                    <div class="rs1">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="contact100-form-btn green" OnClick="btnUpdate_Click"></asp:Button>
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
