using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Aqui se almacenan los productos
            Dictionary<int, List<Producto>> inventario = new Dictionary<int, List<Producto>>();

            // Nombre de la empresa
            string nombreEmpresa;
            Console.Write("Ingrese el nombre de la empresa: ");
            nombreEmpresa = Console.ReadLine();


            while (true)
            {
                Console.Clear(); // limpiar consola
                Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);

                Console.WriteLine("\nMenu principal ...");
                Console.WriteLine("\nIngrese una opcion: ");
                int opMenu;
                //menu de modulos --> cada modulo tiene su propio submenu
                //MENU PRINCIPAL--------
                Console.WriteLine("1.Agregar productos a inventario");
                Console.WriteLine("2.Vender productos del inventario");
                Console.WriteLine("3.Consultar inventario");
                Console.WriteLine("4.Salir");
                Console.Write("\nOpcion: ");

                while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 4)
                {
                    Console.WriteLine("Opcion no valida");
                }


                if (opMenu == 4)//ESTE IF SIRVE PARA CONTROLAR EL BUCLE (SALIR)
                {
                    break;
                }


                Console.Clear();
                switch (opMenu)
                {
                    case 1:
                        //este case para manejar el ingreso de productos ya sea peps o ueps , costo promedio
                        //permitir que el user seleccione que metodo usar
                        while (true)
                        {
                            Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);

                            Console.WriteLine("\nAgregar productos al inventario ...");
                            Console.WriteLine("\nSeleccione un metodo para gestionar el inventario: ");
                            Console.WriteLine("1.Metodo PEPS");
                            Console.WriteLine("2.Metodo UEPS");
                            Console.WriteLine("3.Costo promedio");
                            Console.WriteLine("4.Volver al menu principal");
                            Console.Write("\nOpcion: ");

                            while (!int.TryParse(Console.ReadLine(), out opMenu) || opMenu < 1 || opMenu > 4)
                            {
                                Console.WriteLine("Opcion no valida");
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


                    case 2:
                        //case para manejar la salida(venta) de productos 
                        //el user debe poder seleccionar que metodo usar pepe ueps o costo promedio
                        while (true)
                        {
                            Console.WriteLine("GESTION DE INVENTARIO EMPRESA: " + nombreEmpresa);

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
                        //mostrar inventario
                        mostrarInventario(inventario);
                        Console.ReadKey();
                        break;

                    default:
                        break;
                }

            }
        }

        //seccion de metodos utiles para el flujo del programa general
        static void mostrarInventario(Dictionary<int, List<Producto>> inventario)
        {
            //validar si esta vacio
            if (inventario.Count == 0)
            {
                Console.WriteLine("El inventario esta vacio!");
            }
            else
            {
                foreach (var item in inventario)
                {
                    Console.WriteLine(item);
                }
            }


        }
    }
}
