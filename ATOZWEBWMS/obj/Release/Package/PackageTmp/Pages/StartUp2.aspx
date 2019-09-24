<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartUp2.aspx.cs" Inherits="A2ZWEBWMS.Pages.StartUp2" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Weight Management System</title>

    <!-- Our CSS stylesheet file -->
    <link href="../StartUpDesign/styles.css" rel="stylesheet" />
    <!-- Font Awesome Stylesheet -->
    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <link href="../StartUpDesign/font-awesome.css" rel="stylesheet" />
    <!-- Including Open Sans Condensed from Google Fonts -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans+Condensed:300,700,300italic" />
    <!--[if lt IE 9]>
          <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->

   
</head>

<body>

    <nav id="colorNav">
        <ul>
            <li class="green">
                <a href="#" class="icon-home"></a>

                <ul>
                    <li><a href="OpenNewParty.aspx">Open New Party</a></li>
                    <li><a href="EditParty.aspx">Edit Old Party</a></li>
                    <li><a href="#">Party List Report</a></li>
                </ul>
            </li>
            <li class="red">

                <a href="#" class="icon-facebook"></a>
                <ul>
                    <li><a href="#">Gold Purchase</a></li>
                    <li><a href="#">Gold Purchase Return</a></li>
                    <li><a href="#">Purchase Report</a></li>
                    <li><a href="#">Purchase Return Report</a></li>
                    <li><a href="#">Purchase & Return Report</a></li>
                </ul>
            </li>
            <li class="blue">
                <a href="#" class="icon-twitter"></a>
                <ul>
                    <li><a href="#">Send To Transit</a></li>
                    <li><a href="#">Transit Return</a></li>
                    <li><a href="#">Transit Report</a></li>
                    <li><a href="#">Transit Return Report</a></li>
                </ul>
            </li>
            <li class="yellow">

                <a href="#" class="icon-beaker"></a>
                <br />

                <ul>
                    <li><a href="#">Receive From Carrier</a></li>
                    <li><a href="#">Carrier Receive Report</a></li>
                </ul>
            </li>
            <li class="purple">
                <a href="#" class="icon-envelope"></a>
                <ul>
                    <li><a href="#">Sale Order</a></li>
                    <li><a href="#">Sale Return Order</a></li>
                </ul>
            </li>
        </ul>
    </nav>


    <!-- BSA AdPacks code. Please ignore and remove.-->
    <script src="http://code.jquery.com/jquery-1.8.2.min.js"></script>
    <script src="http://cdn.tutorialzine.com/misc/adPacks/v1.js" async></script>


    <div>
        <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblProcessDate" runat="server" Text="" Visible="false"></asp:Label>

    </div>


</body>
</html>




