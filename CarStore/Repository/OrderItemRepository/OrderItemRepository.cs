using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.OrderItemRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AbContext _context;

        public OrderItemRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> DeleteOrderItem(int orderItemId)
        {
            OrderItem deleteOrderItem = await _context.OrderItem
                .FirstOrDefaultAsync(orderItem => orderItem.Id == orderItemId);

            if (deleteOrderItem != null)
            {
                _context.OrderItem.Remove(deleteOrderItem);
                await _context.SaveChangesAsync();
            }
            return deleteOrderItem;
        }

        public async Task<OrderItem> InsertNewOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<List<OrderItem>> SelectAllOrderItems()
        {
            return await _context.OrderItem.Include(a => a.Product).Include(a => a.Order).ToListAsync();
        }

        public async Task<OrderItem> SelectOrderItemById(int orderItemId)
        {
            return await _context.OrderItem.Include(a => a.Product).Include(a => a.Order).FirstOrDefaultAsync(a => a.Id == orderItemId);
        }

        public async Task<OrderItem> UpdateExistingOrderItem(int orderItemId, OrderItem orderItem)
        {
            OrderItem updateOrderItem = await _context.OrderItem
                .FirstOrDefaultAsync(orderItem => orderItem.Id == orderItemId);
            if (updateOrderItem != null)
            {
                updateOrderItem.Quantity = orderItem.Quantity;
                updateOrderItem.Price = orderItem.Price;
                await _context.SaveChangesAsync();
            }
            return updateOrderItem;
        }
    }
}
