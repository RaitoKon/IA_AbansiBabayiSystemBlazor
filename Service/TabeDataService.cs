using IA_AbansiBabayiSystemBlazor.Data;
using Microsoft.EntityFrameworkCore;

public class TableDataService<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public List<T> Data { get; private set; } = new();
    public event Action? OnChanged;

    public TableDataService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task LoadDataAsync()
    {
        Data = await _context.Set<T>().ToListAsync();
        NotifyChanged();
    }

    public void SetData(List<T> newData)
    {
        Data = newData;
        NotifyChanged();
    }

    public void Add(T item)
    {
        Data.Add(item);
        NotifyChanged();
    }

    public void Delete(T item)
    {
        Data.Remove(item);
        NotifyChanged();
    }

    public void NotifyChanged() => OnChanged?.Invoke();
}
