﻿using DBLib.Models;
using System.Collections.Generic;

namespace Mocker.ViewModels
{
    public class DeveloperViewModel
    {

        public int DevId { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public virtual List<DevAppViewModel> DevApps { get; set; }

        public static implicit operator DeveloperViewModel(Developer v)
        {
            List<DevAppViewModel> devApps = new List<DevAppViewModel>();
            foreach (DevApp d in v.DevApps)
            {
                devApps.Add(d);
            }
            return new DeveloperViewModel
            {
                DevId = v.DevId,
                FullName = v.FullName,
                UserId = v.UserId,
                DevApps = devApps
            };
        }
    }
}