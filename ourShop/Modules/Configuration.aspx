<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="ourShop.Modules.Configuration" %>
<%@ Register TagPrefix="UC" TagName="ContactUC" Src="~/Modules/UC/ContactUC.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

            <div class="container">
                <div class="row">
                    <div class="col-sm-8 col-sm-offset-2">
                        <div class="page-header">
                            <div class="alert alert-info" role="alert">
                                <h2>This is a configuration of your website.</h2>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Main configuration</h4>
                                </div>
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
                <div>
                    <UC:ContactUC ID="ContactUC1" runat="server" />
                </div>

            </div>
            <script type="text/javascript">
                function Mail_OnClick() {
                    window.location.href = "mailto:mariusz.pawlus95@gmail.com";
                };

            </script>
</asp:Content>
