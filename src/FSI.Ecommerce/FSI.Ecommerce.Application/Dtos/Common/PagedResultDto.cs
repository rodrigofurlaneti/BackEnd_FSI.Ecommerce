namespace FSI.Ecommerce.Application.Dtos.Common
{
    public sealed class PagedResultDto<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public long TotalCount { get; }

        public PagedResultDto(
            IReadOnlyList<T> items,
            int pageNumber,
            int pageSize,
            long totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}