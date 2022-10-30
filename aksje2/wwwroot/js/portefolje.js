let id = localStorage.getItem('data');

$(function () {
    hentSaldo();
    hentPortefolje();
})


function hentSaldo() {
    const url = "aksje/hentSaldo?id=" + id;

    $.get(url, function (saldo) {
        if (saldo != -1) {
            document.getElementById("felt-for-saldo").innerHTML = saldo.toFixed(2) + " USD";
        }
        else {
            console.log("Det har oppstått en feil, saldo er ikke tilgjengelig");
        }

    })
}

function hentPortefolje() {
    const url = "aksje/hentPortefolje?id=" + id;

    $.get(url, function (portefolje) {

        if (portefolje != null) {
            formaterPortefolje(portefolje);
        }
        else {
            console.log("Det har oppstått en feil. Venligst prøv igjen senere");
        }
    })
}

function formaterPortefolje(portefolje) {
    let ut = "<table class='table'><thead class='thead-light'>" + "<tr>" +
        "<th scope='col'>Navn</th>" +
        "<th scope='col'>Antall</th>" +
        "<th scope='col'>Verdi (USD)</th>" +
        "<th style='width: 150px' scope = 'col'>selg</th>" +
        "</tr></thead><tbody>";

    for (let aksje of portefolje) {
        ut += "<tr>" +
            "<th scope='row'>" + aksje.aksje.navn + "</th>" +
            "<td>" + aksje.antall + "</td>" +
            "<td>" + aksje.pris.toFixed(2) + "</td>" +
            "<td> <button class='btn btn-danger' data-toggle='modal' data-target='#selg_popupBox' onclick='selg(" + aksje.aksje.id + ")'>selg</button></td>" +
            "</tr>";

        console.log(aksje.aksje.id);
    }

    ut += "</tbody></table>";
    document.querySelector(".portefolje-container").innerHTML = ut;
}

function selg(id) {
    const url = "aksje/hentEn?id=" + id;

    $("#receitBox2").modal('hide');

    $.get(url, function (aksje) {
        //informasjon skal kun opprettes hvis get kallet er suksessfullt
        //kan hende at bestilling navn og kurs bør defineres utenfor en funksjon slik at man ikke trenger å hente de hver gang

        document.getElementById("popupBox").innerHTML = "Selg " + "(" + aksje.navn + ")";
        document.getElementById("selg-knapp").onclick = function () { fullforSalg(aksje.id, aksje.verdi, aksje.navn); }
    });

}


function fullforSalg(aksje_id, verdi, aksje_navn) {
    let input_antall = document.getElementById("typeNumber");
    let antall = Number(input_antall.value);
    let totalPris = verdi * antall;

    if (isNaN(antall) || input_antall == '') {
        console.log("Venligst oppgi et gyldig antall");
    }



    let selg_objekt =
    {
        personId: id,
        aksjeId: aksje_id,
        antall: antall
    }


    let receitHeader = document.getElementById("receitSelgBox");
    let kvitteringMelding = document.getElementById("selg-kvittering-melding");
    let ut = "";

    document.getElementById("typeNumber").value = "";

    $("#selg_popupBox").modal('hide');
    $("#receitBox2").modal('toggle');

    $.post("aksje/selg", selg_objekt, function (OK) {
        if (OK) {
            receitHeader.innerHTML = "Kvittering for salg";

            ut = "<table><tbody>" + "<tr><td>" + "<b>Salg gjennomført</b>" + "</td></tr><tr><td></td></tr>" +
                "<tr><td>" + "Aksje solgt: " + aksje_navn + "</td></tr>" +
                "<tr><td>" + "Antall: " + antall + "</td></tr>" +
                "<tr><td>" + "Totalpris: " + totalPris.toFixed(2) + " $" + "</td></tr>" + "</tbody></table>";

            kvitteringMelding.innerHTML = ut

            hentSaldo();
            hentPortefolje();

        }
        else {
            receitHeader.innerHTML = "Feil";
            ut = "<table><tbody>" + "<tr><td>" + "Det har opstått en feil" + "</td></tr>" +
                "<tr><td>" + "<b>Salget er avbrutt<b>" + "</td></tr>" + "</tbody></table>";

            kvitteringMelding.innerHTML = ut;
        }
    })
}
