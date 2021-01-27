using DBModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mocker.DTOs
{
    public class DeveloperDTO
    {
        [Required]
        public int DevId { get; set; }
        public string FullName { get; set; }
        [Index(IsUnique = true)]
        public string UserId { get; set; }
        public virtual List<DevAppDTO> DevApps { get; set; }

        public static implicit operator DeveloperDTO(Developer v)
        {
            if (v == null)
                return null;

            List<DevAppDTO> devApps = new List<DevAppDTO>();
            if (v.DevApps != null)
            {
                foreach (DevApp d in v.DevApps)
                {
                    devApps.Add(d);
                }
            }
            return new DeveloperDTO
            {
                DevId = v.DevId,
                FullName = v.FullName,
                UserId = v.UserId,
                DevApps = devApps
            };
        }
    }
}