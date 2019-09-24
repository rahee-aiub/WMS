<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZBackupProcess.aspx.cs" Inherits="ATOZWEBWMS.Pages.A2ZBackupProcess" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Backup</title>
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

    <form class="contact100-form validate-form" runat="server">
    <div class="container-contact100">
        <div class="wrap-contact100">
            
                <asp:Label ID="lblIDName" class="label-input100" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblProcessDate" class="label-input100" runat="server" Text=""></asp:Label>
                <span class="contact100-form-title">Backup Process
                </span>

                <div class="wrap-input100 validate-input">
                    <table>
                        <tr>
                            <td class="auto-style1"></td>
                            <td>
                                <div style="border-style: none; border-color: inherit; border-width: 1px; background-color: #DDA0DD; width: 270px;" align="left">
                                    <asp:Label ID="lblSelectDay" runat="server" Text=" Select a Day:" Font-Size="X-Large"
                                        ForeColor="Red"></asp:Label><br />
                                    <br />
                                    <asp:RadioButton ID="rbtSunday" runat="server" AutoPostBack="True" Text="Sunday" Font-Size="X-Large" ForeColor="Black" OnCheckedChanged="rbtSunday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtMonday" runat="server" AutoPostBack="True" Text="Monday" Font-Size="X-Large" OnCheckedChanged="rbtMonday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtTuesday" runat="server" AutoPostBack="True" Text="Tuesday" Font-Size="X-Large" OnCheckedChanged="rbtTuesday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtWednesday" runat="server" AutoPostBack="True" Text="Wednesday" Font-Size="X-Large" OnCheckedChanged="rbtWednesday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtThursday" runat="server" AutoPostBack="True" Text="Thursday" Font-Size="X-Large" OnCheckedChanged="rbtThursday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtFriday" runat="server" AutoPostBack="True" Text="Friday" Font-Size="X-Large" OnCheckedChanged="rbtFriday_CheckedChanged" />&nbsp;&nbsp;<br />
                                    <asp:RadioButton ID="rbtSaturday" runat="server" AutoPostBack="True" Text="Saturday" Font-Size="X-Large" OnCheckedChanged="rbtSaturday_CheckedChanged" />&nbsp;&nbsp;<br />

                                </div>
                            </td>
                        </tr>

                    </table>
                </div>


                 <div class="wrap-input100 validate-input">
                    <asp:Label ID="lblTo" runat="server" Text=" To" Font-Size="X-Large"></asp:Label>

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                   
                          <asp:TextBox ID="txtTo" runat="server" Width="536px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                              Font-Size="Large" AutoPostBack="true"></asp:TextBox><br />


                    <asp:Label ID="lblFrom" runat="server" Text=" From" Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;
                   
                          <asp:TextBox ID="txtFrom" runat="server" Width="536px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                              Font-Size="Large" AutoPostBack="true"></asp:TextBox><br />


                    <asp:Label ID="CtrlTo" runat="server" Text="" Visible="false"></asp:Label>



                </div>


                <div class="container-contact100-form-btn ">
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnBackUp" runat="server" Text="BackUp" class="contact100-form-btn green" OnClick="btnBackUp_Click"></asp:Button>
                    </div>
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <div class="rs1">
                        <asp:Button ID="btnExit" runat="server" Text="Exit" class="contact100-form-btn red" OnClick="btnExit_Click"></asp:Button>
                    </div>
                </div>
            
        </div>
    </div>

     <div class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Database" HeaderStyle-Width="60px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDatabase" runat="server" Text='<%#Eval("DatabaseName") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>

                
            </Columns>

        </asp:GridView>
    </div>

    </form>

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
