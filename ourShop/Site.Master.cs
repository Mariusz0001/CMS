using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillLoginCard();
            CreateNavbarMenu();

        }
        private void CreateNavbarMenu()
        {
            HtmlGenericControl main = UList("MainMenu", "navbar");

            main.Controls.Add(CreateMenuHTML("TechnologyName", "TechnologyURL"));
            main.Controls.Add(CreateMenuParentHTML("TechnologyName2", "TechnologyURL2", "ProductDropDown2", "inventory"));

            HtmlGenericControl ulDropDown = new HtmlGenericControl("ul");
            ulDropDown.Attributes["id"] = "ProductDropDown2";
            ulDropDown.Attributes["class"] = "dropdown-content collection";
            //  foreach (DataRow r in dtDist.Rows)
            {
                ulDropDown.Controls.Add(CreateChildMenuHTML("drop", "drop"));
                ulDropDown.Controls.Add(CreateChildMenuHTML("drop2", "drop2"));
            }


        
            Panel1.Controls.Add(main);
            Panel1.Controls.Add(ulDropDown);
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

        private HtmlGenericControl UList(string id, string cssClass)
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.ID = id;
            ul.Attributes.Add("class", cssClass);
            return ul;
        }
        private HtmlGenericControl CreateMenuHTML(string text, string url)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a href=" + string.Format("http://{0}", url) + ">" + text + "</a>";
            return li;
        }
        private HtmlGenericControl CreateMenuParentHTML(string text,  string url, string dropDownId, string iconName)
        {
            //<li><a class="dropdown-trigger" href="#!" data-target="ProductDropDown">Products<i class="material-icons right">inventory arrow_drop_down</i></a></li>

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a class='dropdown-trigger' href='#!' data-target='"+ dropDownId + "' href=" + string.Format("http://{0}", url) + ">" 
                + text +
               "<i class='material-icons right'>" + iconName + " arrow_drop_down</i></a>";
            return li;
        }
        private HtmlGenericControl CreateChildMenuHTML(string text, string url)
        {
            //<li><a href="/Modules/AddProduct"><span>Add</span><i class="material-icons">add</i></a></li>

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a href=" + string.Format("http://{0}", url) + ">" + text + "</a>";
            return li;
        }
    }
}