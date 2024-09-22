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
                                    //Console.WriteLine("\nMostrando inventario ...");
                                    //Inventario.mostrarProductos();//no es necesario mostrar el inventario cada vez que el user agregue un nuevo producot
                                    Console.WriteLine();
                                    Console.ReadKey();
                                    Console.Clear();
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
                                    Inventario.pepsVenta();
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
                        Inventario.mostrarProductos();
                        Console.WriteLine("Para ver los lotes de un producto ingrese el ID correspondiente. De lo contrario ingrese -1");
                        int opcionId;

                        while (!(int.TryParse(Console.ReadLine(), out opcionId) && (opcionId == -1 || (opcionId >= 1 && opcionId <= Producto.Lote.Count))))
                        {
                            Console.WriteLine("El ID ingresado es invalido o no existe");
                        }
                        if (opcionId == -1)
                        {
                            break;
                        }
                        else
                        {
                            mostrarDetalles(opcionId);

                        }



                        Console.ReadKey();
                        break;
                    case 10:
                        //para testeo
                        Lote.mostrarLotes();
                        Console.ReadKey();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }

            }
        }

        //Seccion de metodos utiles para el flujo del programa general

        static void mostrarDetalles(int opcionId)
        {
            while (true)
            {
                int id = -1;
                Console.Clear();
                //mostramos los lotes de dicho producto
                Lote.mostrarLotes(Producto.Lote, opcionId);

                //pregunta si quiere ver los productos en un lote de los que se muestre
                string codigoIngresado;



                Console.WriteLine("Ingrese el codigo de un lote para ver los productos que contiene. De lo contrario ingrese -1");
                codigoIngresado = Console.ReadLine();
                if (codigoIngresado == "-1")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    foreach (var item in Producto.Lote)
                    {
                        if (item.Id == codigoIngresado)
                        {
                            //eoncontramos el indice del guibajo ya que el numero que nos interesa es el que esta justo despues de este
                            int indiceGuionbajo = codigoIngresado.LastIndexOf('_');

                            id = Convert.ToInt32(codigoIngresado.Substring(indiceGuionbajo + 1));
                            break;
                        }
                    }
                    if (id == -1)
                    {
                        Console.Clear();
                        Console.WriteLine("El codigo ingresado es invalido o no existe");
                        Console.ReadKey();

                    }
                    Inventario.verProductosIndivuduales(Producto.Lote, codigoIngresado, id);

                }
            }
        }

    }
}
