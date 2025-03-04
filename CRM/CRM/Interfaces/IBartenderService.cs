using CRM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Interfaces
{
    public interface IBartenderService
    {
        Task<IEnumerable<BartenderDto>> GetAllBartendersAsync();
        Task<BartenderDto> GetBartenderByIdAsync(int id);
        Task<BartenderDto> CreateBartenderAsync(BartenderDto bartenderDto);
        Task<bool> UpdateBartenderAsync(int id, BartenderDto bartenderDto);
        Task<bool> DeleteBartenderAsync(int id);
    }
}
