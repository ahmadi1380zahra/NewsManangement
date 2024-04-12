using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews.Contracts.Dtos
{
    public class GetAllTheNewWithTagDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GetTagsOfTheNewDto>? TheNewWithTags { get; set; }
    }
}
