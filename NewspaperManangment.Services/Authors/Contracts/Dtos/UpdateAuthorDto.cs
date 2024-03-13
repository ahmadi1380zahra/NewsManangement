using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts.Dtos
{
    public class UpdateAuthorDto
    {
        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }
    }
}
