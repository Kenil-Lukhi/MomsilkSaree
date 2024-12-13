<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" Async="true" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Saree.User.Profile" %>

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

    <%
        string imgUrl = Session["imageUrl"].ToString();
    %>

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_center">
                <div class="align-self-end">
                    <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
                </div>
                <h2>User information</h2>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title md-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">
                                        <img src="<%= Saree.utils.GetImage(imgUrl) %>" id="imgProfile"  class="img-thumbnail" style="width: 150px; height:150px; background-color:black;" />
                                        <div class="middle pt-2">
                                            <a href="Registration.aspx?id=<% Response.Write(Session["userid"]); %>" class="btn btn-warning">
                                                <i class="fa fa-pencil"></i>Edit Details
                                            </a>
                                        </div>
                                    </div>

                                    <div class="userData mt-5" style="margin-left:50px;">
                                        <h6 class="d-block">
                                           FullName : <% Response.Write(Session["PRname"]); %>
                                        </h6>

                                        <h6 class="d-block">
                                           UserName : <% Response.Write(Session["username"]); %>
                                        </h6>

                                        <h6 class="d-block"">
                                            Email : <% Response.Write(Session["PREmail"]); %>
                                        </h6>

                                        <h6 class="d-block"">
                                           Mobile NO : <% Response.Write(Session["PRMobile"]); %>
                                        </h6>
                                        <h6 class="d-block"">
                                            Address : <% Response.Write(Session["PRAddress"]); %>
                                        </h6>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active text-info" id="basicInfo-tab" data-toggle="tab" href="#basicInfo" role="tab" aria-controls="basicInfo" aria-selected="true">
                                                <i class="fa fa-id-badge mr-2"></i>
                                                Basic Info
                                            </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="connectedServices-tab" data-toggle="tab" href="#connectedServices" role="tab" aria-controls="connectedServices" aria-selected="false">
                                                <i class="fa fa-clock-o mr-2"></i>Purchased History
                                            </a>
                                        </li>
                                    </ul>

                                    <div class="tab-content ml-1" id="MyTabContent">
                                        <div class="tab-pane fade show active" id="basicinfo" role="tabpanel" aria-labelledby="basicinfo-tab">
                                            <asp:Repeater runat="server" ID="rUserProfile">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Full Name</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Name") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Username</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Username") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Mobile NO</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Mobile") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Email</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Email") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Post Code</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("PostCode") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Full Name</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Address") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <%--Basic Info end--%>

                                        <%--Order History start--%>
                                        <div class="tab-pane fade" id="connectedServices" role="tabpanel" aria-labelledby="connectedServer-tab">
                                            <asp:Repeater ID="rPurchesHistory" runat="server" OnItemDataBound="rPurchesHistory_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="container">
                                                        <div class="row pt-1 pb-0" style="background-color: lightgray;">
                                                            <div class="col-4">
                                                                <span class="badge badge-pill badge-dark text-white">
                                                                    <%# Eval("SrNo") %>
                                                                </span>
                                                                Payment Mode : <%# Eval("paymentMethod").ToString() == "COD" ? "CASH ON DELIVERY" : Eval("paymentMethod").ToString().ToUpper() %>
                                                            </div>
                                                            <div class="col-4">
                                                                <%# string.IsNullOrEmpty(Eval("CardNO").ToString())? "" : "Cart No : " + Eval("CardNO") %>
                                                            </div>

                                                            <div class="col-4" style="text-align: end;">
                                                                <a href="Invoice.aspx?id=<%# Eval("PaymentID") %>" class="btn btn-info btn-smx`">
                                                                    <i class="fa fa-download mr-2"></i>Invoice
                                                                </a>
                                                                
                                                            </div>
                                                        </div>

                                                        <asp:HiddenField ID="hdnPayment" runat="server" Value='<%# Eval("PaymentID") %>' />

                                                        <asp:Repeater ID="rOrder" runat="server" OnItemCommand="rOrder_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table class="table data-table-export table-responsive-sm table-bordered table-hover">
                                                                    <thead class="bg-dark text-white">
                                                                        <tr>
                                                                            <th>Product Name</th>
                                                                            <th>Unit Price</th>
                                                                            <th>Quentity</th>
                                                                            <th>Total Price</th>
                                                                            <th>Order ID</th>
                                                                            <th>Status</th>
                                                                            <th>Order Date</th>
                                                                            <th>Action</th>
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
                                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# string.IsNullOrEmpty(Eval("Price").ToString())? "" : "₹" + Eval("Price") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblOrder" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass='<%# Eval("Status").ToString() == "Delivered" ? "badge badge-success" : (Eval("Status").ToString() == "Cancel" ? "badge badge-danger" : "badge badge-warning") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblOrdrDt" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lbDeleteOrder" runat="server" style="color: red;" CommandName="CencelOrder" CommandArgument='<%# Eval("OrderDetailsID") %>'>
                                                                            <i class="fa fa-trash-o"></i>Cancel
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
