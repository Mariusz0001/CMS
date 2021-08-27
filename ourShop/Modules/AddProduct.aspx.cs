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
        public override bool LoginRequired
        {
            get
            {
                return true;
            }
        }
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
                  //  RegisterInitialProduct();
                }
            }
        }
        public void RegisterInitialProduct()
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
        public void LoadCategoriesBook()
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
        public void PopulateCategoriesTreeView(List<ourShop.DataBase.CategoriesBook> categoriesList, int parentId, TreeNode treeNode)
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
            /*
             * to do
             * Pokazywanie zwracanych wartości np toast, czy alert
             * */

            try
            {
                int id = -1;

                if (IdFromURL != null)
                    id = IdFromURL.Value;


                if (Price.Value != null && TaxPercent.SelectedItem?.Value != null && Quantity.Value != null && CategoriesTree.SelectedNode?.Value != null && SessionProperties.GetUserId(Session) != null)
                {
                    var ret = DbStoredProcedure.Instance().SetProduct(id, Name.Text, Enabled.Checked, Barcode.Text, double.Parse(Price.Value), int.Parse(TaxPercent.SelectedItem.Value), int.Parse(Quantity.Value), int.Parse(CategoriesTree.SelectedNode.Value), Descritpion.InnerText, SessionProperties.GetUserId(this.Session).Value);
                    
                    if(ret != null && ret.Id > 0)
                        Response.Redirect("/Modules/AddProduct?id=" + ret.Id);

                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "AddProduct.btnSubmit_Click", Utils.GetExceptionMessage(ex));
                throw ex;
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.ForeColor = Color.OrangeRed;

                if (FileUploadControl.HasFile)
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg" || FileUploadControl.PostedFile.ContentType == "image/jpg" || FileUploadControl.PostedFile.ContentType == "image/png" || FileUploadControl.PostedFile.ContentType == "image/gif" || FileUploadControl.PostedFile.ContentType == "image/bmp")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 20002400)
                        {
                            string tempPath = DbFunction.Instance().GetStringSettings("Temp_Path");

                            if (tempPath != null && tempPath.Length > 0)
                            {
                                if (!Directory.Exists(tempPath))
                                {
                                    Directory.CreateDirectory(tempPath);
                                }
                            }
                            else
                            {
                                StatusLabel.Text = "Please specifiy Temp_Path in website configuration.";
                                return;
                            }

                            string filename = Path.GetFileName(DateTime.Now.ToShortTimeString().Replace(":", "_").Replace(".", "_") + Utils.Get8Digits() + FileUploadControl.PostedFile.FileName);

                            FileUploadControl.SaveAs(tempPath + "/" + filename);


                            if (Session["ProductPictureUploadList"] != null)
                            {
                                List<string> productList = (List<string>)Session["ProductPictureUploadList"];
                                productList.Add(tempPath + "/" + filename);
                            }
                            else
                            {
                                List<string> productList = new List<string>();
                                productList.Add(tempPath + "/" + filename);
                                HttpContext.Current.Session.Add("ProductPictureUploadList", productList);
                            }

                            StatusLabel.Text = "File uploaded successfully!";
                            StatusLabel.ForeColor = Color.Green;
                        }
                    }
                    else
                    {
                        StatusLabel.Text = "The file has to be less than 20 Mb!";
                    }
                }
                else
                {
                    StatusLabel.Text = "Only JPG, JPEG, PNG, BMP, GIF files are accepted!";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }
}
 