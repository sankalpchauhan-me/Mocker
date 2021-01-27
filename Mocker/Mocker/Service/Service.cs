using DataInteractionLayer.UnitofWork;
using Mocker.DTOs;
using Mocker.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mocker.Service
{
    public class Service
    {
        protected readonly UnitOfWork _unitOfWork;
        public Service()
        {
            _unitOfWork = new UnitOfWork(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
        }

        public virtual AppEntityDTO GetAppEntity(string devId, string appName, string entityName)
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

        public virtual  DeveloperDTO GetDeveloperById(string id)
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

        public virtual DevAppDTO GetDevAppById(string devId, string appName)
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