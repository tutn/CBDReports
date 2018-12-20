namespace CBD.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_INDUSTRY_CATEGORIES
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(256)]
        public string NAME { get; set; }

        public int? PARENT_ID { get; set; }

        [StringLength(512)]
        public string URL { get; set; }

        [StringLength(512)]
        public string KEY_SELECT { get; set; }

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
