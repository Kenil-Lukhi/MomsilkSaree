<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Saree.User.Login" %>

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
    </script>
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_center">
                <div class="align-self-end">
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                </div>
                <h2>Login</h2>
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
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Username is requred" ControlToValidate="txtUsername" Font-Size="Small" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username" autocomplete="username"></asp:TextBox>
                        </div>


                        <div style="position: relative;">
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" Font-Size="Small" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" autocomplete="current-password"></asp:TextBox>
                            <span onclick="togglePassword()" style="position: absolute; right: 10px; top: 10px; cursor: pointer;">
                                <i class="fa fa-eye-slash" id="toggleEye"></i>
                            </span>
                        </div>
                        
                        <div class="d-flex justify-content-between mb-3">
                            <div class="form-check">
                                <asp:CheckBox ID="rememberMe" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label" for="rememberMe">I agree to the <a href="PrivacyPolicy.aspx"> Privacy Policy</a></label>
                            </div>
                            <a href="ForgetPassword.aspx" class="text-info">Forgot password?</a>
                        </div>

                        <div class="btn-box text-center">
                            <asp:Button ID="Button3" runat="server" Text="Login" CssClass="btn btn-primary rounded-pill w-100" OnClick="btnLogin_Click" />
                        </div>

                        <div class="text-center mt-3">
                            <p>Don't have an account? <a href="Registration.aspx" class="text-primary">Signup</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
