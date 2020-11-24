using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LINQ.ConsoleApp
{
    public delegate void ProcessStarted();
    public delegate void ProcessCompleted(int duration);
    public  class BusinessProcess
    {

        //esiste nel framework un delegate già definito che si chiama EventHandler (formato standard per creare eventi senza dover definire il delegate, lo è già lui)
        // eventargs è un oggetto del framework per paggare eventuali parametri
        public event EventHandler StartedCore; 
        public event ProcessStarted Started;
        public event ProcessCompleted Completed;
        public void ProcessData()
        {
            Console.WriteLine("STARTING PROCESS");
            Thread.Sleep(2000); //attesa
            Console.WriteLine("Porcess Started");
            // sollevo evento started
            if (Started!=null) // guardia: inutile sollevare evento che nessuno ha sottoscritto
            Started(); 
            //qualcuno se lo deve gestire sto evento

            Thread.Sleep(3000); //attesa
            Console.WriteLine("Porcess Completed");
            if (Completed!= null)
            Completed(5000);  
        }
    }
}
