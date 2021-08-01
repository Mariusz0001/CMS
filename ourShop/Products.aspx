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
                                               <%--                      <div class="col m4">
                                    <div class="card">
                                      <div class="card-image">
                                        <img src="http://www.ilikewallpaper.net/ipad-wallpapers/download/2268/Square-Pattern-ipad-wallpaper-ilikewallpaper_com.jpg">
                                        <span class="card-title" style="width:100%; background: rgba(0, 0, 0, 0.5);">Sample2</span>
                                      </div>
                                      <div class="card-content">
                                        <p>I am a very simple card. I am good at containing small bits of information. I am convenient because I require little markup to use effectively.</p>
                                      </div>
                                      <div class="card-action">
                                        <a href="#">This is a link</a>
                                      </div>
                                    </div>
                                  </div>
                                </div>--%>
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

