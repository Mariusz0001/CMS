<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ourShop._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         <div id="index-banner" class="parallax-container white-text">
    <div class="section no-pad-bot">
      <div class="container">
        <br/><br/>
        <h1 class="header center teal-text text-lighten-2">Parallax Template</h1>
        <div class="row center">
          <h5 class="header col s12 light">A modern responsive front-end framework based on Material Design</h5>
        </div>
        <div class="row center">
          <a href="http://materializecss.com/getting-started.html" id="download-button" class="btn-large waves-effect waves-light teal lighten-1">Get Started</a>
        </div>
        <br/><br/>

      </div>
    </div>

    <div class="parallax"><img src="../Content/Images/home2.png" alt="Unsplashed background img 1"/></div>

  </div>
        <div class="section white">
      <div class="row container">
        <h2 class="header">Parallax</h2>
        <p class="grey-text text-darken-3 lighten-3">Parallax is an effect where the background content or image in this case, is moved at a different speed than the foreground content while scrolling.</p>
      </div>
    </div>
        <div class="parallax-container">
      <div class="parallax"><img src="../Content/Images/home1.png" alt="Unsplashed background img 1"/></div>
    </div>
    <div class="section white">
      <div class="row container">
        <h2 class="header">Parallax</h2>
        <p class="grey-text text-darken-3 lighten-3">Parallax is an effect where the background content or image in this case, is moved at a different speed than the foreground content while scrolling.</p>
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
