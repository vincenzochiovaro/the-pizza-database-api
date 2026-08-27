namespace ThePizzaDatabaseAPI.Core.Domains
{
    public class PizzaDomain
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public string? Note { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsStuffCrust { get; set; }
        public bool IsAmerican { get; set; }
        public bool IsWhite { get; set; }
        public required List<string> Ingredients { get; set; }
    }
}