using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFood.Domain.Entities;

namespace ProjectFood.WebUI.Models
{
    public class CommentJsonViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public string CreateDateTime { get; set; }
    }

    public class CommentsJsonViewModel
    {
        public List<Comment> Comments { get; set; }
    }
}