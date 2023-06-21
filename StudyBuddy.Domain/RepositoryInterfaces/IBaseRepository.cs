using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Pagination;

namespace StudyBuddy.Domain.RepositoryInterfaces
{
    public interface IBaseRepository<TDto>
        where TDto : BaseDto
    {
        Task<Guid> AddAsync(TDto entityDto);
        Task<TDto> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(TDto entityDto);
        Task<int> DeleteAsync(Guid id);
        Task<PaginationOutput<TDto>> GetEntities(PaginationInput paginator);
        PaginationOutput<TDto> CreatePaginatedEntities(PaginationInput paginator, List<TDto> entities, int totalEntities);
        Task<bool> EntityExists(Guid id);
    }

}
