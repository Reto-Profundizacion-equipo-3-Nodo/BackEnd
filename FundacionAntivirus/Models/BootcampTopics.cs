using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundacionAntivirus.Models;

public partial class BootcampTopics
{
    [Key]
    public int Id { get; set; }

    public int BootcampId { get; set; }

    public int TopicId { get; set; }

    [ForeignKey("BootcampId")]
    [InverseProperty("BootcampTopics")]
    public virtual Bootcamps Bootcamp { get; set; } = null!;

    [ForeignKey("TopicId")]
    [InverseProperty("BootcampTopics")]
    public virtual Topics Topic { get; set; } = null!;
}
