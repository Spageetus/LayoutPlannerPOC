namespace LayoutPlannerPOC.Data
{
    public class ControlTip
    {
        private string _label = string.Empty;
        private string _iconSrc = string.Empty;
        private static List<ControlTip> _allTips = [];


        public string Label
        {
            get { return this._label; }
            set { this._label = value; }
        }


        public string IconSrc
        {
            get { return this._iconSrc; }
            set
            {
                var pathString = value;
                //if (!System.IO.File.Exists(pathString)) pathString = string.Empty;
                pathString = pathString.Replace('\\', '/').Replace("wwwroot/", string.Empty);
                Console.WriteLine("after replacing: " + pathString);
                this._iconSrc = pathString;
            } 
        }

        public static List<ControlTip> AllTips { get { return _allTips; } }

        public ControlTip(string label, string iconSrc)
        {
            this.Label = label;
            if (!System.IO.File.Exists(iconSrc)) iconSrc = string.Empty;
            
            this.IconSrc = iconSrc;
            ControlTip._allTips.Add(this);
        }

        //this is where I will add premade tips
        public static readonly ControlTip ClearCursor = new ControlTip("Clear Selection", "wwwroot\\Assets\\Icons\\InputPrompts\\keyboard_q.svg");
        public static readonly ControlTip RotateComponent = new ControlTip("Rotate 90\u00B0", "wwwroot\\Assets\\Icons\\InputPrompts\\keyboard_r.svg");
        public static readonly ControlTip PanCamera = new ControlTip("Move camera", "wwwroot\\Assets\\Icons\\InputPrompts\\keyboard_arrows_all.svg");
        public static readonly ControlTip PlaceComponent = new ControlTip("Place Component", "wwwroot\\Assets\\Icons\\InputPrompts\\mouse_left.svg");
        public static readonly ControlTip DeleteComponent = new ControlTip("Delete Component", "wwwroot\\Assets\\Icons\\InputPrompts\\mouse_right.svg");
        public static readonly ControlTip ReturnToCenter = new ControlTip("Go to Center", "wwwroot\\Assets\\Icons\\InputPrompts\\keyboard_h.svg");
        public static readonly ControlTip SaveToHotbar = new ControlTip("Store in Hotbar Slot", "wwwroot\\Assets\\Icons\\InputPrompts\\mouse_left.svg");
        public static readonly ControlTip ClearFromHotbar = new ControlTip("Clear Hotbar Slot", "wwwroot\\Assets\\Icons\\InputPrompts\\mouse_right.svg");


        
    }
}
