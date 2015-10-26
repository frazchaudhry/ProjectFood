using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace ProjectFood.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        [DataType(DataType.MultilineText)]  
        public string CommentText { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
