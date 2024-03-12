using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Catgories.Contracts.Dtos
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
    }
}
