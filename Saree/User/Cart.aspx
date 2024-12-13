<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Saree.User.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
                </div>
                <h2>Your Shopping Cart
                </h2>
            </div>
        </div>
        <div class="container">
            <asp:Repeater ID="rCartItem" runat="server" OnItemCommand="rCartItem_ItemCommand" OnItemDataBound="rCartItem_ItemDataBound">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <td>Name</td>
                                <td>Image</td>
                                <td>Unit Price</td>
                                <td>Quentity</td>
                                <td>Total Price</td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </td>

                        <td>
                            <img width="60" src="<%# Saree.utils.GetImage( Eval("Image")) %>" alt="" />
                        </td>

                        <td>₹<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            <%--<asp:HiddenField ID="hdnProductID" runat="server" Text="<%# Eval("ProductID") %>" />--%>
                            <asp:HiddenField ID="hdnProductID" runat="server" Value='<%# Eval("ProductID") %>' />
                            <asp:HiddenField ID="hdnQuentity" runat="server" Value='<%# Eval("Quentity") %>' />
                            <asp:HiddenField ID="hdnProductQuentity" runat="server" Value='<%# Eval("ProductQuentity") %>' />
                        </td>

                        <td class="product__details__option">
                            <div class="quantity">
                                <div class="pro-qty">
                                    <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text='<%# Eval("Quentity") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revQuentity" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Small" ValidationExpression="[1-9]*" ControlToValidate="txtQuantity" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </td>

                        <td>₹<asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                        </td>

                        <td>
                            <asp:LinkButton ID="lbDelete" runat="server" Text="Remove" CommandName="remove" CommandArgument='<%# Eval("ProductID") %>' OnClientClick="return confirm('Do you want to remove this item from cart?');">
                                    <i class="fa fa-close"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>

                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3"></td>
                        <td class="pl-lg-5">Grand Total : </td>
                        <td>₹<% Response.Write(Session["GrandTotalPrice"]); %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="continue__btn">
                            <a href="Menu.aspx" class="btn btn-info">
                                <i class="fa fa-arrow-circle-left mr-2"></i>Continue Shopping
                            </a>
                        </td>

                        <td colspan="2" >
                            <asp:LinkButton ID="lbUpdateCaer" runat="server" CommandName="UpdateCart" CssClass="btn btn-warning">
                                <i class="fa fa-refresh mr-2"></i>Update Cart
                            </asp:LinkButton>
                        </td>

                        <td colspan="2" >
                            <asp:LinkButton ID="lbCheckout" runat="server" CommandName="checkout" CssClass="btn btn-success">
                                Checkout<i class="fa fa-arrow-circle-right ml-2"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

    </section>
</asp:Content>
