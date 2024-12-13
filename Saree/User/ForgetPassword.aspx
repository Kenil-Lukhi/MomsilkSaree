<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="Saree.User.ForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var second = 5;
            setTimeout(function () {
                var lblMsg = document.getElementById('<%= lblMsg.ClientID%>');
                if (lblMsg) {
                    lblMsg.style.display = "none";
                }
            }, second * 1000);
        }

        function togglePassword() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var toggleEye = document.getElementById('toggleEye');
            if (passwordField.type === "text") {
                passwordField.type = "password";
                toggleEye.classList.remove("fa-eye");
                toggleEye.classList.add("fa-eye-slash");
            } else {

                passwordField.type = "text";
                toggleEye.classList.remove("fa-eye-slash");
                toggleEye.classList.add("fa-eye");
            }
        }

        function toggleConConPassword() {
            var passwordField = document.getElementById('<%= txtConPassword.ClientID %>');
            var toggleEye = document.getElementById('toggleEyeCon');
            if (passwordField.type === "text") {
                passwordField.type = "password";
                toggleEye.classList.remove("fa-eye");
                toggleEye.classList.add("fa-eye-slash");
            } else {

                passwordField.type = "text";
                toggleEye.classList.remove("fa-eye-slash");
                toggleEye.classList.add("fa-eye");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_center">
                <div style="display: flex; flex-direction: column;">
                    <div style="align-self: flex-end;">
                        <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    </div>
                </div>
                <h2>Forget Password</h2>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <img id="userLogin" src="../Images/Login.jpg" class="img-thumbnail" alt="" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form_container">
                        <div>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Email is requred" ControlToValidate="txtEmail" Font-Size="Small" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" TextMode="Email"></asp:TextBox>
                        </div>


                        <div style="position: relative;">
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" Font-Size="Small" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                            <span onclick="togglePassword()" style="position: absolute; right: 10px; top: 10px; cursor: pointer;">
                                <i class="fa fa-eye-slash" id="toggleEye"></i>
                            </span>
                        </div>

                        <div style="position: relative;">
                            <asp:RequiredFieldValidator ID="rfvConPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="txtConPassword" Font-Size="Small" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtConPassword" runat="server" CssClass="form-control" placeholder="Enter Conform Password" TextMode="Password"></asp:TextBox>
                            <span onclick="toggleConConPassword()" style="position: absolute; right: 10px; top: 10px; cursor: pointer;">
                                <i class="fa fa-eye-slash" id="toggleEyeCon"></i>
                            </span>
                            <asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="txtConPassword" ControlToCompare="txtPassword" Operator="Equal" Font-Size="Small" Type="String" ErrorMessage="Passwords do not match!" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                        </div>

                        <div class="btn-box text-center">
                            <%--<asp:Button ID="Button3" runat="server" Text="Login" CssClass="btn btn-primary rounded-pill w-100" />--%>
                            <asp:Button ID="ResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary rounded-pill w-100" OnClick="ResetPassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
