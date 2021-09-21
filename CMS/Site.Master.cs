using CMS.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CMS
{
    public partial class SiteMaster : MasterPage
    {
       private Toolbar _toolbar = new Toolbar();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    ControlLogin();

                    _toolbar.CreateMenu(SessionProperties.GetUserId(Session), Panel1, Toolbar.ToolbarType.Navbar);
                    _toolbar.CreateMenu(SessionProperties.GetUserId(Session), Panel1, Toolbar.ToolbarType.Sidenav);
                }
            }
            catch(Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
            }
        }
        

        public void ControlLogin()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    string firstName = SessionProperties.GetSessionString(Session, "FirstName");
                    string lastName = SessionProperties.GetSessionString(Session, "LastName");
                    string initials = SessionProperties.GetSessionString(Session, "Name").Substring(0,1);

                    if (firstName.Length > 0 && lastName.Length > 0)
                        initials = firstName.Substring(0, 1) + lastName.Substring(0, 1);

                    LoggedCard.Visible = true;
                    UnLoggedCard.Visible = false;

                    NickLabel.Text = SessionProperties.GetSessionString(Session, "Name");
                    NameLabel.Text = firstName + " " + lastName;
                    PositionLabel.Text = SessionProperties.GetSessionString(Session, "Position");
                    
                    LoggedButton.Text = SessionProperties.GetSessionString(Session, "FirstName").Substring(0, 1) + SessionProperties.GetSessionString(Session, "LastName").Substring(0, 1);
                    LoggedButton.Visible = true;
                    LoginButton.Visible = false;
                }
                else
                {
                    LoggedCard.Visible = false;
                    UnLoggedCard.Visible = true;
                    LoginButton.Visible = true;
                    LoggedButton.Visible = false;

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