 using System;
using aksje2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aksje2.DAL
{
    public interface IAksjeRepository
    {
        Task<List<Aksje>> hentAksjer();
        Task<Aksje> hentEn(int id);
        Task<bool> kjopAksje(Salg innSalg);
        Task<double> hentSaldo(int id);
        Task<List<Kjop>> hentPortefolje(int id);
        Task<bool> selg(Selg innSelg);
    }
}

