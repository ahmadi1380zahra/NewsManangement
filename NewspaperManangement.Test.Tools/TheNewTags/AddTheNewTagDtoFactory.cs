using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNewTags
{
    public static class AddTheNewTagDtoFactory
    {
        public static AddTheNewTagDto Create(int theNewId, int tagId)
        {
            return new AddTheNewTagDto
            {
                TheNewId = theNewId,
                TagId = tagId
            };

        }
    }
}
