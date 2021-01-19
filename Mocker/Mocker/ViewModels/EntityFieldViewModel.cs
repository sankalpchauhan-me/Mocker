using DBLib.Models;

namespace Mocker.ViewModels
{
    public class EntityFieldViewModel
    {

        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int EntityId { get; set; }

        public static implicit operator EntityFieldViewModel(EntityField v)
        {
            if (v == null)
                return null;

            return new EntityFieldViewModel
            {
                FieldId = v.FieldId,
                FieldName = v.FieldName,
                FieldType = v.FieldType,
                EntityId = v.EntityId
            };
        }
    }
}