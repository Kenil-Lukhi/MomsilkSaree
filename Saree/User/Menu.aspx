<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Saree.User.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- food section -->

    <section class="food_section layout_padding">
        <div class="container">
            <div class="heading_container heading_center">
                <div class="align-self-end">
                    <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
                </div>
                <h2>Our Saree
                </h2>
            </div>

            <ul class="filters_menu">
                <li class="active" data-filter="*" data-id="0">All</li>
                <asp:Repeater runat="server" ID="rCategory">
                    <ItemTemplate>
                        <li data-filter=".<%# Regex.Replace(Eval("Name").ToString().ToLower(),@"\s+","") %>" data-id='<%# Eval("CategoryID") %>'>
                            <%# Eval("Name") %>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

            <div class="filters-content mt-4">
                <div class="row grid g-4 product-grid">
                    <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rCategory_ItemCommand">

                        <ItemTemplate>
                            <div class="col-sm-6 col-md-4 col-lg-3 all <%# Regex.Replace(Eval("CategoryName").ToString().ToLower(), @"\s+", "") %>">
                                <div class="product-box border rounded overflow-hidden shadow-sm mt-3">
                                    <div class="product-image position-relative">
                                        <div class="new-season-label position-absolute top-0 start-0 m-2 px-3 py-1 rounded">
                                            New Season
           
                                        </div>
                                        <img src="<%# Saree.utils.GetImage(Eval("Image")) %>" alt="<%# Eval("Name") %>" onclick="redirectToDetail('<%# Eval("ProductID") %>')" class="img-fluid w-100" style="cursor: pointer;" />
                                    </div>
                                    <div class="product-details text-center py-3">
                                        <h5 class="product-title mb-2">
                                            <%# Eval("Name") %>
            </h5>
                                        <p class="product-description text-muted mb-2">
                                            <%# Eval("Description") %>
                                        </p>
                                        <div class="product-options d-flex justify-content-between align-items-center px-3">
                                            <h6 class="product-price mb-0">₹<%# Eval("Price") %></h6>
                                            <asp:LinkButton runat="server" ID="lblAddToCart" CssClass="btn btn-primary btn-sm" CommandName="AddToCart" CommandArgument='<%# Eval("ProductID") %>'>
                    Add to Cart <i class="fa fa-shopping-cart"></i>
                </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </section>

    <!-- end food section -->


    <script type="text/javascript">
        function redirectToDetail(productId) {
            window.location.href = 'Details.aspx?ProductID=' + productId;
        }
    </script>
</asp:Content>
