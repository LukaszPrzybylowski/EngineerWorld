using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Model.ForumComment
{
    public class ForumCommentCreate
    {
        public int ForumCommentId { get; set; }

        public int? ParentForumCommentId { get; set; }

        public int ForumId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Must be at least 10-300 characters")]
        [MaxLength(300, ErrorMessage = "Must be at least 10-300 characters")]
        public string Content { get; set; }
    }
}
