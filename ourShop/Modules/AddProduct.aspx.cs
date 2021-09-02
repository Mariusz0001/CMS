using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
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
                   var ret = DbFunction.Instance().GetProduct(IdFromURL.Value, SessionProperties.GetUserId(Session).Value);

                   BindPoperties(ret);

                }
                BindPictureList();
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
            try
            {
                int id = -1;

                if (IdFromURL != null)
                    id = IdFromURL.Value;

                if (Price.Value != null && TaxPercent.SelectedItem?.Value != null && QTY.Value != null && CategoriesTree.SelectedNode?.Value != null && SessionProperties.GetUserId(Session) != null)
                {
                    var ret = DbStoredProcedure.Instance().SetProduct(id, Name.Text, Enabled.Checked, Barcode.Text, double.Parse(Price.Value), int.Parse(TaxPercent.SelectedItem.Value), int.Parse(QTY.Value), int.Parse(CategoriesTree.SelectedNode.Value), Descritpion.InnerText, SessionProperties.GetUserId(this.Session).Value);

                    if (ret != null && ret.Id > 0)
                    {
                        SavePictures(ret.Id.Value);
                        Response.Redirect("/Modules/AddProduct?id=" + ret.Id);
                    }
                }
                else
                {
                    ShowToastMessage("Please check if you selected tax and category.");
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "AddProduct.btnSubmit_Click", Utils.GetExceptionMessage(ex));
                ShowToastMessage("Error occured. " + Utils.GetExceptionMessage(ex));
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
                            string path = this.GetFileDirectoryPath("Temp_Path");
                            string tempPath = GetLocalPhysicalDirectoryPath(path);

                            string filename = Path.GetFileName(DateTime.Now.ToShortTimeString().Replace(":", "_").Replace(".", "_") + Path.GetFileNameWithoutExtension(FileUploadControl.PostedFile.FileName) + Utils.Get8Digits() + Path.GetExtension(FileUploadControl.PostedFile.FileName));

                            FileUploadControl.SaveAs(tempPath + "\\" + filename);

                            var productList = GetProductPictureList();
                            productList.Add(new Beens.ProductsPicture_Been { Id = -1, LocalListId = productList.Count+1, FileName = filename, IdProductPicture = -1, Path = path + "/" + filename, IsEnabled = true, OrderNumber = productList.Count + 1 });
                            HttpContext.Current.Session.Add("ProductPictureUploadList", productList);

                            BindPictureList(productList);

                            StatusLabel.Text = "File uploaded successfully!";
                            StatusLabel.ForeColor = Color.Green;
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
                else
                {
                    StatusLabel.Text = "Please select picture to upload.";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }

        private List<Beens.ProductsPicture_Been> GetProductPictureList()
        {
            List<Beens.ProductsPicture_Been> productList;

            if (Session["ProductPictureUploadList"] != null)
            {
                productList = (List<Beens.ProductsPicture_Been>)Session["ProductPictureUploadList"];
            }
            else
            {
                productList = new List<Beens.ProductsPicture_Been>();
            }

            return productList;
        }
        private void BindPictureList()
        {
            var productList = GetProductPictureList();
            BindPictureList(productList);
        }

        private void BindPictureList(List<Beens.ProductsPicture_Been> productList)
        {
            DataTable dt = Utils.CreateDataTable<Beens.ProductsPicture_Been>(productList);
            ImageGrid.DataSource = dt;
            ImageGrid.DataBind();
        }
        protected void SavePictures(int IdProduct)
        {
            try
            {
                var productList = this.GetProductPictureList();

                foreach (Beens.ProductsPicture_Been picture in productList)
                {
                    var ret = DbStoredProcedure.Instance().SetProductPicture(picture.Id, IdProduct, picture.FileName, picture.Path, picture.IsEnabled, picture.OrderNumber, SessionProperties.GetUserId(this.Session).Value);

                    if (ret?.Id > 0 && !ret.IsError)
                    {
                        string path = GetFileDirectoryPath("AttachedPicturesProducts_Path");

                        File.Move(GetLocalPhysicalDirectoryPath(picture.Path), GetLocalPhysicalDirectoryPath(path) + "\\" + picture.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "AddProduct.SavePictures", Utils.GetExceptionMessage(ex));
                ShowToastMessage("Error occured. " + Utils.GetExceptionMessage(ex));
            }
        }
        private string GetFileDirectoryPath(string SettingsName)
        {
            string path = DbFunction.Instance().GetStringSettings(SettingsName);

            string mappedPath = GetLocalPhysicalDirectoryPath(path);
            if (mappedPath.Length > 0)
            {
                if (!Directory.Exists(mappedPath))
                {
                    Directory.CreateDirectory(mappedPath);
                }
            }

            return path;
        }
        public string GetLocalPhysicalDirectoryPath(string path)
        {
            if (path.Contains('~') || path.Contains("../"))
                return Server.MapPath(path);
            else
                return path;
        }
        protected void ImageGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int rowIndex = Int32.Parse(e.CommandArgument.ToString());

                    if (rowIndex > -1)
                    {
                        GridView grid = sender as GridView;
                        int value = (int)grid.DataKeys[rowIndex].Value;

                        var productList = this.GetProductPictureList();

                        var item = productList.Single(x => x.Id == value);

                        if(item?.Path?.Length > 0)
                        {
                            File.Delete(GetLocalPhysicalDirectoryPath(item.Path)); 
                        }

                        productList.Remove(item);

                        BindPictureList();
                    }
                }
            }
            catch(Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "AddProduct.btnSubmit_Click", Utils.GetExceptionMessage(ex));
                ShowToastMessage("Error occured. " + Utils.GetExceptionMessage(ex));
            }
        }

        protected void ImageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}
 