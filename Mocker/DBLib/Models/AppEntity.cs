﻿using DBLib.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey(Constants.FK_APP_ENTITY)]
        public DevApp DevApp { get; set; }

        /// <summary>
        /// Collection of EntityFields
        /// </summary>
        public ICollection<EntityField> EntityFields { get; set; }
    }
}
