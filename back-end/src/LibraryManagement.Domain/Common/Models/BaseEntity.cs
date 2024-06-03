namespace LibraryManagement.Domain.Common.Models
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}