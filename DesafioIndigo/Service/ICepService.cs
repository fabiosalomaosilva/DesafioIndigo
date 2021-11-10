using DesafioIndigo.Models;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public interface ICepService
    {
        Task<Consulta> Get(string numeroCep);
    }
}