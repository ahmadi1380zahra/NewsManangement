using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class Newspaper
    {
        public Newspaper()
        {
            NewspaperCategories = new();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public HashSet<NewspaperCategory> NewspaperCategories { get; set; }
    }
}
