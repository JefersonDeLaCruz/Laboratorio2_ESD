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
        public int Cantidad { get; set; }
        
        //Todo producto que entra se tratara como un lote y se almacenara aqui
        public static List<Lote> Lotes { get; set; } = new List<Lote>();

        //Este constructor se utilizara la primera que se agregue producto
        //el producto no existe
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
        public void agregarLote(double precio,Lote lote)
        {
            this.Precio = precio;

            Lotes.Add(lote);

            //Actualizar cantidad disponible de producto 
            //sumamos la cantidad en cada lote
            this.Cantidad = this.Cantidad + lote.Cantidad;
        }
    }
}
