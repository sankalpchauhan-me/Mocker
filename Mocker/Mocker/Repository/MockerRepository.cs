using DBLib.Adapter;
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

        //Developers

        //Create
        public Developer InsertDev(Developer d)
        {
            var entity = _context.Developers.Add(d);
            return entity;
        }

        //Read
        public Developer GetDeveloperById(string id)
        {
            return _context.Developers.Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false)).Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).Where(d => d.UserId.Equals(id)).FirstOrDefault();
        }

        //Delete
        public Developer DeleteDeveloperById(string id)
        {
            Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
            if (dev != null)
            {
                return _context.Developers.Remove(dev);
            }
            return null;
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

        //Update
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

        //Dev Apps

        //Create
        public DevApp InsertDevApp(string devId, DevApp devApp)
        {
            Developer dev = GetDeveloperById(devId);
            devApp.DevId = dev.DevId;
            return _context.DevApps.Add(devApp);
        }
        //Read
        public DevApp GetDevApp(string devId, string appName)
        {
            Developer dev = GetDeveloperById(devId);
            return _context.DevApps.Include(d => d.AppEntitiys).Where(d => d.DeactivationFlag.Equals(false)).Where(d => d.DevId == dev.DevId).Include(d => d.AppEntitiys.Select(o => o.EntityFields)).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
        }

        //Update
        public bool UpdateDevApp(string devId, string appName, DevApp devApp)
        {
            Developer dev = GetDeveloperById(devId);
            DevApp da = _context.DevApps.Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
            if (da != null)
            {
                //Prevent user from changing app id while updating
                devApp.DevId = da.DevId;
                devApp.AppId = da.AppId;
                ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(devApp);
                foreach (AppEntity appEntity in devApp.AppEntitiys)
                {
                    ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(appEntity);

                    foreach (EntityField entityField in appEntity.EntityFields)
                    {
                        ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(entityField);
                    }
                }
                return true;
            }
            return false;
        }

        //Delete
        public DevApp DeleteDevApp(string devId, string appName)
        {
            DevApp devApp = GetDevApp(devId, appName);
            if (devApp != null)
            {
                return _context.DevApps.Remove(devApp);
            }
            return null;
        }

        //Activate
        public bool SetDevAppActive(string devId, string appName, bool val)
        {
            Developer dev = GetDeveloperById(devId);
            DevApp da = _context.DevApps.Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
            da.DeactivationFlag = val;
            //Non Generic
            if (da != null && _context.GetType().Equals(typeof(MockSQLContext)))
            {
                ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(da);
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Save()
        {
            int numberOfEntries = _context.SaveChanges();
            return numberOfEntries != 0 ? true : false;
        }

        
    }
}