using fbla_app.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fbla_app.Models
{
    public class HoursReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<StudentHoursReportViewModel> StudentHours { get; set; }
    }
}