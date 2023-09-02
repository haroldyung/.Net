using System.ComponentModel.DataAnnotations.Schema;

namespace TestLogAuditRequest.Models.TestAuditControllerRequest
{
    [Table("REQUEST_RESULT_LOG")]
    public class REQUEST_RESULT_LOG
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid ID { get; set; }
        public string? REQUEST_TRACE_IDENTIFIER { get; set; }
        public string? REQUEST_METHOD { get; set; }
        public string? REQUEST_PATH { get; set; }
        public long? RESPONSE_STATUS_CODE { get; set; }
        public string? EXCEPTION_MESSAGE { get; set; }
        public long? USER_ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TIMESTAMP { get; set; }
    }
}
