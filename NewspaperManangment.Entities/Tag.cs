using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
