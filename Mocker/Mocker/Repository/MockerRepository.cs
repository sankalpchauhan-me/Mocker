﻿using DBLib.AppDBContext;
using DBLib.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            //return _context.Developers.Include("DevApps").Include("DevApps.AppEntitiys").Include("DevApps.AppEntitiys.EntityFields").ToList();
            return _context.Developers.Include(d => d.DevApps).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).ToList();
        }
    }
}