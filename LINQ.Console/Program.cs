
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace LINQ.ConsoleApp
{
    class Program
    {

        //delegato 
        public delegate int Sum(int val1, int val2); // è un nuovo tipo di dato (sum), come se osse una classe
        public static int PrimaSomma(int valore1, int valore2)
        {
            return valore1 + valore2;
        }
        public static void Chiamami(Sum Fz)
        {
            Fz(1, 2);
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("LINQ");

            //string firstName = "Giulia";

            //var lastName = "Soresini";

            //using var file = new StreamWriter();

            //List<int> data = new List<int> { 1, 2, 3, 4 };
            //foreach (var value in data)
            //{
            //    Console.WriteLine("#"+value);
            //}

            // poi se mi creo una classe employee e faccio la lista, posso usare lo stesso foreach, cambia il tipo di value che prima eraint ora employee

            //List<Employee> data = new List<Employee> {};
            //foreach (var value in data)
            //{
            //    Console.WriteLine("#" + value.Name); // tipo viene capito non a runtime, ma quando lo dichiaro direttamente 
            //}

            //var person = new {Nome="Giulia" };
            //var person2 = person;





            //extension methods

            //string example = "230";
            //Console.WriteLine(example.ToDouble()); // METODO CHE HO AGGIUNTO
            //var prefix = example.WithPrefix("[TST]");
            //Console.WriteLine(prefix);




            //uso delegato
            //Sum lamiasomma = new Sum(PrimaSomma); // lamiasomma mia somma è del tipo Sum, perchè il metodo ha la stessa firma del delegate
            //// oppure 
            ////Sum lamiasomma = PrimaSomma; 

            //// POSSO PASSARE LA FUNZIONE A UN ETOFDO CHIAMAMI
            //Chiamami(lamiasomma);



            // esempio business process

            //var process = new BusinessProcess(); // istanza notifier

            //process.Started += Process_Started ; // aggiungo alla lista dei delegate ( mi sottoscrivo)
            //process.Started += Process_Started1;
            //process.Completed += Process_Completed;
            //process.ProcessData();


            //func e action 
            // sono delegate che già esistono, sono più facili da rappresentare perchè fatti coi generics
            Func<int> primaFunc; // è uguale a fare public delegate int primaFunc(); 
            //posso passargli un metodo dopo l'uguale,tipo

            Func<int, int, int> PrimaFunc = PrimaSomma; //l'ultimo int è il ritorno, gli altri due sono i parametri
            // così sto usando il metodo PrimaSomma, che ha questa firma 

            // func sostituisce il delegate, va bene per quelle funzioni che hanno tipo di ingresso (n) e uscita

            Action<int> primaAction; // è una funzione che non ha valori in uscita  (void)

            // posso sempre usare i tipi predefiniti eventhandler o func e action al posto del delegate
            // definsico il mio delegate solo se mi serve un nome specifico 




            // lambda expressions è un delegate qindi il tipo di ritorno deve essere un delegate

            Func<int, int> lambdaZero = x => 2 * x; // su una riga sottintende return, altrimenti lo devo mettere con le graffe 

            // è uguale a scrivere 
            Func<int, int> lambdaZeroZero = Mult;



            //list.where nella slides
            var dataInt = new List<int> { 1, 2, 3, 4, 5, 6 };
            var results = Where(dataInt, x => x > 2); // where rprende qualsiasi classe implementi IEnmerable



            // expression tree

            //supponiamo di avere una lista doi employees
            List<EmployeeInt> employees = new List<EmployeeInt>
            {
                new EmployeeInt{ID=1, Name="Roberto"},
                  new EmployeeInt{ID=2, Name="Alice"},
                    new EmployeeInt{ID=3, Name="Mauro"},
                      new EmployeeInt{ID=4, Name="Roberto"},

            };
            //se volessi fare un where dinamico, in cui prendo da input due stringhe
            // una volta su ID, una volta su Nome ecc.. --> o nel codice mi costruisco tutte le espressioni possibili
            // o rendo dinamica la creazione della lambda che passo a where

            var result = employees.Where("ID", "1"); // mettere where in StringExtensions





            // expression 
            ParameterExpression y = Expression.Parameter(typeof(int), "x"); // "" ho solo il nome del parametro così come viene stampato y si chiama x
            //QUI HO DICHIARATO UN PARAMETRO, CHE POI DEVO USARE, che si chiama y (che è un oggetto che ha due proprietà, cjhe sono il tipo e il nome in cui lo vedo stampato


            Expression<Func<int, int>> squareExpression =
                Expression.Lambda<Func<int, int>>( // lambda vuole un generic che le dica come essere fatta, lielo dico con un deelegate 
                                                   // voglio una lambda che prenda int e dia int
                    Expression.Multiply(y, y), // corpo della lambda, poi parametri (è la firma di lambda, che è un metodo di Expression)
                    new ParameterExpression[] { y });//array di parametry a cui passo il mio y

            //dopo di che faccio il compile e a quel punto posso richiamare func

            Func<int, int> funzione = squareExpression.Compile();
            Console.WriteLine(funzione(3));
        }

        // definisco questo metodo where qui, se invece volessi farlo come extension method dovrei dirgli this IEnumbrable
        private static IEnumerable<int> Where(IEnumerable<int> data, Func<int,bool> condizione)
        {
            var results = new List<int>();
            foreach (int value in data)
            {
                if (condizione(value))
                    results.Add(value);
            }
            return results;
        }
        private static int Mult(int x)
        {
            return 2 * x;
        }

        private static void Process_Completed(int duration)
        {
            Console.WriteLine("il processo è durato: " + duration/1000 + " secondi");
        }

        private static void Process_Started1()
        {
            Console.WriteLine("Altro Handler");
        }

        private static void Process_Started() // metodo con firma del delegate
        {
            Console.WriteLine("Ricevuto, processo avviato!");
        }
    }




    internal class Employee<T> // internal è un modificatore di accesso, per cui vedo la classe solo nell'assembly in cui è defnita
    {
        public string Name { get; set; }
    }

    //internal class Employee
    //{​​   // versione estesa che fa la stessa cosa di quella complessa 
    //    private int _id; // campo privato
    //    public int ID {​​ 
    //    get { // mi restituisce id
    //            return _id;
    //        }
    //        set
    //        {
    //            if(value<=0)
    //                throw new ArgumentException("ID must be positive");
    //            _id = value; 
    //        }
    //    }​​
    //    public string Name {​​ get; set; }​​ 
    //    // questo fa la stessa cosa di sopra, ma non mi spiega come, se cambio getter e setter devo fare come sopra 
    //}​​

    //// value è una keyword standard, equivale a quello che gli passo. Poi su getter e setter posso fare degli if ecc èerchè sono funzioni
    //// value è quello che gli assegno dal codice tipo pipppo.ID = 9 value è 9
    internal class EmployeeInt
    {​​
    public int ID {​​ get; set; }​​ // get e set sono delle vere e proprie funzioni, che io posso riscrivere. Il campo è una variabile senza getter e setter
    public string Name {​​ get; set; }​​
    }​​


    //internal class EmployeeString
    // {​​
    //public string ID {​​ get; set; }​​
    //public string Name {​​ get; set; }​​
    //}​​
}
