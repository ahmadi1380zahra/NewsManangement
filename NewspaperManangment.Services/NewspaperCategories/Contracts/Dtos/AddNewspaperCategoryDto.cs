using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos
{
    public class AddNewspaperCategoryDto
    {
        [Required]
        public int categoryId { get; set; }
        [Required]
        public int newspaperId { get; set; }

    }
}
