using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        static void pepsCompra()
        {
            //hacer uso de colas
            // cola = lista de producto en el inventario
            

        }
        static void pepsVenta()
        {

        }

        //Este metodo agrega un producto/os al inventario 
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
                Console.Write("Cantidad: ");
            }

            Console.Write("Precio unitario: ");
            double precio;
            while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
            {
                Console.WriteLine("Error: Ingrese un precio valido");
                Console.Write("Precio: ");
            }

            //Se agregara este nuevo lote del producto 
            Lote loteNuevo = new Lote(DateTime.Now, cantidad, precio);
            
            //Este sera el Id del nuevo producto agregado 
            int numAlmacenados = inventario.Count;


            //Buscamos si el producto existe en el inventario
            Boolean existe = false;
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                //El producto existe
                if (item.Value.Nombre.ToLower().Equals(nombreProducto.ToLower()))
                {
                    item.Value.agregarLote(precio,loteNuevo);
                    existe=true;
                }
            }
            //El producto no existe
            if (!existe)
            {
                //Agregar el producto
                inventario.Add(numAlmacenados,new Producto(nombreProducto,precio,numAlmacenados,loteNuevo));   
            }

        }

        //Muestra la informacion de los productos
        public static void mostrarProductos()
        {
            Console.WriteLine("Informacion de los productos ...");
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                Console.WriteLine($"Id: {item.Value.Id}, Nombre: {item.Value.Nombre}, Precio: {item.Value.Precio}, Cantidad: {item.Value.Cantidad}");
            }
        }
    }
}
