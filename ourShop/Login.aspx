<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ourShop.Modules.Login" %>
<%@ Register TagPrefix="UC" TagName="LoginUC" Src="~/Modules/UC/LoginUC.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_margin center" style="max-width: 400px; margin-top:90px;">
        <div class="z-depth-1 grey lighten-4">
            <UC:LoginUC ID="LoginUC1" runat="server" />
        </div>
    </div>
</asp:Content>
