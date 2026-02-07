namespace CopisteriaAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            /*
            List<Stampante> stampanti = new List<Stampante>();
            for(int i =0; i < 3; i++)
            {
                stampanti.Add(new Stampante());
            }
            */
            Queue<Studente> studenti = new Queue<Studente>();
            SemaphoreSlim stampantiLibere = new SemaphoreSlim(3); // Supponiamo di avere 3 stampanti disponibili
            Stampante stampanti = new(stampantiLibere);
            Assistente assistente = new(ref studenti, stampantiLibere, stampanti);

            Task t1 = assistente.studentiArrivano();
            Task t2 = assistente.PortaAllaStampante();
            await Task.WhenAll(t1, t2);
        }
    }
}
