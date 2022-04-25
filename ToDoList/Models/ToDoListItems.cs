using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace ToDoList.Models
{
    public class ToDoListItems
    {
        public int ID { get; set; }

        public DateTime AddDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least two characters!")]
        [MaxLength(200, ErrorMessage = "Title must contain a maximum of 200 characters!")]
        public string Title { get; set; }

        public bool IsDone { get; set; }

     
    }
}
