using Microsoft.JSInterop;
using System.Runtime.CompilerServices;
using System.Text.Json;


namespace LayoutPlannerPOC.Components
{
    public class ComponentNode
    {
        public static List<ComponentNode> componentNodes;

        public string Color {  get; set; }
        public double? CellX {  get; set; }
        public double? CellY { get; set; }

        public double? width { get; set; } = 1;
        public double? height { get; set; } = 1;
        public ComponentNode(string color)
        {
            this.Color = color;

            if (componentNodes is null) componentNodes = [];

            componentNodes.Add(this);
        }

        public static void exportToString()
        {
            string jsonString = JsonSerializer.Serialize(componentNodes);
            Console.WriteLine(jsonString);
        }

        public static List<ComponentNode> ImportFromString(string jsonString)
        {
            if(componentNodes is null) componentNodes = new List<ComponentNode>();
            componentNodes = JsonSerializer.Deserialize<List<ComponentNode>>(jsonString);
            foreach(ComponentNode n in componentNodes)
            {
                Console.WriteLine(n.Color);
                Console.WriteLine(n.CellX);
                Console.WriteLine(n.CellY);
            }
            return componentNodes;


        }

    }
}
