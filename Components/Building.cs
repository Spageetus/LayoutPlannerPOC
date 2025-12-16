namespace LayoutPlannerPOC.Components
{
    public class Building
    {
        public static List<Building> BuildingsList { get; set; }
        private static int lastId = 0;
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }

        public Building(string? Name, string? Color, int? Width, int? Length)
        {
            if (Building.BuildingsList is null) BuildingsList = new List<Building>();
            this.Id = lastId++;
            this.Name = Name;
            this.Color = Color;
            this.Width = Width;
            this.Length = Length;
            Building.BuildingsList.Add(this);
           
         
        }
    }
}
