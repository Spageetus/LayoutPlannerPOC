using LayoutPlannerPOC.Data;
using System.Text.Json;

namespace LayoutPlannerPOC
{
    

    //sealed means this class cannot be inherited
    public sealed class StateManager //Singleton for storing data used across components
    {
        struct ChangedState
        {
            public const uint PlacedComponents = 1 << 0;
            public const uint HeldComponent = 1 << 1;
        }


        //Lazy initialization of singleton instance
        //src: https://www.codeproject.com/articles/Thread-Safe-Singleton-in-Csharp-A-Guide-to-Double#comments-section
        private static readonly Lazy<StateManager> _instance = new Lazy<StateManager>(() => new StateManager()); //not exactly sure how this is supposed to work

        private static List<FactoryComponent> _componentsList = new List<FactoryComponent>();

        private static string _selectedComponentType = "none"; //stores the name of the component that has been selected via hotkeys or component browser.

        private static FactoryComponent? heldComponent = null;

        //stores which properties have been altered, but not retrieved
        private static uint _changedStates = 0;

        public static bool StateHasChanged { get { Console.WriteLine(_changedStates);  return _changedStates != 0; } }
        //TODO: use the _changedStates byte to store if/what has changed in the StateManager (ex: components have changed, held component has changed, etc).
        //More accurately, _changedStates tracks which values have been altered, but never retrieved


        //public accessor for singleton instance
        public static StateManager Instance => _instance.Value;

        public static void setSelectedComponentType(string selectedComponentType)
        {
            if (selectedComponentType == _selectedComponentType) return;
            if (selectedComponentType == null) selectedComponentType = "none";
            StateManager._selectedComponentType = selectedComponentType;
            StateManager.heldComponent = FactoryComponent.CreateFromName(selectedComponentType);

            //marking the HeldComponent as being changed
            StateManager._changedStates |= ChangedState.HeldComponent;
        }

        public static string GetSelectedComponentType()
        {
            return StateManager._selectedComponentType;
        }

        public static FactoryComponent? GetHeldComponent()
        {
            //marking the held component as not being changed
            StateManager._changedStates &= ~ChangedState.HeldComponent;
            return heldComponent;
        }

        public static void AddComponent(FactoryComponent c)
        {
            if (c == null) return;
            _componentsList.Add(c);
            //marking the placed components as being changed
            StateManager._changedStates |= ChangedState.PlacedComponents;
        }

        public static bool DeleteComponent(FactoryComponent c)
        {
            bool removed = _componentsList.Remove(c);
            //marking the placed components as being changed 
            if (removed) StateManager._changedStates |= ChangedState.PlacedComponents;
            return removed;
        }

        public static void ClearSelection()
        {
            StateManager._selectedComponentType = "none";
            StateManager.heldComponent = null;
            //marking held component as being changed
            StateManager._changedStates |= ChangedState.HeldComponent;

        }

        public static string GetComponentsListAsJSON()
        {
            return JsonSerializer.Serialize(StateManager._componentsList);
        }

        public static void SetComponentsFromJSON(string jsonString)
        {
            //TODO: Why is this a possible null reference?
            StateManager._componentsList = JsonSerializer.Deserialize<List<FactoryComponent>>(jsonString);
        }

        public static List<FactoryComponent> GetFactoryComponents()
        {
            foreach(FactoryComponent c in _componentsList)
            {
                c.UpdateImageFilePath();
            }
            //marking placed components as being unchanged 
            StateManager._changedStates &= ~ChangedState.PlacedComponents;
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

        public static bool CheckForDuplicates()
        {
            List<int> ids = new List<int>();
            foreach(FactoryComponent c in  _componentsList)
            {
                if (c == null) return false;
                int id = c.Id;
                if(ids.Contains(id))
                {

                    Console.WriteLine($"duplicate id found: {id}");
                    Console.WriteLine(string.Join(',', ids));
                    return true;
                }
                ids.Add(id);
            }
            
            return false;
        }
        
    }
}
