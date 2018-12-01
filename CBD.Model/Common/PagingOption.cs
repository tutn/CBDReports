namespace CBD.Model.Common
{
    public class PagingOption
    {
        public int PageSize { set; get; }
        public int PageNumber { set; get; }
        public string OrderBy { get; set; }
        public bool IsDesc { get; set; }
    }
}