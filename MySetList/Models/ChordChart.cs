using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySetList.Models
{
    public class ChordChart
    {
        public int ID { get; set; }
        public string StoragePath { get; set; }
        public string FileName { get; set; }
        public Guid ChartID { get; set; }
    }
}