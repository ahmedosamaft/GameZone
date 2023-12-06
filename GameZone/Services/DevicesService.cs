
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DevicesService (ApplicationDbContext _context) : IDevicesService
    {
        private readonly ApplicationDbContext _context = _context;

        public IEnumerable<SelectListItem> GetSelectList ( )
        {
            return _context.Devices.OrderBy(d => d.Name).Select(device => new SelectListItem
            {
                Text = device.Name,
                Value = device.Id.ToString()
            }).AsNoTracking(); 
        }
    }
}
