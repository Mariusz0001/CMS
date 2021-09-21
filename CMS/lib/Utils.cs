using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;

namespace CMS
{
    public static class Utils
    {
        public static string GetClientIpAddress(HttpRequestBase request)
        {
            try
            {
                var userHostAddress = request.UserHostAddress;

                IPAddress.Parse(userHostAddress);

                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(xForwardedFor))
                    return userHostAddress;

                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPublicIpAddress(ip)).ToList();

                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }
            catch (Exception)
            {
                return "0.0.0.0";
            }
        }

        public static bool IsPublicIpAddress(string ipAddress)
        {
            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }

        public static string GetServerIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static int? TryParseNullableInt(string val)
        {
            try
            {
                int outValue;
                return int.TryParse(val, out outValue) ? (int?)outValue : null;
            }
            catch
            {
                return null;
            }
        }

        public static double? TryParseNullableDouble(string val)
        {
            try
            {
                double outValue;
                
                var d = Double.TryParse(val, out outValue) ? (double?)outValue : null;

                return outValue;
            }
            catch
            {
                return null;
            }
        }

        public static bool? TryParseNullableBoolean(string val)
        {
            try
            {
                bool outValue;
                return bool.TryParse(val, out outValue) ? (bool?)outValue : null;
            }
            catch
            {
                return null;
            }
        }

        public static String GetExceptionMessage(Exception error)
        {
            if (error != null)
            {
                if (error.InnerException != null && error.InnerException.Message.Count() > 0)
                {
                    return error.InnerException.Message;
                }
                else
                {
                    return error.Message;
                }
            }
            else
            {
                return "No error message";
            }
        }

        public static string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            if (list != null)
            {
                Type type = typeof(T);
                var properties = type.GetProperties();

                DataTable dataTable = new DataTable();
                dataTable.TableName = typeof(T).FullName;
                foreach (PropertyInfo info in properties)
                {
                    dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                }

                foreach (T entity in list)
                {
                    object[] values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }

                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
            return null;
        }

        public static string getParameterFromURL(string param)
        {
            try
            {
                string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                var parameters = sUrl.Split('?');

                foreach (var par in parameters)
                {
                    if (par != null && par.Length > 0 && par.Contains(param + "="))
                    {
                        var urlValue = par.Replace(param + "=", "");

                        return urlValue;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}