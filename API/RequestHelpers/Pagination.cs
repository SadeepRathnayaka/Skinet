using System;

namespace API.RequestHelpers;

public class Pagination<T>(int pageIndex, int pagesize, int count, IReadOnlyList<T> data)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pagesize;
    public int Count { get; set; } = count;
    public IReadOnlyList<T> Data { get; set; } = data;
}
