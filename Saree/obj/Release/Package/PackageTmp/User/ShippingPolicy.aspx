<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ShippingPolicy.aspx.cs" Inherits="Saree.User.ShippingPolicy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .header1 {
            all: unset;
            display: block;
            font-style: normal;
            text-align: center;
            font-size: 3em;
            margin-bottom: 20px;
            margin: 20px;
        }

        .header2 {
            all: unset;
            display: block;
            font-style: normal;
            font-size: 1.5em;
            margin-bottom: 10px;
            margin: 5px;
        }

        .policy-section {
            background-color: #222831;
            padding: 20px;
            margin: 20px auto;
            max-width: 1000px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            color: white;
        }

            .policy-section h2 {
                font-size: 1.5em;
                margin-bottom: 10px;
            }

            .policy-section ul {
                list-style-type: none;
                padding-left: 0;
            }

            .policy-section li {
                margin-bottom: 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="header1">Shipping Policy</h1>
    <div runat="server">
        <div class="policy-section">

            <h2 class="header2">Shipping in India</h2>
            <ul>
                <li>Free shipping on all products for COD and prepaid orders.</li>
                <li>Once the domestic order is placed, it takes 1 to 2 business days to dispatch after placing the order. Then 4 to 5 business days for it to get delivered.</li>
                <li>So, the estimated time would be 10 to 15 working days. You will receive the tracking details once your order has been shipped.</li>
                <li>These timelines may be affected due to current situations.</li>
            </ul>

            <h2>International Shipping</h2>
            <ul>
                <li>FREE SHIPPING WORLDWIDE. PRICE MENTIONED ON WEBSITE IS INCLUDING SHIPPING COST.</li>
                <li>No Cancelation, No Return & No Exchange Policy For International Orders.</li>
                <li>If you have received the product and you do not like it, the same can be shipped back within 2 days and email us the courier receipt on info : <a href="https://mail.google.com/mail/?view=cm&fs=1&to=momaisilk7073@gmail.com">momaisilk7073@gmail.com</a> The shipping charges for sending the product back have to be borne by the customer. 5% of PayPal transaction amount will be deducted from the refund.</li>
                <li>Once we receive the products, a strict quality check would be done by our team. Please make sure that the original packing is intact when you courier it back to us.</li>
                <li>The amount refunded to you would be only for the products.</li>
                <li>We unfortunately do not have an exchange policy for international customers.</li>
                <li>Once an order is placed it cannot be cancelled.</li>
                <li>Once the international order is placed, it takes 15 to 20 working days to dispatch after placing the order.</li>
                <li>If customization is added it will take more 4 to 5 working days to dispatch.</li>
                <li>So, the estimated time would be 20 to 25 working days for the order to get delivered. You will receive the tracking details once your order has been shipped.</li>
                <li>These timelines may be affected due to current situations. Prices are inclusive of shipping charges. Any other charges like import duty has to be borne by the customer.</li>
                <li>If the import duty is not paid by the customer the product will be destroyed/abandoned by the customs department and the refund will not be given for that particular order.</li>
                <li>If the shipping address is wrong or any mistake is made by the customer, then the customer has to make the extra payment for any changes in their address.</li>
                <li>Customer duty has to be paid within 2 days after the shipment arrives at your location/country.</li>
            </ul>
        </div>
    </div>
</asp:Content>
