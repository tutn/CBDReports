namespace CBD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SYS_PAGES
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(64)]
        public string NAME { get; set; }

        [StringLength(256)]
        public string NAME_VI { get; set; }

        [StringLength(256)]
        public string NAME_EN { get; set; }

        [StringLength(256)]
        public string URL { get; set; }

        [StringLength(256)]
        public string ICON { get; set; }

        public int? PARENT_ID { get; set; }

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

        public string PARENT_NAME { get; set; }

        public string USEDSTATE_NAME { get; set; }

        public bool IS_DISABLE { get; set; }
    }

    public class Sidebar
    {
        public List<Node> Nodes { get; set; }
    }

    public class Node
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(64)]
        public string NAME { get; set; }

        [StringLength(256)]
        public string NAME_VI { get; set; }

        [StringLength(256)]
        public string NAME_EN { get; set; }

        [StringLength(256)]
        public string URL { get; set; }

        [StringLength(256)]
        public string ICON { get; set; }

        public int? PARENT_ID { get; set; }

        public int? ORDER { get; set; }

        public int? USED_STATE { get; set; }

        public List<Node> Nodes { get; set; }
    }
}
