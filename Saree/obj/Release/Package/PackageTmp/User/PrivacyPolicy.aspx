<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="PrivacyPolicy.aspx.cs" Inherits="Saree.User.PrivacyPolicy" %>

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

        h1, h2 {
            color: #f7f5f5;
        }

        h1 {
            text-align: center;
            font-size: 2em;
        }

        .policy-section {
            background: #222831;
            padding: 20px;
            margin: 0 auto;
            max-width: 63rem;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            color : #f7f5f5;
            margin-bottom:10px;
        }

            .policy-section h2 {
                font-size: 1.5em;
                margin-top: 20px;
            }

            .policy-section p {
                margin: 15px 0;
            }

            .policy-section ul {
                list-style-type: disc;
                margin-left: 20px;
            }

            .policy-section li {
                margin-bottom: 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server">
        <h1 class="header1">Privacy Policy</h1>
        <div class="policy-section">
            <p>
                This Privacy Policy describes the manner in which MOMAI SILK including any affiliate of MOMAI SILK and/or any
                group company of MOMAI SILK (hereinafter individually and collectively referred to as “Company”) collects, stores
                and/or uses information or data provided by you or in relation to you. This data is collected/ stored and used by your 
                access and/or use of the website and accessing information or conducting transactions of any product offered on or through the Website 
                including offering of Women’s Ethnic Wear on Company’s websites or other platforms (“Products”) and providing a platform 
                for sale and purchase of Products and/or other services provided on the Website or on Company’s other websites or platform
                (hereinafter referred to as “Services”).
            </p>

            <p>This Privacy Policy shall be read in conjunction with the Terms of Use available at Terms of Use. Your access to and use of the Website and Services is conditional upon your acceptance of this Privacy Policy and the Terms of Use. You represent that you are above 18 years of age and competent to enter into legally binding contracts. By accessing and/or using the Website and/or Services, you consent to your information and data being collected, stored and used in the manner laid out in this Privacy Policy.</p>

            <h2>Collection of Information</h2>
            <p>Company may collect the following information and data (hereinafter referred to as “Your Information”) depending on the extent of use of the Website and/or Services by you:</p>
            <ul>
                <li>Name;</li>
                <li>Phone number;</li>
                <li>Address;</li>
                <li>Email ID;</li>
                <li>Age;</li>
                <li>Gender;</li>
                <li>Location;</li>
                <li>Language;</li>
                <li>Shopping interests and preferences;</li>
                <li>Date of birth;</li>
                <li>PAN;</li>
                <li>GST number;</li>
                <li>Government issued ID cards/number; and</li>
                <li>Know-You-Customer (KYC) details;</li>
                <li>Browsing history, including URLs visited before and after your visit to the website;</li>
                <li>Buying behaviour.</li>
            </ul>

            <h2>Use of Information</h2>
            <p>Company shall store and use Your Information collected in accordance with this Privacy Policy for the following purposes:</p>
            <ul>
                <li>Processing your orders and purchases of Products and providing services such as delivering orders to you;</li>
                <li>Providing relevant, useful and appropriate Products and Services to you and sending targeted communications to you;</li>
                <li>Monitoring, improving and troubleshooting the Products and Services provided to you;</li>
                <li>Resolving user complaints including complaints relating to faulty/ defective products, payment disputes, and for other communications with users regarding use of the Website and Services;</li>
                <li>Processing information and documents, including financial or other sensitive personal information as may be provided by you;</li>
                <li>Storing information, documents, and data to train algorithms to optimize the Products and Services;</li>
                <li>Operation, provision or improvement of the Website and Services and for all acts incidental or ancillary to the provision of the Website and Services and for developing and providing new technologies and services;</li>
                <li>Research on users' demographics, interests, and behaviour to better understand and serve users.</li>
            </ul>

            <h2>Cookie Policy</h2>
            <p>Like most other websites, the Company uses cookies to gather information when you access or browse the Website. The Company may also use similar technologies such as pixels and tags. A cookie is a small text file that a website stores on Your computer when you visit the website. The Company uses cookies from third-party partners such as Google Analytics and Facebook Pixel for marketing and analytical purposes.</p>

            <h2>Consent</h2>
            <p>As stated above, by accessing and/or using the Website and/or Services, you consent to Your Information being collected, stored and used in the manner laid out in this Privacy Policy. The collecting and retaining agency in respect of Your Information is Company.</p>

            <h2>Security</h2>
            <p>Company undertakes its best efforts to ensure that its systems and Your Information are secure. It complies with reasonable security practices and procedures by following industry standards such as International Standard IS/ISO/IEC 27001. However, since complete security cannot be ensured by any entity, please note that Company does not guarantee the security of Your Information.</p>

            <h2>Changes to the Privacy Policy</h2>
            <p>Company reserves the right to update or modify this Privacy Policy at any time. Any change to this Privacy Policy shall be deemed to be effective as soon as such change reflects on <a href="">Privacy Policy Page</a>. You are advised to check this Privacy Policy periodically to keep yourself updated on any changes.</p>
        </div>
    </div>
</asp:Content>
