using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBW.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}