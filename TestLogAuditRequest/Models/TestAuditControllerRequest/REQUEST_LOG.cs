using System.ComponentModel.DataAnnotations.Schema;

namespace TestLogAuditRequest.Models.TestAuditControllerRequest
{
    [System.ComponentModel.DataAnnotations.Schema.Table("REQUEST_LOG")]
    public class REQUEST_LOG
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid ID { get; set; }
        public string? REQUEST_IP_ADDRESS { get; set; }
        public string? REQUEST_METHOD { get; set; }
        public string? REQUEST_PATH { get; set; }
        public string? REQUEST_USER_AGENT { get; set; }
        public string? REQUEST_TRACE_IDENTIFIER { get; set; }
        public string? REQUEST_QUERY_STRING { get; set; }
        public string? REQUEST_BODY { get; set; }
        public long? USER_ID { get; set; }
        public string? USER_CLAIMS { get; set; }
        public long? CASES_ID { get; set; }
        public string? CASE_NO { get; set; }
        public long? QUERY_TARGET_ID { get; set; }
        public long? QUERY_CATEGORY_ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TIMESTAMPE { get; set; }
    }
}
