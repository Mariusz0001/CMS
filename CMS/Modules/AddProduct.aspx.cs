using CMS.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Modules
{
    public partial class AddProduct : CMS.EditForm
    {

        public override object GetData()
        {
            Beens.Get_Product_Result ret = null;

            var productList = new List<Beens.ProductsPicture_Been>();
            HttpContext.Current.Session.Add("ProductPictureUploadList", productList);

            if (IdFromURL > 0)
            {
                 ret = DbFunction.Instance().GetProduct(IdFromURL.Value, SessionProperties.GetUserId(Session).Value);
            }
            
            LoadCategoriesBook(ret?.IdCategoriesBook);
            BindPictureList();

            return ret;
        }
        
        
        public void LoadCategoriesBook(int? selectedId = null)
        {
            using (var dbo = new CMSEntities())
            {
                var categories = dbo.CategoriesBook
                           .Where(s => s.Enabled == true && (s.IdCategoriesBook_Parent == null || s.IdCategoriesBook_Parent == 0));
                if (categories != null)
                {
                    var categoriesList = categories.ToList();
                    this.PopulateCategoriesTreeView(categoriesList, 0, null, selectedId);
                }
            }
        }

        public void PopulateCategoriesTreeView(List<CMS.DataBase.CategoriesBook> categoriesList, int parentId, TreeNode treeNode, int? selectedId = null)
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

                    if (selectedId != null && item.Id == selectedId)
                    {
                        child.Selected = true;
                        treeNode.Expanded = true;
                    }

                    if (parentId == 0)
                    {
                        using (var dbo = new CMSEntities())
                        {
                            CategoriesTree.Nodes.Add(child);
                            var newChild = dbo.CategoriesBook
                                   .Where(s => s.Enabled == true && s.IdCategoriesBook_Parent == item.Id);

                            if (newChild != null)
                            {
                                PopulateCategoriesTreeView(newChild.ToList(), int.Parse(child.Value), child, selectedId);
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

                if (Price.Value != null && IdTaxPercentagesBook.SelectedItem?.Value != null && QTY.Value != null && CategoriesTree.SelectedNode?.Value != null && SessionProperties.GetUserId(Session) != null)
                {
                    var ret = DbStoredProcedure.Instance().SetProduct(id, Name.Text, Enabled.Checked, Barcode.Text, double.Parse(Price.Value.Replace('.', ',')), int.Parse(IdTaxPercentagesBook.SelectedItem.Value), int.Parse(QTY.Value), int.Parse(CategoriesTree.SelectedNode.Value), Description.InnerText, SessionProperties.GetUserId(this.Session).Value);

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
                if (FileUploadControl.HasFile)
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg" || FileUploadControl.PostedFile.ContentType == "image/jpg" || FileUploadControl.PostedFile.ContentType == "image/png" || FileUploadControl.PostedFile.ContentType == "image/gif" || FileUploadControl.PostedFile.ContentType == "image/bmp")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 20002400)
                        {
                            int productId = -1;

                            if (IdFromURL > 0)
                                productId = IdFromURL.Value;


                            if (productId > 0)
                            {
                                string path = GetFileDirectoryPath("AttachedPicturesProducts_Path");
                                string localPath = GetLocalPhysicalDirectoryPath(path);
                                
                                string filename = Path.GetFileName(Path.GetFileNameWithoutExtension(FileUploadControl.PostedFile.FileName) + DateTime.Now.ToShortTimeString().Replace(":", "_").Replace(".", "_") + "_" + Utils.Get8Digits() + Path.GetExtension(FileUploadControl.PostedFile.FileName));

                                FileUploadControl.SaveAs(localPath + "\\" + filename);

                                var ret = DbStoredProcedure.Instance().SetProductPicture(-1, productId, filename, path + "/" + filename, true, 0, SessionProperties.GetUserId(this.Session).Value);

                                if (ret?.IsError == true)
                                {
                                    SetStatusLabel("SavePictures error occured. " + ret.Message, Color.Red);
                                }
                                
                                BindPictureList();
                                SetStatusLabel("File uploaded successfully as " + filename, Color.Green);
                            }
                            else
                            {
                                string path = this.GetFileDirectoryPath("Temp_Path");
                                string tempPath = GetLocalPhysicalDirectoryPath(path);

                                string filename = Path.GetFileName(Path.GetFileNameWithoutExtension(FileUploadControl.PostedFile.FileName) + DateTime.Now.ToShortTimeString().Replace(":", "_").Replace(".", "_") + "_" + Utils.Get8Digits() + Path.GetExtension(FileUploadControl.PostedFile.FileName));

                                FileUploadControl.SaveAs(tempPath + "\\" + filename);

                                var productList = GetProductPictureList();
                                productList.Add(new Beens.ProductsPicture_Been { Id = -1, LocalListId = productList.Count + 1, FileName = filename, IdProduct = productId, Path = path + "/" + filename, IsEnabled = true, OrderNumber = productList.Count + 1 });
                                HttpContext.Current.Session.Add("ProductPictureUploadList", productList);
                                BindPictureList(productList);

                                SetStatusLabel("File uploaded successfully as " + filename, Color.Green);
                            }
                        }
                        else
                        {
                            SetStatusLabel("The file has to be less than 20 Mb!", Color.Red);
                        }
                    }
                    else
                    {
                        SetStatusLabel("Only JPG, JPEG, PNG, BMP, GIF files are accepted!", Color.Red);
                    }
                }
                else
                {
                    SetStatusLabel("Please select picture to upload.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                SetStatusLabel("The file could not be uploaded. The following error occured: " + ex.Message, Color.Red);
            }
        }

        private List<Beens.ProductsPicture_Been> GetProductPictureList()
        {
            var productList = new List<Beens.ProductsPicture_Been>();
            int localId = 1;

            if (IdFromURL > 0)
                productList = DbFunction.Instance().GetProductPictureList(IdFromURL.Value);

            foreach (var localPic in GetProductLocalPictureList())
            {
                if (localPic.Id == -1 || productList.Where(f => f.Id == localPic.Id).FirstOrDefault() == null)
                    productList.Add(localPic);
            }

            foreach(var pic in productList)
            {
                pic.LocalListId = localId;
                localId++;
            }

            return productList;
        }
        private List<Beens.ProductsPicture_Been> GetProductLocalPictureList()
        {
            List<Beens.ProductsPicture_Been> pictureList;

            if (Session["ProductPictureUploadList"] != null)
            {
                pictureList = (List<Beens.ProductsPicture_Been>)Session["ProductPictureUploadList"];
            }
            else
            {
                pictureList = new List<Beens.ProductsPicture_Been>();
            }
            return pictureList;

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
                    string path = GetFileDirectoryPath("AttachedPicturesProducts_Path");
                    
                    File.Move(GetLocalPhysicalDirectoryPath(picture.Path), GetLocalPhysicalDirectoryPath(path) + "\\" + picture.FileName);

                    picture.Path = path + "/" + picture.FileName ;

                    var ret = DbStoredProcedure.Instance().SetProductPicture(picture.Id, IdProduct, picture.FileName, picture.Path, picture.IsEnabled, picture.OrderNumber, SessionProperties.GetUserId(this.Session).Value);

                    if (ret?.IsError == true )
                    {
                        ShowToastMessage("SavePictures error occured. " + ret.Message);
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
                        var item = productList.Where(x => x.LocalListId == value).FirstOrDefault();

                        if (item != null)
                        {
                            if (item.Id > 0)
                            {
                                var ret = DbStoredProcedure.Instance().SetProductPicture(item.Id, item.IdProduct, item.FileName, item.Path, false, item.OrderNumber, SessionProperties.GetUserId(this.Session).Value);
                            }

                            if (item.Path?.Length > 0)
                            {
                                File.Delete(GetLocalPhysicalDirectoryPath(item.Path));
                            }

                            productList.Remove(item);
                            Session["ProductPictureUploadList"] = productList;
                        }

                        BindPictureList();

                        SetStatusLabel("File deleted successfully", Color.Green);
                    }
                }
            }
            catch(Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "AddProduct.btnSubmit_Click", Utils.GetExceptionMessage(ex));
                ShowToastMessage("Error occured. " + Utils.GetExceptionMessage(ex));
                SetStatusLabel("Error occured." + Utils.GetExceptionMessage(ex), Color.Red);
            }
        }
        
        protected void ImageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void SetStatusLabel(string Message, Color color)
        {
            StatusLabel.ForeColor = color;
            StatusLabel.Text = Message;
        }
    }
}
 