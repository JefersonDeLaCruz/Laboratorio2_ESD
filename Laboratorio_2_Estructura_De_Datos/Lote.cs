using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Lote
    {
        public DateTime fechaIngreso { get; set; }

        // Cantidad de producto que contiene el lote
        public int Cantidad { get; set; }

        // Precio unitario de cada producto del lote
        public double Precio { get; set; }

        // Precio total del lote
        public double precioTotal;

        public Lote(DateTime fechaIngreso, int cantidad, double precio)
        {
            this.fechaIngreso = fechaIngreso;
            this.Cantidad = cantidad;
            this.Precio = precio;
            this.precioTotal = precio * cantidad;
        }
    }
}
