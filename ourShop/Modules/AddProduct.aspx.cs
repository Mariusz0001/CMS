using Npgsql;
using NpgsqlTypes;
using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop.Modules
{
    public partial class AddProduct : MainPage
    {
        public override int IdModule
        {
            get
            {
                return 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadCategoriesBook();

                if (IdFromURL > 0)
                {
                   /* to do
                    * pobrać z dbo kontrolera i ustawić w kontrolkach 
                    * */
                }
                else
                {
                    /* to do
                    * użyć dbo kontrolera
                    * */
                    RegisterInitialProduct();
                }
            }
        }
        private void RegisterInitialProduct()
        {
            if (IdFromURL == null)
            {
                using (var dbo = new ourShopEntities())
                {

                    var _id = new NpgsqlParameter("_id", NpgsqlDbType.Integer);
                    _id.Direction = System.Data.ParameterDirection.InputOutput;
                    _id.Value = -1;

                    var ret = dbo.Database.ExecuteSqlCommand("call public.add_initial_product(@_id);", _id);

                    int? new_id = null;
                    try
                    {
                        new_id = int.Parse(_id.NpgsqlValue.ToString());
                    }
                    catch
                    {

                    }

                    if (new_id != null && new_id > 0)
                    {
                        Response.Redirect("/Modules/AddProduct?id=" + new_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "Can not create initial product. Check connection with DB, table Product, procedure add_initial_product()", true);
                        Response.Redirect("/Home");
                    }
                }
            }
        }
        private void LoadCategoriesBook()
        {
            using (var dbo = new ourShopEntities())
            {
                var categories = dbo.CategoriesBook
                           .Where(s => s.Enabled == true && (s.IdCategoriesBook_Parent == null || s.IdCategoriesBook_Parent == 0));
                if (categories != null)
                {
                    var categoriesList = categories.ToList();
                    this.PopulateCategoriesTreeView(categoriesList, 0, null);
                }
            }
        }
        private void PopulateCategoriesTreeView(List<ourShop.DataBase.CategoriesBook> categoriesList, int parentId, TreeNode treeNode)
        {
            try
            {
                foreach (var item in categoriesList)
                {
                    TreeNode child = new TreeNode
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    };

                    //child.SelectAction = TreeNodeSelectAction.Select;


                    if (parentId == 0)
                    {
                        using (var dbo = new ourShopEntities())
                        {
                            CategoriesTree.Nodes.Add(child);
                            var newChild = dbo.CategoriesBook
                                   .Where(s => s.Enabled == true && s.IdCategoriesBook_Parent == item.Id);

                            if (newChild != null)
                            {
                                PopulateCategoriesTreeView(newChild.ToList(), int.Parse(child.Value), child);
                            }
                        }
                    }
                    else
                    {
                        treeNode.ChildNodes.Add(child);
                    }
                }
            }
            catch 
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (var dbo = new ourShopEntities())
            {
                dbo.Database.ExecuteSqlCommand("call public.add_product({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});",
                    IdFromURL.Value,
                    Name.Text,
                    Enabled.Checked,
                    Barcode.Text,
                    float.Parse(Price.Value),
                    int.Parse(TaxPercent.SelectedItem.Value),
                    int.Parse(Quantity.Value),
                    int.Parse(CategoriesTree.SelectedNode.Value),
                    Descritpion.InnerText);
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.ForeColor = Color.OrangeRed;

                if (FileUploadControl.HasFile)
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 20002400)
                        {
                            using (var dbo = new ourShopEntities())
                            {
                                var picPath = dbo.Settings
                                            .Where(s => s.Name == "AttachedPicturesProducts_Path")
                                            .FirstOrDefault();

                                if (picPath != null && picPath.ValueString.Length > 0)
                                {
                                    if (!Directory.Exists(picPath.ValueString))
                                    {
                                        Directory.CreateDirectory(picPath.ValueString);
                                    }

                                    string filename = Path.GetFileName(DateTime.Now.ToShortTimeString().Replace(":", "_").Replace(".", "_") + FileUploadControl.PostedFile.FileName);
                                    FileUploadControl.SaveAs(picPath.ValueString + "/" + filename);

                                    StatusLabel.Text = "Upload status: File uploaded successfully!";
                                    StatusLabel.ForeColor = Color.Green;

                                }
                                else
                                {
                                    StatusLabel.Text = "Upload status: AttachedPicturesProducts_Path not set! Please, set it in 'Configuration' module.";
                                }
                            }
                        }
                        else
                        {
                            StatusLabel.Text = "Upload status: The file has to be less than 20 Mb!";
                        }
                    }
                    else
                    {
                        StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }

        }
    }
}
 