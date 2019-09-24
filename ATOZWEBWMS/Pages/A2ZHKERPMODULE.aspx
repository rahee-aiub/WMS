<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZHKERPMODULE.aspx.cs" Inherits="A2ZWEBWMS.Pages.A2ZHKERPMODULE" %>



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
        <asp:Label ID="lblName" CssClass="name" runat="server" Text="House Keeping"></asp:Label>


        <asp:Button ID="btnParameter" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Parameter Maintenance" class="contact100-form-btn green" Style="left: 36px; top: 139px; position: fixed; width: 311px;" OnClick="btnParameter_Click"></asp:Button>

        <asp:Button ID="btnUserMaint" runat="server" Font-Bold="true" Font-Size="X-Large" Text="User's Maintenance" class="contact100-form-btn red" Style="left: 36px; top: 210px; position: fixed; width: 311px;" OnClick="btnUserMaint_Click"></asp:Button>

        <asp:Button ID="btnHolidayMaint" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Holiday Maintenance" class="contact100-form-btn green" Style="left: 36px; top: 280px; position: fixed; width: 311px;" OnClick="btnHolidayMaint_Click"></asp:Button>

        <asp:Button ID="btnDatabase" runat="server" Text="Database" Font-Bold="true" Font-Size="X-Large" class="contact100-form-btn red" Style="left: 36px; top: 350px; position: fixed; width: 311px;" OnClick="btnDatabase_Click"></asp:Button>

        <asp:Button ID="btnProcessEnd" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Process End" class="contact100-form-btn green" Style="left: 36px; top: 420px; position: fixed; width: 311px;" OnClick="btnProcessEnd_Click"></asp:Button>

        <asp:Button ID="btnBack" runat="server" Font-Bold="true" Text="Back" class="contact100-form-btn red" Style="left: 95px; top: 500px; position: fixed" OnClick="btnBack_Click"></asp:Button>

        <asp:Button ID="btnLogOut" runat="server" Font-Bold="true" Text="Log Out" class="contact100-form-btn green" Style="left: 95px; top: 570px; position: fixed" OnClick="btnLogOut_Click"></asp:Button>




        <div id="DivUserMaint" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnUserAdd" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Add New User's" class="contact100-form-btn red" Style="left: 10px;width: 311px;" OnClick="btnUserAdd_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnUserEdit" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Edit User's" class="contact100-form-btn green" Style="left: 10px;width: 311px;" OnClick="btnUserEdit_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnUserBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Style="left: 70px; width: 96px; Height:35px" OnClick="btnUserBack_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div id="DivHolidayMaint" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnHolType" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Holiday Type" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnHolType_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnHolWeekly" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Weekly Holiday" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnHolWeekly_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnHolNational" runat="server" Font-Bold="true" Font-Size="X-Large" Text="National Holiday" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnHolNational_Click"></asp:Button>

                    </td>
                </tr>


                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnHolBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnHolBack_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div id="DivDatabaseMaint" runat="server">
            <table style="width: 340px; height: 350px; background-color: #e9e9e9;">

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnBackup" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Backup Database" class="contact100-form-btn red" Style="width: 311px;" OnClick="btnBackup_Click"></asp:Button>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnRestore" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Restore Database" class="contact100-form-btn green" Style="width: 311px;" OnClick="btnRestore_Click"></asp:Button>

                    </td>
                </tr>

                
                <tr>

                    <td style="text-align: center">
                        <asp:Button ID="btnDatabaseBack" runat="server" class="contact100-form-btn red" Text="Back"
                            Height="35" Width="96px" OnClick="btnDatabaseBack_Click" />

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

