namespace ECommerce_MVC.Data.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> getAllAsync();
        Task<T> getByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T Entity);

    }

}
