using DesafioIndigo.Models;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public interface ICepService
    {
        Consulta Get(string numeroCep);
    }
}