using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBModels.Models
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
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
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

        public bool DeactivationFlag { get; set; }

        /// <summary>
        /// Must have a AppEntity, Dependent on AppEntity
        /// </summary>
        [ForeignKey(ModelConstants.FK_ENTITY_FIELD)]
        public AppEntity AppEntitiy { get; set; }
    }
}
