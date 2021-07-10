using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIManagers.Models
{
    public class Managers_DB
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Lanzamiento { get; set; }
        public string Desarrollador { get; set; }
    }
}
