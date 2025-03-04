using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class BartenderService : IBartenderService
    {
        private readonly ApplicationDbContext _context;

        public BartenderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BartenderDto>> GetAllBartendersAsync()
        {
            return await _context.Bartenders
                .Select(b => new BartenderDto
                {
                    BartenderId = b.BartenderId,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    DrinksPosted = b.DrinksPosted,
                    Email = b.Email,
                    LastPosted = b.LastPosted
                }).ToListAsync();
        }

        public async Task<BartenderDto> GetBartenderByIdAsync(int id)
        {
            var bartender = await _context.Bartenders
                .Where(b => b.BartenderId == id)
                .Select(b => new BartenderDto
                {
                    BartenderId = b.BartenderId,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    DrinksPosted = b.DrinksPosted,
                    Email = b.Email,
                    LastPosted = b.LastPosted
                }).FirstOrDefaultAsync();

            return bartender;
        }

        public async Task<BartenderDto> CreateBartenderAsync(BartenderDto bartenderDto)
        {
            var bartender = new Bartender
            {
                FirstName = bartenderDto.FirstName,
                LastName = bartenderDto.LastName,
                DrinksPosted = bartenderDto.DrinksPosted,
                Email = bartenderDto.Email,
                LastPosted = bartenderDto.LastPosted
            };

            _context.Bartenders.Add(bartender);
            await _context.SaveChangesAsync();

            bartenderDto.BartenderId = bartender.BartenderId;
            return bartenderDto;
        }

        public async Task<bool> UpdateBartenderAsync(int id, BartenderDto bartenderDto)
        {
            var bartender = await _context.Bartenders.FindAsync(id);
            if (bartender == null)
            {
                return false;
            }

            bartender.FirstName = bartenderDto.FirstName;
            bartender.LastName = bartenderDto.LastName;
            bartender.DrinksPosted = bartenderDto.DrinksPosted;
            bartender.Email = bartenderDto.Email;
            bartender.LastPosted = bartenderDto.LastPosted;

            _context.Entry(bartender).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBartenderAsync(int id)
        {
            var bartender = await _context.Bartenders.FindAsync(id);
            if (bartender == null)
            {
                return false;
            }

            _context.Bartenders.Remove(bartender);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
