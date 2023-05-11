namespace ZebraPuzzle
{
    public abstract record BaseRule
    {
        public int? Position { get; set; }
        public Color? Color { get; set; }
        public Nationality? Nationality { get; set; }
        public Pet? Pet { get; set; }
        public Drink? Drink { get; set; }
        public Smoke? Smoke { get; set; }

        public int?[] Values => new int?[] {
            Position,
            (int?)Color,
            (int?)Nationality,
            (int?)Pet,
            (int?)Drink,
            (int?)Smoke,
        };
    }
}