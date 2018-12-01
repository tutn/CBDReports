namespace CBD.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_SYS_DIMDATE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DateKey { get; set; }

        [StringLength(50)]
        public string DateFullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateFull { get; set; }

        public int? Year { get; set; }

        public int? Quarter { get; set; }

        [StringLength(50)]
        public string QuarterName { get; set; }

        public int? QuarterKey { get; set; }

        public int? Month { get; set; }

        public int? MonthKey { get; set; }

        [StringLength(50)]
        public string MonthName { get; set; }

        public int? DayOfMonth { get; set; }

        public int? NumberOfDaysInTheMonth { get; set; }

        public int? DayOfYear { get; set; }

        public int? WeekOfYear { get; set; }

        public int? WeekOfYearKey { get; set; }

        public int? ISOWeek { get; set; }

        public int? ISOWeekKey { get; set; }

        public int? WeekDay { get; set; }

        [StringLength(50)]
        public string WeekDayName { get; set; }

        public int? FiscalYear { get; set; }

        public int? FiscalQuarter { get; set; }

        public int? FiscalQuarterKey { get; set; }

        public int? FiscalMonth { get; set; }

        public int? FiscalMonthKey { get; set; }

        public int? IsWorkDayKey { get; set; }

        [StringLength(50)]
        public string IsWorkDayDescription { get; set; }

        public int? IsPublicHolidayKey { get; set; }

        [StringLength(50)]
        public string IsPublicHolidayDescription { get; set; }
    }
}
