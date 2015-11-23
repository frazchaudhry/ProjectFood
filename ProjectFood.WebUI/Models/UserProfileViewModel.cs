using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectFood.Domain.Entities;

namespace ProjectFood.WebUI.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string PasswordHash { get; set; }
        public string Email { get; set; }

        [Display(Name = "About Me")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}