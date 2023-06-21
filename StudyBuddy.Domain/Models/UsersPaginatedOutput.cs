using StudyBuddy.Domain.Pagination;
namespace StudyBuddy.Domain.Models
{
    public class UsersPaginatedOutput
    {
        public PaginationMetadata Metadata { get; set; }

        public List<UserOutput> Entities { get; set; }
    }
}
