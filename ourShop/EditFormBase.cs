using System;
using System.Web.UI.WebControls;
using ourShop.DataBase;
using System.Reflection;
using System.Web.UI;

namespace ourShop
{
    public abstract class EditFormBase : MainPage
    {
        public abstract object GetData();
        public virtual void _Page_Load(object sender, EventArgs e)
        {

        }

        public virtual void BindProperties(object Been)
        {
            /*
             * 
             * to do
             * 
             * bindowanie tax
             * category
             * description
             * pictures
             * */

            try
            {
                foreach (var propertyInfo in Been.GetType().GetProperties())
                {
                    BindControl(propertyInfo, Been);
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "MainPage.BindPoperties " + Been.ToString(), Utils.GetExceptionMessage(ex));
            }
        }
        private void BindControl(PropertyInfo propertyInfo, object Been)
        {
            try
            {
                foreach (Control ctrl in this.Form.Controls)
                {

                    if (ctrl is ContentPlaceHolder)
                    {
                        ContentPlaceHolder chp = ((System.Web.UI.WebControls.ContentPlaceHolder)ctrl);

                        foreach (Control controll in chp.Controls)
                        {
                            if (controll.ID == propertyInfo.Name)
                            {
                                try
                                {
                                    if (controll is TextBox)
                                    {
                                        TextBox tb = ((TextBox)controll);

                                        tb.Text = propertyInfo.GetValue(Been, null).ToString();
                                        return;
                                    }
                                    else if (controll is Label)
                                    {
                                        Label lb = ((Label)controll);

                                        lb.Text = propertyInfo.GetValue(Been, null).ToString();
                                        return;
                                    }
                                    else if (controll is System.Web.UI.HtmlControls.HtmlInputGenericControl)
                                    {
                                        System.Web.UI.HtmlControls.HtmlInputGenericControl cb = ((System.Web.UI.HtmlControls.HtmlInputGenericControl)controll);

                                        cb.Value = propertyInfo.GetValue(Been, null).ToString();
                                        return;
                                    }
                                    else if (controll is System.Web.UI.HtmlControls.HtmlInputCheckBox)
                                    {
                                        System.Web.UI.HtmlControls.HtmlInputCheckBox cb = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)controll);

                                        if (Utils.TryParseNullableBoolean(propertyInfo.GetValue(Been, null).ToString()).Value)
                                            cb.Checked = true;
                                        else
                                            cb.Checked = false;

                                        return;
                                    }
                                    else if(controll is DropDownList)
                                    {
                                        DropDownList dl = ((DropDownList)controll);
                                        dl.SelectedValue = propertyInfo.GetValue(Been, null).ToString();

                                        return;
                                    }
                                    else if (controll is System.Web.UI.HtmlControls.HtmlTextArea)
                                    {
                                        System.Web.UI.HtmlControls.HtmlTextArea ta = controll as System.Web.UI.HtmlControls.HtmlTextArea;
                                        ta.Value = propertyInfo.GetValue(Been, null).ToString();
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "MainPage.BindPoperties " + Been.ToString(), Utils.GetExceptionMessage(ex));
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 BindProperties(GetData());
            }

            _Page_Load(sender, e);
        }
    }
}