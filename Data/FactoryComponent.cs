using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection;
using System.Text.Json;

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

    struct Rotation
    {
        public const int CW360 = 0;
        public const int CW0 = 0;
        public const int CW90 = 1;
        public const int CW180 = 2;
        public const int CW270 = 3;
        
    }




    public class FactoryComponent
    {
        private static int nextId = 0;
        public int Id  { get; set; }
        public string? Name { get; set; }
        public string? ImageFilePath { get; set; }
        public int? Height { get; set; } = 1;
        public int? Width { get; set; } = 1;
        public  int Rotation { get; set; } = 0;
        public int? X { get; set; }
        public int? Y { get; set; }
        public FactoryComponent() { }

        public FactoryComponent(FactoryComponentType type, int? overrideId = null)
        {
            if (type == FactoryComponentType.None || type > FactoryComponentType.Miner)
            {
                return;
            }

            switch (type)
            {
                case FactoryComponentType.TheHUB:
                    this.Name = "the_hub";
                    this.SetSize(14, 26);
                    break;
                case FactoryComponentType.AWESOMESink:
                    this.Name = "awesome_sink";
                    this.SetSize(16, 13);
                    break;
                case FactoryComponentType.Constructor:
                    this.Name = "constructor";
                    this.SetSize(8, 10);
                    break;
                case FactoryComponentType.Assembler:
                    this.Name = "assembler";
                    this.SetSize(6, 9);
                    break;
                case FactoryComponentType.Manufacturer:
                    this.Name = "manufacturer";
                    this.SetSize(18, 20);
                    break;
                case FactoryComponentType.Packager:
                    this.Name = "packager";
                    this.SetSize(8, 8);
                    break;
                case FactoryComponentType.Refinery:
                    this.Name = "refinery";
                    this.SetSize(10, 22);
                    break;
                case FactoryComponentType.Blender:
                    this.Name = "blender";
                    this.SetSize(18, 16);
                    break;
                case FactoryComponentType.ParticleAccelerator:
                    this.Name = "particle_accelerator";
                    this.SetSize(24, 38);
                    break;
                case FactoryComponentType.Converter:
                    this.Name = "converter";
                    this.SetSize(16, 16);
                    break;
                case FactoryComponentType.Smelter:
                    this.Name = "smelter";
                    this.SetSize(6, 9);
                    break;
                case FactoryComponentType.Foundry:
                    this.Name = "foundry";
                    this.SetSize(10, 9);
                    break;
                case FactoryComponentType.Miner:
                    this.Name = "miner";
                    this.SetSize(6, 14);
                    break;
                default: return;
             }
            
            if(overrideId == null)
            {
                this.Id = FactoryComponent.nextId++;
            }
            else
            {
                this.Id = (int)overrideId;
            }
        }

        public FactoryComponent(int id, string name, string imageFilePath, int height, int width, int rotation, int x, int y)
        {
            this.Id = id;
            this.Name = name;
            this.ImageFilePath = imageFilePath;
            this.Height = height;
            this.Width = width;
            this.Rotation = rotation;
            this.X = x;
            this.Y = y;
        }



        public void SetLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void SetSize(int width, int height)
        {
            if (width <= 0) width = 1;
            if (height <= 0) height = 1;
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

        public bool ContainsPoint(int col, int row)
        {
            if (this.X > col) return false;
            if (this.X + this.Width < col) return false;
            if(this.Y > row) return false;
            if(this.Y + this.Height < row) return false;
            return true;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

