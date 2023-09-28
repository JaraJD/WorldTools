using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.DTO;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure.Repositories
{
    public class StoredEventRepository : IStoredEventRepository
    {
        private readonly Context _context;

        public StoredEventRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<string> RegisterEvent(StoredEvent storedEvent)
        {
            _context.Add(storedEvent);
            await _context.SaveChangesAsync();

            return "Event created";
        }
    }
}
