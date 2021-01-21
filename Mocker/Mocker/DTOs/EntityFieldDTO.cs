using DBLib.Models;

namespace Mocker.DTOs
{
    public class EntityFieldDTO
    {

        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int EntityId { get; set; }


        public static implicit operator EntityFieldDTO(EntityField v)
        {
            if (v == null)
                return null;

            return new EntityFieldDTO
            {
                FieldId = v.FieldId,
                FieldName = v.FieldName,
                FieldType = v.FieldType,
                EntityId = v.EntityId
            };
        }
    }
}