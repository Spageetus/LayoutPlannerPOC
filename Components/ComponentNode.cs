namespace LayoutPlannerPOC.Components
{
    public class ComponentNode
    {
        public List<ComponentNode> componentNodes;

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


    }
}
