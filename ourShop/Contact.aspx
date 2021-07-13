<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ourShop.Contact"%>
<%@ Register TagPrefix="UC" TagName="ContactUC" Src="~/Modules/UC/ContactUC.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
            <div class="panel panel-default">
                <div class="panel-body border-bottom">
                     <div class="container_margin">
                        <div class="section">

                          <div class="row">
                            <div class="col s12 center">
                              <h3><i class="mdi-content-send brown-text"></i></h3>
                              <h4>Contact Us</h4>
                              <p class="left-align light">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam scelerisque id nunc nec volutpat. Etiam pellentesque tristique arcu, non consequat magna fermentum ac. Cras ut ultricies eros. Maecenas eros justo, ullamcorper a sapien id, viverra ultrices eros. Morbi sem neque, posuere et pretium eget, bibendum sollicitudin lacus. Aliquam eleifend sollicitudin diam, eu mattis nisl maximus sed. Nulla imperdiet semper molestie. Morbi massa odio, condimentum sed ipsum ac, gravida ultrices erat. Nullam eget dignissim mauris, non tristique erat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;</p>
                            </div>
                          </div>

                        </div>
                         
                 <div>
                    <UC:ContactUC ID="ContactUC1" runat="server" />
                </div>
                      </div>
                </div>
            </div>
</asp:Content>

