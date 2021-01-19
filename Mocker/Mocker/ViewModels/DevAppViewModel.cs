﻿using DBLib.Models;
using System.Collections.Generic;

namespace Mocker.ViewModels
{
    public class DevAppViewModel
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public int DevId { get; set; }
        public virtual List<AppEntityViewModel> AppEntitiys { get; set; }

        public static implicit operator DevAppViewModel(DevApp v)
        {
            if (v == null)
                return null;

            List<AppEntityViewModel> appEntityViewModels = new List<AppEntityViewModel>();
            foreach (AppEntity d in v.AppEntitiys)
            {
                appEntityViewModels.Add(d);
            }
            return new DevAppViewModel
            {
                AppId = v.AppId,
                AppName = v.AppName,
                DevId = v.DevId,
                AppEntitiys = appEntityViewModels
            };
        }
    }
}