using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AbContext _context;


        public OrderRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            Order deleteOrder = await _context.Order
                .FirstOrDefaultAsync(order => order.Id == orderId);

            if (deleteOrder != null)
            {
                _context.Order.Remove(deleteOrder);
                await _context.SaveChangesAsync();
            }
            return deleteOrder;
        }

        public async Task<Order> InsertNewOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> SelectAllOrder()
        {
            return await _context.Order.Include(o => o.User).ToListAsync();
        }

        public async Task<Order> SelectOrderById(int orderId)
        {
            return await _context.Order.FirstOrDefaultAsync(a => a.Id == orderId);
        }

        public async Task<Order> UpdateExistingOrder(int orderId, Order order)
        {
            Order updateOrder = await _context.Order
                .FirstOrDefaultAsync(order => order.Id == orderId);
            if (updateOrder != null)
            {
                updateOrder.User = order.User;
                updateOrder.OrderDateTime = order.OrderDateTime;
                await _context.SaveChangesAsync();
            }
            return updateOrder;
        }
    }
}
