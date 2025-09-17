namespace TechNest.Domain.Sharing
{
    public class ProductParams
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }
        public int? MaxPageSize { get; set; } = 10;
        private int _pageSize = 10;
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = (int)(value > MaxPageSize ? MaxPageSize : value); }
        }
        public int? PageNumber { get; set; } = 1;

    }
}
