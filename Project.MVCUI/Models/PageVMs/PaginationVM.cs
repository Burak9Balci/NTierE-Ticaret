﻿using PagedList;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class PaginationVM
    {
        public IPagedList<ProductVM> PageProducts { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public ProductVM Product { get; set; }
    }
}