namespace StudyBuddy.Domain.Pagination
{
    public class PaginationInput
    {
        public int Page { get; set; }
        public int Size { get; set; }


        public PaginationInput()
        {
            Page = 1;
            Size = 20;
        }
    }

}
