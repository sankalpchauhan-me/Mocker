using DBLib.Models;
using System.Collections.Generic;

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
            List<EntityFieldViewModel> entityFieldViewModels = new List<EntityFieldViewModel>();
            foreach (EntityField d in v.EntityFields)
            {
                entityFieldViewModels.Add(d);
            }
            return new AppEntityViewModel
            {
                EntityId = v.EntityId,
                EntityName = v.EntityName,
                AppId = v.AppId,
                EntityFields = entityFieldViewModels
            };
        }
    }
}