google.charts.load('current', { 'packages': ['corechart'] });

function obtenerDatosFiltro(seleccion) {
    var seleccionado = seleccion.value;

    // Pide información sobre el filtro que se va a utilizar
    $.ajax({
        type: "POST",
        url: "/ConsultaDinamica/ObtenerDatosFiltro/",
        content: "application/json; charset=utf-8",
        data: { "seleccion" : seleccionado },
        dataType: "json",
        success: function (d) {
            $("#selectFiltros").empty()
            $.each(d, function (key, item) {
                html = "<option value=\"" + item + "\">" + item + "</option>";
                $("#selectFiltros").append(html);
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}

function realizarConsulta() {

    var form = $("#formConsulta");

    // Pide al controlador el resultado de ejecutar la consulta
    $.ajax({
        type: "POST",
        url: "/ConsultaDinamica/RealizarConsulta/",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: form.serialize(),
        success: function (res) {

            var arrayRes = [];
            arrayRes.push([res[0].Key, "Total"]);
            res.shift();
            $("#contenedorPrincipal").empty();

            $.each(res, function (key, item) {

                html = 
                    "<div class=\"col\" style=\"width: 18rem;\">" +
                        "<div class=\"card-header\">" +
                            "<h6> Resultados para " + item.Key + "</h5>" +
                        "</div>" +
                        "<div class=\"card-body\">" +
                            "<p class=\"card-text\">" +
                                "Existen " + item.Value + " accidentes reportados" + " en la base de datos." +
                            "</p>" +
                        "</div>" +
                    "</div>";

                $("#contenedorPrincipal").append(html);
                arrayRes.push([item.Key, item.Value]);
            });

            try {
                var data = google.visualization.arrayToDataTable(arrayRes);
                var chart = new google.visualization.PieChart(document.getElementById('contenedorChart'));
                chart.draw(data);
            } catch (err) {
                alert('No se ha podido crear el gráfico');
            }
            
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });

    return false;
}

function consultaIndicador(btn) {
    var indicador = btn.value;

    // Pide al controlador el resultado de ejecutar la consulta
    $.ajax({
        type: "POST",
        url: "ConsultaIndicador/RealizarConsulta/",
        content: "application/json; charset=utf-8",
        data: { "indicador": indicador },
        dataType: "json",
        success: function (res) {

            var arrayRes = [];
            arrayRes.push([res[0].Key, "Total"]);
            res.shift();
            $("#contenedorPrincipal").empty();

            $.each(res, function (key, item) {
                html = "<div class=\"col\">" +
                    "<div class=\"jumbotron\">" +
                    "<h1 class=\"lead\"> Resultados para " + item.Key + ": </p >" +
                    "<p>" + item.Value + " accidentes reportados" + "</p>" +
                    "</div>" +
                    "</div> ";
                $("#contenedorPrincipal").append(html);
                arrayRes.push([item.Key, item.Value]);
            });

            try {
                var data = google.visualization.arrayToDataTable(arrayRes);
                var chart = new google.visualization.PieChart(document.getElementById('contenedorChart'));
                chart.draw(data);
            } catch (err) {
                alert('No se ha podido crear el gráfico');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}


