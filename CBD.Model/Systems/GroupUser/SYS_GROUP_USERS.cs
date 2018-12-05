namespace CBD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SYS_GROUP_USERS
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(128)]
        public string USER_NAME { get; set; }

        public int? GROUP_ID { get; set; }

        public int? ORDER { get; set; }

        public int? USED_STATE { get; set; }

        [StringLength(256)]
        public string DESCRIPTION { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(64)]
        public string CREATED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(64)]
        public string MODIFIED_BY { get; set; }
    }
}
