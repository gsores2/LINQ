using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.ConsoleApp
{
    public static class StringExtension //classe con metodi di estensione alla stringa 
    {
        // DEVONO ESSERE STATIC CLASSE E METODO DEGLI EXTENSION METHODS!!!!!!!!!!!!!!!
        public static double ToDouble( this string value) // THIS STRING PERCHE' STO ESTENDENDO STRING
        {
           double.TryParse(value, out double convertedValue); // se non va a buon fine converte value è 0 di default
            return convertedValue; 
        }

        // potrei passare dei parametri 
        public static string WithPrefix(this string value, string prefix) // qui passo un vero parametro anche
        {
            return $"{prefix}-{value}"; //$ fa string interpolation (come +), quando apro graffe posso far riferimento ad altre variabili del mio odice. 
                                        // OPPURE
                                        //return prefix + "-" + value;
                                        // OPPURE
                                        //return string.Format(
                                        //    "{0}-{1}",
                                        //    prefix,
                                        //    value
                                        //);
        }
    }
    }
}
