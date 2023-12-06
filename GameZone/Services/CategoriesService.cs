
namespace GameZone.Services;

public class CategoriesService (ApplicationDbContext context) : ICategoriesService
{
    private readonly ApplicationDbContext _context = context;

    public IEnumerable<SelectListItem> GetSelectList ( )
    {
        return _context.Categories.OrderBy(c => c.Name).Select(category => new SelectListItem
        {
            Text = category.Name,
            Value = category.Id.ToString()
        }).AsNoTracking();
    }
}
