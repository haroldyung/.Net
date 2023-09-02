using System.ComponentModel.DataAnnotations.Schema;

namespace TestLogAuditRequest.Models.TestAuditControllerRequest
{

    [Table("REQUEST_QUERY_CATEGORY")]
    public class REQUEST_QUERTY_CATEGORY
    {

        [System.ComponentModel.DataAnnotations.Key]
        public long ID { get; set; }
        public string? CATEGORY_NAME { get; set; }
        public string? CATEGORY_PATH { get; set; }
        public string CATEGORY_PATH_REGEX { get; set; }
        public bool INUSE { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime MODIFIED_DATE { get; set; }
    }
}
