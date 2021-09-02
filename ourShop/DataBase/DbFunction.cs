using Npgsql;
using NpgsqlTypes;
using ourShop.Beens;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace ourShop.DataBase
{
    public class DbFunction : DBFunctionBase
    {
        public static DbFunction _object;
        
        public DbFunction()
        {
        }
        
        public static DbFunction Instance()
        {
            if (_object == null)
            {
                _object = new DbFunction();
            }
            return _object;
        }

        public Boolean IsUserHasPermission(int userId, string PermissionName)
        {
            using (var dbo = new ourShopEntities())
            {
                var _ret = CreateNpgsqlParameter("_ret", NpgsqlDbType.Boolean, false, System.Data.ParameterDirection.InputOutput);

                dbo.Database.ExecuteSqlCommand("select public.get_isuserhaspermission(@_userId, @_permissionName, @_ret);",
                    CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, userId),
                    CreateNpgsqlParameter("_permissionName", NpgsqlDbType.Varchar, PermissionName), 
                    _ret);
                if (_ret != null && _ret.Value != null && Boolean.Parse(_ret.Value.ToString()) == true)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Get_MenuToolbar_Result> GetMenuToolbar(int? userId)
        {
            var paramList = new List<ObjectParameter>();
            
            paramList.Add(CreateObjectParameter("_userid", typeof(int), userId));

            var ret = base.GetTableFunction("public", "get_menutoolbar", paramList);
            var result = new List<Beens.Get_MenuToolbar_Result>();

            if (ret != null)
            {
                foreach (var record in ret)
                {
                    result.Add(new Beens.Get_MenuToolbar_Result
                    {
                        ToolbarId = Utils.TryParseNullableInt(record[0].ToString()),
                        IdParentToolbar = Utils.TryParseNullableInt(record[1].ToString()),
                        ToolbarName = record[2].ToString(),
                        IconName = record[3].ToString(),
                        URL = record[4].ToString()
                    });
                }
                
                return result;
            }
            return null;
        }

        public List<Get_CategoriesList_Result> GetProductsList(string categoryName = null, int? userId = null)
        {
            var paramList = new List<ObjectParameter>();
            

            paramList.Add(CreateObjectParameter("_productcategory", typeof(string), categoryName));
            paramList.Add(CreateObjectParameter("_userid", typeof(int), userId));

            var ret = base.GetTableFunction("public", "get_productslist", paramList);
            var result = new List<Beens.Get_CategoriesList_Result>();

            if (ret != null)
            {
                foreach (var record in ret)
                {
                    result.Add(new Beens.Get_CategoriesList_Result
                    {
                        Id = Utils.TryParseNullableInt(record[0].ToString()),
                        IdCategoriesBook = Utils.TryParseNullableInt(record[1].ToString()),
                        IdProductsStatusBook = Utils.TryParseNullableInt(record[2].ToString()),
                        IdTaxPercentagesBook = Utils.TryParseNullableInt(record[3].ToString()),
                        CategoryName = record[4].ToString(),
                        StatusName = record[5].ToString(),
                        TaxValue =  Utils.TryParseNullableInt(record[6].ToString()),
                        Name = record[7].ToString(),
                        Barcode = record[8].ToString(),
                        Price = Utils.TryParseNullableDouble(record[9].ToString()),
                        Qty = Utils.TryParseNullableInt(record[10].ToString()),
                        Description = record[11].ToString(),
                    });
                }
                
                return result;
            }
            return null;
        }

        public string GetStringSettings(string SetingsName)
        {
            string str = null;

            using (var dbo = new ourShopEntities())
            {
                var ret = dbo.Settings
                            .Where(s => s.Name == SetingsName)
                            .FirstOrDefault();

                if(ret != null)
                {
                    str = ret.ValueString;
                }
                
            }
            return str;
        }

        public Get_Product_Result GetProduct(int id, int userId)
        {
            var paramList = new List<ObjectParameter>();


            paramList.Add(CreateObjectParameter("_id", typeof(int), id));
            paramList.Add(CreateObjectParameter("_userid", typeof(int), userId));

            var ret = base.GetTableFunction("public", "get_product", paramList);

            if (ret != null)
            {
                foreach (var record in ret)
                {
                    return new Get_Product_Result
                    {
                        Id = Utils.TryParseNullableInt(record[0].ToString()),
                        IdCategoriesBook = Utils.TryParseNullableInt(record[1].ToString()),
                        IdTaxPercentagesBook = Utils.TryParseNullableInt(record[2].ToString()),
                        IdProductsStatusBook = Utils.TryParseNullableInt(record[3].ToString()),
                        Name = record[4].ToString(),
                        Barcode = record[5].ToString(),
                        Price = Utils.TryParseNullableDouble(record[6].ToString()),
                        QTY = Utils.TryParseNullableInt(record[7].ToString()),
                        Enabled = Utils.TryParseNullableBoolean(record[8].ToString()),
                        Description = record[9].ToString()
                    };
                }
            }
            return null;
        }
    }
}