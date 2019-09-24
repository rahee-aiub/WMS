<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZWSERPMODULE.aspx.cs" Inherits="A2ZWEBWMS.Pages.A2ZWSERPMODULE" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
    <title>WMS</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="Slide Down Box Menu with jQuery and CSS3" />
    <meta name="keywords" content="jquery, css3, sliding, box, menu, cube, navigation, portfolio, thumbnails" />
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" />
    <link href="../css/Sstyle.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/css/main.css" rel="stylesheet" />
    <link href="../PartyOpenPageDesign/css/util.css" rel="stylesheet" />
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

        

        .idname {
            font-size: 18px;
            position: absolute;
            left: 5px;
            top: 30px;
            color: white;
            font-family: 'Bodoni MT';
        }

        .processdate {
            font-size: 17px;
            position: absolute;
            left: 5px;
            top: 55px;
            color: white;
        }
        .name {
            font-size: 30px;
            position: absolute;
            left: 5px;
            top: 70px;
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
        <asp:Label ID="lblName" CssClass="name" runat="server" Text="Transit Service"></asp:Label>


        <asp:Button ID="btnParty" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Party" class="contact100-form-btn green" Style="left: 36px; top: 139px; position: fixed; width: 311px;" OnClick="btnParty_Click"></asp:Button>

        <asp:Button ID="btnPurchase" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Purchase" class="contact100-form-btn red" Style="left: 36px; top: 205px; position: fixed; width: 311px;" OnClick="btnPurchase_Click"></asp:Button>

        <asp:Button ID="btnTransit" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Transit" class="contact100-form-btn green" Style="left: 36px; top: 270px; position: fixed; width: 311px;" OnClick="btnTransit_Click"></asp:Button>

        <asp:Button ID="btnReceive" runat="server" Text="Receive" Font-Bold="true" Font-Size="X-Large" class="contact100-form-btn red" Style="left: 36px; top: 340px; position: fixed; width: 311px;" OnClick="btnReceive_Click"></asp:Button>

        <asp:Button ID="btnSale" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sale" class="contact100-form-btn green" Style="left: 36px; top: 410px; position: fixed; width: 311px;" OnClick="btnSale_Click"></asp:Button>

        <asp:Button ID="btnReports" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Reports" class="contact100-form-btn red" Style="left: 36px; top: 480px; position: fixed; width: 311px; bottom: 256px;" OnClick="btnReports_Click"></asp:Button>

        <asp:Button ID="btnSpecial" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Special" class="contact100-form-btn green" Style="left: 36px; top: 550px; position: fixed; width: 311px;" OnClick="btnSpecial_Click"></asp:Button>

        <asp:Button ID="btnBack" runat="server" Font-Bold="true" Text="Back" class="contact100-form-btn red" Style="left: 100px; top: 620px; position: fixed" OnClick="btnBack_Click"></asp:Button>

        <asp:Button ID="btnLogOut" runat="server" Font-Bold="true" Text="Log Out" class="contact100-form-btn green" Style="left: 100px; top: 685px; position: fixed" OnClick="btnLogOut_Click"></asp:Button>




        <div id="DivParty" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnNewParty" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Open New Party" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnNewParty_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnEditParty" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Edit Party" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnEditParty_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnPartBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Style="width: 96px; Height:35px" OnClick="btnPartyBack_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div id="DivPurchase" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnGoldPurchase" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Gold Purchase" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnGoldPurchase_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnGPurchaseReturn" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Gold Purchase Return" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnGPurchaseReturn_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnPurchaseBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnPurchaseBack_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div id="DivTransit" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSendTransit" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Send to Transit" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnSendTransit_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnTransitReturn" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Transit Return" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnTransitReturn_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnTransitBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnTransitBack_Click" />

                    </td>
                </tr>
            </table>
        </div>


        <div id="DivReceive" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnReceiveCarrier" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Receive From Carrier" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnReceiveCarrier_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnReceiveDirect" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Receive Direct" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnReceiveDirect_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnReceiveBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnReceiveBack_Click" />

                    </td>
                </tr>
            </table>
        </div>


        <div id="DivSale" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSaleParty" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sale to Party" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnSaleParty_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSaleReturn" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sale Return" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnSaleReturn_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSaleBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnSaleBack_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div id="DivReports" runat="server">
            <table style="width: 340px; height: 550px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnPurchaseReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Gold Purchase Reports" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnPurchaseReport_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnPurchaseReturnReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Purchase Return Reports" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnPurchaseReturnReport_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnTransitReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Transit Report" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnTransitReport_Click"></asp:Button>

                    </td>
                </tr>
                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnTransitReceiveReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Transit Receive Report" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnTransitReceiveReport_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSalesReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sales Report" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnSalesReport_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnSalesReturnReport" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sales Return Report" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnSalesReturnReport_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnReportsBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnReportsBack_Click" />

                    </td>
                </tr>
            </table>
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

