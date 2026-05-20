using System;

namespace GestionGranja
{
    class Program
    {
        static void Main(string[] sender)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("MENÚ DE GESTIÓN DE GRANJA");
            Console.ResetColor();

            Console.WriteLine("--- CONFIGURACIÓN INICIAL ---\n");

            decimal dineroInicial = LeerDecimalPositivo("Ingrese el dinero inicial (Q): ");
            int cantidadEmpleados = LeerEnteroPositivo("Ingrese la cantidad de empleados: ");
            decimal sueldoEmpleado = LeerDecimalPositivo("Ingrese el sueldo por empleado (Q): ");
            int mesesSimular = LeerEnteroPositivo("Ingrese los meses a simular: ");

            Console.WriteLine("\n--- DIMENSIONES DEL TERRENO ---");
            int filas = LeerEnteroPositivo("Ingrese las filas de la matriz: ");
            int columnas = LeerEnteroPositivo("Ingrese las columnas de la matriz: ");

            Granja miGranja = new Granja(filas, columnas, dineroInicial, cantidadEmpleados, sueldoEmpleado, mesesSimular);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nGranja creada correctamente.");
            Console.ResetColor();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

            bool continuarJugando = true;

            while (miGranja.mesesRestantes > 0 && miGranja.capital > 0 && continuarJugando)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" Dinero actual: Q{miGranja.capital} | Meses restantes: {miGranja.mesesRestantes}");
                Console.ResetColor();

                Console.WriteLine("\n1. Sembrar");
                Console.WriteLine("2. Regar Parcelas");
                Console.WriteLine("3. Consultar Parcela");
                Console.WriteLine("4. Avanzar de Mes");
                Console.WriteLine("5. Salir");

                int opcion = LeerEnteroRango("\nSeleccione una opción (1-5): ", 1, 5);

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("\n--- SEMBRAR ---");

                        int totalFilasMatriz = miGranja.cuadricula.GetLength(0);
                        int totalColumnasMatriz = miGranja.cuadricula.GetLength(1);

                        int filaSeleccionada = LeerEnteroRango($"Ingrese fila (1-{totalFilasMatriz}): ", 1, totalFilasMatriz) - 1;
                        int columnaSeleccionada = LeerEnteroRango($"Ingrese columna (1-{totalColumnasMatriz}): ", 1, totalColumnasMatriz) - 1;

                        if (miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tipoSiembra != "Vacia")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError: La parcela ya está ocupada.");
                            Console.ResetColor();
                            Console.WriteLine("Presione una tecla para regresar...");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\nTipos de cultivo:");
                        Console.WriteLine("1. Papa  (2 meses | Cosecha: Q450)");
                        Console.WriteLine("2. Tomate (3 meses | Cosecha: Q650)");
                        Console.WriteLine("3. Fresa  (4 meses | Cosecha: Q900)");

                        int opcionCultivo = LeerEnteroRango("Seleccione cultivo (1-3): ", 1, 3);

                        if (opcionCultivo == 1)
                        {
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tipoSiembra = "Papa";
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tiempoParaCosecha = 2;
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].valorCosecha = 450;
                        }
                        else if (opcionCultivo == 2)
                        {
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tipoSiembra = "Tomate";
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tiempoParaCosecha = 3;
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].valorCosecha = 650;
                        }
                        else if (opcionCultivo == 3)
                        {
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tipoSiembra = "Fresa";
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tiempoParaCosecha = 4;
                            miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].valorCosecha = 900;
                        }

                        miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].progresoCrecimiento = 0;
                        miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].estaRegada = false;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nCultivo de {miGranja.cuadricula[filaSeleccionada, columnaSeleccionada].tipoSiembra} sembrado en [{filaSeleccionada + 1}, {columnaSeleccionada + 1}].");
                        Console.ResetColor();

                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.WriteLine("\n--- REGAR PARCELA ---");

                        Console.Write("Ingrese fila: ");
                        int filaRegar = int.Parse(Console.ReadLine());

                        Console.Write("Ingrese columna: ");
                        int colRegar = int.Parse(Console.ReadLine());

                        if (filaRegar >= 1 && filaRegar <= miGranja.cuadricula.GetLength(0) &&
                            colRegar >= 1 && colRegar <= miGranja.cuadricula.GetLength(1))
                        {
                            Parcela pRegar = miGranja.cuadricula[filaRegar - 1, colRegar - 1];

                            if (pRegar.tipoSiembra == "Vacia")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nError: La parcela está vacía.");
                                Console.ResetColor();
                            }
                            else if (pRegar.estaRegada == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nError: Ya se regó esta parcela este mes.");
                                Console.ResetColor();
                            }
                            else if (miGranja.capital < 40)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nError: No hay dinero suficiente (Costo: Q40).");
                                Console.ResetColor();
                            }
                            else
                            {
                                miGranja.capital = miGranja.capital - 40;
                                pRegar.estaRegada = true;

                                miGranja.totalRiegos = miGranja.totalRiegos + 1;
                                miGranja.acumuladoEgresos = miGranja.acumuladoEgresos + 40;

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"\nParcela [{filaRegar}, {colRegar}] regada.");
                                Console.WriteLine($"Dinero restante: Q{miGranja.capital}");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError: Coordenadas fuera de rango.");
                            Console.ResetColor();
                        }

                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("\n--- CONSULTAR PARCELA ---");

                        Console.Write("Ingrese fila: ");
                        int filaBuscar = int.Parse(Console.ReadLine());

                        Console.Write("Ingrese columna: ");
                        int colBuscar = int.Parse(Console.ReadLine());

                        if (filaBuscar >= 1 && filaBuscar <= miGranja.cuadricula.GetLength(0) &&
                            colBuscar >= 1 && colBuscar <= miGranja.cuadricula.GetLength(1))
                        {
                            Parcela pActual = miGranja.cuadricula[filaBuscar - 1, colBuscar - 1];

                            Console.WriteLine("\n=============================");
                            Console.WriteLine($"DATOS DE LA PARCELA [{filaBuscar}, {colBuscar}]");
                            Console.WriteLine("=============================");
                            Console.WriteLine($"Cultivo: {pActual.tipoSiembra}");

                            if (pActual.tipoSiembra != "Vacia")
                            {
                                Console.WriteLine($"Progreso: {pActual.progresoCrecimiento} / {pActual.tiempoParaCosecha} meses");

                                if (pActual.estaRegada == true)
                                {
                                    Console.WriteLine("Regada este mes: Sí");
                                }
                                else
                                {
                                    Console.WriteLine("Regada este mes: No");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Estado: Vacía / Disponible");
                            }
                            Console.WriteLine("=============================");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError: Coordenadas fuera de rango.");
                            Console.ResetColor();
                        }

                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("\n--- AVANZAR DE MES ---");

                        decimal costoSueldos = miGranja.empleados * miGranja.pagoMensual;
                        miGranja.capital = miGranja.capital - costoSueldos;
                        miGranja.acumuladoEgresos = miGranja.acumuladoEgresos + costoSueldos;

                        Console.WriteLine($"Pago de sueldos: -Q{costoSueldos}");

                        int filasMatriz = miGranja.cuadricula.GetLength(0);
                        int columnasMatriz = miGranja.cuadricula.GetLength(1);

                        for (int f = 0; f < filasMatriz; f++)
                        {
                            for (int c = 0; c < columnasMatriz; c++)
                            {
                                Parcela pActual = miGranja.cuadricula[f, c];

                                if (pActual.tipoSiembra != "Vacia")
                                {
                                    if (pActual.estaRegada == true)
                                    {
                                        pActual.progresoCrecimiento = pActual.progresoCrecimiento + 2;
                                    }
                                    else
                                    {
                                        pActual.progresoCrecimiento = pActual.progresoCrecimiento + 1;
                                    }

                                    pActual.estaRegada = false;

                                    if (pActual.progresoCrecimiento >= pActual.tiempoParaCosecha)
                                    {
                                        miGranja.capital = miGranja.capital + pActual.valorCosecha;
                                        miGranja.acumuladoIngresos = miGranja.acumuladoIngresos + pActual.valorCosecha;

                                        miGranja.totalCosechas = miGranja.totalCosechas + 1;

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Planta madura en [{f + 1}, {c + 1}]: Se vendió {pActual.tipoSiembra} por +Q{pActual.valorCosecha}");
                                        Console.ResetColor();

                                        pActual.tipoSiembra = "Vacia";
                                        pActual.progresoCrecimiento = 0;
                                        pActual.tiempoParaCosecha = 0;
                                        pActual.valorCosecha = 0;
                                    }
                                }
                            }
                        }

                        miGranja.mesesRestantes = miGranja.mesesRestantes - 1;
                        miGranja.mesesSimulados = miGranja.mesesSimulados + 1;

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nMes finalizado.");
                        Console.WriteLine($"Dinero: Q{miGranja.capital} | Meses que quedan: {miGranja.mesesRestantes}");
                        Console.ResetColor();

                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("\nTerminando programa...");
                        continuarJugando = false;
                        break;
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==================================================");
            Console.WriteLine("                REPORTE FINAL                     ");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            int vaciasAlFinal = 0;
            int sembradasAlFinal = 0;
            int fTotal = miGranja.cuadricula.GetLength(0);
            int cTotal = miGranja.cuadricula.GetLength(1);

            for (int f = 0; f < fTotal; f++)
            {
                for (int c = 0; c < cTotal; c++)
                {
                    if (miGranja.cuadricula[f, c].tipoSiembra == "Vacia")
                    {
                        vaciasAlFinal = vaciasAlFinal + 1;
                    }
                    else
                    {
                        sembradasAlFinal = sembradasAlFinal + 1;
                    }
                }
            }

            Console.WriteLine($"Dinero final:              Q{miGranja.capital}");
            Console.WriteLine($"Total de ingresos:         Q{miGranja.acumuladoIngresos}");
            Console.WriteLine($"Total de egresos:          Q{miGranja.acumuladoEgresos}");
            Console.WriteLine($"Meses simulados:           {miGranja.mesesSimulados}");
            Console.WriteLine($"Total de riegos:           {miGranja.totalRiegos}");
            Console.WriteLine($"Total de cosechas:         {miGranja.totalCosechas}");
            Console.WriteLine($"Parcelas sembradas al fin: {sembradasAlFinal}");
            Console.WriteLine($"Parcelas vacías al fin:    {vaciasAlFinal}");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.ReadKey();
        }

        static int LeerEnteroPositivo(string mensaje)
        {
            int resultado;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out resultado) && resultado > 0) return resultado;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Ingrese un entero mayor a cero.");
                Console.ResetColor();
            }
        }

        static decimal LeerDecimalPositivo(string mensaje)
        {
            decimal resultado;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (decimal.TryParse(entrada, out resultado) && resultado > 0) return resultado;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Ingrese un valor mayor a cero.");
                Console.ResetColor();
            }
        }

        static int LeerEnteroRango(string mensaje, int min, int max)
        {
            int resultado;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out resultado) && resultado >= min && resultado <= max)
                {
                    return resultado;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Ingrese un número entre {min} y {max}.");
                Console.ResetColor();
            }
        }
    }
}S