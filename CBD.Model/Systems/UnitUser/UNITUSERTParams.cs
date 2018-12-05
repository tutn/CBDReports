namespace CBD.Model.UnitUser
{
    using CBD.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class UNITUSERTParams : PagingOption
    {
        public string USER_NAME { get; set; }

        public int? UNIT_ID { get; set; }

        public int? USED_STATE { get; set; }
    }
}
