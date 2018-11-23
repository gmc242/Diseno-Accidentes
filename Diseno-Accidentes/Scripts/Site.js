// Clases para manejar el patrón observer en la última consulta

// Clase abstract observer
class Observer {

    update(datos) { }

}

// Clase concrete observer
class GraficoObserver extends Observer {

    constructor() {
        super();
        this._tipoGrafico = 'column';
    }

    set tipoGrafico(tipo) { this._tipoGrafico = tipo; }

    // Función que junta datasets y luego dibuja el gráfico
    anadirGrafico(datos, grafico) {

        var data = google.visualization.arrayToDataTable(datos[0]);

        // Une los datos de la consulta para realizar el gráfico
        for (var i = 1; i < datos.length; i++) {
            var temp = google.visualization.arrayToDataTable(datos[i]);
            data = google.visualization.data.join(data, temp, 'full', [[0, 0]], [1], [1]);
        }

        grafico.draw(data);
    }

    update(datos) {

        // Dependiendo del parámetro del observable crea el gráfico necesario y lo manda a dibujar con los datos
        switch (this._tipoGrafico) {

            case 'pie':
                this.anadirGrafico(datos, new google.visualization.PieChart(document.getElementById('contenedorChart')));
                break;
            case 'column':
                this.anadirGrafico(datos, new google.visualization.ColumnChart(document.getElementById('contenedorChartColumn')));
                break;
            case 'bar':
                this.anadirGrafico(datos, new google.visualization.BarChart(document.getElementById('contenedorChartBar')));
                break;
            default:
                this.anadirGrafico(datos, new google.visualization.ColumnChart(document.getElementById('contenedorChart')));
                break;
        }

    }
    
}

// Clase Observable concreta, por el dominio reducido del problema y por usa JS, no se especifica Observable abstracto
class ObservableDatos {

    constructor() {
        this._observadores = [];
        this._indicadores = [];
        this._datos = [];
    }

    get datos() { return this._datos; }
    get indicadores() { return this._indicadores; }
    get observadores() { return this._observadores; }

    set datos(datos) { this._datos = datos; }

    anadirObservador(obs) { this._observadores.push(obs); }

    // Función para "avisar" a los observadores
    notify() {

        // Guarda la referencia de this, por como funciona JS
        var this_obj = this;

        // Actualiza cada uno de los observers, se mandan los datos como parámetro
        $.each(this._observadores, function (key, obs) {
            obs.update(this_obj._datos);
        });

    }

    // Función que cambia los indicadores y como resultado los datos, como colateral también los observadores
    anadirIndicador(indicador) {

        // Se añade el indicador solo si no existía antes

        if (!this._indicadores.includes(indicador)) {

            this._indicadores.push(indicador);
        }

        // Si el indicador está, más bien se elimina

        else {
            this._indicadores.splice(this._indicadores.indexOf(indicador, 0), 1);
        }

        // De todas maneras el set de datos se actualiza y manda a actualizar los observers

        // La matriz va a tener los resultados de la consulta de cada uno de los indicadores
        var this_obj = this;

        // Pide al controlador el resultado de ejecutar las consultas
        $.ajax({
            type: "POST",
            url: "ConsultaObserver/RealizarConsulta/",
            content: "application/json; charset=utf-8",
            data: { "indicadores": this._indicadores },
            dataType: "json",
            success: function (res) {

                $("#contenedorPrincipal").empty();

                // La matriz va a tener los resultados de la consulta de cada uno de los indicadores
                var matriz = [];

                $.each(res, function (key, res_consulta) {

                    var arrayRes = [];
                    arrayRes.push([res_consulta[0].Key, "Total"]);
                    res_consulta.shift();

                    $.each(res_consulta, function (key, item) {
                        // Añade al array el valor junto con su agregado
                        arrayRes.push([item.Key, item.Value]);
                    });

                    // Añade el array de resultados a la matriz
                    matriz.push(arrayRes);

                });

                // Actualiza los datos
                this_obj.datos = matriz;

                // Llama a los observadores a actualizarse con los datos nuevos
                this_obj.notify();

            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error!!');
            }
        });
    }

}

var observable = new ObservableDatos();

// Funciones de ayuda para el manejo de la consulta dinámica

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

// Función que realiza la petición al controlador en la consulta por indicador

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

            $("#contenedorPrincipal").empty();

            var arrayRes = [];
            arrayRes.push([res[0].Key, "Total"]);
            res.shift();

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

                // Añade al array el valor junto con su agregado
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


// Funciones para ayudar al manejo del patrón observer
function anadirIndicador(btn) {
    var value = btn.value;
    observable.anadirIndicador(value);
}

function inicializarObserver() {
    // Inicializa el observable
    observable = new ObservableDatos();

    // Crea y agrega un observer de tipo Column
    observable.anadirObservador(new GraficoObserver());

    // Crea otro observer con graficos de tipo Bar
    var obs2 = new GraficoObserver();
    obs2.tipoGrafico = 'bar';
    observable.anadirObservador(obs2);
}

// Funcionalidad con respecto al mapa
