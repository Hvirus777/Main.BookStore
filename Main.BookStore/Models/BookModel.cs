﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Main.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter the title of your book")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please enter the author name")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose the language of your book")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages{ get; set; }  
        public DateTime? CreatedOn{ get; set; } 
        public DateTime? UpdatedOn{ get; set; } 
    }
}
