using System;
using System.ComponentModel.DataAnnotations;

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
