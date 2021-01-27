﻿using DBLib.Adapter;
using DBLib.AppDBContext;
using DBLib.Models;
using Mocker.Repository;
using Mocker.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Mocker.UnitofWork
{
    public class UnitOfWork
    {
        //private DBAdapter _context = null;
        //private DbRepository<Developer> _devRepository;
        //private DbRepository<DevApp> _appRepository;
        //private DbRepository<AppEntity> _entityRepository;
        //private DbRepository<EntityField> _fieldRepository;

        //public UnitOfWork()
        //{
        //    _context = new MockSQLContext(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
        //}

        ///// <summary>  
        ///// Get/Set Property for developer repository.  
        ///// </summary>  
        //public DbRepository<Developer> DeveloperRepository
        //{
        //    get
        //    {
        //        if (this._devRepository == null)
        //            this._devRepository = new DbRepository<Developer>(_context);
        //        return _devRepository;
        //    }
        //}

        ///// <summary>  
        ///// Get/Set Property for app repository.  
        ///// </summary>  
        //public DbRepository<DevApp> AppRepository
        //{
        //    get
        //    {
        //        if (this._appRepository == null)
        //            this._appRepository = new DbRepository<DevApp>(_context);
        //        return _appRepository;
        //    }
        //}

        ///// <summary>  
        ///// Get/Set Property for Entity repository.  
        ///// </summary>  
        //public DbRepository<AppEntity> EntityRepository
        //{
        //    get
        //    {
        //        if (this._entityRepository == null)
        //            this._entityRepository = new DbRepository<AppEntity>(_context);
        //        return _entityRepository;
        //    }
        //}

        //public DbRepository<EntityField> FieldRepository
        //{
        //    get
        //    {
        //        if (this._fieldRepository == null)
        //            this._fieldRepository = new DbRepository<EntityField>(_context);
        //        return _fieldRepository;
        //    }
        //}

        //public void Save()
        //{
        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {

        //        var outputLines = new List<string>();
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //            }
        //        }
        //        System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

        //        throw e;
        //    }

        //}

       
    }
}