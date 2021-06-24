<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginUC.ascx.cs" Inherits="ourShop.Modules.UC.LoginUC" %>

<body>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
    <div class="container">
        <div class="col s12 row">
            <div class="row">
                <br>
                <h5 class="center">Please, login into your account</h5>
            </div>
            <div class="row">
                <div class="input-field col s12">
                    <i class="material-icons prefix">person</i>
                    <label class="col control-label" for="Name">Name</label>
                    <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="UserName" ErrorMessage="Name is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s12">
                    <i class="material-icons prefix">lock_outline</i>
                    <label class="col control-label" for="Password">Password</label>
                    <input id="Password" class="form-control" type="password" runat="server" >
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="Password" ErrorMessage="Password is required." CssClass="invalid align-items-baseline"></asp:RequiredFieldValidator>
                </div>
            </div>
                <div id="progressBar" runat="server" class="progress">
                      <div class="indeterminate"></div>
                  </div>
                <div class="row">
                    <asp:Label ID="ResultLabel" runat="server" Text="" CssClass="red-text"></asp:Label>
                </div>
            <div class="row ">
                <div class="input-field col s12">
                    <asp:LinkButton ID="btnLogin" CssClass="btn waves-effect waves-light col s12 l12" runat="server" Text="Login" UseSubmitBehavior="true" OnClick="btnLogin_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <p class="margin medium-small"><a href="#">Forgot password?</a></p>
                </div>
            </div>
        </div>
    </div>
        </asp:Panel>
        <script type="text/javascript">
            $(".progress").hide(100);

            function OnSubmit(s, e) {
                $(".progress").show(100);
                $("#ResultLabel").fadeOut();
                $("#btnLogin").fadeOut();
            };
        </script>
</body>