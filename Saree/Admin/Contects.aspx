﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Contects.aspx.cs" Inherits="Saree.Admin.Contects" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content" style="padding-top: 0;">

        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
        </div>

        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Contect List</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rContect" runat="server" OnItemCommand="rContect_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">SrNo.</th>
                                                                        <th>Contect Name</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Email</th>
                                                                        <th>Subject</th>
                                                                        <th>Message</th>
                                                                        <th>Contect Date</th>
                                                                        <th class="datatable-nosort">Delete</th>
                                                                    </tr>
                                                                </thead>

                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("SrNo") %> </td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Phone") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("Subject") %></td>
                                                                <td><%# Eval("Message") %></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="LnkDelete" Text="Delete" runat="server" CssClass="badge badge-danger" CommandArgument='<%# Eval("ContactID") %>' CommandName="Delete" OnClientClick="return confirm('Do you went to delete this Contect?');">
                                                                        <i class="ti-trash"></i>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
