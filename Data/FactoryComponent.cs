using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace LayoutPlannerPOC.Data
{
    public enum FactoryComponentType
    {
        None = 0,
        TheHUB,
        AWESOMESink,
        Constructor,
        Assembler,
        Manufacturer,
        Packager,
        Refinery,
        Blender,
        ParticleAccelerator,
        Converter,
        Smelter,
        Foundry,
        Miner
    };
    

    public class FactoryComponent
    {
        public int Id  { get; set; }
        public string? Name { get; set; }
        public string? ImageFilePath { get; set; }
        public int? Height { get; set; } = 1;
        public int? Width { get; set; } = 1;
        public  int Rotation { get; set; } = 0;
        public int? X { get; set; }
        public int? Y { get; set; }
        public FactoryComponent() { }

        public void SetLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void SetSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        
        public static FactoryComponent? CreateFromName(string name)
        {
            FactoryComponent component = new FactoryComponent();
            component.Name = name;
            switch (name)
            {
                case ("the_hub"):
                    component.SetSize(14, 26);
                    break;
                case ("awesome_sink"):
                    component.SetSize(16, 13);
                    break;
                case ("constructor"):
                    component.SetSize(8, 10);
                    break;
                case ("smelter"):
                    component.SetSize(6, 9);
                    break;
                case ("assembler"):
                    component.SetSize(10, 15);
                    break;
                case ("manufacturer"):
                    component.SetSize(18, 20);
                    break;
                case ("packager"):
                    component.SetSize(8, 8);
                    break;
                case ("refinery"):
                    component.SetSize(10, 22);
                    break;
                case ("blender"):
                    component.SetSize(18, 16);
                    break;
                case ("particle_accelerator"):
                    component.SetSize(24, 38);
                    break;
                case ("converter"):
                    component.SetSize(16, 16);
                    break;
                case ("foundry"):
                    component.SetSize(10, 9);
                    break;
                case ("miner"):
                    component.SetSize(6, 14);
                    break;
                
                default:
                    return null;
            }

            component.ImageFilePath = FactoryComponent.FindComponentFilePath(name, 0);
            return component;

        }

        //TODO: Use this method instead of the "create from name"
        public static FactoryComponent? Create(FactoryComponentType type)
        {
            if (type == FactoryComponentType.None) return null;
            switch (type)
            {
                case FactoryComponentType.Constructor:
                    return null;
                default: return null;
            }
        }
        private static string? FindComponentFilePath(string name, int rotation)
        {
            string svgFilePath = "wwwroot\\Assets\\ComponentGraphics\\" + name + rotation + ".svg";
            svgFilePath = $"wwwroot\\Assets\\ComponentGraphics\\{name}\\{name + rotation}.svg";
            Console.WriteLine(svgFilePath);
            if (System.IO.File.Exists(svgFilePath))
            {
                Console.WriteLine("SVG File found");
                return svgFilePath;
            }
            Console.WriteLine("Could not find SVG File: " + svgFilePath);
            return null;
        }


    }
}

