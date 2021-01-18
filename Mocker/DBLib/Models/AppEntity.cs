using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class AppEntity
    {

        /// <summary>
        /// Getter and Setter for Entity Id
        /// </summary>
        [Key]
        public int EntityId { get; set; }

        /// <summary>
        /// Getter and Setter for Entity Name
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Getter and Setter for App Id
        /// Foreign Key Reference to @DevApp
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// Must have a DevApp, Dependent on DevApp
        /// </summary>
        [ForeignKey("AppId")]
        public DevApp DevApp { get; set; }

        /// <summary>
        /// Collection of EntityFields
        /// </summary>
        public ICollection<EntityField> EntityFields {get; set;}
    }
}
