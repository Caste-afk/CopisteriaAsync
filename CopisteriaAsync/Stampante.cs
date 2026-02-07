using System;
using System.Collections.Generic;
using System.Text;

namespace CopisteriaAsync
{
    internal class Stampante
    {
        Studente s;
        readonly SemaphoreSlim stampantiLibere;
        //public bool occupata;

        public Stampante(SemaphoreSlim stampantiLibere)
        {
            this.s = s; 
            this.stampantiLibere = stampantiLibere;
        }

        public async Task stampa(Studente s)
        {
            //occupata = true;
            Console.WriteLine($"Stampante sta stampando il file {s.file} del studente{s.id} con priorità {s.priorita}.");
            await Task.Delay(2500);
            Console.WriteLine($"Stampante ha finito di stampare il file {s.file} del studente{s.id} con priorità {s.priorita}.");
            stampantiLibere.Release();
            //occupata = false;
        }
    }
}
