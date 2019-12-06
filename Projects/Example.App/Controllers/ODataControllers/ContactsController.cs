using System.Threading;
using System.Threading.Tasks;
using Example.App.Controllers.ODataControllers.Interfaces;
using Example.App.Data.Context;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers.ODataControllers
{
    public class ContactsController : ODataController, IODataController
    {
        private readonly IContactsDbContext _context;

        public ContactsController(IContactsDbContext context)
        {
            _context = context;
        }
       
        // http://localhost:5000/odata/Contacts?$select=Name,eyeColor,sport&$filter=eyeColor eq 'Blue'&$count=true
        [EnableQuery(PageSize = 20)]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Ok(await Task.FromResult(_context.Contacts));
        }
    }
}
