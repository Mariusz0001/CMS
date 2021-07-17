using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {

        private Toolbar _toolbar = new Toolbar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillLoginCard();
                _toolbar.CreateMenu(SessionProperties.GetUserId(Session), Panel1, Toolbar.ToolbarType.Sidenav);
            }
        }

        public void FillLoginCard()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    LoggedCard.Visible = true;
                    UnLoggedCard.Visible = false;

                    NickLabel.Text = SessionProperties.GetSessionString(Session, "Name");
                    NameLabel.Text = SessionProperties.GetSessionString(Session, "FirstName") + " " + SessionProperties.GetSessionString(Session, "LastName");
                    PositionLabel.Text = SessionProperties.GetSessionString(Session, "Position");
                }
                else
                {
                    LoggedCard.Visible = false;
                    UnLoggedCard.Visible = true;

                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
            }
        }

        protected void LogoutButton_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Home.aspx");
        }
    }
}