using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NewspaperManangment.Persistance.EF.Mediate.Tag4;

public class GetTagDto2
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
}

public class GetTagDtoById : IQuery
{
    public int Id { get; set; }
    public Pagination pagination { get; set; }
}

public class Pagination
{
    [FromQuery(Name = "limit")] 
    public int Limit  { get; set; }
    [FromQuery(Name = "offset")] 
    public int Offset { get; set; }
}
