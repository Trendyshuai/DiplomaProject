using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMovie.WebApplication.Models
{
    public class TreeModel
    {

        public int id { get; set; }

        public string text { get; set; }


        public string state { get; set; }


        public string iconCls { get; set; }

        public List<TreeModel> children { get; set; }

    }
}