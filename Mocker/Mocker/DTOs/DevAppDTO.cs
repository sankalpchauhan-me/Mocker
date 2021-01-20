using DBLib.Models;
using System.Collections.Generic;

namespace Mocker.DTOs
{
    public class DevAppDTO
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public int DevId { get; set; }
        public virtual List<AppEntityDTO> AppEntitiys { get; set; }

        public static implicit operator DevAppDTO(DevApp v)
        {
            if (v == null)
                return null;

            List<AppEntityDTO> appEntitys = new List<AppEntityDTO>();
            foreach (AppEntity d in v.AppEntitiys)
            {
                appEntitys.Add(d);
            }
            return new DevAppDTO
            {
                AppId = v.AppId,
                AppName = v.AppName,
                DevId = v.DevId,
                AppEntitiys = appEntitys
            };
        }
    }
}