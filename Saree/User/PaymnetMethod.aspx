<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" Async="true" AutoEventWireup="true" CodeBehind="PaymnetMethod.aspx.cs" Inherits="Saree.User.PaymnetMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .rounded {
            border-radius: 1rem
        }

        .nav-pills .nav-link {
            color: #555
        }

            .nav-pills .nav-link.active {
                color: white
            }

        .bold {
            font-weight: bold
        }

        .input-group .radio-label {
            margin-right: 20px;
            font-size: 16px;
            color: #555;
            display: flex;
            align-items: center;
            cursor: pointer;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            transition: background-color 0.3s, border-color 0.3s;
        }

            .input-group .radio-label:hover {
                background-color: #f0f0f0;
                border-color: #007bff;
            }

        .input-group input[type="radio"] {
            margin-right: 10px;
            accent-color: #007bff; /* Change the color to match your theme */
        }

        .input-group .radio-label input[type="radio"] {
            display: none;
        }

        .input-group .radio-label span {
            display: inline-block;
            width: 20px;
            height: 20px;
            border: 2px solid #007bff;
            border-radius: 50%;
            margin-right: 10px;
            position: relative;
        }

        .input-group .radio-label input[type="radio"]:checked + span::after {
            content: '';
            width: 12px;
            height: 12px;
            background-color: #007bff;
            border-radius: 50%;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        .card {
            padding: 40px 50px;
            border-radius: 20px;
            border: none;
            box-shadow: 1px 5px 10px 1px rgba(0, 0, 0, 0.2)
        }
    </style>
    <script>
        /*For disappearing alert message*/
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        /*For tooltip*/
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section" style="background-image: url('../Images/payment-bg.png'); width: 100%; height: 100%; background-repeat: no-repeat; background-size: auto; background-attachment: fixed; background-position: left;">

        <div class="container py-5">
            <div class="align-self-end">
                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="row mb-4">
                <div class="col-lg-8 mx-auto text-center">
                    <h1 class="display-6">Order Information</h1>
                </div>
            </div>
            <div class="row pb-5">
                <div class="col-lg-6 mx-auto">
                    <div class="card ">
                        <div class="card-header">
                            <div class="tab-content">
                                <div id="credit-card" class="tab-pane fade show active pt-3">
                                    <div role="form">
                                        <div class="form-group">
                                            <label for="txtName">
                                                <h6>Customer Name</h6>
                                            </label>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is required" ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name must be in characters" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName" ValidationGroup="card">*</asp:RegularExpressionValidator>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Customer name"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtAdress">
                                                <h6>Street Address</h6>
                                            </label>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Address is required" ControlToValidate="txtAdress" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtAdress" runat="server" CssClass="form-control" placeholder="Address" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="City is required" ControlToValidate="txtCity" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group mb-6">
                                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="State is required" ControlToValidate="txtState" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Country is required" ControlToValidate="txtCountry" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" placeholder="Country"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group mb-6">
                                                    <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ErrorMessage="Postal Code is required" ControlToValidate="txtPostalCode" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REVPostCode" runat="server" ErrorMessage="PostCode Number must be of 6 digit" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{6}$" ControlToValidate="txtPostalCode"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" placeholder="Postal Code" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAdress">
                                                <h6>Contect Details</h6>
                                            </label>
                                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Address is required" ControlToValidate="txtAdress" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="card">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Mobile Number is required" ControlToValidate="txtMobileNo" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Mobile Number must have 10 digit" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtMobileNo"></asp:RegularExpressionValidator>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile Number" ToolTip="Mobile Number" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email address is required" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" ToolTip="Email" TextMode="Email"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAdress">
                                                <h6>Payment Method</h6>
                                            </label>
                                            <div class="input-group">
                                                <label class="radio-label">
                                                    <asp:RadioButton ID="COD" runat="server" GroupName="PaymentMethod" />
                                                    <span></span>Cash on Delivery
                                                </label>
                                                <label class="radio-label">
                                                    <asp:RadioButton ID="Online" runat="server" GroupName="PaymentMethod" />
                                                    <span></span>Online Payment
                                                </label>
                                            </div>
                                        </div>

                                        <div class="card-footer">
                                            <asp:LinkButton ID="lbCardSubmit" runat="server" CssClass="subscribe btn btn-info btn-block shadow-sm" ValidationGroup="card" OnClick="lbCardSubmit_Click">Confirm Order</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <b class="badge badge-success badge-pill shadow-sm">Order Total: ₹ <% Response.Write(Session["grandTotalPrice"]); %> </b>
                            <div class="pt-1">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="card" HeaderText="Fix the following errors" Font-Names="Segoe Script" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
