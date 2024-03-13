using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts.Dtos
{
    public class AddAuthorDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
    }
}
