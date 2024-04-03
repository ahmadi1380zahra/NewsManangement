using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNewTags
{
    public class TheNewTagBuilder
    {
     private readonly TheNewTag _theNewTag;
        public TheNewTagBuilder(int theNewId,int tagId)
        {
            _theNewTag = new TheNewTag
            {
                TheNewId = theNewId,
                TagId = tagId
            };
        }
        public TheNewTag Build()
        {
            return _theNewTag;
        }
    }
}
