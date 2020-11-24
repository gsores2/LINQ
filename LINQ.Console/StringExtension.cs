using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.ConsoleApp
{
    public static class StringExtension //classe con metodi di estensione alla stringa
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> data, string property, string propertyValue)
        {​​ // data non è un vero parametro, è quello su cui faccio il metodo Where nel main
            var results = new List<T>();



            foreach (T value in data)
                //if (condizione(value))
                results.Add(value);



            return results as IEnumerable<T>;
        }​​
         
        public static double ToDouble( this string value) // THIS STRING PERCHE' STO ESTENDENDO STRING
        {
           double.TryParse(value, out double convertedValue); // se non va a buon fine converte value è 0 di default
            return convertedValue; 
        }

        // potrei passare dei parametri 
        public static string WithPrefix(this string value, string prefix) // qui passo un vero parametro anche
        {
            return $"{prefix}-{value}"; //$ fa string interpolation (come +), quando apro graffe posso far riferimento ad altre variabili del mio odice. 
        }
    }
}
