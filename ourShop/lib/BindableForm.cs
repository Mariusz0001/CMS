using System;
using System.Web.UI.WebControls;
using ourShop.DataBase;
using System.Reflection;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ourShop
{
    public class BindableForm
    {
        public static void BindProperties(object Been, ControlCollection controls)
        {
            try
            {
                if (Been != null)
                {
                    foreach (var propertyInfo in Been.GetType().GetProperties())
                    {
                        BindContentPlaceHolder(propertyInfo, Been, controls);
                    }
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "MainPage.BindPoperties " + Been.ToString(), Utils.GetExceptionMessage(ex));
            }
        }

        private static void BindContentPlaceHolder(PropertyInfo propertyInfo, object Been, ControlCollection controls)
        {
            try
            {
                Boolean ret;
                foreach (Control ctrl in controls)
                {
                    if(ctrl is ContentPlaceHolder)
                        ret = BindControls(propertyInfo, Been, ctrl.Controls);
                    else
                        ret = BindControls(propertyInfo, Been, controls);

                    if (ret)
                        break;
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "MainPage.BindPoperties " + Been.ToString(), Utils.GetExceptionMessage(ex));
            }
        }

        private static Boolean BindControls(PropertyInfo propertyInfo, object Been, ControlCollection controls)
        {
            foreach (Control controll in controls)
            {
                if (controll.ID == propertyInfo.Name)
                {
                    try
                    {
                        if (controll is TextBox)
                        {
                            TextBox tb = ((TextBox)controll);

                            tb.Text = GetPropertyValue(propertyInfo, Been);
                            return true;
                        }
                        else if (controll is Label)
                        {
                            Label lb = ((Label)controll);

                            lb.Text = GetPropertyValue(propertyInfo, Been);
                            return true;
                        }
                        else if (controll is System.Web.UI.HtmlControls.HtmlInputGenericControl)
                        {
                            System.Web.UI.HtmlControls.HtmlInputGenericControl cb = ((System.Web.UI.HtmlControls.HtmlInputGenericControl)controll);

                            cb.Value = GetPropertyValue(propertyInfo, Been);

                            return true;
                        }
                        else if (controll is System.Web.UI.HtmlControls.HtmlInputCheckBox)
                        {
                            System.Web.UI.HtmlControls.HtmlInputCheckBox cb = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)controll);

                            if (Utils.TryParseNullableBoolean(GetPropertyValue(propertyInfo, Been)).Value)
                                cb.Checked = true;
                            else
                                cb.Checked = false;

                            return true;
                        }
                        else if (controll is DropDownList)
                        {
                            DropDownList dl = ((DropDownList)controll);
                            dl.SelectedValue = GetPropertyValue(propertyInfo, Been);

                            return true;
                        }
                        else if (controll is System.Web.UI.HtmlControls.HtmlTextArea)
                        {
                            System.Web.UI.HtmlControls.HtmlTextArea ta = controll as System.Web.UI.HtmlControls.HtmlTextArea;
                            ta.Value = GetPropertyValue(propertyInfo, Been);

                            return true;
                        }
                    }
                    catch
                    {
                    }
                }

            }
            return false;
        }

        private static string GetPropertyValue(PropertyInfo propertyInfo, object Been)
        {
            string type = propertyInfo.GetMethod.ToString();
            if (type.Contains("Double") || type.Contains("Float") || type.Contains("Decimal"))
            {
                return propertyInfo.GetValue(Been, null).ToString().Replace(',', '.');
            }
            else
            {
                return propertyInfo.GetValue(Been, null).ToString();
            }
        }
    }
}