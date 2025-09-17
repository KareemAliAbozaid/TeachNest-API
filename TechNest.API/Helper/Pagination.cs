namespace TechNest.API.Helper
{
    public class Pagination<T>where T : class
    {
        public Pagination(int pageNumber, int pageSize, int totalRecords, IEnumerable<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
