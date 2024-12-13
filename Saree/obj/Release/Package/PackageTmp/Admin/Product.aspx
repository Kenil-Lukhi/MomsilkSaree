<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Saree.Admin.Product" %>

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

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgProduct.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
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
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Product</h4>
                                            <div>
                                                <div class="form-group">
                                                    <label>Product Name</label>
                                                    <div>
                                                        <asp:TextBox ID="productNameTxt" runat="server" CssClass="form-control" placeholder="Enter Product name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Product name is requred" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="productNameTxt"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="hdnid" runat="server" Value="0" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Description</label>
                                                    <div>
                                                        <asp:TextBox ID="txtProductDiscrtoption" runat="server" CssClass="form-control" placeholder="Enter Product description" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="discription is requred" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductDiscrtoption"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Long Description</label>
                                                    <div>
                                                        <asp:TextBox ID="txtLongDescription" runat="server" CssClass="form-control" placeholder="Enter Long description" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Long discription is requred" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtLongDescription"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Product Price(₹)</label>
                                                    <div>
                                                        <asp:TextBox ID="txtProductPrice" runat="server" CssClass="form-control" placeholder="Enter Product price"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Product price is requred" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductPrice"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Price must be numeric and formatted correctly" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductPrice" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Product Qunatity</label>
                                                    <div>
                                                        <asp:TextBox ID="txtProductQunatity" runat="server" CssClass="form-control" placeholder="Enter Product price" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Product Qunatity is requred" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductQunatity"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Qunatity must be non nagative" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductQunatity" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Image</label>
                                                    <div>
                                                        <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Product Categody</label>
                                                    <div>
                                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" DataSourceID="sqlDataSource1" DataTextField="CategoryName" DataValueField="CategoryID" AppendDataBoundItems="True" runat="server">
                                                            <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Product category is required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [Category]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>

                                        <div class="form-check pl-4">
                                            <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" CssClass="form-check-input" />
                                        </div>
                                        <div class="pb-5">
                                            <asp:Button ID="btnAddOrUpdate" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddOrUpdate_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnClear_Click" />
                                        </div>
                                        <div>
                                            <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail" />
                                        </div>
                                    </div>

                                 <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                    <h4 class="sub-title">Product List</h4>
                                    <div class="card-block table-border-style">
                                        <div class="table-responsive">
                                            <asp:Repeater ID="RProcuct" runat="server" OnItemCommand="RProcuct_ItemCommand" OnItemDataBound="RProcuct_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table class="table data-table-export table-hover nowrap">
                                                        <thead>
                                                            <tr>
                                                                <th class="table-plus">Name</th>
                                                                <th>Image</th>
                                                                <th>Price(₹)</th>
                                                                <th>Quentity</th>
                                                                <th>Category</th>
                                                                <th>Discription</th>
                                                                <th>Long Discription</th>
                                                                <th>IsActive</th>
                                                                <th>CreatedDate</th>
                                                                <th class="datatable-nosort">Action</th>
                                                            </tr>
                                                           </thead>
                                                    <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="table-plus"><%# Eval("Name") %></td>

                                                        <td>
                                                            <img alt="" width="40" src="<%# Saree.utils.GetImage(Eval("Image")) %>"" />
                                                        </td>
                                                        <td><%# Eval("Price") %></td>

                                                        <td>
                                                            <asp:Label runat="server" ID="lblQuentity" Text='<%# Eval("Quentity") %>'></asp:Label>
                                                        </td>

                                                        <td><%# Eval("CategoryName") %></td>

                                                        <td><%# Eval("Description") %></td>

                                                        <td><%# Eval("LongDescription") %></td>

                                                        <td>
                                                            <asp:Label runat="server" ID="lblIsActive" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                        </td>

                                                        <td><%# Eval("CreatedDate") %></td>

                                                        <td>
                                                            <asp:LinkButton ID="LnkEdit" Text="Edit" runat="server" CssClass="badge badge-primary" CommandArgument='<%# Eval("ProductID") %>' CommandName="Edit" CausesValidation="false">
                                                                <i class="ti-pencil"></i>
                                                            </asp:LinkButton>
                                                            
                                                            <%--<asp:LinkButton ID="LnkDelete" Text="Delete" runat="server" CssClass="badge badge-danger" CommandArgument='<%# Eval("ProductID") %>' CommandName="Delete" OnClientClick="return confirm('Do you went to delete this product?');" CausesValidation="false">
                                                                <i class="ti-trash"></i>
                                                            </asp:LinkButton> --%>

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
