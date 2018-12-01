namespace CBD.Model.Sys_Page
{
    using CBD.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class PAGEParams : PagingOption
    {
        public string CODE { get; set; }

        public string NAME { get; set; }

        public string NAME_VI { get; set; }

        public string NAME_EN { get; set; }

        public int? PARENT_ID { get; set; }

        public int? ORDER { get; set; }

        public int? USED_STATE { get; set; }
    }
}
