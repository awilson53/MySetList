using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Song Text (start chord lines with a period)")]
        public string SongText { get; set; }
        
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Display(Name = "Song Title")]
        public string SongTitle { get; set; }
    }
}