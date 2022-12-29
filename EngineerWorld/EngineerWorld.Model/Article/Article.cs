using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Model.Article
{
    public class Article : ArticleCreate
    {
        public string Username { get; set; }
        public int ApplicationUserId { get; set; }
        public string Profession { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
