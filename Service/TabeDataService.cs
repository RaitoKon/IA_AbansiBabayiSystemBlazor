using IA_AbansiBabayiSystemBlazor.Data;
using IA_AbansiBabayiSystemBlazor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class TableDataService<T> where T : class
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    private readonly IHubContext<AutoUpdateHub> _hubContext;

    public List<T> Data { get; private set; } = new();
    public event Action? OnChanged;

    public TableDataService(IDbContextFactory<ApplicationDbContext> contextFactory, IHubContext<AutoUpdateHub> hubContext)
    {
        _contextFactory = contextFactory;
        _hubContext = hubContext;
    }

    public async Task LoadDataAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        Data = await context.Set<T>().ToListAsync();
        OnChanged?.Invoke();
    }

    public async Task SetData(List<T> newData)
    {
        Data = newData;
        await NotifyChangedAsync();
    }

    public async Task Add(T item)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Add(item);
        await context.SaveChangesAsync();

        // Reload the data after adding to keep Data consistent
        await LoadDataAsync();
        await NotifyChangedAsync();
    }

    public async Task Delete(T item)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Remove(item);
        await context.SaveChangesAsync();

        // Reload the data after deleting to keep Data consistent
        await LoadDataAsync();
        await NotifyChangedAsync();
    }

    public async Task NotifyChangedAsync()
    {
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", typeof(T).Name);
    }
}
