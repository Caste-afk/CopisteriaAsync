using System;
using System.Collections.Generic;
using System.Text;

namespace CopisteriaAsync
{
    internal class Assistente
    {
        SemaphoreSlim stampantiLibere;
        SemaphoreSlim prendeCoda;
        Queue<Studente> codaStudenti;
        Stampante stampanti;

        public Assistente(ref Queue<Studente> codaStudenti, SemaphoreSlim stampantiLibere, Stampante stampanti)
        {
            this.codaStudenti = codaStudenti;
            this.stampantiLibere = stampantiLibere;
            this.stampanti = stampanti;
            prendeCoda = new(1);
        }

        public async Task studentiArrivano()
        {
            while (true)
            {
                await Task.Delay(1000);
                await prendeCoda.WaitAsync();
                try
                {
                    Random random = new Random();
                    int priorita = random.Next(1, 4);
                    string file = $"file_{random.Next(1, 100)}.txt";
                    Studente studente = new Studente(priorita, file);

                    codaStudenti.Enqueue(studente);
                    Console.WriteLine($"Studente{Studente.indx} con priorità {priorita} e file {file} è arrivato.");
                }finally
                {
                    prendeCoda.Release();
                }
            }
        }

        public async Task PortaAllaStampante()
        {
            while (true)
            {
                Studente s = null;
                await prendeCoda.WaitAsync();//aggiunge alla coda
                try
                {
                    if(codaStudenti.Count > 0)
                    {
                        codaStudenti = new Queue<Studente>(codaStudenti.OrderByDescending(s => s.priorita));
                        s = codaStudenti.Dequeue();
                    }
                }
                finally
                {
                    prendeCoda.Release();
                }

                if(s == null)
                {
                    await Task.Delay(500);
                    continue;
                }

                await stampantiLibere.WaitAsync();
                //await CercaStampantiLibere(s);
                //await stampanti.stampa(s);
                try
                {
                    Task.Run(async () =>
                    {
                        stampanti.stampa(s);
                    });
                }
                finally
                {
                    stampantiLibere.Release();
                }
            }
        }
        /*
        private async Task CercaStampantiLibere(Studente studente)
        {
            foreach (var s in stampanti)
            {
                if (s.occupata == false)
                {
                    await s.stampa(studente);
                }
            }
        }*/
    }
}
