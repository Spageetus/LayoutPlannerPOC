using LayoutPlannerPOC.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace LayoutPlannerPOC.Components.Pages
{
    public partial class Index : IAsyncDisposable 
    {
        public bool ShowCreate {  get; set; }
        public bool ShowEdit { get; set; }
        public int EditingId { get; set; }
        
        private FactoryComponentContext? _context;
        public FactoryComponent? NewFactoryComponent { get; set; }

        public List<FactoryComponent>? FactoryComponents { get; set; }

        public FactoryComponent? FactoryComponentToUpdate { get; set; }

        
        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await ShowFactoryComponents();
        }

        public async ValueTask DisposeAsync()
        {
            if (_context is not null) await _context.DisposeAsync();
            _context = null;
        }

        //---------------Create---------------///
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
            await ShowFactoryComponents();
        }

        //----------------Read-----------------///
        public async Task ShowFactoryComponents()
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();

            if(_context is not null)
            {
                Console.WriteLine("Context is not null when calling ShowFactoryComponents");
                
                FactoryComponents = await _context.FactoryComponents.ToListAsync();
            }

            if (_context is not null) await DisposeAsync(); //sometimes ive seen this throw an error...  
        }


        //---------------Edit----------------///

        public async Task ShowEditForm(FactoryComponent ourFactoryComponent)
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();
            FactoryComponentToUpdate = _context.FactoryComponents.FirstOrDefault(x => x.Id == ourFactoryComponent.Id);
            ShowEdit = true;
            EditingId = ourFactoryComponent.Id;
        }

        public async Task UpdateFactoryComponent()
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();
            if(_context is not null)
            {
                if (FactoryComponentToUpdate is not null) _context.FactoryComponents.Update(FactoryComponentToUpdate);
                await _context.SaveChangesAsync();
                await DisposeAsync();
            }
            ShowEdit = false;
            await ShowFactoryComponents(); //make sure to show the factory components again
        }

        //---------------Delete--------------///

        public async Task DeleteFactoryComponent(FactoryComponent ourFactoryComponent)
        {
            _context ??= await FactoryComponentContextFactory.CreateDbContextAsync();
            if (_context is not null)
            {
                if (ourFactoryComponent is not null) _context.FactoryComponents.Remove(ourFactoryComponent);
                await _context.SaveChangesAsync();
                await DisposeAsync();
                await ShowFactoryComponents();
            }
        }
    }
}
