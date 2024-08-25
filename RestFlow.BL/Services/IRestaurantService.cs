using RestFlow.DAL.Entities;

namespace RestFlow.BL.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetById(int id);
        Task<Restaurant> Create(string name, string password);
        Task<Restaurant> Login(string name, string password);
        Task Delete(int id);
    }

}
