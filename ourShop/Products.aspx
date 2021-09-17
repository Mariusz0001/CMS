<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ourShop.Products"%>
<%@ Register TagPrefix="UC" TagName="ProductCardUC" Src="~/Modules/UC/ProductCardUC.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
                                
            <div class="panel panel-default">
                <div class="panel-body border-bottom">
                     <div class="container_margin">
                        <div class="section">
                          <div class="row">
                            <div class="col s12 center">
                              <h3><i class="mdi-content-send brown-text"></i></h3>
                                <asp:PlaceHolder ID="UserControlHolder" runat="server" ></asp:PlaceHolder>       
                          </div>
                        </div>
                      </div>
                </div>
            </div>
             </div>

    <script type="text/javascript">
     /*   window.onload = load;

        function load(s,e)
        {
            try {
                var id = getParameterByName("category");
                alert(id);
            }
            catch (e) {
                alert("initForm" + e);
            }
        }*/
    </script>
</asp:Content>

