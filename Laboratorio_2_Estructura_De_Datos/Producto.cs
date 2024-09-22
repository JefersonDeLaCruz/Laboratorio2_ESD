using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Producto
    {
        //El id se generara al momento de crear una instancia de esta clase y
        //corresponde con el indice de almacenamiento en el diccionario donde se almacenan
        //los productos
        public int Id { get; set; }

        public string Nombre { get; set; }

        //Se actualiza con el precio unitario de cada producto del lote mas reciente
        public double Precio { get; set; } 
        
        //Se calcula a partir de la cantidad que hay en cada lote
        //Se actualiza con cada compra y venta
        public int Cantidad { get; set; }
        
        //Todo producto que entra se tratara como un lote y se almacenara aqui
        //Esta lista nos sirve para costo promedio
        public List<Lote> Lotes { get; set; } = new List<Lote>();

        //Saldo, cantidad de dinero que hay dependiendo de la cantidad producto en existencias y su costo
        public double saldo = 0;
        //El saldo es cambiante se actualia con compra y venta
        //Calcula el saldo de acuerdo a las existencias de productos y su costo por lote
        public void actualizarSaldo()
        {
            foreach (Lote lote in Lotes)
            {
                saldo = saldo + (lote.Precio*lote.Cantidad);
            }
        }

        //Esto es para UEPS ya que se debe usar una pila
        public Stack<Lote> lotesEnPila = new Stack<Lote>();
        //Devuelve una copia de la lista de lotes como una pila de lotes
        public void actualizarLotesenPila()
        {
            foreach (Lote lote in Lotes)
            {
                lotesEnPila.Push(lote);
            }
        }

            //Este constructor se utilizara la primera vez que se agregue producto,
            //el producto no existe todavia
            public Producto(string nombre, double precio, int id, Lote lote) 
        {
            this.Id = id;

            this.Nombre = nombre;

            this.Precio = precio;

            Lotes.Add(lote);

            //Actualizar cantidad disponible de producto 
            //sumamos la cantidad en cada lote
            this.Cantidad = this.Cantidad + lote.Cantidad;
        }

        //Se utilizara cuando solo queremos agregar mas producto
        //el producto ya existe, es decir, solo queremos agregar un nuevo lote
        public void agregarLote(double precio, Lote lote)
        {
            //Actualizo el precio con el precio del nuevo lote
            this.Precio = precio;

            //Agrego el nuevo lote
            Lotes.Add(lote);

            //Actualizar cantidad disponible de producto 
            //Sumamos la cantidad de producto que trae el nuevo lote con las existencias(Cantidad de producto actual)
            this.Cantidad = this.Cantidad + lote.Cantidad;
        }

        //Muestra los lotes del producto
        public void mostrarLotes()
        {
            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
            Console.WriteLine("\nPRODUCTO:");
            Color.ResetTextColor(); // Reestablecer color por defecto
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine($"\tId: {Id}\t|\t Nombre: {Nombre}\t|\t Precio: {Precio}\t|\t Cantidad: {Cantidad}");
            Console.WriteLine("---------------------------------------------------------------------------------------");

            Color.SetTextColor(ConsoleColor.DarkBlue); // Color
            Console.WriteLine("\nLOTES DEL PRODUCTO:");
            Color.ResetTextColor(); // Reestablecer color por defecto
            foreach (Lote lote in Lotes)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"\tId: {lote.Id}\t|\t Precio unitario: {lote.Precio}\t|\t Cantidad: {lote.Cantidad}\t|\t Fecha: {lote.fechaIngreso}\t");
            }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

        }
    }
}
