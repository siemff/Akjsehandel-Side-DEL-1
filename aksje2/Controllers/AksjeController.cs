using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aksje2.DAL;
using aksje2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aksje2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        private readonly IAksjeRepository db;

        public AksjeController(IAksjeRepository aksjeDb)
        {
            db = aksjeDb;
        }

        public async Task<List<Aksje>> hentAksjer()
        {
            return await db.hentAksjer();
        }

        public async Task<Aksje> hentEn(int id)
        {
            return await db.hentEn(id);
        }

        public async Task<bool> kjopAksje(Salg innSalg)
        {
            return await db.kjopAksje(innSalg);
        }

        public async Task<double> hentSaldo(int id)
        {
            return await db.hentSaldo(id);
        }

        public async Task<List<Kjop>> hentPortefolje(int id)
        {
            return await db.hentPortefolje(id);
        }

        public async Task<bool> selg(Selg innSelg)
        {
            return await db.selg(innSelg);
        }


    }
}