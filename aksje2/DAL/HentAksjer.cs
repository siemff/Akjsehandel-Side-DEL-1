using System;
using Avapi;
using Avapi.AvapiTIME_SERIES_DAILY;
using System.Collections.Generic;
using System.Linq;
using aksje2.Model;
using Avapi.AvapiTIME_SERIES_INTRADAY;
using System.Threading.Tasks;
using YahooFinanceApi;


namespace aksje2.DAL
{
    public class HentAksjer
    {
        public static async Task<Aksje> initialiserAksje(string innSymbol)
        {
            var securities = await Yahoo.Symbols(innSymbol).Fields(Field.Symbol, Field.RegularMarketPrice, Field.RegularMarketOpen, Field.RegularMarketDayHigh,
                  Field.RegularMarketDayLow, Field.RegularMarketVolume).QueryAsync();

            var stockSecurity = securities[innSymbol];

            var symbol = stockSecurity[Field.Symbol];        
            var low = stockSecurity[Field.RegularMarketDayLow];
            var high = stockSecurity[Field.RegularMarketDayHigh];
            var open = stockSecurity[Field.RegularMarketOpen];
            var price = stockSecurity[Field.RegularMarketPrice];       
            var volume = stockSecurity[Field.RegularMarketVolume];

            var formatLow = Math.Round(low, 2);
            var formatHigh = Math.Round(high, 2);
            var formatOpen = Math.Round(open, 2);
            var formatPrice = Math.Round(price, 2);           

            Aksje nyAksje = new Aksje();

            nyAksje.navn = symbol;
            nyAksje.low = formatLow;
            nyAksje.high = formatHigh;
            nyAksje.open = formatOpen;
            nyAksje.verdi = formatPrice;
            nyAksje.omsetning = volume;

            return nyAksje;
        }
       
    }
}

