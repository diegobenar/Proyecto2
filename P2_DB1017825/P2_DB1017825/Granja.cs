using System;

namespace GestionGranja
{
    public class Granja
    {
        public Parcela[,] cuadricula { get; set; }
        public decimal capital { get; set; }
        public int empleados { get; set; }
        public decimal pagoMensual { get; set; }
        public int mesesRestantes { get; set; }

        public int totalRiegos { get; set; }
        public int totalCosechas { get; set; }
        public decimal acumuladoIngresos { get; set; }
        public decimal acumuladoEgresos { get; set; }
        public int mesesSimulados { get; set; }

        public Granja(int filas, int columnas, decimal capitalInicial, int cantidadEmpleados, decimal sueldo, int meses)
        {
            this.capital = capitalInicial;
            this.empleados = cantidadEmpleados;
            this.pagoMensual = sueldo;
            this.mesesRestantes = meses;

            this.totalRiegos = 0;
            this.totalCosechas = 0;
            this.acumuladoIngresos = 0;
            this.acumuladoEgresos = 0;
            this.mesesSimulados = 0;

            this.cuadricula = new Parcela[filas, columnas];

            for (int f = 0; f < filas; f++)
            {
                for (int c = 0; c < columnas; c++)
                {
                    this.cuadricula[f, c] = new Parcela();
                }
            }
        }
    }
}
