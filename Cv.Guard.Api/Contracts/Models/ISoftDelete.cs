namespace Cv.Guard.Api.Contracts.Models
{
    public interface ISoftDelete
    {
        public DateTime DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
