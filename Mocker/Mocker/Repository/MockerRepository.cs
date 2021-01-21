﻿using DBLib.Adapter;
using DBLib.AppDBContext;
using DBLib.Models;
using Mocker.Utils;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            return _context.Developers.Include(d => d.DevApps).Where(d=>d.DeactivationFlag.Equals(false)).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).ToList();
        }

        public Developer InsertDev(Developer d)
        {
            var entity = _context.Developers.Add(d);
            return entity;
        }

        public Developer DeleteDev(string id)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            _context.Developers.Remove(dev);
            return dev;
        }

        public Developer GetDeveloperById(string id)
        {
            return _context.Developers.Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false)).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).Where(d => d.UserId.Equals(id)).FirstOrDefault();
        }

        public Developer DeleteDeveloperById(string id)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            return _context.Developers.Remove(dev);
        }

        public bool SetDeveloperActive(string id, bool val)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            dev.DeactivationFlag = val;

            //Non Generic
            if (dev != null && _context.GetType().Equals(typeof(MockSQLContext)))
            {
                ((MockSQLContext)_context).Set<Developer>().AddOrUpdate(dev);
                return true;
            }else{
                return false;
            }
        }
        //Refactor
        public bool UpdateDeveloper(string id, Developer developer)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            //Non Generic
            if (dev != null && _context.GetType().Equals(typeof(MockSQLContext)))
            {
                //Prevent user from changing the user id
                developer.UserId = dev.UserId;
                ((MockSQLContext)_context).Set<Developer>().AddOrUpdate(developer);
                foreach (DevApp devApp in developer.DevApps)
                {
                    ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(devApp);
                    foreach (AppEntity appEntity in devApp.AppEntitiys)
                    {
                        ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(appEntity);

                        foreach (EntityField entityField in appEntity.EntityFields)
                        {
                            ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(entityField);
                        }
                    }
                }
                return true;
            }

            return false;
        }

        public bool Save()
        {
            int numberOfEntries = _context.SaveChanges();
            return numberOfEntries != 0 ? true : false;
        }
    }
}