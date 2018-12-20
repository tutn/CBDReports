namespace CBD.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_INDUSTRY_GLOBAL_YEARLY
    {
        public int ID { get; set; }

        [StringLength(64)]
        public string EXCHANGE_DATE { get; set; }

        public double? OPEN_PRICE { get; set; }

        public double? HIGH_PRICE { get; set; }

        public double? LOW_PRICE { get; set; }

        public double? LAST_PRICE { get; set; }

        public DateTime? EXCHANGE_TIME { get; set; }

        public double? SET_PRICE { get; set; }

        public double? CHANGE_PRICE { get; set; }

        public double? VOL { get; set; }

        public int? CATEGORY_ID { get; set; }

        public double? AVG_PRICE { get; set; }

        public double? TOTAL_VOL { get; set; }

        public double? PRIORDAY_SET_PRICE { get; set; }

        public double? PRIORDAY_OP_INT { get; set; }

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
