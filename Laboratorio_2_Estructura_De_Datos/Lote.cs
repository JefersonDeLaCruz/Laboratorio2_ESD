﻿using System;
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

        public string Id { get; set; }//

        public Lote(DateTime fechaIngreso, int cantidad, double precio,string id)
        {
            this.fechaIngreso = fechaIngreso;
            this.Cantidad = cantidad;
            this.Precio = precio;
            this.precioTotal = precio * cantidad;
            this.Id = id;
        }

        public override string ToString()
        {
            
            return $"Codigo: {this.Id}\t{this.fechaIngreso}\t {this.Cantidad}\t{this.Precio}\t{this.precioTotal}";
        }

        public static void mostrarLotes()
        {
            
            foreach (var item in Producto.Lote)
            {

                
                Console.WriteLine(item);
                
            }
            Console.ReadKey();
            Console.ReadKey();

        }
        public static void mostrarLotes(List<Lote> Lote, int opcionId)
        {

            foreach (var item in Lote)
            {
                string codigo = item.Id;
                int indiceGuinbajo = codigo.LastIndexOf('_');
                int indiceBuscado = Convert.ToInt32(codigo.Substring(2, indiceGuinbajo - 2));

                if (indiceBuscado == opcionId)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
