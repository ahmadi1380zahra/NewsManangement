using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts
{
    public interface NewspaperRepository
    {
        void Add(Newspaper newspaper);
    }
}
