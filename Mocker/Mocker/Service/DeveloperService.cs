using DBLib.Models;
using Mocker.DTOs;
using Mocker.UnitofWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace Mocker.Service
{
    public class DeveloperService
    {
        private readonly UnitOfWork _unitOfWork;

        public DeveloperService()
        {
            _unitOfWork = new UnitOfWork();
        }

        //Create
        public DeveloperDTO InsertDev(Developer dev)
        {
            DeveloperDTO dto = new DeveloperDTO();
            try
            {
                //using (var scope = new TransactionScope())
                dto = _unitOfWork.DeveloperRepository.Insert(dev);
                _unitOfWork.Save();
                // scope.complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dto;
        }

        //Read
        public DeveloperDTO GetDeveloperById(string id)
        {
            DeveloperDTO dto = new DeveloperDTO();
            try
            {
               dto =  _unitOfWork.DeveloperRepository.GetWithInclude()
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

        //Update
        public bool UpdateDeveloper(string id, Developer developer)
        {
            try
            {
                Developer dev = _unitOfWork.DeveloperRepository.Get(d => d.UserId.Equals(id));
                if(dev!=null)
                {
                    developer.UserId = dev.UserId;
                    _unitOfWork.DeveloperRepository.Update(developer);
                    foreach (DevApp devApp in developer.DevApps)
                    {
                        _unitOfWork.AppRepository.Update(devApp);
                        foreach (AppEntity appEntity in devApp.AppEntitiys)
                        {
                            _unitOfWork.EntityRepository.Update(appEntity);

                            foreach (EntityField entityField in appEntity.EntityFields)
                            {
                                _unitOfWork.FieldRepository.Update(entityField);
                            }
                        }
                    }
                    _unitOfWork.Save();
                    return true;
                }
            } catch(Exception e)
            {
                throw e;
            }
            return false;
        }

        //Delete
        public DeveloperDTO DeleteDeveloper(string id)
        {
            DeveloperDTO dto = new DeveloperDTO();
            try
            {
                Developer developer = _unitOfWork.DeveloperRepository.GetWithInclude().Where(d => d.UserId.Equals(id)).FirstOrDefault();
                if (developer != null)
                {
                     _unitOfWork.DeveloperRepository.Delete(developer);
                    _unitOfWork.Save();
                }

            } catch(Exception ex)
            {
                throw ex;
            }
            return dto;
        }


        //Activate
        public bool SetDeveloperActive(string id, bool val)
        {
            Developer dev = _unitOfWork.DeveloperRepository.GetWithInclude().Where(d => d.UserId.Equals(id)).FirstOrDefault();
            dev.DeactivationFlag = val;

            if (dev != null)
            {
                _unitOfWork.DeveloperRepository.Update(dev);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}