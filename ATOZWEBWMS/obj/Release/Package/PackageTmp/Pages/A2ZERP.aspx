<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZERP.aspx.cs" Inherits="A2ZWEBWMS.Pages.A2ZERP" %>



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
        <asp:Label ID="lblMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblNewProcDate" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lblNoRec" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblIDName" CssClass="idname" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblProcessDate" CssClass="processdate" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblName" CssClass="name" runat="server" Text="Transit Management System"></asp:Label>




        <asp:Button ID="btnWSmodule" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Transit Service" class="contact100-form-btn green" Style="left: 36px; top: 150px; position: fixed; width: 368px;" OnClick="btnWSmodule_Click"></asp:Button>
        <asp:Button ID="btnHKmodule" runat="server" Font-Bold="true" Font-Size="X-Large" Text="House Keeping" class="contact100-form-btn red" Style="left: 36px; top: 289px; position: fixed; width: 368px;" OnClick="btnHKmodule_Click"></asp:Button>

        <asp:Button ID="btnLogOut" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Log Out" class="contact100-form-btn green" Style="left: 136px; top: 398px; position: fixed; width: 168px;" OnClick="btnLogOut_Click"></asp:Button>


        <div id="DivDetails" runat="server">
            <table class="style1" style="width: 650px; height: 50px; background-color: #00FFFF;">
                <thead>
                    <tr>
                        <th colspan="4" style="background-color: #669999">
                            <asp:Label ID="lblAcDetails" runat="server" Text="START OF DAY" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </th>
                    </tr>

                    <tr>
                        <td style="background-color: #669999">
                            <asp:Label ID="lblLastProcDate" runat="server" Text="Last Transaction Date" Font-Size="Large" Font-Bold="true"></asp:Label>
                        </td>

                        <td class="auto-style1">
                            <asp:TextBox ID="txtLastProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                Width="375px" BorderStyle="Ridge" Font-Size="X-Large" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #669999">
                            <asp:Label ID="lblNewProcDt" runat="server" Text="Today's Transaction Date is" Font-Size="Large" Font-Bold="true"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtNewProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                Width="375px" BorderStyle="Ridge" Font-Size="X-Large" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td style="background-color: #669999">
                            <asp:Label ID="lblSOD" runat="server" Text="Please Type START OF DAY" Font-Size="Large" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStDay" runat="server" MaxLength="12" autocomplete="off" Style="font-size: Large" ForeColor="Red" Width="135px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #669999">
                            <asp:Label ID="lblProessing" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Start of Day" CssClass="button green size-100"
                                Height="27" OnClientClick="return ValidationBeforeUpdate()" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Back" CssClass="button red size-100"
                                Height="27" OnClick="Button2_Click" />
                        </td>
                    </tr>
                </thead>
            </table>

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
                        <asp:Label ID="lblMsg2" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>

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

