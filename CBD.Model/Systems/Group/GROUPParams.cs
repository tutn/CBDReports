namespace CBD.Model.Group
{
    using CBD.Model.Common;

    public partial class GROUPParams : PagingOption
    {
        public string CODE { get; set; }

        public string NAME { get; set; }

        public int? ORDER { get; set; }

        public int? USED_STATE { get; set; }
    }
}
