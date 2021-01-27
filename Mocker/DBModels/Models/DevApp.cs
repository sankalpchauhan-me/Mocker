using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.Models
{
    public class DevApp
    {
        /// <summary>
        /// Getter and Setter for App Id
        /// </summary>
        [Key]
        public int AppId { get; set; }

        /// <summary>
        /// Getter and Setter for App Name
        /// </summary>
        [StringLength(150, ErrorMessage = "AppName<=150")]
        [Index(IsUnique = true)]
        public string AppName { get; set; }

        /// <summary>
        /// Getter and Setter for DevId
        /// Foreign Key Reference to @Developer
        /// </summary>
        public int DevId { get; set; }

        /// <summary>
        /// Must have a developer, App is dependent on developer
        /// </summary>
        [ForeignKey(ModelConstants.FK_DEV_APP)]
        public Developer Developer { get; set; }

        public bool DeactivationFlag { get; set; }

        /// <summary>
        /// Collection of App Entities
        /// Navigation Prooperty
        /// </summary> 
        public virtual ICollection<AppEntity> AppEntitiys { get; set; }
    }
}
