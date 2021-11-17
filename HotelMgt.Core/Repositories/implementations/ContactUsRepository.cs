using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.implementations
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private readonly HotelMgtDbContext _context;
        private readonly DbSet<ContactUs> _dbSet;
        public ContactUsRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ContactUs>();
        }
    }
}
