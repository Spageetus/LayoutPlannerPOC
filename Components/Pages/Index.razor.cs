using LayoutPlannerPOC.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LayoutPlannerPOC.Components.Pages
{
    public partial class Index
    {
        public bool ShowCreate {  get; set; }
        
        private FactoryComponentContext? _context;
        public FactoryComponent? NewFactoryComponent { get; set; }

        public List<FactoryComponent>? FactoryComponents2 { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await ShowFactoryComponents();
        }

        public void ShowCreateForm()
        {
            NewFactoryComponent = new FactoryComponent();
            ShowCreate = true;
        }

        public async Task CreateNewFactoryComponent()
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();
            if(NewFactoryComponent is not null)
            {
                _context?.FactoryComponents.Add(NewFactoryComponent);
                _context?.SaveChangesAsync();
            }


            ShowCreate = false;
        }

        public async Task ShowFactoryComponents()
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();

            if(_context is not null)
            {
                Console.WriteLine("Context is not null when calling ShowFactoryComponents");
                
                FactoryComponents2 = await _context.FactoryComponents.ToListAsync();
            }

            if (_context is not null) await _context.DisposeAsync();
        }
    }
}
