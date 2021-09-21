<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUC.ascx.cs" Inherits="CMS.Modules.UC.ContactUC" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <blockquote>
        If you have any questions please contact with me by:
                    <button id="Mail" class="btn btn-link" onclick="Mail_OnClick()" data-toggle="tooltip" data-placement="top" title="mariusz.pawlus95@gmail.com">
                        <img src="../Content/icons/013-mail.png" width="20" />
                        E-mail
                    </button>

        <button data-target="PhoneModal" class="btn modal-trigger">
            <img src="../Content/icons/077-mobile.png" width="20" />Phone</button>

    </blockquote>
    <div id="PhoneModal" class="modal">
        <div class="modal-content">
            <h5>Contact by phone</h5>
            <br />
            <p class="text-lighten-1">Mariusz Pawlus</p>
            <p class="text-lighten-2">+48 669 020 193</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">Ok</a>
        </div>
    </div>

    <script type="text/javascript">
        function Mail_OnClick() {
            window.location.href = "mailto:mariusz.pawlus95@gmail.com";
        };

        $(document).ready(function () {
            $('.modal').modal();
        });
    </script>
</body>
</html>