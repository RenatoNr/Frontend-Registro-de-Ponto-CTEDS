using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Registro_de_Ponto_CTEDS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public Clock Clock { get; set; }
        public bool IsAdmin { get; set; }
    }
}
