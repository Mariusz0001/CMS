﻿using CMS.DataBase;
using System;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CMS.Modules
{
    public partial class Login : MainPage
    {
        public override bool LoginRequired
        {
            get
            {
                return false;
            }
        }
        public override int IdModule
        {
            get
            {
                return 3;
            }
        }
    }
}