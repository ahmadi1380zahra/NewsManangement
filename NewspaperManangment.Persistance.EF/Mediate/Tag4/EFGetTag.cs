using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;

namespace NewspaperManangment.Persistance.EF.Mediate.Tag4;

public class EFGetTag 
    (EFDataContext dataContext)
    : IQueryHandler<GetTagDtoById,GetTagDto2?>
{
    public async Task<GetTagDto2?> Handle(GetTagDtoById query,
        BasicCommand? basicCommand = null)
    {
        return await dataContext.Set<Tag>()
                .Where(t=>t.Id== query.Id)
                .Select(c=>new GetTagDto2
                {
                    Id = c.Id,
                    Title = c.Title
                }).FirstOrDefaultAsync();

            
    }
}

public class GetAll(EFDataContext dataContext)
    : IQueryHandler<List<GetAllTag>>
{
    public async Task<List<GetAllTag>> Handle(BasicCommand? basicCommand = null)
    {
        return await dataContext.Set<Tag>()
            .Select(c=>new GetAllTag
            {
                Id = c.Id,
                Title = c.Title
            }).ToListAsync(); 
    }
}



public class GetAllTag : IQuery
{
    public int Id { get; set; }
    public string Title { get; set; }
}