using System.Numerics;

namespace LayoutPlannerPOC.Data
{
    public class FactoryComponent
    {
        public int Id  { get; set; }
        public string? Name { get; set; }
        public string? ImageFilePath { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Rotation { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public FactoryComponent() { }
        public FactoryComponent(string name)
        {
            switch (name)
            {
                case ("blue"): 
                    this.ImageFilePath = "..\\Assets\\test_svg_blue.svg";
                    this.Height = 1;
                    this.Width = 1;
                    this.Rotation = 0;
                    break;
                case ("red"):
                    this.ImageFilePath = "..\\Assets\\test_svg_red.svg";
                    this.Height = 1;
                    this.Width = 1;
                    this.Rotation = 0;
                    break;
                case ("green"):
                    this.ImageFilePath = "..\\Assets\\test_svg_green.svg";
                    this.Height = 1;
                    this.Width = 1;
                    this.Rotation = 0;
                    break;
                case ("purple"):
                    this.ImageFilePath = "..\\Assets\\test_svg_purple.svg";
                    this.Height = 1;
                    this.Width = 1;
                    this.Rotation = 0;
                    break;
                case ("yellow"):
                    this.ImageFilePath = "..\\Assets\\test_svg_yellow.svg";
                    this.Height = 1;
                    this.Width = 1;
                    this.Rotation = 0;
                    break;
                default:

                    break;
            }

        }

        
    }
}

