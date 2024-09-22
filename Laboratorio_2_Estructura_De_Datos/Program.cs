using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Mensaje de bienvenida
            Color.SetTextColor(ConsoleColor.Cyan); // Color
            Console.WriteLine("BIENVENIDO AL PROGRAMA DE GESTION DE INVENTARIO");
            Color.ResetTextColor(); // Reestablecer color por defecto

            //Nombre de la empresa
            string nombreEmpresa;
            Console.Write("\nIngrese el nombre de la empresa: ");
            Color.SetTextColor(ConsoleColor.Green); // Color
            nombreEmpresa = Console.ReadLine();
            Color.ResetTextColor(); // Reestablecer color por defecto

            while (true)
            {
                Console.Clear(); //Limpiar consola
                Console.WriteLine("***********************************************************");
                Console.Write("GESTION DE INVENTARIO EMPRESA: ");
                Color.SetTextColor(ConsoleColor.Green); // Color
                Console.WriteLine(nombreEmpresa);
                Color.ResetTextColor(); // Reestablecer color por defecto
                Console.WriteLine("***********************************************************");


                //Menu principal 
                Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                Console.WriteLine("\nMenu principal.");
                Color.ResetTextColor(); // Reestablecer color por defecto
                int opMenu; //Esta misma variable se reutiliza en todos los menus
                Console.WriteLine("1.Agregar productos a inventario");
                Console.WriteLine("2.Vender productos del inventario");
                Console.WriteLine("3.Consultar inventario");
                Console.WriteLine("4.Salir");
                Color.SetTextColor(ConsoleColor.Green); // Color
                Console.Write("\nOpcion: ");
                // Validacion de opcion ingresada
                while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 4)
                {
                    Color.SetTextColor(ConsoleColor.Red); // Color
                    Console.WriteLine("Opcion no valida");
                    Color.SetTextColor(ConsoleColor.Green); // Color
                    Console.Write("Opcion: ");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                }
                Color.ResetTextColor(); // Reestablecer color por defecto

                //Para salir del menu principal (bucle while) el programa finaliza
                if (opMenu == 4)
                {
                    break;
                }




                Console.Clear(); //Limpiar consola
                switch (opMenu)
                {
                    //Agregar producto a inventario
                    case 1:
                        //Este case es para manejar el ingreso de productos al inventario
                        while (true)
                        {
                            Console.Clear();//Limpiar consola

                            Console.WriteLine("***********************************************************");
                            Console.Write("GESTION DE INVENTARIO EMPRESA: ");
                            Color.SetTextColor(ConsoleColor.Green); // Color
                            Console.WriteLine(nombreEmpresa);
                            Color.ResetTextColor(); // Reestablecer color por defecto
                            Console.WriteLine("***********************************************************");                        // Agregar producto al inventario de productos
                                                                                                                           // Menu para seleccionar el metodo de gestion de inventario a usar
                            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                            Console.WriteLine("\nAgregar productos al inventario");
                            Color.ResetTextColor(); // Reestablecer color por defecto
                            Console.WriteLine("1.Agregar productos");
                            Console.WriteLine("2.Mostrar inventario de productos");
                            Console.WriteLine("3.Volver al menu principal");
                            Color.SetTextColor(ConsoleColor.Green); // Color
                            Console.Write("\nOpcion: ");
                            // Validacion de opcion ingresada
                            while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 3)
                            {
                                Color.SetTextColor(ConsoleColor.Red); // Color
                                Console.WriteLine("Opcion no valida");
                                Color.SetTextColor(ConsoleColor.Green); // Color
                                Console.Write("Opcion: ");
                                Color.ResetTextColor(); // Reestablecer color por defecto
                            }
                            Color.ResetTextColor(); // Reestablecer color por defecto

                            //Vuelve al menu principal
                            if (opMenu == 3)
                            {
                                Color.SetTextColor(ConsoleColor.Green); // Color
                                Console.WriteLine("\nVolviendo al menu principal.");
                                Color.ResetTextColor(); // Reestablecer color por defecto
                                Thread.Sleep(1000);
                                break;
                            }

                            switch (opMenu)
                            {
                                case 1:
                                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                                    Console.WriteLine("\nAgregar producto a inventario");
                                    Color.ResetTextColor(); // Reestablecer color por defecto
                                    Inventario.agregarProductosAInventario();
                                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                                    Console.WriteLine("Producto agregado.");
                                    Color.ResetTextColor(); // Reestablecer color por defecto
                                    //Console.WriteLine("\nMostrando inventario ...");
                                    //Inventario.mostrarProductos();//no es necesario mostrar el inventario cada vez que el user agregue un nuevo producot
                                    Console.WriteLine();
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;

                                //Consultar inventario
                                case 2:
                                    //Mostrar inventario
                                    Console.Clear();
                                    Console.WriteLine();
                                    Inventario.mostrarProductos();
                                    Console.WriteLine("\nPresione una tecla para volver . . .");
                                    Console.ReadKey();
                                    break;

                                default:
                                    Console.WriteLine("Error desconocido");
                                    break;
                            }
                        }
                        break;





                     //Vender productos del inventario
                    case 2:
                        //case para manejar la salida(venta) de productos 
                        //el usuario debe poder seleccionar que metodo usar peps ueps o costo promedio
                        while (true)
                        {
                            Console.Clear();//Limpiar consola
                            Console.WriteLine("***********************************************************");
                            Console.Write("GESTION DE INVENTARIO EMPRESA: ");
                            Color.SetTextColor(ConsoleColor.Green); // Color
                            Console.WriteLine(nombreEmpresa);
                            Color.ResetTextColor(); // Reestablecer color por defecto
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine("\nVender productos del inventario.");
                            Console.WriteLine("\nSeleccione un metodo para gestionar el inventario: ");
                            Console.WriteLine("1.Metodo PEPS");
                            Console.WriteLine("2.Metodo UEPS");
                            Console.WriteLine("3.Costo promedio");
                            Console.WriteLine("4.Volver al menu principal");
                            Console.WriteLine("5.Mostrar inventario de productos");
                            Color.SetTextColor(ConsoleColor.Green); // Color
                            Console.Write("\nOpcion: ");
                            // Validacion de opcion ingresada
                            while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 5)
                            {
                                Color.SetTextColor(ConsoleColor.Red); // Color
                                Console.WriteLine("Opcion no valida");
                                Color.SetTextColor(ConsoleColor.Green); // Color
                                Console.Write("Opcion: ");
                                Color.ResetTextColor(); // Reestablecer color por defecto
                            }
                            Color.ResetTextColor(); // Reestablecer color por defecto

                            //Vuelve al menu principal
                            if (opMenu == 4)
                            {
                                Color.SetTextColor(ConsoleColor.Green); // Color
                                Console.WriteLine("\nVolviendo al menu principal.");
                                Color.ResetTextColor(); // Reestablecer color por defecto
                                Thread.Sleep(1000);
                                break;
                            }

                            switch (opMenu)
                            {
                                case 1:
                                    break;

                                case 2:
                                    //Gestion de invetario por metodo UEPS
                                    Inventario.ventaUEPS();
                                    break;

                                case 3:
                                    //Gestion de invetario por metodo Costo Promedio
                                    Inventario.costoPromedio();
                                    break;

                                case 5:
                                    //Mostrar inventario
                                    Console.Clear();
                                    Console.WriteLine();
                                    Inventario.mostrarProductos();
                                    Console.WriteLine("\nPresione una tecla para volver . . .");
                                    Console.ReadKey();
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;




                    //Consultar inventario
                    case 3:
                        //Mostrar inventario.Se puede seleccionar mostrar lotes de un producto
                        Inventario.mostrarProductos();
                        Console.WriteLine("\nPara ver los lotes de un producto ingrese el ID correspondiente. " +
                            "\nDe lo contrario ingrese -1 para salir.");
                        int opcionId;
                        Color.SetTextColor(ConsoleColor.Green); // Color
                        Console.Write("\nID: ");
                        //Comprobar que el id del lote existe buscar el id en el inventario
                        while (!int.TryParse(Console.ReadLine(), out opcionId) || (opcionId != -1 && opcionId < 1 )||  opcionId > Inventario.inventario.Count)
                        {
                            Color.SetTextColor(ConsoleColor.Red); // Color
                            Console.WriteLine("Error: el ID ingresado es invalido o no existe");
                            Color.SetTextColor(ConsoleColor.Green); // Color
                            Console.Write("ID: ");
                            Color.ResetTextColor(); // Reestablecer color por defecto
                        }
                        Color.ResetTextColor(); // Reestablecer color por defecto

                        if (opcionId == -1) // Salir
                        {
                            break;
                        }
                        else
                        {
                            //Mostrar los lotes del producto seleccionado 
                            //El metodo mostrarLotes esta en la clase Producto
                            Inventario.inventario[opcionId].mostrarLotes();

                        }
                        Console.WriteLine("\nPresione una tecla para volver . . .");
                        Console.ReadKey();
                        break;

                    default:
                        break;
                }

            }
        }

        //Seccion de metodos utiles para el flujo del programa general

    }
}
