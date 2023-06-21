namespace StudyBuddy.Domain.Pagination
{
    public class PaginationOutput<TDto>
    {
        public PaginationMetadata Metadata { get; set; }
        public List<TDto> Entities { get; set; }


        public PaginationOutput(List<TDto> entities, PaginationMetadata metadata)
        {
            this.Entities = entities;
            this.Metadata = metadata;
        }
    }

}
