﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Entities
{
    public class Author
    {
        public Author()
        {
            TheNews = new();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public HashSet<TheNew> TheNews { get; set; }

    }
}
