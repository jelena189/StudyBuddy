using System;
namespace StudyBuddy.Domain.Pagination
{
    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }

        public PaginationMetadata(int totalCount, int currentPage, int pageSize)
        {
            this.TotalCount = totalCount;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.HasPrevious = (currentPage > 1);
            this.HasNext = (currentPage < this.TotalPages);
        }
    }

}
