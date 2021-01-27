using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic.Temp
{
    public class TempEmployee
    {
        public TempEmployee()
        {

        }
        public TempEmployee(int id, string FirstName, string LastName)
        {
            this.id = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
