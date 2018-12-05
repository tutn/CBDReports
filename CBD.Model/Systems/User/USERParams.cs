namespace CBD.Model.User
{
    using CBD.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class USERParams : PagingOption
    {
        public string USER_NAME { get; set; }

        public string FULL_NAME { get; set; }

        public string EMAIL { get; set; }

        public int? USED_STATE { get; set; }
    }
}
