using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos
{
    public class GetNewspaperCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string NewspaperName { get; set; }
    }
}
