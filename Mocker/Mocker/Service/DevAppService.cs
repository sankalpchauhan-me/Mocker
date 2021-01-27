using DataInteractionLayer.UnitofWork;
using DBModels.Models;
using Mocker.DTOs;
using Mocker.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace Mocker.Service
{
    public class DevAppService : Service
    {

        public DevAppService() : base()
        {
        }

        //Create
        public DevAppDTO InsertDevApp(string devId, DevApp devApp)
        {
            DevAppDTO dto = new DevAppDTO();
            try
            {
                DeveloperDTO devDTO = GetDeveloperById(devId);
                devApp.DevId = devDTO.DevId;
                dto = _unitOfWork.AppRepository.Insert(devApp);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dto;
        }

        //Read
        public override DevAppDTO GetDevAppById(string devId, string appName)
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

        //Update
        public bool UpdateDevApp(string devId, string appName, DevApp devApp)
        {
            try
            {
                DeveloperDTO dev = GetDeveloperById(devId);
                DevApp da = _unitOfWork.AppRepository.GetWithInclude().Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
                if (da != null)
                {
                    //Prevent user from changing app id and foreign key while updating
                    devApp.DevId = da.DevId;
                    devApp.AppId = da.AppId;
                    _unitOfWork.AppRepository.Update(devApp);
                    foreach (AppEntity appEntity in devApp.AppEntitiys)
                    {
                        _unitOfWork.EntityRepository.Update(appEntity);

                        foreach (EntityField entityField in appEntity.EntityFields)
                        {
                            _unitOfWork.FieldRepository.Update(entityField);
                        }
                    }
                    _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Delete
        public DevAppDTO DeleteDevApp(string devId, string appName)
        {
            DeveloperDTO dev = GetDeveloperById(devId);
            try
            {
                DevApp devApp = _unitOfWork.AppRepository.GetWithInclude().Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
                if (devApp != null)
                {
                    DevAppDTO dto =  _unitOfWork.AppRepository.Delete(devApp);
                    _unitOfWork.Save();
                    return dto;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Activate
        public bool SetDevAppActive(string devId, string appName, bool val)
        {
            DeveloperDTO dev = GetDeveloperById(devId);
            DevApp da = _unitOfWork.AppRepository.GetWithInclude().Where(d => d.DevId == dev.DevId).Where(d => d.AppName.Equals(appName)).FirstOrDefault();
            if (da != null)
            {
                da.DeactivationFlag = val;
                _unitOfWork.AppRepository.Update(da);
                _unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}