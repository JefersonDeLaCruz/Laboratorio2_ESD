using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Producto
    {

        int Id { get; set; } //autogenerada
        string Nombre { get; set; }
        double Precio { get; set; }
        int cantidad { get; set; }
        string fecha { get; set; }//auto generada

        public Producto(string nombre, double precio, int cantidad)
        {
            //el id se generara al momento de crear las instancias de cada producto
            Nombre = nombre;
            Precio = precio;
            this.cantidad = cantidad;
        }
    }
}
