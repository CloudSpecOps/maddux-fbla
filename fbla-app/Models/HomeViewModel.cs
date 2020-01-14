using fbla_app.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fbla_app.Models
{
    public class HomeViewModel
    {
        public IList<Community> Communities { get; set; }
    }
}