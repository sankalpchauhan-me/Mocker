using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FieldName { get; set; }

        /// <summary>
        /// Getter and Setter for Feild Type
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// Getter and Setter for EntityId
        /// Foreign Key Reference to @AppEntity
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Must have a AppEntity, Dependent on AppEntity
        /// </summary>
        [ForeignKey("EntityId")]
        public AppEntity AppEntitiy { get; set; }
    }
}
