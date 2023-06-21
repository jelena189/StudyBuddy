using StudyBuddy.Domain.Pagination;

namespace StudyBuddy.Repositories.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, PaginationInput paginator)
        {
            return source.Skip((paginator.Page - 1) * paginator.Size).Take(paginator.Size);
        }
    }

}
