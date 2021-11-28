using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.InputModel
{
    public class EstrelaInputModel
    {
        public string IdOficina { get; set; }
        public string IdUsuario { get; set; }
        public double Estrelas { get;  set; }
    }
}
