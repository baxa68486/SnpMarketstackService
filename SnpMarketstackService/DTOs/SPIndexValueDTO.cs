namespace SnpMarketstackService.DTOs
{
    public class SPIndexValueDTO
    {
        public string Date { get; init; }
        public string Exchange { get; init; }
        public double Open { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Close { get; init; }
        public double Volume { get; init; }
        public double? Adj_High { get; init; }
        public double? Adj_Low { get; init; }
        public double? Adj_Close { get; init; }
        public double? Adj_Open { get; init; }
        public double? Adj_Volume { get; init; }
    }
}
