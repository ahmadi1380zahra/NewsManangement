using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class TheNewTag
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public TheNew TheNew { get; set; }
        public int TheNewId { get; set; }
    }
}
