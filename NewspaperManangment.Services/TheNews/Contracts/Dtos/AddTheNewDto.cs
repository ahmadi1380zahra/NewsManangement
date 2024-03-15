using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews.Contracts.Dtos
{
    public class AddTheNewDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public List<int> TagsId { get; set; }
        [Required]
        public int NewsPaperCategoryId { get; set; }
    }
}
