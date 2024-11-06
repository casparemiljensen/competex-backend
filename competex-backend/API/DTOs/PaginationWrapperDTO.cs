using competex_backend.Common.Helpers;

namespace competex_backend.API.DTOs
{
    public class PaginationWrapperDTO<T>
    {
        public T Values { get; set; }
        public PageInfo PageInfo { get; set; }
        public PaginationWrapperDTO(T values, int pageSize, int pageNumber, int totalPages) {
            PageInfo = new PageInfo(pageNumber, pageSize, totalPages);
            Values = values;
        }
        public override string ToString()
        {
            return $"pageSize: {PageInfo.PageSize}, pageNumber: {PageInfo.PageNumber}, totalPages: {PageInfo.TotalPages}";
        }
    }
}
