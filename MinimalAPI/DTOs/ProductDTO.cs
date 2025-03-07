namespace MinimalAPI.DTOs
{
    public record ProductDTO
    {
        public long Id { get; init; }
        public required string Name { get; init; }
        public double Price { get; init; }
    }
}
