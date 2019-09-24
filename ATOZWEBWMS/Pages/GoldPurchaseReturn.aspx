<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoldPurchaseReturn.aspx.cs" Inherits="ATOZWEBWMS.Pages.GoldPurchaseReturn" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Gold Purchase</title>
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

        .grid_scroll {
            overflow: auto;
            height: 230px;
            width: 500px;
            
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are You Sure You Want to Update Information?');
        }


    </script>

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            <%--$("#<%= ddlLocation.ClientID %>").chosen();--%>
            $("#<%= ddlPurchasePartyName.ClientID %>").chosen();
           <%-- $("#<%= ddlTrackingPartyName.ClientID %>").chosen();
            $("#<%= ddlKarat.ClientID %>").chosen();--%>

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                <%--$("#<%= ddlLocation.ClientID %>").chosen();--%>
                $("#<%= ddlPurchasePartyName.ClientID %>").chosen();
                <%-- $("#<%= ddlTrackingPartyName.ClientID %>").chosen();
                $("#<%= ddlKarat.ClientID %>").chosen();--%>

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
                <span class="contact100-form-title">Gold Purchase Return
                </span>


                <label class="label-input100" for="first-name">Purchase Party Name</label>

                <div class="wrap-input100 validate-input">
                    <asp:DropDownList ID="ddlPurchasePartyName" runat="server" class="input100" Height="50px">

                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                    <script type="text/javascript" src="../scripts/chosen.jquery.js"></script>
                    <span class="focus-input100"></span>
                </div>


                <div align="center">
                    <asp:Panel ID="pnlProperty" runat="server" Height="320px">
                        <table class="style1">
                            <tr>

                                <td>
                                    <h6>
                                        <asp:Label ID="Label14" runat="server" Text="Code" ForeColor="Red"></asp:Label></h6>
                                    <asp:DropDownList ID="ddlCode" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Width="150px" Font-Size="Small">
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <h6>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label10" runat="server" Text="karat" ForeColor="Red"></asp:Label></h6>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlKarat" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Width="100px" Font-Size="Small">
                                        <asp:ListItem Value="0">-Sel-</asp:ListItem>
                                        <asp:ListItem Value="22">22</asp:ListItem>
                                        <asp:ListItem Value="21">21</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <h6>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label6" runat="server" Text="Gross Wt" ForeColor="Red"></asp:Label></h6>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtGrossWt" runat="server" TabIndex="2" Width="150px" Height="30px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Small" onkeypress="return IsDecimalKey(event)"
                                        onFocus="javascript:this.select()"></asp:TextBox>
                                </td>

                                <td>

                                    <br />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="BtnAddItem" runat="server" TabIndex="6" Text="Add" Width="80px" Font-Size="Small"
                                        ForeColor="White" Height="30px" Font-Bold="True"
                                        CssClass="button green" OnClick="BtnAddItem_Click" />
                                </td>
                            </tr>
                        </table>

                         <br />

                        <%-- <br />--%>
                        <div align="center" class="grid_scroll">
                            <asp:GridView ID="gvDetails" runat="server" HeaderStyle-BackColor="YellowGreen"
                                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" EnableModelValidation="True" OnRowDeleting="gvItemDetails_RowDeleting" OnRowDataBound="gvDetails_RowDataBound">
                                <HeaderStyle BackColor="YellowGreen" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <AlternatingRowStyle BackColor="WhiteSmoke" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KeyNo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrnKeyNo" runat="server" Text='<%# Eval("RefTrnKeyNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PartyName" HeaderText="Code">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AccKarat" HeaderText="Karat" >
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="TrnAmount" HeaderText="GrossWt">
                                        <HeaderStyle Width="150px" HorizontalAlign="Right"/>
                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    </asp:BoundField>

                                    <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                        <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                                    </asp:CommandField>
                                </Columns>


                            </asp:GridView>

                             Total:
                    <asp:Label ID="lblTotalValue" runat="server" Text="" Width="107px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </div>


                    </asp:Panel>
                </div>


                 <br />
                 <br />

                <div class="container-contact100-form-btn ">

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

        <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label>

    </div>






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
