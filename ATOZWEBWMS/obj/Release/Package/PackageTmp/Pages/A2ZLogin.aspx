<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZLogin.aspx.cs" Inherits="A2ZWEBWMS.Pages.A2ZLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1 user-scalable=no">
    <meta name="description" content="Coming soon, Bootstrap, Bootstrap 3.0, Free Coming Soon, free coming soon, free template, coming soon template, Html template, html template, html5, Code lab, codelab, codelab coming soon template, bootstrap coming soon template">

    <title>Login </title>
    <!-- ============ Google fonts ============ -->

    <link href="../css/csss.css" rel="stylesheet" />
    <link href="../css/csss2.css" rel="stylesheet" />

    <!-- ============ Add custom CSS here ============ -->

    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/font-awesome.css" rel="stylesheet" />
      <style type="text/css">
			body{
				background:#333 url(../images/bg.jpg) repeat top left;
				font-family:Arial;
			}
            .lbls{
                font-size:35px;
              
            }
          </style>
    
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="OrgPass" runat="server" />

        <div id="custom-bootstrap-menu" class="navbar navbar-default " role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#"></a>
                    <br />
                   <asp:Label ID="Label7" CssClass="lbls" runat="server" Width="600px" Text="Transit Management System"></asp:Label>

                </div>
                <asp:Label ID="lblAuthentic" runat="server" Width="350"  Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>

                <div class="collapse navbar-collapse navbar-menubuilder">
                </div>
            </div>
        </div>
        <div id="DivLogin" runat="server">
            <div class="container" runat="server">

               

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" id="divMain" runat="server">
                    <div class="registrationform">
                        <div class="form-horizontal">
                            <fieldset>
                                <legend>Login<i class="fa fa-pencil pull-right"></i></legend>
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="ID: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtUserId" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Password: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="8" CssClass="form-control"
                                            TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-lg-10 col-lg-offset-2">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
                                        &nbsp;&nbsp;
                                    <asp:Button ID="btnChangePassOpenPanel" runat="server" CssClass="btn btn-primary" Text="Change" OnClick="btnChangePassOpenPanel_Click" />
                                        <input type="hidden" runat="server" id="hidTripDetails" value="" />

                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>


                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" id="divChangePass" runat="server">
                    <div class="registrationform">
                        <div class="form-horizontal">
                            <fieldset>
                                <legend>Change Password<i class="fa fa-pencil pull-right"></i></legend>
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="ID: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtBoothUserId" runat="server" AutoPostBack="True" OnTextChanged="txtBoothUserId_TextChanged" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Old Password: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="8" CssClass="form-control"
                                            TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="New Password: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="8" CssClass="form-control"
                                            TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Confirm Password: " CssClass="col-lg-2 control-label"></asp:Label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="8" CssClass="form-control"
                                            TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-10 col-lg-offset-2">
                                        <asp:Button ID="btnPasswordChange" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnPasswordChange_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />
                                        <input type="hidden" runat="server" id="Hidden1" value="" />

                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>


            </div>
        </div>


        <div id="DivUpdateMSG" runat="server">
        <table style="width: 340px; height: 130px; background-color: #e9e9e9;">
            
            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblMsg1" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblMsg2" runat="server" ForeColor="Black" Font-Bold="true" ></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:Button ID="btnUpdMsg" runat="server" Text="OK"
                        Height="27" Width="96px" OnClick="btnUpdMsg_Click" />
                    
                </td>
            </tr>
        </table>
    </div>



        <script src="../js/jquery.js"></script>
        <script src="../js/bootstrap.min.js"></script>
        <script src="../js/jquery.backstretch.js"></script>

      
        <asp:Label ID="lblOrgPassH" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblhdnfldVariable" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUserBranchNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblUserLabel" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="UnitAddress1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="UnitNameB" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="UnitAddress1B" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="hdnIDFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblProcessDate" runat="server" Text="" Visible="false"></asp:Label>

         <asp:Label ID="DisplayMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblMobileFlag" runat="server" Text="" Visible="false"></asp:Label>

    </form>
</body>
</html>
