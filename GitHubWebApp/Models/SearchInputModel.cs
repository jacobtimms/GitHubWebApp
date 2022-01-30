using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubWebApp.Models
{
    public class SearchInputModel
    {
        [Required(ErrorMessage = "Search field cannot be left blank")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "GitHub usernames cannot contain spaces")]
        public string SearchInput { get; set; }
    }
}
