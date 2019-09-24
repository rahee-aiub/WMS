<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartUp3.aspx.cs" Inherits="A2ZWEBWMS.Pages.StartUp3" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
    <title>WMS</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="Slide Down Box Menu with jQuery and CSS3" />
    <meta name="keywords" content="jquery, css3, sliding, box, menu, cube, navigation, portfolio, thumbnails" />
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" />
    <link href="../css/Sstyle.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background: #333 url(../images/bg.jpg) repeat top left;
            font-family: Arial;
        }

        span.reference {
            position: fixed;
            left: 10px;
            bottom: 10px;
            font-size: 12px;
        }

            span.reference a {
                color: #aaa;
                text-transform: uppercase;
                text-decoration: none;
                text-shadow: 1px 1px 1px #000;
                margin-right: 30px;
            }

                span.reference a:hover {
                    color: #ddd;
                }

        ul.sdt_menu {
            margin-top: 150px;
        }

        /*h1.title {
            text-indent: -9000px;
            background: transparent url(../images/title.png) no-repeat top left;
            width: 633px;
            height: 69px;
        }*/

        .lbls {
            font-size: 35px;
            position: absolute;
            right: 50px;
            top: 50px;
        }

        .processdate {
            font-size: 17px;
            position: absolute;
            right: 10px;
            top: 30px;
            color: white;
        }

        .idname {
            font-size: 18px;
            position: absolute;
            right: 250px;
            top: 30px;
            color: white;
            font-family: 'Bodoni MT';
        }

        .name {
            font-size: 30px;
            position: absolute;
            left: 25px;
            top: 30px;
            color: white;
        }
       
    </style>
</head>

<body>
    <form runat="server">
        
                <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblIDName" CssClass="idname" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblProcessDate" CssClass="processdate" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblName" CssClass="name" runat="server" Text="Weight Management System"></asp:Label>
          
        <div class="content">
           
            <ul id="sdt_menu" class="sdt_menu">
                <li>
                    <a href="#">
                        <img src="../images/1.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Party</span>
                            <span class="sdt_descr">Open or Edit Party</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="OpenNewParty.aspx">Open New Party</a>
                        <a href="EditParty.aspx">Edit Old party</a>
                        <a href="PartyListReport.aspx">Party Statement</a>
                    </div>
                </li>
                <li>
                    <a href="#">
                        <img src="../images/2.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Purchase</span>
                            <span class="sdt_descr">Gold purchase at DXB</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="GoldPurchase.aspx">Gold Purchase</a>
                        <a href="GoldPurchaseReturn.aspx">Purchase Return</a>
                        <a href="PurchaseReports.aspx">Reports</a>

                    </div>
                </li>

                <li>
                    <a href="#">
                        <img src="../images/3.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Transit</span>
                            <span class="sdt_descr">Gold Send to Transit</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="TransitSend.aspx">Send To Transit</a>
                        <a href="TransitSendReturn.aspx">Transit Return</a>
                        <a href="TransitReports.aspx">Reports</a>
                    </div>
                </li>
                <li>
                    <a href="#">
                        <img src="../images/4.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Receive</span>
                            <span class="sdt_descr">Gold Receive From Carrier</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="ReceiveFromCarrier.aspx">Receive from Carrier</a>
                        <a href="ReceiveDirect.aspx">Direct Receive</a>
                        <a href="ReceiveReports.aspx">Reports</a>
                    </div>
                </li>
                <li>
                    <a href="#">
                        <img src="../images/5.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Sale</span>
                            <span class="sdt_descr">Sale Gold To Party</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="SaleToParty.aspx">Sale to Party</a>
                        <a href="SaleToPartyReturn.aspx">Sale Return</a>
                        <a href="SaleReports.aspx">Reports</a>
                  
                    </div>
                </li>
                   <li>
                    <a href="#">
                        <img src="../images/7.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Reports</span>
                            <span class="sdt_descr">All business Reports</span>
                        </span>
                    </a>
                    <div class="sdt_box">
                        <a href="#">Party Statement</a>
                        <a href="#">Total Stock</a>
                       
                    </div>
                </li>
                <li>
                    <a href="LogOut.aspx">
                        <img src="../images/6.jpg" alt="" />
                        <span class="sdt_active"></span>
                        <span class="sdt_wrap">
                            <span class="sdt_link">Log Out</span>
                            <span class="sdt_descr">Log out from site</span>
                        </span>
                    </a>

                </li>
            </ul>
        </div>
        <div>
            <%--<span class="reference">
                <a href="http://tympanus.net/codrops/2010/07/16/slide-down-box-menu/">back to the Codrops Tutorial</a>
				<a href="http://www.flickr.com/photos/arcticpuppy/sets/72157622090180990/">Images by tibchris</a>
            </span>--%>
        </div>
        <div>
        </div>
    </form>
    <!-- The JavaScript -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>

    <script src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript">
        $(function () {
            /**
            * for each menu element, on mouseenter, 
            * we enlarge the image, and show both sdt_active span and 
            * sdt_wrap span. If the element has a sub menu (sdt_box),
            * then we slide it - if the element is the last one in the menu
            * we slide it to the left, otherwise to the right
            */
            $('#sdt_menu > li').bind('mouseenter', function () {
                var $elem = $(this);
                $elem.find('img')
                     .stop(true)
                     .animate({
                         'width': '170px',
                         'height': '170px',
                         'left': '0px'
                     }, 400, 'easeOutBack')
                     .andSelf()
                     .find('.sdt_wrap')
                     .stop(true)
                     .animate({ 'top': '140px' }, 500, 'easeOutBack')
                     .andSelf()
                     .find('.sdt_active')
                     .stop(true)
                     .animate({ 'height': '170px' }, 300, function () {
                         var $sub_menu = $elem.find('.sdt_box');
                         if ($sub_menu.length) {
                             var left = '170px';
                             if ($elem.parent().children().length == $elem.index() + 1)
                                 left = '-170px';
                             $sub_menu.show().animate({ 'left': left }, 200);
                         }
                     });
            }).bind('mouseleave', function () {
                var $elem = $(this);
                var $sub_menu = $elem.find('.sdt_box');
                if ($sub_menu.length)
                    $sub_menu.hide().css('left', '0px');

                $elem.find('.sdt_active')
                     .stop(true)
                     .animate({ 'height': '0px' }, 300)
                     .andSelf().find('img')
                     .stop(true)
                     .animate({
                         'width': '0px',
                         'height': '0px',
                         'left': '85px'
                     }, 400)
                     .andSelf()
                     .find('.sdt_wrap')
                     .stop(true)
                     .animate({ 'top': '25px' }, 500);
            });
        });
    </script>
</body>
</html>

