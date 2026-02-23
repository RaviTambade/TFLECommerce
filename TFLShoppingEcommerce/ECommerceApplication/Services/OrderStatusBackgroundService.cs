using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

public class OrderStatusBackgroundService
    : BackgroundService, IOrderStatusBackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OrderStatusBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void UpdateShippedOrders()
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider
                        .GetRequiredService<IOrderStatusBackgroundRepository>();

        repo.UpdateShippedOrders();
    }

    public void UpdatePendingOrders()
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider
                        .GetRequiredService<IOrderStatusBackgroundRepository>();

        repo.UpdatePendingOrders();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            UpdatePendingOrders();
            UpdateShippedOrders();
            UpdateProcessingOrders();
            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken); // dev: 3 min
        }
    }

    public void UpdateProcessingOrders()
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider
                        .GetRequiredService<IOrderStatusBackgroundRepository>();

        repo.UpdateProcessingOrders();
    }
}