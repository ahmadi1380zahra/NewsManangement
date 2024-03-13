using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class NewspaperCategory
    {
        public int Id { get; set; }
        public Newspaper Newspaper { get; set; }
        public int NewspaperId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
