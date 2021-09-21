<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="CMS.Modules.Configuration" %>
<%@ Register TagPrefix="UC" TagName="ContactUC" Src="~/Modules/UC/ContactUC.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container_margin">
        <div class="row">
            <div class="section">
                <h4>Manage website</h4>
                <p>
                    This module was created to ensure the possibility of managing the website from the interface level.<br />
                    Here you can change the page settings like page name, title, configuration settings etc.<br />
                    Also you can add new your products or categories there.<br />
                    Please be careful as these changes affect your entire site.
                </p>
                <div>
                    <UC:ContactUC ID="ContactUC2" runat="server" />
                </div>
            </div>


            <div class="card" style="min-height: 600px; padding: 10px;">
                <div class="card-tabs">
                    <div class="col s12">
                        <ul class="tabs tabs-fixed-width">
                            <li class="tab col s2"><a class="active" href="#products">Products</a></li>
                            <li class="tab col s2"><a href="#categories">Categories</a></li>
                            <li class="tab col s2"><a href="#about">About site</a></li>
                            <li class="tab col s2"><a href="#contact">Contact site</a></li>
                            <li class="tab col s2"><a href="#configuration">Web configuration</a></li>
                        </ul>
                    </div>

                    <div class="card-content grey lighten-4">
                        <div id="products" class="col s12">
                            <div style="width: 100%; height: 500px; overflow: scroll" >
                            <asp:GridView ID="ProductGrid" runat="server" AutoGenerateColumns="false" Width="100%" Height="500px" DataKeyNames="Id" ViewStateMode="Enabled" AllowSorting="True" AllowPaging="False" ShowFooter="False">
                                <Columns>
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
                        <div id="categories" class="col s12">

                            <asp:GridView ID="CategoriesGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="Id" ViewStateMode="Enabled">
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                </Columns>
                            </asp:GridView>

                        </div>
                        <div id="about" class="col s12">

                        </div>
                        <div id="contact" class="col s12">

                        </div>

                        <div id="configuration" class="col s12">
                            <div class="panel-body">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label" for="PageTitle">Page title</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="PageTitle" runat="server" CssClass="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvcandidate"
                                            runat="server" ControlToValidate="PageTitle" ErrorMessage="Please write PageTitile" CssClass="invalid"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-9 col-sm-offset-4">
                                <asp:Button ID="btnSubmit" Style="float: right; margin-top: 10px; margin-right: 10px;" class="btn btn-primary" runat="server" Text="Submit" UseSubmitBehavior="true" OnClick="btnSubmit_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('ul.tabs').tabs({
            swipeable: false,
            responsiveThreshold: Infinity,
            fixedTabWidth: true
        });

        function Mail_OnClick() {
            window.location.href = "mailto:mariusz.pawlus95@gmail.com";
        };

    </script>
</asp:Content>
