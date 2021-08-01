<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCardUC.ascx.cs" Inherits="ourShop.Modules.UC.ProductCardUC" %>

<body>
    <div class="col m4">
        <div class="card">
            <div class="card-image">
                <asp:Image ID="ProductCardImage" runat="server" />
            </div>
            <div class="card-content">
                <asp:Label ID="ProductCardPrice" runat="server" Text="" CssClass="card-title" Font-Size="Smaller"></asp:Label>
                <asp:Label ID="ProductCardTitle" runat="server" Text="" CssClass="card-title" Font-Size="Larger"></asp:Label>
                <asp:Label ID="ProductCardLabel" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</body>