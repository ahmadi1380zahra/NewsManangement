using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts.Dtos
{
    public class AddNewsPaperDto
    {
        public string Title { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
