using System.Threading.Tasks;

namespace EchoBot111.Services
{
    public interface iazureservice
    {

        Task<azureluismodel> exceute(string textuer);
    }
}
