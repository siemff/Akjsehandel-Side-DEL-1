using System;
using System.Collections.Generic;
using aksje2.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YahooFinanceApi;

namespace aksje2.Model
{
    public static class DBInit
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AksjeDB>();                

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();                                  

                Aksje aapl = await HentAksjer.initialiserAksje("AAPL");
                Aksje ibm = await HentAksjer.initialiserAksje("IBM");
                Aksje tsla = await HentAksjer.initialiserAksje("TSLA");
                Aksje dnb = await HentAksjer.initialiserAksje("DNB");
                Aksje spot = await HentAksjer.initialiserAksje("SPOT");
                Aksje twtr = await HentAksjer.initialiserAksje("TWTR");
                Aksje nflx = await HentAksjer.initialiserAksje("NFLX");
                Aksje goog = await HentAksjer.initialiserAksje("GOOG");


                context.akjser.Add(aapl);
                context.akjser.Add(ibm);
                context.akjser.Add(tsla);
                context.akjser.Add(dnb);
                context.akjser.Add(spot);
                context.akjser.Add(twtr);
                context.akjser.Add(nflx);
                context.akjser.Add(goog);


                List<Kjop> liste = new List<Kjop>();


                Person nyPerson = new Person { fornavn = "Line", etternavn = "Hansen", saldo = 10000, kjop = liste};

                List<Kjop> kjopListePortefolje = new List<Kjop>();
                Portfolje nyPortfolje = new Portfolje();
                nyPortfolje.aksjer = kjopListePortefolje;

                nyPerson.portfolje = nyPortfolje;

                context.personer.Add(nyPerson);
                context.porteFoljer.Add(nyPortfolje);
                context.SaveChanges();
            }
        }
    }
}

