using System;
namespace aksje2.Model
{
// endre omsetning til å ha navn: volume

    public class Aksje
    {
        public int id { get; set; }
        public string navn { get; set; }        
        public double low { get; set; }
        public double high { get; set; }
        public double open { get; set; }
        public double verdi { get; set; }       
        public double omsetning { get; set; }
    }
}

