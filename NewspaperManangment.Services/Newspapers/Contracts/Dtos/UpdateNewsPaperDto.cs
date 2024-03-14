using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts.Dtos
{
    public class UpdateNewsPaperDto
    {
        [Required]
        public string Title { get; set; }
    }
}
