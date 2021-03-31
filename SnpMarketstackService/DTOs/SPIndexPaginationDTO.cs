namespace SnpMarketstackService.DTOs
{
    public class SPIndexPaginationDTO
    {
        public int Limit { get; init; }
        public int Offset { get; init; }
        public int Count { get; init; }
        public int Total { get; init; }
    }
}
