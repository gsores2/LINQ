using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo_LINQToObject
{
    public class Esercizio
    {

        // Creazione liste 
        public static List<Product> CreateProductList()
        {
            var lista = new List<Product>
            {
                new Product { ID = 1, Name = "Telefono", UnitPrice = 300.99 },
                new Product { ID = 2, Name = "Computer", UnitPrice = 800 },
                new Product { ID = 3, Name = "Tablet", UnitPrice = 550.99 }

            };
            return lista;
        }


        public static List<Order> CreateOrderList()
            {
            var lista = new List<Order>(); // altro modo per fare lista, prima vuota poi aggiungo
            var order = new Order
            {
                ID = 1,
                ProductId = 1,
                Quantity = 4
            };
            lista.Add(order);

            var order1 = new Order
            {
                ID = 2,
                ProductId = 2,
                Quantity = 1
            };
            lista.Add(order1);

            var order2 = new Order
            {
                ID = 3,
                ProductId = 1,
                Quantity = 1
            };
            lista.Add(order2);



            return (lista);
        }


        // Esecuzione immediata e ritardata

        public static void DeferredExecution()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();


            // vediamo i risultati 
            foreach (var p in productList) // typer inference 
            {
                Console.WriteLine("{0} - {1} - {2}",p.ID, p.Name, p.UnitPrice);
            }

            foreach (var o in orderList) // typer inference 
            {
                Console.WriteLine("{0} - {1} - {2}", o.ID, o.ProductId,o.Quantity);
            }


            // 1. Creazione Query DIFFERITA
            var list = productList // productList è da dove prendo i dati (è la sorgente dati), quindi sa chce sto prendendo un oggetto di tipo Product
                .Where(product => product.UnitPrice >= 400) // filtralista prodotti per prezzo unitatio
                .Select( p => new { Nome = p.Name, Prezzo = p.UnitPrice }); //li seleziona e li mette in una nuova lista che ha dentro Nome e Prezzo
            //tipo di ritorno di Select è un IEnumerable ANONYMOUS, che viene applicato sull' IEnumerable<Product> restituito da Where
            // sono le cosiddette fluent API (in cascata)
            // product è il record dove vado a prendere, è come se li scorresse


            // Aggiungo Prodotto alla lista
            productList.Add(new Product { ID = 4, Name = "Bici", UnitPrice = 500.99 });

            //Risultati della query, che viene eseguita qui DIFFERITA (vedo anche bici)
            Console.WriteLine("Esecuzione differita:   ");
            foreach (var p in list) //voglio sapere il risultato della query, è qui che esegue la query e guarda cosa sta in productList
            {
                Console.WriteLine("{0} - {1} ", p.Nome, p.Prezzo);
            }




            //2. esecuzione immediata
            var list1 = productList
                .Where(product => product.UnitPrice >= 400)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList(); // forzo esecuzione subito (in questo caso mi dà lista di tipo anonimo, non IEnumerable)

            // se ora ggiungo nuovo prodotto non lo vedo perchè esegue subito la query
            productList.Add(new Product { ID = 5, Name = "Divano", UnitPrice = 450.99});
           
            
            // Risultati

            Console.WriteLine("Esecuzione Immediata:   ");

            foreach (var p in list1) // mi serve comunque 
            {
                Console.WriteLine("{0} - {1} ", p.Nome, p.Prezzo);
            }


            //fino ad ora method syntax, ora vedo anche gli altri modi 

           
        }

        // Sintassi
        public static void Syntax()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();



            //Method Syntax
            var methodList = productList
                .Where(p => p.UnitPrice <= 600)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();



            // Query
            var queryList =
                (from p in productList // sorgente
                where p.UnitPrice <= 600 //condizione
                select new { Nome = p.Name, Prezzo = p.UnitPrice }).ToList();//select

            //to list va fatto a parte se non metto le tonde
            //queryList.ToList();  // dopo questo diventa lista, mi restituisce una lista 

        }

        // Operatori
        public static void Operators()
        {

            var productList = CreateProductList();
            var orderList = CreateOrderList();

            Console.WriteLine("Lista Prodotti");
            foreach (var p in productList)
            {
                Console.WriteLine("{0} - {1} - {2}", p.ID, p.Name, p.UnitPrice);
            }

            Console.WriteLine("Lista Ordini");
            foreach (var o in orderList)
            {
                Console.WriteLine("{0} - {1} - {2}", o.ID, o.ProductId, o.ProductId);
            }



            #region OFTYPE
            //Filtro OfType, mi serve arraylist perchè abbia senso
            var list = new ArrayList();
            list.Add(productList); // posso mettere una lista dentro la Lista
            list.Add("Ciao!!");
            list.Add(123);
            // se avessi fatto list.Add(new Product{} comunque avrei un tipo prodotto non intero)

            var typeQuery =
                from item in list.OfType<int>() // vglio solo int 
                select item;

            Console.WriteLine("Filtro OfType:  ");
            foreach (var item in typeQuery)
            {
                Console.WriteLine(item);
            }
            #endregion


            #region ELEMENT
            // Element
            Console.WriteLine("Esempio Element: ");
            int[] empty = { };
            var el1 = empty.FirstOrDefault();// se non do default allora mi dà eccezione 
            Console.WriteLine( el1);

            var p1 = productList.ElementAt(0).Name; // inizia a contare da 0
            Console.WriteLine(p1);

            #endregion


            #region ORDINAMENTO
            // Ordinamento di prodotti per nome e prezzo
            Console.WriteLine("\r\nOrdinamento:\r\n");

            productList.Add(new Product { ID = 4, Name = "Telefono", UnitPrice = 1000 });
            //query syntax
            var orderedList =
                from p in productList 
                orderby p.Name ascending, p.UnitPrice descending
                select new { Nome = p.Name, Prezzo = p.UnitPrice };

            // Method syntax
            var orderedList2 = productList
                .OrderBy(p => p.Name) // su cosa ordino (P è UN PRODOTTO, LA PROPRIETA' SU CUI FACCIO ORDINAMENTO E' NAME)
                .ThenByDescending(p => p.UnitPrice)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .Reverse(); 


            foreach (var item in orderedList)
            {
                Console.WriteLine("{0} - {1}", item.Nome, item.Prezzo);
            }

            Console.WriteLine("\r\nReverse:\r\n");
            foreach (var item in orderedList2)
            {
                Console.WriteLine("{0} - {1}", item.Nome, item.Prezzo);
            }
            #endregion

            #region QUANTIFICATIORI
            //Quantificatori 
            var hasProductWithT = productList.Any(p => p.Name.StartsWith("T")); //mi aspetto mi ritorni un true
            var allProductsWithT = productList.All(p => p.Name.StartsWith("T")); ; //mi aspetto false
            Console.WriteLine("\r\nCi sono prodotti che iniziano con la T? {0}", hasProductWithT);
            Console.WriteLine("Tutti i prodotti iniziano con la T? {0}", allProductsWithT);
            #endregion


            #region GROUP BY

            //GroupBy

            Console.WriteLine("\r\nGroupBy:\r\n");

            Console.WriteLine("\r\nQuerySyntax:\r\n");
            // query syntax
            //raggruppo ordini per product id

            var groupByList = // è un ienumerable di un igrouping
                from o in orderList//orderlist è una lista che quindi è un ienumerable che ha come extension method groupby
                group o by o.ProductId into groupList
                select groupList; // in questo caso sono uguali grouplist e groupbylist

            foreach (var o in groupByList) // ciclo su ogni raggruppamento che ha sentro una chiave e una serie di ordini 
            {
                Console.WriteLine(o.Key); // groupby mi ridà un IGrouping
                // mi raggruppa in base a una chiave
                // ho raggruppato per productId, quindi in questo o ho la chiave che è productId e tutti gli ordini che hanno quel product id


                foreach (var item in o) // poi ciclo dentro alla singola chiaver
                { // int lo usa per raggruppare (chiave) --> nella classe Igrouping sta scritto che nel foreach ignoro la chiave

                    Console.WriteLine($"\t {item.ProductId } - {item.ProductId}");
                }
            }

            //Methodsyntax 

            Console.WriteLine("\r\nMethodSyntax:\r\n");
            var groupByList2 =
                orderList
                .GroupBy(o => o.ProductId); //groupby è un extensin method di ienumerable

            foreach (var o in groupByList2)
            {
                Console.WriteLine(o.Key); // groupby mi ridà un IGrouping
                // mi raggruppa in base a una chiave
                // ho raggruppato per productId, quindi in questo o ho la chiave che è productId e tutti gli ordini che hanno quel product id


                foreach (var item in o) // poi ciclo dentro alla singola chiaver
                { // int lo usa per raggruppare (chiave) --> nella classe Igrouping sta scritto che nel foreach ignoro la chiave

                    Console.WriteLine($"\t {item.ProductId } - {item.ProductId}");
                }
            }

            #endregion 






        }
    }
}
