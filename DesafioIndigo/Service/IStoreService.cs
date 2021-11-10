using DesafioIndigo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public interface IStoreService
    {
        Task<List<Consulta>> GatAllAsync();
        Task SaveAsync(Consulta consulta);
    }
}