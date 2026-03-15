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
            if (selectedComponentType == _selectedComponentType) return;
            if (selectedComponentType == null) selectedComponentType = "none";
            StateManager._selectedComponentType = selectedComponentType;
            StateManager.heldComponent = FactoryComponent.CreateFromName(selectedComponentType);
        }

        public static string GetSelectedComponentType()
        {
            return StateManager._selectedComponentType;
        }

        public static FactoryComponent? GetHeldComponent()
        {
            return heldComponent;
        }

        public static void AddComponent(FactoryComponent c)
        {
            if (c == null) return;
            _componentsList.Add(c);
        }

        public static bool DeleteComponent(FactoryComponent c)
        {
            return _componentsList.Remove(c);
        }

        public static void ClearSelection()
        {
            StateManager._selectedComponentType = "none";
            StateManager.heldComponent = null;
            
        }

        public static List<FactoryComponent> GetFactoryComponents()
        {
            return _componentsList;
        }

        public static FactoryComponent? GetComponentAtLocation(int x, int y)
        {
            foreach (FactoryComponent component in _componentsList)
            {
                if(component.ContainsPoint(x, y)) return component;
            }
            return null;
        }

        
    }
}
