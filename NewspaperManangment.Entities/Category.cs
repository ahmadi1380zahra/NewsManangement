using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class Category
    {
        public Category()
        {
            Tags = new();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public HashSet<Tag> Tags { get; set; }
    }
}
