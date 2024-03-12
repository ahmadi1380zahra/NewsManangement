using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Tags.Contracts.Dtos
{
    public class UpdateTagDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
