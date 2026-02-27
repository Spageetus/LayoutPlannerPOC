using LayoutPlannerPOC.Components;
using LayoutPlannerPOC.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Swift;

namespace LayoutPlannerPOC
{
    //sealed means this class cannot be inherited
    public sealed class StateManager //Singleton for storing data used across components
    {
        //Lazy initialization of singleton instance
        //src: https://www.codeproject.com/articles/Thread-Safe-Singleton-in-Csharp-A-Guide-to-Double#comments-section
        private static readonly Lazy<StateManager> _instance = new Lazy<StateManager>(() => new StateManager()); //not exactly sure how this is supposed to work

        private static List<FactoryComponent> _componentsList = new List<FactoryComponent>();

        private static string _selectedComponentType = "none"; //stores the name of the component that has been selected via hotkeys or component browser.

        private static FactoryComponent? heldComponent = null;


        //public accessor for singleton instance
        public static StateManager Instance => _instance.Value;

        public static void setSelectedComponentType(string selectedComponentType)
        {
            if(selectedComponentType == null) selectedComponentType = "none";

            StateManager._selectedComponentType = selectedComponentType;
        }

        public static string GetSelectedComponentType()
        {
            return StateManager._selectedComponentType;
        }

        public static FactoryComponent? GetHeldComponent()
        {
            return heldComponent;
        }

        public static List<FactoryComponent> GetFactoryComponents()
        {
            return _componentsList;
        }

        



    }
}
