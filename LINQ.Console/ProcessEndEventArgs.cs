using System;

namespace LINQ.ConsoleApp
{
    public class ProcessEndEventArgs: EventArgs 
    {
        public int Duration { get; set; } // qui dentro posso aggiungerci quello che voglio
    }
}