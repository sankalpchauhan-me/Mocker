using DBLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            foreach(DevApp d in v.DevApps)
            {
                DevAppViewModel devAppViewModel = new DevAppViewModel();
                devAppViewModel = d;
                devApps.Add(devAppViewModel);
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