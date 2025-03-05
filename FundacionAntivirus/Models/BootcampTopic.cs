using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundacionAntivirus.Models;

// public partial class BootcampTopic
// {
//     [Key]
//     public int Id { get; set; }

//     public int BootcampId { get; set; }

//     public int TopicId { get; set; }

//     [ForeignKey("BootcampId")]
//     [InverseProperty("BootcampTopic")]
//     public virtual Bootcamp Bootcamp { get; set; } = null!;

//     [ForeignKey("TopicId")]
//     [InverseProperty("BootcampTopic")]
//     public virtual Topic Topic { get; set; } = null!;
// }
public partial class BootcampTopic
{
    [Key]
    public int Id { get; set; }

    public int BootcampId { get; set; }

    public int TopicId { get; set; }

    [ForeignKey("BootcampId")]
    public virtual Bootcamp Bootcamp { get; set; } = null!;

    [ForeignKey("TopicId")]
    public virtual Topic Topic { get; set; } = null!;
}