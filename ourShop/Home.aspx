<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ourShop._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         <div id="index-banner" class="parallax-container white-text">
    <div class="section no-pad-bot">
      <div class="container">
        <br/><br/>
        <h1 class="header center teal-text text-lighten-2">Rims collection</h1>
        <div class="row center">
          <h5 class="header col s12 light">Modern, solid wheels at special prices</h5>
        </div>
        <div class="row center">
          <a href="/Products?category=Rims" id="download-button" class="btn-large waves-effect waves-light teal lighten-1">Show rims</a>
        </div>
        <br/><br/>

      </div>
    </div>

    <div class="parallax"><img src="../Content/Images/b2.png" alt="Unsplashed background img 1"/></div>

  </div>
    <div class="section white">
        <div class="col s12 row container">
            <div class="col s4 l4 m4">
                <div class="row center">
                    <i class="material-icons large">local_shipping</i>
                </div>
                <div class="row center">
                    <h5>Free delivery</h5>
                    <p>All purchases over $ 50 have free shipping!</p>
                </div>
            </div>
            <div class="col s4 l4 m4 center">
                <i class="material-icons large">savings</i>
                <div class="row center">
                <h5>10%</h5>
                <p>Discount on purchases for an all new Members</p>
                   </div>
            </div>
             <div class="col s4 l4 m4 center">
            <div class="row center">
                <i class="material-icons large">alternate_email</i>
                <div class="row center">
                <h5 class="center">5%</h5>
                <p>Discount on purchases for subscribing to the e-mail newsletter</p>
            </div>
                </div>
                 </div>
        </div>


    </div>
        <div class="parallax-container">
      <div class="parallax"><img src="../Content/Images/b1.png" alt="Unsplashed background img 1"/></div>
    </div>
    <div class="section white">
        <div class="row container">
        <h2 class="header">Reliably</h2>
        <p class="grey-text text-darken-3 lighten-3">Elegant, properly maintained rims are the perfect complement to any car. If you are looking for high-quality aluminum rims or more practical and definitely cheaper steel rims, we encourage you to carefully study the offer of our store. Among the dozens of different models of always eye-catching alloy wheels, there is sure to be a product that you will like. The presented alloy wheels are top-class products, imported from the most famous and respected manufacturers of this type of car accessories. These are products that will emphasize the line of each car, making it more attractive in the eyes of each recipient. For people who are looking for an economical product, we have prepared a wide set of steel rims in the most popular sizes. Enjoy your shopping!</p>
      </div>
    </div>
    <div class="parallax-container">
      <div class="parallax"><img src="../Content/Images/city-blurred-hd.jpg" alt="Unsplashed background img 1"/></div>
    </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('.parallax').parallax();
 });

        </script>

</asp:Content>
