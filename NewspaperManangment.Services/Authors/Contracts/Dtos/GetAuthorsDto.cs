using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts.Dtos
{
    public class GetAuthorsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
