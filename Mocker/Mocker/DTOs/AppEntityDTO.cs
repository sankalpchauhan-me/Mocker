using DBLib.Models;
using System.Collections.Generic;

namespace Mocker.DTOs
{
    public class AppEntityDTO
    {

        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int AppId { get; set; }


        public List<EntityFieldDTO> EntityFields { get; set; }

        public static implicit operator AppEntityDTO(AppEntity v)
        {

            List<EntityFieldDTO> entityFields = new List<EntityFieldDTO>();
            if (v.EntityFields != null)
            {
                foreach (EntityField d in v.EntityFields)
                {
                    entityFields.Add(d);
                }
            }
            return new AppEntityDTO
            {
                EntityId = v.EntityId,
                EntityName = v.EntityName,
                AppId = v.AppId,
                EntityFields = entityFields
            };
        }
    }
}