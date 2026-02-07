using System;
using System.Collections.Generic;
using System.Text;

namespace CopisteriaAsync
{
    internal class Studente
    {
        public int priorita;
        public string file;
        public static int indx=0;
        public int id;
        public Studente(int priorita, string file)
        {
            this.priorita = priorita;
            this.file = file;
            id = indx;
            indx++;
        } 
    }
}
