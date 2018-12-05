namespace CBD.Model.Unit
{
    using CBD.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class UNITParams : PagingOption
    {
        public string CODE { get; set; }

        public string NAME { get; set; }

        public int? PARENT_ID { get; set; }

        public int? ORDER { get; set; }

        public int? USED_STATE { get; set; }
    }
}
