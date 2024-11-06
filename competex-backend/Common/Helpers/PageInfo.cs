namespace competex_backend.Common.Helpers
{
    public class PageInfo
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PageInfo(int pageNumber, int pageSize, int totalPages)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}
