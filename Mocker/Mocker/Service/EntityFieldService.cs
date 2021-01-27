using DataInteractionLayer.UnitofWork;
using DBLib.Models;
using Mocker.DTOs;
using Mocker.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mocker.Service
{
    public class EntityFieldService
    {

        private readonly UnitOfWork _unitOfWork;

        public EntityFieldService()
        {
            _unitOfWork = new UnitOfWork(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
        }

        //Create
        public EntityFieldDTO InsertEntityField(string devId, string appName, string entityName, EntityField entityField)
        {
            EntityFieldDTO dto = new EntityFieldDTO();
            AppEntityDTO app = GetAppEntity(devId, appName, entityName);
            try
            {
                entityField.EntityId = app.EntityId;
                EntityField check = _unitOfWork.FieldRepository.GetWithInclude().Where(d => d.FieldName.Equals(entityField.FieldName)).Where(d => d.EntityId.Equals(app.EntityId)).FirstOrDefault();
                if (check != null)
                    return null;
                dto = _unitOfWork.FieldRepository.Insert(entityField);
                _unitOfWork.Save();
            } catch(Exception e)
            {
                throw e;
            }
            return dto;
        }

        //Read
        public EntityFieldDTO GetEntityField(string devId, string appName, string entityName, string fieldName)
        {

            AppEntityDTO app = GetAppEntity(devId, appName, entityName);
            try
            {
                if (app != null)
                {
                    return _unitOfWork.FieldRepository.GetWithInclude().Where(d => d.DeactivationFlag.Equals(false))
                        .Where(d => d.EntityId == app.EntityId).Where(d => d.FieldName.Equals(fieldName))
                        .FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return null;

        }

        //Update
        public bool UpdateEntityField(string devId, string appName, string entityName, string fieldName, EntityField entityField)
        {
            EntityFieldDTO ef = GetEntityField(devId, appName, entityName, fieldName);
            if (ef != null)
            {
                try
                {
                    //Prevent user from changing references
                    entityField.FieldId = ef.FieldId;
                    entityField.EntityId = ef.EntityId;
                    _unitOfWork.FieldRepository.Update(entityField);
                    _unitOfWork.Save();
                    return true;
                } catch(Exception e)
                {
                    throw e;
                }
            }
            return false;
        }

        //Delete
        public EntityFieldDTO DeleteEntityField(string devId, string appName, string entityName, string fieldName)
        {
            EntityFieldDTO dto = new EntityFieldDTO();
            AppEntityDTO da = GetAppEntity(devId, appName, entityName);
            try
            {
                EntityField entityField = _unitOfWork.FieldRepository.GetWithInclude().Where(d => d.EntityId == da.EntityId).Where(d => d.FieldName.Equals(fieldName)).FirstOrDefault();
                if (entityField != null)
                {
                    dto = _unitOfWork.FieldRepository.Delete(entityField);
                    _unitOfWork.Save();
                    return dto;
                }
            } catch(Exception e)
            {
                throw e;
            }
            return null;
        }

        //Activation
        public bool SetEntityFieldActive(string devId, string appName, string entityName, string fieldName, bool val)
        {
            AppEntityDTO da = GetAppEntity(devId, appName, entityName);
            EntityField ef = _unitOfWork.FieldRepository.GetWithInclude().Where(d => d.EntityId == da.EntityId).Where(d => d.FieldName.Equals(fieldName)).FirstOrDefault();
            try
            {
                if (ef != null)
                {
                    ef.DeactivationFlag = val;
                    _unitOfWork.FieldRepository.Update(ef);
                    _unitOfWork.Save();
                    return true;
                }
            } catch(Exception e)
            {
                throw e;
            }
            return false;
        }

        //Helpers
        private AppEntityDTO GetAppEntity(string devId, string appName, string entityName)
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

        private DeveloperDTO GetDeveloperById(string id)
        {
            DeveloperDTO dto = new DeveloperDTO();
            try
            {
                dto = _unitOfWork.DeveloperRepository.GetWithInclude()
                     .Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false))
                     .Include(d => d.DevApps.Select(o => o.AppEntitiys))
                     .Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields)))
                     .Where(d => d.UserId.Equals(id)).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dto;
        }

        private DevAppDTO GetDevAppById(string devId, string appName)
        {
            DevAppDTO dto = new DevAppDTO();
            try
            {
                DeveloperDTO dev = GetDeveloperById(devId);
                if (dev != null)
                {
                    return _unitOfWork.AppRepository.GetWithInclude().Include(d => d.AppEntitiys).Where(d => d.DeactivationFlag.Equals(false)).Where(d => d.DevId == dev.DevId)
                        .Include(d => d.AppEntitiys.Select(o => o.EntityFields)).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}