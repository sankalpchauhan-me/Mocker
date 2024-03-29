﻿using DataInteractionLayer.UnitofWork;
using DBModels.Models;
using Mocker.DTOs;
using Mocker.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mocker.Service
{
    public class MainService : Service
    {

        public MainService() : base()
        {
        }

        public List<DeveloperDTO> GetAll()
        {
            try
            {
                List<Developer> fulldata = _unitOfWork.DeveloperRepository.GetWithInclude()
                    .Include(d => d.DevApps).Where(d => d.DeactivationFlag.Equals(false))
                    .Include(d => d.DevApps.Select(o => o.AppEntitiys))
                    .Include(d => d.DevApps.Select(o => o.AppEntitiys.Select(e => e.EntityFields))).ToList();
                List < DeveloperDTO > developerDTO = new List<DeveloperDTO>();
                foreach (Developer d in fulldata)
                {
                    developerDTO.Add(d);
                }
                return developerDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}