using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillLoginCard();
        }
        private void FillLoginCard()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    LoggedCard.Visible = true;
                    UnLoggedCard.Visible = false;

                    NickLabel.Text = GetSessionString("Name");
                    NameLabel.Text = GetSessionString("FirstName") + " " + GetSessionString("LastName");
                    PositionLabel.Text = GetSessionString("Position");
                }
                else
                {
                    LoggedCard.Visible = false;
                    UnLoggedCard.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string GetSessionString(String sessionVariableName)
        {
            try
            {
                if (Session[sessionVariableName] != null)
                    return Session[sessionVariableName].ToString();
            }
            catch
            {
            }
            return String.Empty;
        }
        protected void LogoutButton_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Home.aspx");
        }
    }
}