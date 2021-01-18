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
        public virtual ICollection<DevAppViewModel> DevApps { get; set; }

        public static implicit operator DeveloperViewModel(Developer v)
        {
            return new DeveloperViewModel
            {
                DevId = v.DevId,
                FullName = v.FullName,
                UserId = v.UserId,
                DevApps = (ICollection<DevAppViewModel>)v.DevApps
            };
        }
    }
}