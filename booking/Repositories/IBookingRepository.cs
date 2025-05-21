using booking.Data.Entities;
using System.Linq.Expressions;

namespace booking.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingEntity> AddAsync(BookingEntity entity);
        Task<bool> AlreadyExistsAsync(Expression<Func<BookingEntity, bool>> expression);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<bool> DeleteAsync(Expression<Func<BookingEntity, bool>> expression);
        Task<IEnumerable<BookingEntity>> GetAllAsync(string userEmail);
        Task<BookingEntity> GetAsync(Expression<Func<BookingEntity, bool>> expression);
        Task RollbackTransactionAsync();
        Task<bool> SaveAsync();
    }
}