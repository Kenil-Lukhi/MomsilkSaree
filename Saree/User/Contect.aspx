<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contect.aspx.cs" Inherits="Saree.User.Contect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
        width: 80%;
        margin: 0 auto;
        padding: 20px;
        background-color: #fff;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }
    h1 {
        text-align: center;
        color: #333;
    }
    .contact-info {
        margin-bottom: 20px;
    }
    .contact-info h3 {
        color: #555;
    }
    .contact-info p {
        margin: 5px 0;
    }
    .form-container {
        background-color: #f9f9f9;
        padding: 20px;
        border-radius: 5px;
    }
    .form-container input, .form-container textarea {
        width: 100%;
        padding: 10px;
        margin: 10px 0;
        border: 1px solid #ccc;
        border-radius: 5px;
    }
    .form-container button {
        padding: 10px 20px;
        background-color: #333;
        color: #fff;
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }
    .form-container button:hover {
        background-color: #555;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <h1>Contact Us</h1>
    <div class="contact-info">
        <h3>CONTACT INFORMATION</h3>
        <p>We love to hear from you on our customer service, merchandise, website, or any topics you want to share with us. Your comments and suggestions will be appreciated. Please complete the form below.</p>
        <p><strong>E-mail:</strong> <a href="mailto:info@almaarifashion.com">info@almaarifashion.com</a></p>
        <p><strong>Whatsapp Us On:</strong> +91 9033009044</p>
        <p><strong>Customer Support:</strong> 11:00 AM to 6:00 PM (Mon-Sat), Sunday Closed</p>
        <p><strong>Store Address:</strong> 88-89, Avadh Textile Market, Opp. New Bombay Market, Umarwada, Surat-395010</p>
    </div>

    <div class="form-container">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" placeholder="Your Name" required="required"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Your Email" required="required"></asp:TextBox>
        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" placeholder="Your Message" required="required"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
</div>
</asp:Content>
