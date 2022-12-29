using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Model.ArticleComment
{
    public class ArticleCommentCreate
    {
        public int ArticleCommentId { get; set; }
        public int? ParentAticleCommentId { get; set; }
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Must be at least 10-300 characters")]
        [MaxLength(300, ErrorMessage = "Must be at least 10-300 characters")]
        public string Content { get; set; }
    }
}
