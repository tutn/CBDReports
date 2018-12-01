namespace CBD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SYS_UNITS
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(256)]
        public string NAME { get; set; }

        public int? PARENT_ID { get; set; }

        public int? USED_STATE { get; set; }

        [StringLength(256)]
        public string DESCRIPTION { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(64)]
        public string CREATED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(64)]
        public string MODIFIED_BY { get; set; }

        public string PARENT_NAME { get; set; }

        public string USEDSTATE_NAME { get; set; }

        public bool IS_DISABLE { get; set; }
    }
}
