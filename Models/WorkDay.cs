using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Registro_de_Ponto_CTEDS.Models
{
    public class WorkDay
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime MissWorDate { get; set; }
    }
}
