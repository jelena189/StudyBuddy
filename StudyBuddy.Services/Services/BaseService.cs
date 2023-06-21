using AutoMapper;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Domain.ServiceInterfaces;
using StudyBuddy.Repositories.Entities;
using StudyBuddy.Services.ErrorHandling;

namespace StudyBuddy.Services.Services
{
    public class BaseService<T, TDto, TRepository> : IBaseService<TDto>
        where TDto : BaseDto
        where T : BaseEntity
        where TRepository : IBaseRepository<TDto>
    {
        protected readonly TRepository repository;
        protected IMapper Mapper { get; set; }
        protected readonly IHttpContextWrapper httpContextWrapper;
        public BaseService(TRepository repository, IMapper mapper, IHttpContextWrapper httpContextWrapper)
        {
            this.repository = repository;
            this.Mapper = mapper;
            this.httpContextWrapper = httpContextWrapper;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var result = await repository.DeleteAsync(id);

            if (result == 0)
            {
                throw new NotFoundException($"There is no entity with id: {id}");
            }
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            var entityDto = await repository.GetByIdAsync(id);
            if (entityDto == null)
                throw new NotFoundException($"There is no entity with id: {id}.");
            return entityDto;
        }

        public virtual Task<PaginationOutput<TDto>> GetEntitiesAsync(PaginationInput paginator)
        {
            return repository.GetEntities(paginator);
        }

        public Task<Guid> InsertAsync(TDto dto)
        {
            return repository.AddAsync(dto);
        }

        public virtual async Task<int> UpdateAsync(TDto dto)
        {
            return await repository.UpdateAsync(dto);
        }
    }
}
