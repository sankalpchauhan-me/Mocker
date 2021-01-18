using DBLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mocker.ViewModels
{
    public class AppEntityViewModel
    {

        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int AppId { get; set; }


        public List<EntityFieldViewModel> EntityFields { get; set; }

        public static implicit operator AppEntityViewModel(AppEntity v)
        {
            return new AppEntityViewModel
            {
                EntityId = v.EntityId,
                EntityName = v.EntityName,
                AppId = v.AppId,
                EntityFields = (List<EntityFieldViewModel>)v.EntityFields

            };
        }
    }
}