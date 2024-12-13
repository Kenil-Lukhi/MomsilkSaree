<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Saree.User.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">--%>
    <style>
        .custom-circle-icon {
    display: inline-block;
    width: 12px; /* Adjust the size as needed */
    height: 12px;
    background-color: #4CAF50; /* Green color */
    border-radius: 50%; /* Makes it a circle */
    vertical-align: middle;
}

.key-features {
    font-family: Arial, sans-serif;
    color: #000; /* Black text color */
}

    .key-features h3 {
        font-weight: bold;
        margin-bottom: 10px;
    }

    .key-features ul {
        list-style-type: disc; /* Bullet points */
        padding-left: 20px; /* Indentation for list */
    }

    .key-features li {
        margin-bottom: 8px; /* Space between each feature */
        line-height: 1.5; /* Spacing between lines for readability */
    }

    .key-features strong {
        font-weight: bold; /* Bold for emphasis */
    }

.features-container {
    text-align: center;
    background-color: #f4c7bd;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    padding: 20px;
    max-width: 700px;
    margin: 0 auto;
}

    .features-container h3 {
        color: #333333;
        margin-bottom: 15px;
        font-size: 24px;
        font-weight: bold;
    }

    .features-container p {
        color: #555555;
        margin-bottom: 10px;
        font-size: 16px;
        line-height: 1.6;
    }

        .features-container p:last-child {
            margin-bottom: 0;
            font-style: italic;
        }

.d-flex-forDetails {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Shop Detail Start -->
    <%
        string imgUrl = Session["ProductImage"].ToString();
    %>
    <div class="container">
        <div class="heading_container heading_center">
            <div class="align-self-end">
                <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
    <div class="container-fluid py-5">
        <div class="row px-xl-5">
            <div class="col-lg-5 pb-5">
                <div id="product-carousel" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner border">
                        <img class="w-100 h-100" src='<%= Saree.utils.GetImage(imgUrl) %>' alt="Image">
                    </div>
                </div>
            </div>

            <div class="col-lg-7 pb-5">
                <h3 class="font-weight-semi-bold"><% Response.Write(Session["ProductName"]); %> </h3>
                <div class="d-flex mb-3">
                </div>

                <h4 class="font-weight-semi-bold">
                    <span style="color: gray; font-weight: normal;">M.R.P. </span>
                    <span>₹<% Response.Write(Session["ProductPrice"]); %></span>
                </h4>
                <div class="font-weight-semi-bold mb-4" style="font-size: small;">
                    Tax included. Shipping calculated at checkout.
                </div>

                <h4 class="mb-4">Description</h4>
                <div class="d-flex mb-3">
                    <p class="text-dark font-weight-medium mb-0 mr-3">
                        <% Response.Write(Session["ProductDescription"]); %>
                    </p>
                </div>
                <div class="d-flex mb-3">
                    <p class="text-dark font-weight-medium mb-0 mr-3">
                        <% Response.Write(Session["ProductLongDescription"]); %>
                    </p>
                </div>

                <div class="d-flex mb-3">
                    <p class="text-dark font-weight-medium mb-0">
                        <i class="fa fa-globe" style="color: blue; margin-right: 0.5rem;"></i>Free Worldwide Shipping
                   
                    </p>
                </div>
                <div class="d-flex mb-3">
                    <p class="text-dark font-weight-medium mb-0">
                        <i class="fa fa-check-circle text-dark mr-2"></i>Cash On Delivery Available. ( India )
                   
                    </p>
                </div>
                <div class="d-flex mb-3">
                    <p class="text-dark font-weight-medium mb-0">
                        <i class="fa fa-circle text-success mr-2"></i>In stock, ready to ship
   
                    </p>
                </div>


                <div class="d-flex align-items-center mb-4 pt-2">
                    <div class="product__details__option">
                        <div class="quantity">
                            <div class="pro-qty">
                                <asp:TextBox ID="txtQuantity" runat="server" Text="1"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revQuentity" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Small" ValidationExpression="[1-9]*" ControlToValidate="txtQuantity" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <asp:LinkButton ID="lbCheckout" runat="server" OnClick="lbCheckout_Click" CssClass="btn btn-primary">
                        <i class="fa fa-shopping-cart mr-1"></i>Add To Cart
                    </asp:LinkButton>
                </div>

                <div class="d-flex mb-3">
                    <%--<p class="text-dark font-weight-medium mb-0 mr-3">--%>
                    <h5 class="font-weight-semi-bold">Pure Cotton Linen Saree Weaved With Zari Comes With Tassels ₹ <% Response.Write(Session["ProductPrice"]); %></h5>
                    <%--</p>--%>
                </div>

                <div class="d-flex mb-3">
                    <div class="key-features">
                        <h3>Key Features:</h3>
                        <ul>
                            <li><strong>Material:</strong> Made from a luxurious blend of pure cotton and linen, providing a soft, breathable, and comfortable drape.</li>
                            <li><strong>Design:</strong> The saree features a refined zari weave that adds a touch of subtle shimmer and sophistication.</li>
                            <li><strong>Tassel Edges:</strong> Finished with charming tassels on the edges, giving the saree a contemporary and stylish touch.</li>
                            <li><strong>Occasion:</strong> Perfect for casual outings, office wear, festive celebrations, and cultural events, this saree is a versatile addition to your ethnic wardrobe.</li>
                        </ul>
                    </div>
                </div>

                <div class="d-flex mb-3">
                    <div class="key-features">
                        <h3>Additional Information:</h3>
                        <ul>
                            <li><strong>Length:</strong> 6.30 meters with blouse</li>
                            <li><strong>Blouse Piece:</strong> Comes with an unstitched blouse piece, allowing for customized tailoring to your desired fit.</li>
                            <li>Care Instructions: Dry clean recommended to maintain the fabric’s texture and sheen.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="d-flex-forDetails mb-3">
        <div class="features-container">
            <h3>Why Choose Cotton Linen?</h3>
            <p class="text-dark font-weight-medium mb-0">
                Cotton linen is celebrated for its breathability, durability, and natural texture, making it an ideal fabric for sarees that are comfortable to wear throughout the day. Its blend ensures that you enjoy the softness of cotton and the sturdy feel of linen, providing a perfect balance for any season.
           
            </p>
            <p class="text-dark font-weight-medium mb-0">
                Elevate your ethnic collection with this pure cotton linen saree, a seamless fusion of traditional elegance and modern comfort.
           
            </p>
            <p class="text-dark font-weight-medium mb-0">
                There Might Be Minor Colour Variation Between Actual Product And Image Shown On Screen Due To Lighting On The Photography.
           
            </p>
        </div>
    </div>
    <!-- Shop Detail End -->
</asp:Content>

