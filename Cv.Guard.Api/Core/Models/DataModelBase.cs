using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cv.Guard.Api.Contracts.Models;

namespace Cv.Guard.Api.Core.Models
{
	public class DataModelBase : IAuditTrail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime DeletedDate { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
