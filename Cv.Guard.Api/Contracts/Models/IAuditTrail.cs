using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cv.Guard.Api.Contracts.Models
{
    public interface IAuditTrail : ISoftDelete, IUpdate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
