using System;
using System.Collections.Generic;
using System.Text;

namespace balsmthin
{
    internal class Balkezesek
    {
        private string nev;
        private DateOnly elso;
        private DateOnly utolso;
        private int suly;
        private int magassag;

        public Balkezesek(string nev, DateOnly elso, DateOnly utolso, int suly, int magassag)
        {
            this.Nev = nev;
            this.Elso = elso;
            this.Utolso = utolso;
            this.Suly = suly;
            this.Magassag = magassag;
        }

        public string Nev { get => nev; set => nev = value; }
        public DateOnly Elso { get => elso; set => elso = value; }
        public DateOnly Utolso { get => utolso; set => utolso = value; }
        public int Suly { get => suly; set => suly = value; }
        public int Magassag { get => magassag; set => magassag = value; }

        public override string ToString()
        {
            return $"Név: {Nev}, Első: {Elso:yyyy-MM-dd}, Utolsó: {Utolso:yyyy-MM-dd}, Súly: {Suly}, Magasság: {Magassag}";
        }
    }
}
