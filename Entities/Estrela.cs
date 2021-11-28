using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Entities
{
    public class Estrela
    {
        public Guid Id { get; set; }
        public string IdOficina { get; set; }
        public string IdUsuario { get; set; }
        public double Estrelas { get; set; }
    }
}
