

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundAntivirus.Models
{
    public class BootcampInstitution
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }
        [ForeignKey("Bootcamp")]
        public int BootcampId { get; set; }
        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public virtual Institution Institution { get; set; } = null!;
        public virtual Bootcamp Bootcamp { get; set; } = null!;
        public virtual Topic Topic { get; set; } = null!;
    }

}