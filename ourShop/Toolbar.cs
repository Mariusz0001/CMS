using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ourShop
{
    public class Toolbar
    {
        public enum ToolbarType
        {
            Navbar = 1,
            Toolbar = 2,
            Sidenav = 3,
        }

        public Toolbar()
        {

        }

        public void CreateMenu(int? userId, System.Web.UI.WebControls.Panel panel, ToolbarType type)
        {
            try
            {

                switch (type)
                {
                    case ToolbarType.Navbar:
                        {
                            HtmlGenericControl main = UList("MainMenu", "navbar");

                            GenerateToolbarList(userId, panel, main, type);
                            break;
                        }
                    case ToolbarType.Sidenav:
                        {
                            HtmlGenericControl mainMenu = UList("MobileMenu", "sidenav");

                            GenerateToolbarList(userId, panel, mainMenu, type);

                            mainMenu.Controls.Add(AddSidenavSeparator());
                            mainMenu.Controls.Add(AddSidenavSubfooter("All about us"));

                            mainMenu.Controls.Add(CreateMenuHTML("About", "/About", "group_work"));
                            mainMenu.Controls.Add(CreateMenuHTML("Contact", "/Contact", "contact_page"));

                            break;
                        }
                }
             
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
            }
        }
       
        public void GenerateToolbarList(int? userId, System.Web.UI.WebControls.Panel panel, HtmlGenericControl UList, ToolbarType type)
        {
            try
            {
                var result = DbFunction.Instance().GetMenuToolbar(userId);

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if (item?.ToolbarName.Length > 0)
                        {
                            var childs = result.Where(f => f.IdParentToolbar == item.ToolbarId);

                            if (item.IdParentToolbar == null)
                            {
                                if (childs.Count() > 0)
                                {
                                    string toolbarId = "Toolbar" + type.ToString() + item.ToolbarId;

                                    UList.Controls.Add(CreateMenuParentHTML(item.ToolbarName, toolbarId, item.IconName));

                                    HtmlGenericControl ulDropDown = new HtmlGenericControl("ul");
                                    
                                    ulDropDown.Attributes["id"] = toolbarId;
                                    ulDropDown.Attributes["class"] = "dropdown-content collection";

                                    foreach (var child in childs)
                                    {
                                        ulDropDown.Controls.Add(CreateChildMenuHTML(child.ToolbarName, child.URL, child.IconName));
                                        panel.Controls.Add(ulDropDown);
                                    }
                                }
                                else
                                {
                                    UList.Controls.Add(CreateMenuHTML(item.ToolbarName, item.URL, item.IconName));
                                }
                            }
                        }
                    }
                    panel.Controls.Add(UList);
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
            }
        }
        private HtmlGenericControl UList(string id, string cssClass)
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.ID = id;
            ul.Attributes.Add("class", cssClass);
            return ul;
        }
        private HtmlGenericControl CreateMenuHTML(string text, string url, string iconName)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a class='waves-effect' href='" + string.Format("{0}", url) + "'>" + text +
                 "<i class='material-icons right'>" + iconName + "</i></a>";
            return li;
        }
        private HtmlGenericControl CreateMenuParentHTML(string text, string dropDownId, string iconName)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");

            li.InnerHtml = "<a class='dropdown-trigger waves-effect' data-target='" + dropDownId + "'>"
                + text +
             "<i class='material-icons right'>" + iconName + " arrow_drop_down</i></a>";
            return li;
        }
        private HtmlGenericControl CreateChildMenuHTML(string text, string url, string iconName)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a href='" + string.Format("{0}", url) + "'>" + text +
               "<i class='material-icons right'>" + iconName + "</i></a>";
            return li;
        }
        private HtmlGenericControl AddSidenavSeparator()
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<div class='divider'></div>";
            return li;
        }
        private HtmlGenericControl AddSidenavSubfooter(string text)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = "<a class='subheader'>" + text +"</a>";
            return li;
        }
    }
}