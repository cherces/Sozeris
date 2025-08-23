using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Data.Repositories;

public class DeliveryHistoryRepository : IDeliveryHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryHistoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DeliveryHistory>> GetAllDeliveryHistoryAsync()
    {
        return await _context.DeliveryHistory.AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<DeliveryHistory>> GetDeliveryHistoryByDateAsync(DateTime date)
    {
        return await _context.DeliveryHistory
            .Where(dh => dh.DeliveryDate == date)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DeliveryHistory?> GetDeliveryHistoryBySubscriptionAndDateAsync(int subscriptionId, DateTime date)
    {
        return await _context.DeliveryHistory
            .FirstOrDefaultAsync(dh => dh.SubscriptionId == subscriptionId && dh.DeliveryDate == date);
    }

    public async Task<DeliveryHistory> AddDeliveryHistoryAsync(DeliveryHistory deliveryHistory)
    {
        _context.DeliveryHistory.Add(deliveryHistory);
        await _context.SaveChangesAsync();
        return deliveryHistory;
    }

    public async Task<DeliveryHistory> UpdateDeliveryHistoryAsync(DeliveryHistory deliveryHistory)
    {
        _context.DeliveryHistory.Update(deliveryHistory);
        await _context.SaveChangesAsync();
        return deliveryHistory;
    }
}