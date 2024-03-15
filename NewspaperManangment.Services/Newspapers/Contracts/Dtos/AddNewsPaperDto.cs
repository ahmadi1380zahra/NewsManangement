using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts.Dtos
{
    public class AddNewsPaperDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public List<int> CategoriesId { get; set; }
    }
}
