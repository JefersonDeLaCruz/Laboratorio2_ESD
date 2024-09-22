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
        public static void pepsVenta()
        {
            //primero en entrar primero en salir
            mostrarProductos();
            Console.WriteLine("Ingrese el ID del producto que sera vendida");
            int idProducto;


            List<string> codigos = new List<string>();
            while (!int.TryParse(Console.ReadLine(), out idProducto) || idProducto < 1 || idProducto > inventario.Count)
            {
                Console.WriteLine("Entrada no valida");
            }
            Console.WriteLine($"El producto seleccionado es: {inventario[idProducto].Nombre}\nIngrese la cantidad a vender");
            int cantidadSaliente;

            while (!int.TryParse(Console.ReadLine(), out cantidadSaliente) || cantidadSaliente < 1 || cantidadSaliente > inventario[idProducto].Cantidad)
            {
                Console.WriteLine("Entrada no valida");
            }


            List<int> indicesProductosEnLote = new List<int>();
            int z = 0;
            List<Lote> lotes = new List<Lote>();
            lotes = inventario[idProducto].Lote;
            foreach (var item in lotes)
            {
                string codigo = item.Id;
                int indiceGuionbajo = codigo.LastIndexOf('_');
                int indiceBuscado = Convert.ToInt32(codigo.Substring(2, indiceGuionbajo - 2));

                if (indiceBuscado == idProducto)
                {
                    codigos.Add(item.Id);
                    indicesProductosEnLote.Add(z);
                }
                z++;
            }
            bool ventaRealizada = false;
            while (!ventaRealizada)
            {

                int productosVendidos = 0;
                int aux = cantidadSaliente;
                for (int i = 0; i < indicesProductosEnLote.Count; i++)
                {

                    //creamos una cola que contenga los productos del lote mas antiguo
                    List<Producto> productos = new List<Producto>();
                    productos = Producto.LotesIndivuduales[indicesProductosEnLote[i]];

                    Queue<Producto> colaP = new Queue<Producto>(productos);


                    while (productosVendidos < cantidadSaliente && colaP.Count > 0)
                    {
                        var producto = colaP.Dequeue();
                        Console.WriteLine($"Vendiendo {productosVendidos+1}: {producto.Nombre}");
                        productosVendidos++;
                        
                    }
                    if (colaP.Count <= 0 && productosVendidos < cantidadSaliente)
                    {
                        //si este se cumple es por que el lote quedo vacio pero aun falta vender mas producto
                        Producto.LotesIndivuduales[indicesProductosEnLote[i]].Clear();
                        lotes[indicesProductosEnLote[i]].Cantidad = 0;
                        Inventario.inventario[idProducto].Cantidad = Inventario.inventario[idProducto].Cantidad - productosVendidos;
                        Console.WriteLine($"Productos en el lote {lotes[indicesProductosEnLote[i]].Id} han sido vendidos");
                        aux -= productosVendidos;
                        Console.ReadKey();
                    }
                    else if (colaP.Count > 0 &&  productosVendidos >= cantidadSaliente)
                    {
                        
                        //este se cumple si el lote actual dio abasto la venta
                        //cantidadSaliente -= productosVendidos;
                        Producto.LotesIndivuduales[indicesProductosEnLote[i]].RemoveRange(0,aux);
                        lotes[indicesProductosEnLote[i]].Cantidad = lotes[indicesProductosEnLote[i]].Cantidad - aux;
                        Inventario.inventario[idProducto].Cantidad = Inventario.inventario[idProducto].Cantidad - aux;
                        Console.ReadKey();
                    }

                    if (productosVendidos >= cantidadSaliente)
                    {
                        ventaRealizada = true;
                        break;
                    }
                }
                if (productosVendidos >= cantidadSaliente)
                {
                    ventaRealizada = true;
                    break;
                }


            }
            Console.WriteLine("Venta realizada con exito");


            
            Console.ReadKey();






        }

        //Este metodo agrega un producto/os al inventario 
        public static void agregarProductosAInventario()
        {
            //Datos necesarios 
            Console.Write("Nombre del producto: ");
            string nombreProducto = Console.ReadLine();

            while (nombreProducto.Length < 2)
            {
                Console.WriteLine("El nombre del producto debe contener al menos dos caracteres");
                nombreProducto = Console.ReadLine();
            }
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

            //Este sera el Id del nuevo producto agregado 
            int numAlmacenados = inventario.Count + 1;
            //le agregue el '+1' ya que al probarlo el id del primer producto era == count osea  = 0




            //Buscamos si el producto existe en el inventario
            Boolean existe = false;
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                //El producto existe
                if (item.Value.Nombre.ToLower().Equals(nombreProducto.ToLower()))
                {

                    //Se agregara este nuevo lote del producto
                    //en la siguiente linea de codigo se genera autogenera el id del lote de la siguiente manera
                    //abn_c --> donde a y b son las primeras dos letras del nombre del producto dentro del lote
                    //n es el id del producto dentro del lote y c sera el Lote.count + 1
                    //abn servira para identificar de que producto esta compuesto el lote
                    //c sera el id del lote como tal
                    string idLote = $"{item.Value.Nombre.Substring(0, 2)}{item.Value.Id}_{item.Value.Lote.Count + 1}";
                    Lote loteNuevo = new Lote(DateTime.Now, cantidad, precio, idLote);
                    item.Value.agregarLote(precio, loteNuevo);
                    //
                    List<Producto> listaProductos = new List<Producto>();
                    for (int i = 0; i < loteNuevo.Cantidad; i++)
                    {
                        Producto p = new Producto(nombreProducto, loteNuevo.Precio, numAlmacenados);
                        listaProductos.Add(p);
                    }
                    Producto.LotesIndivuduales.Add(listaProductos);
                    existe = true;
                }
            }
            //El producto no existe
            if (!existe)
            {
                string idLote = $"{nombreProducto.Substring(0, 2)}{numAlmacenados}_{1}";
                Lote loteNuevo = new Lote(DateTime.Now, cantidad, precio, idLote);
                //Agregar el producto
                inventario.Add(numAlmacenados, new Producto(nombreProducto, precio, numAlmacenados, loteNuevo));

                //---------
                List<Producto> listaProductos = new List<Producto>();
                for (int i = 0; i < loteNuevo.Cantidad; i++)
                {
                    Producto p = new Producto(nombreProducto, loteNuevo.Precio, numAlmacenados);
                    listaProductos.Add(p);
                }
                Producto.LotesIndivuduales.Add(listaProductos);
                //------
            }

        }

        //Muestra la informacion de los productos
        public static void mostrarProductos()
        {
            //muestra los productos dentro del diccionario
            Console.WriteLine("Informacion de los productos ...");
            //---> variable item de tipo KeyValuePair<int, Producto> almacena tanto la llave como el el valor
            //item.value accede al objeto producto almacenado como valor
            //al ser item.value == instancia de un producto podemos acceder a sus campos con el operador punto
            Console.WriteLine($"{"ID",-5} {"Nombre",-25} {"Precio",-10} {"Cantidad",-10}");
            Console.WriteLine(new string('-', 50));
            foreach (KeyValuePair<int, Producto> item in inventario)
            {
                Console.WriteLine($"{item.Value.Id,-5} {item.Value.Nombre,-25} {item.Value.Precio.ToString("F2"),-10} {item.Value.Cantidad,-10}");
                Console.WriteLine(new string('-', 50));
            }
        }

        public static void verProductosIndivuduales(List<Lote> Lote, string codigoIngresado, int id)
        {
            //muestra los productos por unidad dentro de cada lote
            //la longitud de la lista de Lote con lotesIndividuales es la misma
            //entonces necesitamos extrar el id del lote del codigo generado
            //por ejemplo si el codigo es col1_3 necesitamos extraer el 3


            if (codigoIngresado == "-1")
            {
                Console.Clear();
                Console.WriteLine("Pulse cualquier tecla para regresar al menu principal");
            }
            else
            {
                foreach (var item in Lote)
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

                }
                else
                {
                    //ahora que ya tenemos el id simplemente mostramos todos los items en ese indice de la lista de listas
                    int i = 1;
                    Console.WriteLine($"{"NOMBRE",-15} {"PRECIO UNITARIO",10}");
                    Console.WriteLine(new string('-', 50));
                    foreach (var item in Producto.LotesIndivuduales[id - 1])
                    {
                        Console.WriteLine($"{i}. {item}");
                        i++;
                    }

                    Console.WriteLine();
                    Console.ReadKey();
                }

            }


        }


    }
}
