using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class TheNew
    {
        public TheNew()
        {
            TheNewTags = new();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public int View { get; set; }
        public NewspaperCategory NewspaperCategory { get; set; }
        public int NewspaperCategoryId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public HashSet<TheNewTag> TheNewTags { get; set; }
    }
}
