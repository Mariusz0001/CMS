<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsConfiguration.aspx.cs" Inherits="ourShop.Modules.ProductsConfiguration"  ValidateRequest="false"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/utils.js"></script>

    <div class="container_margin row">
    

        <div class="section">
            <h4>Product management</h4>
            <p>
                This is the module to adding/editing products. You can add product images, or delete it.<br />
                Here you can change product price or description.
            </p>
        </div>

        <div class="row">

            <div id="products" class="col s12">
                <div class="row right-align">
                    <asp:LinkButton ID="AddButton" runat="server" CssClass="waves-effect waves-light btn" OnClientClick="AddButton_Click();" UseSubmitBehavior="False" ValidateRequestMode="Disabled">Add<i class="material-icons right">add</i></asp:LinkButton>
                    <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-primary" OnClick="EditButton_Click" UseSubmitBehavior="False" ValidateRequestMode="Disabled">Edit<i class="material-icons right">edit</i></asp:LinkButton>
                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn btn-primary" OnClick="DeleteButton_Click" UseSubmitBehavior="False" ValidateRequestMode="Disabled">Delete<i class="material-icons right">delete</i></asp:LinkButton>
                </div>
                <div style="width: 100%; height: 500px; overflow: auto">
                    <asp:GridView ID="ProductGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="ProductGrid_RowCommand" Width="100%" Height="500px" DataKeyNames="Id" ViewStateMode="Enabled" AllowSorting="True" AllowPaging="False" ShowFooter="False" OnRowEditing="ProductGrid_RowEditing"
                        >
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="edit" HeaderText="Edit" Text="Edit" ControlStyle-CssClass="waves-effect waves-light btn" />
                            <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Image ID="PicturePath" runat="server" Visible="true" Height="100px" Width="100px" ImageUrl='<%# Eval("PicturePath") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:BoundField DataField="IdCategoriesBook" Visible="false" />
                            <asp:BoundField DataField="IdProductsStatusBook" Visible="false" />
                            <asp:BoundField DataField="IdTaxPercentagesBook" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" Visible="true" />
                            <asp:BoundField DataField="StatusName" HeaderText="StatusName" Visible="true" />
                            <asp:BoundField DataField="TaxValue" HeaderText="TaxValue" Visible="true" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" Visible="true" />
                            <asp:BoundField DataField="Price" HeaderText="Price" Visible="true" />
                            <asp:BoundField DataField="QTY" HeaderText="QTY" Visible="true" />
                            <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div> 
  
    <script type="text/javascript">

        function AddButton_Click(s, e) {
            window.open('/Modules/EditForm/ProductMngm.aspx', 'Product', 'directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=1000,height=800');
        };

    </script>

</asp:Content>
    