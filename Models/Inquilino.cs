﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaAlvarez.Models
{
    public class Inquilino
    {
        public int IdInquilino { get; set; }

        public string Dni { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
    }
}
