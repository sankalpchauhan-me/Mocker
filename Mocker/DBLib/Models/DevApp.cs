﻿using DBLib.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLib.Models
{
    /// <summary>
    /// Model class foe DevApp
    /// </summary>
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
        public string AppName { get; set; }

        /// <summary>
        /// Getter and Setter for DevId
        /// Foreign Key Reference to @Developer
        /// </summary>
        public int DevId { get; set; }

        /// <summary>
        /// Must have a developer, App is dependent on developer
        /// </summary>
        [ForeignKey(Constants.FK_DEV_APP)]
        public Developer Developer { get; set; }

        /// <summary>
        /// Collection of App Entities
        /// </summary>
        /// 
        public virtual ICollection<AppEntity> AppEntitiys { get; set; }
    }
}
