using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySetList.Models
{
    public class SetList
    {
        public int ID { get; set; }

        [Display(Name = "Set List Name")]
        public string SetListName { get; set; }
        
        [Display(Name = "Chord Charts")]
        public virtual ICollection<ChordChart> ChordCharts { get; set; }
    }
}