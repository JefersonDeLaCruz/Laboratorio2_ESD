using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_2_Estructura_De_Datos
{
    internal class Color
    {
        // Metodo para establecer el color del texto en la consola
        public static void SetTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        // Metodo para restablecer el color por defecto de la consola
        public static void ResetTextColor()
        {
            Console.ResetColor();
        }
    }
}
