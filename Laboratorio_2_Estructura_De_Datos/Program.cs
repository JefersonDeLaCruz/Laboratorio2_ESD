using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Nombre de la empresa
            string nombreEmpresa;
            Console.Write("Ingrese el nombre de la empresa: ");
            nombreEmpresa = Console.ReadLine();

            while (true)
            {
                Console.Clear(); //Limpiar consola
                Console.WriteLine("***********************************************************");
                Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);
                Console.WriteLine("***********************************************************");


                //Menu principal 
                Console.WriteLine("\nMenu principal ...");
                int opMenu; //Esta misma variable se reutiliza en todos los menus
                Console.WriteLine("1.Agregar productos a inventario");
                Console.WriteLine("2.Vender productos del inventario");
                Console.WriteLine("3.Consultar inventario");
                Console.WriteLine("4.Salir");
                Console.Write("\nOpcion: ");
                // Validacion de opcion ingresada
                while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 4)
                {
                    Console.WriteLine("Opcion no valida");
                    Console.Write("\nOpcion: ");
                }

                //Para salir del menu principal (bucle while) el programa finaliza
                if (opMenu == 4)
                {
                    break;
                }

                Console.Clear(); //Limpiar consola
                switch (opMenu)
                {
                    case 1:
                        //Este case es para manejar el ingreso de productos ya sea peps, ueps o costo promedio
                        //El usuario selecciona el metodo de gestion de inventario a usar
                        while (true)
                        {
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);
                            Console.WriteLine("***********************************************************");                            // Agregar producto al inventario de productos
                            // Menu para seleccionar el metodo de gestion de inventario a usar
                            Console.WriteLine("\nAgregar productos al inventario ...");
                            Console.WriteLine("1.Agregar productos");
                            Console.WriteLine("2.Volver al menu principal");
                            Console.Write("Opcion: ");
                            // Validacion de opcion ingresada
                            while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 2)
                            {
                                Console.WriteLine("Opcion no valida");
                                Console.Write("Opcion: ");
                            }

                            //Vuelve al menu principal
                            if (opMenu == 2)
                            {
                                Console.WriteLine("Volviendo al menu principal ...");
                                Thread.Sleep(2000);
                                break;
                            }

                            switch (opMenu)
                            {
                                case 1:
                                    Console.WriteLine("\nAgregar producto a inventario");
                                    Inventario.agregarProductosAInventario();
                                    Console.WriteLine("Producto agregado ...");
                                    Console.WriteLine("\nMostrando inventario ...");
                                    Inventario.mostrarProductos();
                                    Console.WriteLine();
                                    Console.ReadKey();
                                    break;

                                default:
                                    Console.WriteLine("Error desconocido");
                                    break;
                            }
                        }
                        break;


                    case 2:
                        //case para manejar la salida(venta) de productos 
                        //el user debe poder seleccionar que metodo usar pepe ueps o costo promedio
                        while (true)
                        {
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine("\nVender productos del inventario ...");
                            Console.WriteLine("\nSeleccione un metodo para gestionar el inventario: ");
                            Console.WriteLine("1.Metodo PEPS");
                            Console.WriteLine("2.Metodo UEPS");
                            Console.WriteLine("3.Costo promedio");
                            Console.WriteLine("4.Volver al menu principal");
                            Console.Write("\nOpcion: ");

                            while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 4)
                            {
                                Console.WriteLine("Opcion no valida");
                                Console.Write("Opcion: ");
                            }


                            if (opMenu == 4)//ESTE IF SIRVE PARA CONTROLAR EL BUCLE (SALIR)
                            {
                                break;
                            }

                            switch (opMenu)
                            {
                                case 1:
                                    break;

                                case 2:
                                    break;

                                case 3:
                                    
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;
                    case 3:
                        //Mostrar inventario
                        break;

                    default:
                        break;
                }

            }
        }

        //Seccion de metodos utiles para el flujo del programa general
       
    }
}
