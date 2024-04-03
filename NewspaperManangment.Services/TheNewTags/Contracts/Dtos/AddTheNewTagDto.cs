using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNewTags.Contracts.Dtos
{
    public class AddTheNewTagDto
    {
        [Required]
        public int TheNewId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
}
