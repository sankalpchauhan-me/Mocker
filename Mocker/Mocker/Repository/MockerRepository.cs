using DBLib.AppDBContext;
using DBLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mocker.Repository
{
    public class MockerRepository
    {
        private MockDBContext _context;

        public MockerRepository()
        {
            _context = new MockDBContext();
        }

        public MockerRepository(MockDBContext context)
        {
            _context = context;
        }

        public List<Developer> GetAllInfo()
        {
            return _context.Developers.Include("DevApps").ToList();
        }
    }
}