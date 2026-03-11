using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.JSInterop;


namespace LayoutPlannerPOC.Components
{
    //TODO: Refactor code to use this InputHandler class instead
    public static class InputHandler
    {
        [Inject]
        public static IJSRuntime JSRuntime { get; set; }
        public static async Task HandleKeyDown(KeyboardEventArgs e)
        {
            Console.WriteLine($"Handling key down event: {e.Key}");
            switch (e.Key)
            {
                default: Console.WriteLine($"No event for key: {e.Key}");
                    break;
                //panning movements (WASD)
                case ("w"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panUp");
                    break;
                case ("a"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panLeft");
                    break;
                case ("s"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panDown");
                    break;
                case ("d"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panRight");
                    break;
                //panning movements (arrow keys)
                case ("ArrowUp"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panUp");
                    break;
                case ("ArrowDown"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panDown");
                    break;
                case ("ArrowLeft"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panLeft");
                    break;
                case ("ArrowRight"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.panRight");
                    break;
                //return home (H)
                case ("h"):
                    await JSRuntime.InvokeVoidAsync("canvasInterop.goHome");
                    break;
                case ("q"):
                    StateManager.setSelectedComponentType("none");
                    StateManager.ClearSelection();
                    break;
                //Hotbar keys
                //case ("1"):
                //    StateManager.setSelectedComponentType(hotbarButtons[1].StoredComponentType); //is there a way to suppress these warnings??? or even better, verify the buttons !null
                //    break;
                //case ("2"):
                //    StateManager.setSelectedComponentType(hotbarButtons[2].StoredComponentType);
                //    break;
                //case ("3"):
                //    StateManager.setSelectedComponentType(hotbarButtons[3].StoredComponentType);
                //    break;
                //case ("4"):
                //    StateManager.setSelectedComponentType(hotbarButtons[4].StoredComponentType);
                //    break;
                //case ("5"):
                //    StateManager.setSelectedComponentType(hotbarButtons[5].StoredComponentType);
                //    break;
                //case ("6"):
                //    StateManager.setSelectedComponentType(hotbarButtons[6].StoredComponentType);
                //    break;
                //case ("7"):
                //    StateManager.setSelectedComponentType(hotbarButtons[7].StoredComponentType);
                //    break;
                //case ("8"):
                //    StateManager.setSelectedComponentType(hotbarButtons[8].StoredComponentType);
                //    break;
                //case ("9"):
                //    StateManager.setSelectedComponentType(hotbarButtons[9].StoredComponentType);
                //    break;
                //case ("0"):
                //    StateManager.setSelectedComponentType(hotbarButtons[0].StoredComponentType);
                //    break;
            }
        }
        

        public static async Task HandleMouseEvents(MouseEventArgs e)
        {

        }
    }
}
