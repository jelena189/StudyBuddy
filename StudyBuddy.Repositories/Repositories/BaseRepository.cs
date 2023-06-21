using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Repositories.Entities;
using StudyBuddy.Repositories.Extensions;

namespace StudyBuddy.Repositories.Repositories
{
    public class BaseRepository<T, TDto> : IBaseRepository<TDto>
        where T : BaseEntity
        where TDto : BaseDto
    {
        protected StudyBuddyContext Context { get; set; }
        protected DbSet<T> Entities { get; set; }
        protected IMapper Mapper { get; set; }

        public BaseRepository(StudyBuddyContext context, IMapper mapper)
        {
            this.Context = context;
            Entities = context.Set<T>();
            this.Mapper = mapper;
        }

        public virtual async Task<Guid> AddAsync(TDto entityDto)
        {
            T entity = Mapper.Map<T>(entityDto);

            Entities.Add(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            T entity = await Entities.FindAsync(id);

            if (entity != null)
            {
                Entities.Remove(entity);
                return await Context.SaveChangesAsync();
            }

            return 0;
        }

        public Task<bool> EntityExists(Guid id)
        {
            return Entities.AnyAsync(e => e.Id == id);
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await Entities.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            TDto entityDto = Mapper.Map<TDto>(entity);
            return entityDto;
        }

        public virtual async Task<PaginationOutput<TDto>> GetEntities(PaginationInput paginator)
        {
            var entities = Entities.Paginate<T>(paginator);
            var dtos = await Mapper.ProjectTo<TDto>(entities).ToListAsync();

            return CreatePaginatedEntities(paginator, dtos, Entities.Count());
        }

        public PaginationOutput<TDto> CreatePaginatedEntities(PaginationInput paginator, List<TDto> entities, int totalEntities)
        {
            int entitiesCount = entities.Count;
            if (entitiesCount < paginator.Size)
                paginator.Size = entitiesCount;

            PaginationMetadata metadata = new PaginationMetadata(totalEntities, paginator.Page, paginator.Size);
            PaginationOutput<TDto> result = new PaginationOutput<TDto>(entities, metadata);

            return result;
        }
        public virtual Task<int> UpdateAsync(TDto entityDto)
        {
            T entity = Mapper.Map<T>(entityDto);

            Entities.Update(entity);
            return Context.SaveChangesAsync();
        }
    }

}
