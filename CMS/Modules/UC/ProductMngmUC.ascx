<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductMngmUC.ascx.cs" Inherits="CMS.Modules.UC.ProductMngmUC" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    
      <script src="../Content/Controls/tinymce/tinymce.js"></script>
        <script src="../Scripts/utils.js"></script>

    
                        
    <div class="container_margin row">

        <div class="section">
            <h4>Product management</h4>
            <p>This is the module to adding/editing products. You can add product images, or delete it.<br />
               Here you can change product price or description.</p>
        </div>
      
        <div class="row">
            <div class="col s12 m8">
                <div class="row">
                    <div class="input-field col l6">
                        <label style="color: crimson;">*</label>
                        <label class="col control-label" for="Name">Name</label>
                        <asp:TextBox ID="Name" runat="server" CssClass="form-control" Font-Size="Larger" Height="40px" Font-Bold="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvcandidasate" runat="server"
                            ControlToValidate="Name" ErrorMessage="Name field is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col s8 m4">
                        <label class="control-label" for="Enabled">Enabled (visible for customers)</label>
                        <div class="switch">
                            <label>
                                <input type="checkbox" id="Enabled" name="Enabled" runat="server" />
                                <span class="lever" />
                            </label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6 m6">
                        <label class="col control-label" for="Barcode">Barcode</label>
                        <asp:TextBox ID="Barcode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="input-field col s4 m3">
                        <label style="color: crimson;">*</label>
                        <label class="col control-label" for="QTY">Quantity</label>
                        <input class="form-control" type="number" placeholder="" id="QTY" name="QTY" runat="server" min="0" step="1" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server" ControlToValidate="QTY" ErrorMessage="Quantity is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-field col s6 m4">
                        <label style="color: crimson;">*</label>
                        <label class="col control-label" for="Price">Price after tax</label>
                        <input class="form-control validate" type="number" placeholder="" id="Price" name="Price" runat="server" min="0" step="0.01" onchange="setTwoNumberDecimal" />

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            runat="server" ControlToValidate="Price" ErrorMessage="Please, add price after tax of this product." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-field col s4 m3">
                        <div class="left-align caption">
                            <label style="color: crimson;">*</label>
                            <label class="control-label" for="IdTaxPercentagesBook">Tax percent</label>
                        </div>

                        <asp:DropDownList ID="IdTaxPercentagesBook" runat="server" Width="80px" Font-Size="Medium" DataSourceID="TaxBookDS" DataTextField="Value" DataValueField="Id" CssClass="browser-default input-field m6" DataTextFormatString="{0} %"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ControlToValidate="IdTaxPercentagesBook" ErrorMessage="Please, select TAX percent on this product." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                        <asp:SqlDataSource ID="TaxBookDS" runat="server" ConnectionString="<%$ ConnectionStrings:CMSConnectionString %>" ProviderName="<%$ ConnectionStrings:CMSConnectionString.ProviderName %>" SelectCommand='SELECT "Id", "Value" FROM "TaxPercentagesBook"'></asp:SqlDataSource>

                    </div>

                </div>


            </div>

            <div class="col s4 m4">
                <div class="row" style="display: flex; position: relative; float: left; margin-left: 15px; height: 500px; max-height: 500px; min-width: 250px;">
                    <div class="input-field">

                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>

                        <div class="row">
                            <div class="col s4">
                                <label style="color: crimson;">*</label>
                                <label class="col control-label" for="CategoriesTree">Category</label>
                            </div>
                            <div class="col s4">
                                <button type="button" class="btn btn-secondary" style="min-width: 100px; float: left; margin-left: 30px;">Add new</button>
                            </div>
                        </div>
                        <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:TreeView ID="CategoriesTree" runat="server" ImageSet="Custom"
                                    ExpandImageUrl="~/Content/icons/inferno_expand.png" CollapseImageUrl="~/Content/icons/inferno_collapse.png"
                                    ExpandDepth="0" CssClass="treeView">
                                    <HoverNodeStyle CssClass="hoverNode" />
                                    <NodeStyle CssClass="treeNode"></NodeStyle>
                                    <SelectedNodeStyle CssClass="selectedNode" />
                                    <LeafNodeStyle CssClass="leafNode" />
                                </asp:TreeView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="CategoriesTree" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <div class="card-panel">
            <div class="row card-content s12">

                <div class="row">
                    <div class="col s4">
                        <asp:FileUpload ID="FileUploadControl" runat="server" />
                    </div>
                    <div class="col s4">
                        <asp:LinkButton ID="UploadButton" runat="server" CssClass="btn btn-primary" OnClick="UploadButton_Click" UseSubmitBehavior="False" ValidationGroup="PictureUpload" ValidateRequestMode="Disabled">Upload<i class="material-icons right">file_upload</i></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-action">
                <asp:Label runat="server" ID="StatusLabel" Text="" />
            </div>


            <asp:GridView ID="ImageGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="LocalListId" ViewStateMode="Enabled" OnRowCommand="ImageGrid_RowCommand" OnRowDeleting="ImageGrid_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" />
                    <asp:BoundField DataField="IdProductPicture" HeaderText="Image Id" Visible="false" />
                    <asp:BoundField DataField="FileName" HeaderText="FileName" Visible="False"></asp:BoundField>
                    <asp:TemplateField ControlStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Image ID="Path" runat="server" Visible="true" Height="200px" Width="300px" ImageUrl='<%# Eval("Path") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField Text="Delete" ControlStyle-CssClass="btn" ControlStyle-Width="80%" CommandName="Delete"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="row">
            <label style="color: crimson;">*</label>
            <label class="col control-label" for="Description">Description</label>
            <textarea id="Description" name="Description" rows="2" runat="server"></textarea>
        </div>


         <div class="divider"></div>
        <div class="card" style="margin-top:30px;">
                    <div class="input-field col s12">
                        <asp:LinkButton ID="btnSubmit" CssClass="btn waves-effect waves-light col s12 l12" runat="server" Text="Save" UseSubmitBehavior="true" OnClick="btnSubmit_Click"></asp:LinkButton>
                    </div>
         </div>

    </div>
            <script type="text/javascript">
                function setTwoNumberDecimal(event) {
                    this.value = parseFloat(this.value).toFixed(2);
                }

                $(document).ready(function () {// Initialize your tinyMCE Editor with your preferred options
                    tinymce.init({
                        selector: 'textarea',
                        height: 300,
                        menubar: true,
                        plugins: [
                            'advlist autolink link lists charmap print preview hr anchor pagebreak',
                            'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                            'table emoticons template paste help'
                        ],
                        toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | ' +
                            'bullist numlist outdent indent | ' +
                            'forecolor backcolor emoticons',
                        menu: {
                            favs: { title: 'My Favorites', items: 'code visualaid | searchreplace | emoticons' }
                        },
                        menubar: 'favs file edit view format tools table help',
                        content_css: [
                            '//www.tiny.cloud/css/codepen.min.css'
                        ]
                    });
                });
            </script>
       


    <script type="text/javascript">
        $(document).ready(function () {
            $('.modal').modal();
        });


    </script>
</body>
</html>