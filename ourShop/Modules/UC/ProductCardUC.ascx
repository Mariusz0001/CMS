<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCardUC.ascx.cs" Inherits="ourShop.Modules.UC.ProductCardUC" %>

<body>
    <div class="col m4">
        <div class="card productCard waves-effect">
            <div class="card-image img-hover-zoom">
                <asp:Image ID="ProductCardImage" runat="server" />
                
            </div>
            <div class="card-content">
                <asp:Label ID="ProductCardPrice" runat="server" CssClass="card-title" Font-Size="X-Large"></asp:Label>
                <asp:Label ID="ProductCardTitle" runat="server" CssClass="card-title" Font-Size="Larger"></asp:Label>
                <asp:Label ID="ProductCardLabel" runat="server" ></asp:Label>
            </div>
        </div>
    </div>
</body>