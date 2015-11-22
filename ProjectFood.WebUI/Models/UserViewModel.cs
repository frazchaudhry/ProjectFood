using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using ProjectFood.Domain.Entities;

namespace ProjectFood.WebUI.Models
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

    public class UserLoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public Role Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        public string RoleName { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }

    public class UserProfileViewModel
    {
        
    }
}