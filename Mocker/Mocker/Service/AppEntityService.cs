using DataInteractionLayer.UnitofWork;
using DBModels.Models;
using Mocker.DTOs;
using Mocker.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace Mocker.Service
{
    public class AppEntityService : Service
    {

        public AppEntityService(): base()
        {
        }

        //Create
        public AppEntityDTO InsertAppEntity(string devId, string appName, AppEntity appEntity)
        {
            AppEntityDTO dto = new AppEntityDTO();
            DevAppDTO app = GetDevAppById(devId, appName);
            try
            {
                appEntity.AppId = app.AppId;
                AppEntity check = _unitOfWork.EntityRepository.GetWithInclude().Where(d => d.EntityName.Equals(appEntity.EntityName)).Where(d => d.AppId.Equals(app.AppId)).FirstOrDefault();
                if (check != null)
                    return null;
                dto = _unitOfWork.EntityRepository.Insert(appEntity);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
            return dto;
        }

        //Read
        public override AppEntityDTO GetAppEntity(string devId, string appName, string entityName)
        {

            DevAppDTO app = GetDevAppById(devId, appName);
            try
            {
                if (app != null)
                {
                    return _unitOfWork.EntityRepository.GetWithInclude().Where(d => d.DeactivationFlag.Equals(false)).Where(d => d.AppId == app.AppId).Where(d => d.EntityName.Equals(entityName))
                        .Include(o => o.EntityFields).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;

        }

        //Update
        public bool UpdateAppEntity(string devId, string appName, string entityName, AppEntity appEntity)
        {
            AppEntityDTO ae = GetAppEntity(devId, appName, entityName);
            try
            {
                if (ae != null)
                {
                    //Prevent user from changing references
                    appEntity.AppId = ae.AppId;
                    appEntity.EntityId = ae.EntityId;

                    _unitOfWork.EntityRepository.Update(appEntity);
                    foreach (EntityField ef in appEntity.EntityFields)
                    {
                        _unitOfWork.FieldRepository.Update(ef);
                    }
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        //Delete
        public AppEntityDTO DeleteAppEntity(string devId, string appName, string entityName)
        {
            AppEntityDTO dto = new AppEntityDTO();
            DevAppDTO da = GetDevAppById(devId, appName);
            AppEntity appEntity = _unitOfWork.EntityRepository.GetWithInclude().Where(d => d.AppId == da.AppId).Where(d => d.EntityName.Equals(entityName)).FirstOrDefault();
            if (appEntity != null)
            {
                dto = _unitOfWork.EntityRepository.Delete(appEntity);
                _unitOfWork.Save();
            }
            return dto;
        }

        //Activate
        public bool SetAppEntityActive(string devId, string appName, string entityName, bool val)
        {
            DevAppDTO da = GetDevAppById(devId, appName);
            try
            {
                AppEntity ae = _unitOfWork.EntityRepository.GetWithInclude().Where(d => d.AppId == da.AppId).Where(d => d.EntityName.Equals(entityName)).FirstOrDefault();
                if (ae != null)
                {
                    ae.DeactivationFlag = val;
                    _unitOfWork.EntityRepository.Update(ae);
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
    }
}