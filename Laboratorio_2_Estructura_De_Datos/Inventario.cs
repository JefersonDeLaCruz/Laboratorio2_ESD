using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Inventario
    {
        //Este diccionario almacena los productos es estatica 
        //ya que debemos hacer uso facilmente desde el main
        //Clave = Id del producto. 
        //Valor = Un Producto
        public static Dictionary<int, Producto> inventario = new Dictionary<int, Producto>();



        //PEPS Primero en Entrar Primero en Salir
        public static void ventaPEPS()
        {
            

        }





        //UEPS Ultimo en Entrar Primero en Salir
        public static void ventaUEPS()
        {
            Console.Clear();
            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
            Console.WriteLine("\nGESTION DE INVENTARIO METODO UEPS");
            Color.ResetTextColor(); // Reestablecer color por defecto

            /*
             La idea es utilizar una pila(stack).

             La pila contendra los lotes del producto, por lo tanto, la pila sera de tipo Lote,
             es decir, cada elemento de la pila es un Lote.
             */

            //Mostrar productos para que seleccione un id de producto a vender
            Inventario.mostrarProductos();

            Producto producto = null; //Variable de referencia al producto seleccionado

            //Se validara que el id exista
            Boolean existe = false;
            int idProducto = 0;
            while (!existe) 
            {
                Console.Write("Ingrese el ID del producto a vender (-1 para salir): ");
                while (!int.TryParse(Console.ReadLine(), out idProducto) || (idProducto < 1 && idProducto != -1))
                {
                    Color.SetTextColor(ConsoleColor.Red); // Color
                    Console.WriteLine("Error: ingrese un id valido");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                    Console.Write("Ingrese el ID del producto a vender (-1 para salir): ");
                }

                if (idProducto == -1)
                {
                    Console.WriteLine("Volviendo ...");
                    Thread.Sleep(1000);
                    break;
                }

                foreach (KeyValuePair<int, Producto> item in inventario)
                {
                    //El producto existe
                    if (item.Value.Id == idProducto)
                    {
                        existe = true;
                        producto = item.Value; //Almacenamos el producto en una variable 
                        break; //Salimos del foreach
                    }
                }

                if (!existe)
                {
                    Color.SetTextColor(ConsoleColor.Red); // Color
                    Console.WriteLine("Error: el id no existe");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                }
            }

            if (idProducto != -1)
            {
                //Mostrar lotes
                inventario[idProducto].mostrarLotes();

                Console.Write("\nIngrese la cantidad de producto a vender: ");
                int cantidad;
                while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1 || cantidad > producto.Cantidad)
                {
                    if (cantidad > producto.Cantidad)
                    {
                        Color.SetTextColor(ConsoleColor.Red); // Color
                        Console.WriteLine("Excedio la cantidad de producto en existencias");
                        Color.ResetTextColor(); // Reestablecer color por defecto
                    }
                    else
                    {
                        Color.SetTextColor(ConsoleColor.Red); // Color
                        Console.WriteLine("Error: Ingrese una cantidad valida");
                        Color.ResetTextColor(); // Reestablecer color por defecto
                    }
                    Console.Write("Ingrese la cantidad de producto a vender: ");
                }

                //LLegado a este punto las validaciones ya estan realizadas el id del producto existe
                //y hay suficiente producto para vender

                //IMPOTANTE: en la clase producto hay un stack pensada
                //especificamente para hacer el UEPS

                //IMPORTANTE: Restamos la cantidad de producto vendido al lote mas nuevo si falta restamos
                //tambien del de mas abajo en la pila. Esto para cumplir con el metodo UEPS.

                //MUY IMPORTANTE 
                //EN LA CLASE PRODUCTO HAY UNA PILA DE LOTESPERO NO ESTA INICIALIZADA CON LOS LOTES
                producto.actualizarLotesenPila(); //Basicamente solo copiasmos cada lote en la lista de lotes a la pila de lotes

                Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                Console.WriteLine("\nRESUMEN DE LA VENTA: ");
                Color.ResetTextColor(); // Reestablecer color por defecto
                double costoProductosVendidos = 0;
                int cantidadRespaldo = cantidad;
                while (cantidad > 0)
                {
                    //Hay que restar del de mas abajo ?
                    if (cantidad - producto.lotesEnPila.Peek().Cantidad >= 0)
                    {
                        //Se vende a precio unitario del ultimo lote, lote mas nuevo
                        costoProductosVendidos = costoProductosVendidos + ((cantidad - (cantidad - producto.lotesEnPila.Peek().Cantidad)) * producto.lotesEnPila.Peek().Precio);

                        //Actualizamos la variable Cantidad del producto
                        producto.Cantidad = producto.Cantidad - producto.lotesEnPila.Peek().Cantidad;

                        cantidad = cantidad - producto.lotesEnPila.Peek().Cantidad; //Restamos lo vendido

                        //Eliminamos el lote de la pila del producto ya que se vendio todo
                        Console.WriteLine("Un lote vendido. Cantidad: " + producto.lotesEnPila.Pop().Cantidad);

                        //Eliminamos el ultimo lote de la lista de lotes del producto ya que se vendio todo
                        producto.Lotes.RemoveAt(producto.Lotes.Count-1);

                        //ELIMINAMOS PRODUCTO DE INVENTARIO PORQUE NO QUEDA NADA
                        if (producto.Cantidad == 0)
                        {
                            inventario.Remove(idProducto);
                            existe = false; //El producto ya no existe

                        }

                    }
                    else
                    {
                        //Se vende a precio unitario del ultimo lote, lote mas nuevo
                        costoProductosVendidos = costoProductosVendidos + (cantidad * producto.lotesEnPila.Peek().Precio);

                        Console.WriteLine("Se vendio la siguiente cantidad de el ultimo lote: " + cantidad);

                        //Actualizamos la variable Cantidad del producto
                        producto.Cantidad = producto.Cantidad - cantidad;

                        
                        //ACTUALIZAMOS LA CANTIDAD DE PRODUCTO EN LA PILA DEL PRODUCTO
                        producto.lotesEnPila.Peek().Cantidad = producto.lotesEnPila.Peek().Cantidad - cantidad;

                        //ACTUALIZAMOS LA CANTIDAD DE PRODUCTO EN LA LISTA DEL PRODUCTO
                        //Eliminamos el lote de la lista de lotes del producto
                        producto.Lotes.RemoveAt(producto.Lotes.Count - 1);
                        producto.Lotes.Add(producto.lotesEnPila.Peek()); //Copiamos el lote del stack actualizado como nuevo elemento de la lista de
                                                                         //lotes del producto

                        cantidad = cantidad - cantidad; //Restamos lo vendido
                    }
                }
                //Resumen venta
                Color.SetTextColor(ConsoleColor.Red); // Color
                Console.WriteLine($"Cantidad: {cantidadRespaldo}, Costo total de la venta: {costoProductosVendidos}");
                Color.ResetTextColor(); // Reestablecer color por defecto

                //Mostrar lotes
                if (existe)
                {
                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                    Console.WriteLine("\nPRODUCTO RESTANTE:");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                    inventario[idProducto].mostrarLotes();
                }
                else
                {
                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                    Console.WriteLine("\nSe agotaron las existencias del producto");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                }


                Console.WriteLine("\nPresione una tecla para volver . . .");
                Console.ReadKey();
            }
        }




        //Metodo de gestion de inventario costo promedio
        public static void costoPromedio()
        {
            Console.Clear();
            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
            Console.WriteLine("\nGESTION DE INVENTARIO METODO COSTO PROMEDIO");
            Color.ResetTextColor(); // Reestablecer color por defecto

            /*
             La idea es utilizar una pila(stack).

             La pila contendra los lotes del producto, por lo tanto, la pila sera de tipo Lote,
             es decir, cada elemento de la pila es un Lote.
             */

            //Mostrar productos para que seleccione un id de producto a vender
            Inventario.mostrarProductos();

            Producto producto = null; //Variable de referencia al producto seleccionado

            //Se validara que el id exista
            Boolean existe = false;
            int idProducto = 0;
            while (!existe)
            {
                Console.Write("Ingrese el ID del producto a vender (-1 para salir): ");
                while (!int.TryParse(Console.ReadLine(), out idProducto) || (idProducto < 1 && idProducto!=-1))
                {
                    Color.SetTextColor(ConsoleColor.Red); // Color
                    Console.WriteLine("Error: ingrese un id valido");

                    Color.ResetTextColor(); // Reestablecer color por defecto
                    Console.Write("Ingrese el ID del producto a vender (-1 para salir): ");
                }

                if (idProducto == -1)
                {
                    Console.WriteLine("Volviendo ...");
                    Thread.Sleep(1000);
                    break;
                }

                foreach (KeyValuePair<int, Producto> item in inventario)
                {
                    //El producto existe
                    if (item.Value.Id == idProducto)
                    {
                        existe = true;
                        producto = item.Value; //Almacenamos el producto en una variable 
                        break; //Salimos del foreach
                    }
                }

                if (!existe)
                {
                    Color.SetTextColor(ConsoleColor.Red); // Color
                    Console.WriteLine("Error: el id no existe");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                }
            }

            if (idProducto != -1)
            {
                //Mostrar lotes
                inventario[idProducto].mostrarLotes();

                Console.Write("\nIngrese la cantidad de producto a vender: ");
                int cantidad;
                while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1 || cantidad > producto.Cantidad)
                {
                    if (cantidad > producto.Cantidad)
                    {
                        Color.SetTextColor(ConsoleColor.Red); // Color
                        Console.WriteLine("Excedio la cantidad de producto en existencias");
                        Color.ResetTextColor(); // Reestablecer color por defecto
                    }
                    else
                    {
                        Color.SetTextColor(ConsoleColor.Red); // Color
                        Console.WriteLine("Error: Ingrese una cantidad valida");
                        Color.ResetTextColor(); // Reestablecer color por defecto
                    }
                    Console.Write("Ingrese la cantidad de producto a vender: ");
                }

                //LLegado a este punto las validaciones ya estan realizadas el id del producto existe
                //y hay suficiente producto para vender



                //Con este metodo siempre vendemos el ultimo elemento el mas nuevo pero no lo vendemos al precio al que se compro
                //ese lote si no al costo promedio
                double costoPromedio = 0;
                //Calcular costo promedio
                //ACTUALIZAR SALDO DEL PRODUCTO
                inventario[idProducto].actualizarSaldo();
                costoPromedio = producto.saldo / producto.Cantidad; //A ESTE PRECIO SE VENDERAN TODOS LOS PRODUCTOS



                Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                Console.WriteLine("\nRESUMEN DE LA VENTA: ");
                Color.ResetTextColor(); // Reestablecer color por defecto
                double costoProductosVendidos = 0;
                int cantidadRespaldo = cantidad;
                while (cantidad > 0)
                {
                    //Hay que restar del de mas abajo ?
                    if (cantidad - inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad >= 0)
                    {
                        //Se vende a precio unitario del ultimo lote, lote mas nuevo
                        costoProductosVendidos = costoProductosVendidos + ((cantidad - (cantidad - inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad)) * costoPromedio);

                        //Actualizamos la variable Cantidad del producto
                        producto.Cantidad = producto.Cantidad - inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad;

                        cantidad = cantidad - inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad; //Restamos lo vendido

                        //Eliminamos el lote de la pila del producto ya que se vendio todo
                        Console.WriteLine("Un lote vendido. Cantidad: " + inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad);

                        //Eliminamos el ultimo lote de la lista de lotes del producto ya que se vendio todo
                        producto.Lotes.RemoveAt(producto.Lotes.Count - 1);

                        //ELIMINAMOS PRODUCTO DE INVENTARIO PORQUE NO QUEDA NADA
                        if (producto.Cantidad == 0)
                        {
                            inventario.Remove(idProducto);
                            existe = false; //El producto ya no existe

                        }
                    }
                    else
                    {
                        //Se vende a precio unitario del ultimo lote, lote mas nuevo
                        costoProductosVendidos = costoProductosVendidos + (cantidad * costoPromedio);

                        Console.WriteLine("Se vendio la siguiente cantidad de el ultimo lote: " + cantidad);

                        //Actualizamos la variable Cantidad del producto
                        producto.Cantidad = producto.Cantidad - cantidad;

                        //ACTUALIZAMOS LA CANTIDAD DE PRODUCTO EN LA LISTA DE LOTES SE VENDIO UNA PARTE DEL LOTE
                        inventario[idProducto].Lotes[inventario[idProducto].Lotes.Count - 1].Cantidad -= cantidad;

                        cantidad = cantidad - cantidad; //Restamos lo vendido
                    }
                }
                //Resumen venta
                Color.SetTextColor(ConsoleColor.Red); // Color
                Console.WriteLine("Se vendio al siguiente costo promedio: " + costoPromedio);
                Console.WriteLine($"Cantidad: {cantidadRespaldo}, Costo total de la venta: {costoProductosVendidos}");
                Color.ResetTextColor(); // Reestablecer color por defecto

                //Mostrar lotes
                if (existe)
                {
                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                    Console.WriteLine("\nPRODUCTO RESTANTE:");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                    inventario[idProducto].mostrarLotes();
                }
                else
                {
                    Color.SetTextColor(ConsoleColor.DarkBlue); // Color
                    Console.WriteLine("\nSe agotaron las existencias del producto");
                    Color.ResetTextColor(); // Reestablecer color por defecto
                }


                Console.WriteLine("\nPresione una tecla para volver . . .");
                Console.ReadKey();
            }

        }




        //Este metodo agrega un producto/os al inventario (agrega lotes a un producto existente o en su defecto crea
        //un nuevo producto con un nuevo lote)
        public static void agregarProductosAInventario()
        {
            //Datos necesarios 
            Console.Write("Nombre del producto: ");
            string nombreProducto = Console.ReadLine();

            Console.Write("Cantidad a agregar del producto: ");
            int cantidad;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1)
            {
                Console.WriteLine("Error: Ingrese una cantidad valida");
                Console.Write("Cantidad a agregar del producto: ");
            }

            Console.Write("Precio unitario: ");
            double precio;
            while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
            {
                Console.WriteLine("Error: Ingrese un precio valido");
                Console.Write("Precio unitario: ");
            }

            //Este sera el Id del nuevo producto agregado comienza en 1 
            int numAlmacenados = inventario.Count + 1;

            //Se creara un nuevo lote del producto 
            Lote loteNuevo; 

            //Buscamos si el producto existe en el inventario
            Boolean existe = false;
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                //El producto existe
                if (item.Value.Nombre.ToLower().Equals(nombreProducto.ToLower()))
                {
                    //Creamos el id del lote nuevo
                    int idLoteNuevo = item.Value.Lotes.Count + 1;
                    //Creamos el nuevo lote
                    loteNuevo = new Lote(DateTime.Now, cantidad, precio, idLoteNuevo);

                    //Solo agregamos el nuevo lote porque el producto existe
                    item.Value.agregarLote(precio, loteNuevo);
                    existe = true;
                }
            }

            //El producto no existe
            if (!existe)
            {
                //Creamos el nuevo lote su id es 1 ya que es el primer lote
                loteNuevo = new Lote(DateTime.Now, cantidad, precio, 1);

                //Agregar el producto
                inventario.Add(numAlmacenados, new Producto(nombreProducto, precio, numAlmacenados, loteNuevo));
            }

        }






        //Muestra la informacion de los productos los productos del diccionario inventario
        public static void mostrarProductos()
        {
            //Muestra los productos dentro del diccionario
            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
            Console.WriteLine("INVENTARIO DE PRODUCTOS");
            Color.ResetTextColor(); // Reestablecer color por defecto

            //---> variable item de tipo KeyValuePair<int, Producto> almacena tanto la llave como el el valor
            //item.value accede al objeto producto almacenado como valor
            //al ser item.value == instancia de un producto podemos acceder a sus campos con el operador punto
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine($"\tId: {item.Value.Id}\t|\t Nombre: {item.Value.Nombre}\t|\t Precio: {item.Value.Precio}\t|\t Cantidad: {item.Value.Cantidad}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }
    }
}
