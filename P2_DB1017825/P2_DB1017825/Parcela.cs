using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGranja
{
    public class Parcela
    {
        public string tipoSiembra { get; set; }
        public int progresoCrecimiento { get; set; }
        public int tiempoParaCosecha { get; set; }
        public bool estaRegada { get; set; }
        public decimal valorCosecha { get; set; }

        public Parcela()
        {
            tipoSiembra = "Vacia";
            progresoCrecimiento = 0;
            tiempoParaCosecha = 0;
            estaRegada = false;
            valorCosecha = 0;
        }
    }
}
