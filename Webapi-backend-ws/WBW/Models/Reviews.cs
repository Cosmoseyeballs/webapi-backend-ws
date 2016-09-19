using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBW.Models
{
    public class Reviews : TableEntity
    {
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}