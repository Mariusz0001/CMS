<%@ Page Title="ourShop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ourShop.Modules.AddProduct"  ValidateRequest="false"%>



<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">

    <body class="bcg-img">
        <link href="../Content/Controls/tinymce/tinymce.css" rel="stylesheet" />
        <script src="../Content/Controls/tinymce/tinymce.js"></script>

        <script src="../Scripts/utils.js"></script>

        <form id="mainForm" class="form-group" runat="server">

            <div class="panel panel-default">
                <div class="panel-body border-bottom">
                    <h2>Add product</h2>
                </div>

            </div>

             <div class="input-field col s6">
            <select data-id="26" id="MySelct" serachname="myselectsearch" searchable="your placeholder for seach box">
              <option value="some1">Some One</option>
              <option value="SeamSeam">SeamSeam</option>
              <option value="toott">Toott</option>
              <option value="Flat">Flat</option>
            </select>
            <label for="roof_type">My Select</label>
</div>
            <div class="form-row">
                <div class="col-sm-4">
                    <label style="color: crimson;">*</label>
                    <label class="col-sm-4 control-label" for="Name">Name</label>
                    <asp:TextBox ID="Name" runat="server" CssClass="form-control" Font-Size="Larger" Height="40px" Font-Bold="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvcandidasate" runat="server"
                        ControlToValidate="Name" ErrorMessage="Name field is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-4">
                    <label class="control-label" for="Enabled">Enabled</label>
                    <asp:CheckBox ID="Enabled" runat="server" CssClass="form-check" Checked="true" />
                </div>
                <div class="col-sm-4">
                </div>
            </div>

            <div class="form-row">
                <div class="col-sm-4">
                    <label class="col-sm-4 control-label" for="Barcode">Barcode</label>
                    <asp:TextBox ID="Barcode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <label style="color: crimson;">*</label>
                    <label class="col-sm-10 control-label" for="Price">Price after tax</label>
                    <input class="form-control" type="number" placeholder="" id="Price" name="Price" runat="server" min="0" step="0.01" onchange="setTwoNumberDecimal" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ControlToValidate="Price" ErrorMessage="Please, add price after tax of this product." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2">

                    <label style="color: crimson;">*</label>
                    <label class="col-sm-10 control-label" for="TaxPercent">Tax percent</label>
                    <asp:DropDownList ID="TaxPercent" runat="server" Width="80px" DataSourceID="TaxBookDS" DataTextField="Value" DataValueField="Id" CssClass="form-control" DataTextFormatString="{0} %"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="TaxPercent" ErrorMessage="Please, select TAX percent on this product." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="TaxBookDS" runat="server" ConnectionString="<%$ ConnectionStrings:ourShopConnectionString %>" ProviderName="<%$ ConnectionStrings:ourShopConnectionString.ProviderName %>" SelectCommand='SELECT "Id", "Value" FROM "TaxPercentagesBook"'></asp:SqlDataSource>

                </div>
            </div>
            <div class="form-row">

                <div class="col-sm-4">
                    <label style="color: crimson;">*</label>
                    <label class="col-sm-10 control-label" for="Quantity">Quantity</label>
                    <input class="form-control" type="number" placeholder="" id="Quantity" name="Quantity" runat="server" min="0" step="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="Quantity" ErrorMessage="Quantity is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2">


                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>


                    <label style="color: crimson;">*</label><label class="col-sm-10 control-label" for="CategoriesTree">Category</label>
                    <button type="button" class="btn btn-secondary" style="min-width: 100px;">Add new</button>
                    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TreeView ID="CategoriesTree" runat="server" ImageSet="Custom" ExpandImageUrl="../Content/icons/inferno_expand.png" CollapseImageUrl="../Content/icons/inferno_collapse.png" ExpandDepth="0" CssClass="treeView">
                                <HoverNodeStyle CssClass="hoverNode" />
                                <NodeStyle CssClass="treeNode"></NodeStyle>
                                <SelectedNodeStyle CssClass="selectedNode" />
                                <LeafNodeStyle CssClass="leafNode" />
                            </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>
            <div class="form-row">
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#fileUploadModal">
                </button>
                <asp:Button runat="server" ID="Button1" class="btn btn-light" Text="Upload" CausesValidation="True" OnClick="UploadButton_Click" ValidationGroup="PictureUpload" UseSubmitBehavior="False" />

            </div>
            <asp:GridView ID="ImageGrid" runat="server">
                <Columns>
                    <asp:BoundField></asp:BoundField>
                </Columns>
                <Columns>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="ImageGridDS" runat="server" ConnectionString="<%$ ConnectionStrings:ourShopConnectionString %>" ProviderName="<%$ ConnectionStrings:ourShopConnectionString.ProviderName %>" SelectCommand='SELECT "Id", "Value" FROM "TaxPercentagesBook"'></asp:SqlDataSource>

            <div class="row">
                <label style="color: crimson;">*</label>
                <label class="col-sm-10 control-label" for="Descritpion">Description</label>
                <textarea id="Descritpion" name="Descritpion" cols="30" rows="2" runat="server"></textarea>
            </div>


            <div class="" style="margin-top: 20px;">
                <div class="">
                    <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" UseSubmitBehavior="true" OnClick="btnSubmit_Click"></asp:Button>
                </div>
            </div>

            <div class="modal fade" id="fileUploadModal" data-controls-modal="fileUploadModal" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="fileUploadModalLabel">Picture uploading</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">

                            <div class="col-sm-4">
                                <label class="col-sm-4 control-label" for="Sort">SortNumber</label>
                                <asp:TextBox ID="Sort" runat="server" CssClass="form-control" ValidationGroup="PictureUpload"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server" ControlToValidate="Sort" ErrorMessage="Please, select sort on this picture." CssClass="invalid align-items-baseline" ValidationGroup="PictureUpload"></asp:RequiredFieldValidator>
                            </div>
                            <div class="row">
                                <asp:FileUpload ID="FileUploadControl" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ControlToValidate="FileUploadControl" ErrorMessage="Please, select sort on this picture." CssClass="invalid align-items-baseline" ValidationGroup="PictureUpload"></asp:RequiredFieldValidator>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="StatusLabel" Text="Upload status: " />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button runat="server" type="button" class="btn btn-primary" onclick="UploadButton_Click">Save changes</button>
                            <asp:Button runat="server" ID="Upload" class="btn btn-primary" Text="Upload" CausesValidation="True" OnClick="UploadButton_Click" ValidationGroup="PictureUpload" UseSubmitBehavior="False" />
                        </div>
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
                        width: 1000,
                        height: 300,
                        menubar: true,
                        plugins: [
                            'advlist autolink link lists charmap print preview hr anchor pagebreak',
                            'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                            'table emoticons template paste help'
                        ],
                        toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | ' +
                            'bullist numlist outdent indent | ' +
                            'forecolor backcolor emoticons | help',
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


        </form>
    </body>
</asp:Content>
    

