﻿using DBLib.Adapter;
using DBLib.AppDBContext;
using DBLib.Models;
using Mocker.Utils;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Mocker.Repository
{
    public class MockerRepository
    {
        private DBAdapter _context;

        public MockerRepository()
        {
            _context = new MockSQLContext(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
        }

        public MockerRepository(DBAdapter context)
        {
            _context = context;
        }

        public List<Developer> GetAllInfo()
        {
            return _context.Developers.Include(d => d.DevApps).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).ToList();
        }

        public Developer InsertDev(Developer d)
        {
            var entity = _context.Developers.Add(d);
            return entity;
        }

        public Developer GetDeveloperById(string id)
        {
            return _context.Developers.Include(d => d.DevApps).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).Where(d => d.UserId.Equals(id)).FirstOrDefault();
        }

        public Developer DeleteDeveloperById(string id)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            return _context.Developers.Remove(dev);
        }

        public bool Save()
        {
            int numberOfEntries =_context.SaveChanges();
            return numberOfEntries != 0 ? true : false;
        }
    }
}