using DBLib.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLib.Models
{
    public class EntityField
    {
        /// <summary>
        /// Getter and Setter for Feild Id
        /// </summary>
        [Key]
        public int FieldId { get; set; }

        /// <summary>
        /// Getter and Setter for Field Name
        /// </summary>
        [Required]
        public string FieldName { get; set; }

        /// <summary>
        /// Getter and Setter for Feild Type
        /// </summary>
        [Required]
        public string FieldType { get; set; }

        /// <summary>
        /// Getter and Setter for EntityId
        /// Foreign Key Reference to @AppEntity
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Must have a AppEntity, Dependent on AppEntity
        /// </summary>
        [ForeignKey(Constants.FK_ENTITY_FIELD)]
        public AppEntity AppEntitiy { get; set; }
    }
}
