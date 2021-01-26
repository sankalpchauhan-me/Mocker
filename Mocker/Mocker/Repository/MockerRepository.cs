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
    //TODO: Modularize
    public class MockerRepository
    {
        private DBAdapter _context;

        public MockerRepository()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["env"].Equals("dev"))
                _context = new MockSQLContext(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
            else if(System.Configuration.ConfigurationManager.AppSettings["env"].Equals("test"))
                _context = new MockSQLContext(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING_TEST].ConnectionString);
        }

        public MockerRepository(DBAdapter context)
        {
            _context = context;
        }

        public List<Developer> GetAllInfo()
        {
            return _context.Developers.Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false))
                .Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).ToList();
        }

        //Developers

        ////Create
        //public Developer InsertDev(Developer d)
        //{
        //    var entity = _context.Developers.Add(d);
        //    return entity;
        //}

        ////Read
        public Developer GetDeveloperById(string id)
        {
            return _context.Developers.Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false))
                .Include(d => d.DevApps.Select(o => o.AppEntitiys)).Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields)))
                .Where(d => d.UserId.Equals(id)).FirstOrDefault();
        }

        ////Delete
        //public Developer DeleteDeveloperById(string id)
        //{
        //    Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
        //    if (dev != null)
        //    {
        //        return _context.Developers.Remove(dev);
        //    }
        //    return null;
        //}

        //public bool SetDeveloperActive(string id, bool val)
        //{
        //    Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
        //    dev.DeactivationFlag = val;

        //    //Non Generic
        //    if (dev != null && _context.GetType().Equals(typeof(MockSQLContext)))
        //    {
        //        ((MockSQLContext)_context).Set<Developer>().AddOrUpdate(dev);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ////Update
        //// TODO: Refactor
        //public bool UpdateDeveloper(string id, Developer developer)
        //{
        //    Developer dev = _context.Developers.Where(d => d.UserId.Equals(id)).FirstOrDefault();
        //    // TODO: Make Generic 
        //    //Non Generic
        //    if (dev != null && _context.GetType().Equals(typeof(MockSQLContext)))
        //    {
        //        //Prevent user from changing the user id
        //        developer.UserId = dev.UserId;
        //        ((MockSQLContext)_context).Set<Developer>().AddOrUpdate(developer);
        //        foreach (DevApp devApp in developer.DevApps)
        //        {
        //            ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(devApp);
        //            foreach (AppEntity appEntity in devApp.AppEntitiys)
        //            {
        //                ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(appEntity);

        //                foreach (EntityField entityField in appEntity.EntityFields)
        //                {
        //                    ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(entityField);
        //                }
        //            }
        //        }
        //        return true;
        //    }

        //    return false;
        //}

        //Dev Apps

        //Create
        //public DevApp InsertDevApp(string devId, DevApp devApp)
        //{
        //    Developer dev = GetDeveloperById(devId);
        //    devApp.DevId = dev.DevId;
        //    return _context.DevApps.Add(devApp);
        //}
        ////Read
        public DevApp GetDevApp(string devId, string appName)
        {
            Developer dev = GetDeveloperById(devId);
            if (dev != null)
            {
                return _context.DevApps.Include(d => d.AppEntitiys).Where(d => d.DeactivationFlag.Equals(false)).Where(d => d.DevId == dev.DevId)
                    .Include(d => d.AppEntitiys.Select(o => o.EntityFields)).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
            }
            return null;
        }

        ////Update
        //// TODO: Make Generic & Refactor
        //public bool UpdateDevApp(string devId, string appName, DevApp devApp)
        //{
        //    Developer dev = GetDeveloperById(devId);
        //    DevApp da = _context.DevApps.Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
        //    if (da != null)
        //    {
        //        //Prevent user from changing app id and foreign key while updating
        //        devApp.DevId = da.DevId;
        //        devApp.AppId = da.AppId;
        //        ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(devApp);
        //        foreach (AppEntity appEntity in devApp.AppEntitiys)
        //        {
        //            ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(appEntity);

        //            foreach (EntityField entityField in appEntity.EntityFields)
        //            {
        //                ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(entityField);
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        ////Delete
        //public DevApp DeleteDevApp(string devId, string appName)
        //{
        //    DevApp devApp = GetDevApp(devId, appName);
        //    if (devApp != null)
        //    {
        //        return _context.DevApps.Remove(devApp);
        //    }
        //    return null;
        //}

        ////Activate
        //public bool SetDevAppActive(string devId, string appName, bool val)
        //{
        //    Developer dev = GetDeveloperById(devId);
        //    DevApp da = _context.DevApps.Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
        //    da.DeactivationFlag = val;
        //    // TODO: Make Generic
        //    //Non Generic
        //    if (da != null && _context.GetType().Equals(typeof(MockSQLContext)))
        //    {
        //        ((MockSQLContext)_context).Set<DevApp>().AddOrUpdate(da);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //AppEntity

        //Create
        //public AppEntity InsertAppEntity(string devId, string appName, AppEntity appEntity)
        //{
        //    //Alternative for Candidate Key
        //    // Here we will first check wether we have an entity that is residing inside the app with the same name, if yes we will not proceed
        //    DevApp app = GetDevApp(devId, appName);
        //    appEntity.AppId = app.AppId;
        //    AppEntity check = _context.AppEntitiys.Where(d => d.EntityName.Equals(appEntity.EntityName)).Where(d => d.AppId.Equals(app.AppId)).FirstOrDefault();
        //    if (check != null)
        //        return null;
        //    return _context.AppEntitiys.Add(appEntity);
        //}

        //Read
        public AppEntity GetAppEntity(string devId, string appName, string entityName)
        {
            // DONE:
            //ENTITYNAME IS NOT UNIQUE (Candidate Key?)
            //Edge Case: Same Entity Name and AppId inside Table could collide
            //Sankalp: Solved in @InsertAppEntity 

            DevApp app = GetDevApp(devId, appName);
            if (app != null)
            {
                return _context.AppEntitiys.Where(d => d.DeactivationFlag.Equals(false)).Where(d => d.AppId == app.AppId).Where(d => d.EntityName.Equals(entityName))
                    .Include(o => o.EntityFields).FirstOrDefault();
            }
            return null;

        }

        //Update
        //public bool UpdateAppEntity(string devId, string appName, string entityName, AppEntity appEntity)
        //{
        //    AppEntity ae = GetAppEntity(devId, appName, entityName);
        //    if (ae != null)
        //    {
        //        //Prevent user from changing references
        //        appEntity.AppId = ae.AppId;
        //        appEntity.EntityId = ae.EntityId;

        //        ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(appEntity);
        //        foreach (EntityField ef in appEntity.EntityFields)
        //        {
        //            ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(ef);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        ////Delete
        //public AppEntity DeleteAppEntity(string devId, string appName, string entityName)
        //{
        //    AppEntity appEntity = GetAppEntity(devId, appName, entityName);
        //    if (appEntity != null)
        //    {
        //        return _context.AppEntitiys.Remove(appEntity);
        //    }
        //    return null;
        //}

        ////Activate
        //public bool SetAppEntityActive(string devId, string appName, string entityName, bool val)
        //{
        //    DevApp da = GetDevApp(devId, appName);
        //    AppEntity ae = _context.AppEntitiys.Where(d => d.AppId == da.AppId).Where(d => d.EntityName.Equals(entityName)).FirstOrDefault();
        //    //Non Generic
        //    if (ae != null && _context.GetType().Equals(typeof(MockSQLContext)))
        //    {
        //        ae.DeactivationFlag = val;
        //        ((MockSQLContext)_context).Set<AppEntity>().AddOrUpdate(ae);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        // EntityFields

        //Create
        public EntityField InsertEntityField(string devId, string appName, string entityName, EntityField entityField)
        {
            //Alternative for Candidate Key
            // Here we will first check wether we have a field that is residing inside this AppEntity with the same name, if yes we will not proceed
            AppEntity app = GetAppEntity(devId, appName, entityName);
            entityField.EntityId = app.EntityId;
            EntityField check = _context.EntityFields.Where(d => d.FieldName.Equals(entityField.FieldName)).Where(d => d.EntityId.Equals(app.EntityId)).FirstOrDefault();
            if (check != null)
                return null;
            return _context.EntityFields.Add(entityField);
        }

        //Read
        public EntityField GetEntityField(string devId, string appName, string entityName, string fieldName)
        {
            // DONE:
            //WARNING: ENTITYFIELDNAME IS NOT UNIQUE (Candidate Key?)
            //Edge Case: Same Field Name and EntityId inside Table could collide
            //Sankalp: Solved in @InsertEntityField

            AppEntity app = GetAppEntity(devId, appName, entityName);
            if (app != null)
            {
                return _context.EntityFields.Where(d => d.DeactivationFlag.Equals(false))
                    .Where(d => d.EntityId == app.EntityId).Where(d => d.FieldName.Equals(fieldName))
                    .FirstOrDefault();
            }
            return null;

        }

        //Update
        public bool UpdateEntityField(string devId, string appName, string entityName, string fieldName, EntityField entityField)
        {
            EntityField ef = GetEntityField(devId, appName, entityName, fieldName);
            if (ef != null)
            {
                //Prevent user from changing references
                entityField.FieldId = ef.FieldId;
                entityField.EntityId = ef.EntityId;

                ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(entityField);
                return true;
            }
            return false;
        }

        //Delete
        public EntityField DeleteEntityField(string devId, string appName, string entityName, string fieldName)
        {
            EntityField entityField = GetEntityField(devId, appName, entityName, fieldName);
            if (entityField != null)
            {
                return _context.EntityFields.Remove(entityField);
            }
            return null;
        }

        //Activation
        public bool SetEntityFieldActive(string devId, string appName, string entityName, string fieldName, bool val)
        {
            AppEntity da = GetAppEntity(devId, appName, entityName);
            EntityField ef = _context.EntityFields.Where(d => d.EntityId == da.EntityId).Where(d => d.FieldName.Equals(fieldName)).FirstOrDefault();
            //Non Generic
            if (ef != null && _context.GetType().Equals(typeof(MockSQLContext)))
            {
                ef.DeactivationFlag = val;
                ((MockSQLContext)_context).Set<EntityField>().AddOrUpdate(ef);
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