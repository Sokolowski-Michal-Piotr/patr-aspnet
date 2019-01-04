using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IMockyService
    {
        Task<string> GetAsync();
    }
}
