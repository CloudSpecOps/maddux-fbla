using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fbla_app.Models
{
    public class StudentHoursReportViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public int Grade { get; set; }
        public int CommunityId { get; set; }
        public string CommunityName { get; set; }
        public System.DateTime ServiceDate { get; set; }
        public decimal CommunityHours { get; set; }
        public string ServiceAward { get; set; }
        public string PrimaryUserId { get; set; }
    }
}