﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.Models
{
    public class Developer
    {

        /// <summary>
        /// Getter and Setter for Developer Id
        /// </summary>
        [Key]
        public int DevId { get; set; }

        /// <summary>
        /// Getter and Setter for Full Name
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Getter and Setter for User Id
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "4<=UserID<=100")]
        [Index(IsUnique = true)]
        public string UserId { get; set; }

        public bool DeactivationFlag { get; set; }

        /// <summary>
        /// Collection of Apps under this Developer
        /// Navigation Prooperty
        /// </summary>
        public virtual ICollection<DevApp> DevApps { get; set; }
    }
}
