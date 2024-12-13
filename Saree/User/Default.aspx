<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Saree.User.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- offer section -->
    <div class="hero_area" style="background-color: black; margin-bottom: 120px;">
        <asp:Panel ID="pnSliderUC" runat="server"></asp:Panel>
    </div>
    <section class="offer_section layout_padding-bottom">
        <div class="offer_container">
            <div class="container ">
                <div class="row">
                    <asp:Repeater ID="rCategory" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6">
                                <div class="box ">
                                    <div class="img-box">
                                        <a href="Menu.aspx?id=<%# Eval("CategoryID") %>">
                                            <img src="<%# Saree.utils.GetImage(Eval("Image")) %>" alt="">
                                        </a>
                                    </div>
                                    <div class="detail-box">
                                        <h5><%# Eval("Name") %>
                                        </h5>
                                        <h6>
                                            <span>20%</span> Off
                                        </h6>
                                        <a href="Menu.aspx?id=<%# Eval("CategoryID") %>">Order Now
                                    <svg version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 456.029 456.029" style="enable-background: new 0 0 456.029 456.029;" xml:space="preserve">
                                        <g>
                                            <g>
                                                <path d="M345.6,338.862c-29.184,0-53.248,23.552-53.248,53.248c0,29.184,23.552,53.248,53.248,53.248
                     c29.184,0,53.248-23.552,53.248-53.248C398.336,362.926,374.784,338.862,345.6,338.862z" />
                                            </g>
                                        </g>
                                        <g>
                                            <g>
                                                <path d="M439.296,84.91c-1.024,0-2.56-0.512-4.096-0.512H112.64l-5.12-34.304C104.448,27.566,84.992,10.67,61.952,10.67H20.48
                     C9.216,10.67,0,19.886,0,31.15c0,11.264,9.216,20.48,20.48,20.48h41.472c2.56,0,4.608,2.048,5.12,4.608l31.744,216.064
                     c4.096,27.136,27.648,47.616,55.296,47.616h212.992c26.624,0,49.664-18.944,55.296-45.056l33.28-166.4
                     C457.728,97.71,450.56,86.958,439.296,84.91z" />
                                            </g>
                                        </g>
                                        <g>
                                            <g>
                                                <path d="M215.04,389.55c-1.024-28.16-24.576-50.688-52.736-50.688c-29.696,1.536-52.224,26.112-51.2,55.296
                     c1.024,28.16,24.064,50.688,52.224,50.688h1.024C193.536,443.31,216.576,418.734,215.04,389.55z" />
                                            </g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                    </svg>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <section class="food_section layout_padding-bottom">
        <div class="container">
            <div class="heading_container heading_center">
                <h2>Our Saree
                </h2>
            </div>

            <div class="filters-content mt-4">
                <div class="row g-4 product-grid">
                    <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rProduct_ItemCommand">
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
            <div class="btn-box">
                <a href="Menu.aspx">View More
                </a>
            </div>
        </div>
        </div>
    </section>

    <!-- end food section -->

    <!-- about section -->

    <section class="about_section layout_padding-bottom">
        <div class="container  ">

            <div class="row">
                <div class="col-md-6 ">
                    <div class="img-box">
                        <img src="../TemplateFile/images/Saree/16.jpg" alt="">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="detail-box">
                        <div class="heading_container">
                            <h2>We Are Momai Silk
                            </h2>
                        </div>
                        <p>
                            At Momai Silk Sarees, our design philosophy blends tradition with innovation to create 
                            timeless elegance. Each saree reflects a deep respect for Indian heritage while embracing
                            modern aesthetics. Drawing inspiration from ancient motifs, nature, and contemporary art, 
                            we craft sarees that are both classic and unique. Our meticulous attention to detail, 
                            from color selection to intricate embroidery, ensures each piece is stunning and comfortable. 
                        </p>
                        <p>
                            Designed for versatility, our sarees enhance beauty and confidence for any occasion, 
                            whether grand or intimate. Experience the perfect fusion of tradition and contemporary style
                            with every Momai Silk Saree.
                        </p>

                        <a href="About.aspx">Read More
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- end about section -->


    <!-- client section -->

    <%--<section class="client_section layout_padding-bottom pt-5">
        <div class="container">
            <div class="heading_container heading_center psudo_white_primary mb_45">
                <h2>What Says Our Customers
                </h2>
            </div>
            <div class="carousel-wrap row ">
                <div class="owl-carousel client_owl-carousel">
                    <div class="item">
                        <div class="box">
                            <div class="detail-box">
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam
               
                                </p>
                                <h6>Moana Michell
                                </h6>
                                <p>
                                    magna aliqua
               
                                </p>
                            </div>
                            <div class="img-box">
                                <img src="../TemplateFile/images/client1.jpg" alt="" class="box-img">
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="box">
                            <div class="detail-box">
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam
               
                                </p>
                                <h6>Mike Hamell
                                </h6>
                                <p>
                                    magna aliqua
               
                                </p>
                            </div>
                            <div class="img-box">
                                <img src="../TemplateFile/images/client2.jpg" alt="" class="box-img">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>--%>

    <!-- end client section -->


    <script type="text/javascript">
        function redirectToDetail(productId) {
            window.location.href = 'Details.aspx?ProductID=' + productId;
        }
    </script>
</asp:Content>
