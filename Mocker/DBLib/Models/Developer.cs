using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Developer Model
    /// </summary>
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
        public string UserId { get; set; }

        /// <summary>
        /// Collection of Apps under this Developer
        /// </summary>
        public virtual ICollection<DevApp> DevApps { get; set; }
    }
}
