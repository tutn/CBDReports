namespace CBD.Model.UnitGroupPage
{
    using CBD.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class UNITGROUPPAGEParams : PagingOption
    {
        public int? UNIT_ID { get; set; }

        public int? GROUP_ID { get; set; }

        public string USER_NAME { get; set; }

        public int? PAGE_ID { get; set; }

        public int? USED_STATE { get; set; }
    }
}
