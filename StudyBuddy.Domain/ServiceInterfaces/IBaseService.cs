using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Pagination;

namespace StudyBuddy.Domain.ServiceInterfaces
{
    public interface IBaseService<TDto>
        where TDto : BaseDto
    {
        Task<TDto> GetByIdAsync(Guid id);
        Task<PaginationOutput<TDto>> GetEntitiesAsync(PaginationInput paginator);
        Task DeleteAsync(Guid id);
        Task<int> UpdateAsync(TDto dto);
        Task<Guid> InsertAsync(TDto dto);
    }

}
