using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookah.Models
{
    public class Book
    {
        [Key]
        public string BookID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }
        [Required]
        [Display(Name = "Date Published")]
        public string Date_Published { get; set; }
        [Required]
        [Display(Name = "Edition")]
        public string Edition { get; set; }
        [Required]
        [Display(Name = "ISBN Number")]
        public string ISBN { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Book Condition")]
        public string Book_Condition { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Book Image")]
        public string file { get; set; }
    }
}
