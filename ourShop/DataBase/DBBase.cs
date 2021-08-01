using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ourShop.DataBase
{
    public class DBBase
    {
        public NpgsqlParameter CreateNpgsqlParameter(string name, NpgsqlDbType type, object value, System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input)
        {
            var _parameter = new NpgsqlParameter(name, type);
            _parameter.Direction = direction;
            _parameter.Value = value;

            return _parameter;
        }

        public ObjectParameter CreateObjectParameter(string name, Type type, object value)
        {
            var param = value != null ?
               new ObjectParameter(name, value) :
               new ObjectParameter(name, type);
            
            return param;
        }
    }
}