$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Consultas/GetColaboradorCapacity",
        dataType: "json",
        async: false,
        success: function (data) {
            var template = $('#hidden-template').html();

            var qtdColab = 0;
            var totalRow = 1;
            $.each(data, function (i, v) {
                var capAtual = 0;
                var item = $(template).clone();
                var $divIni = $("<div>", { "class": "row", "id": "colabRow" + totalRow });
                var $divfim = $("</div>");

                $(item).find('.img-circle').attr('src', v.UrlFoto);
                //Find the 
                $(item).find('.widget-user-username').html(v.Nome);
                $.each(v.Capacidade, function (j, val) {
                    //Change 'bar' to '-Bar'
                    capAtual += val.Horas;
                    if (val.Horas > 0) {
                        $(item).find('#capacity').append("<li><a href='#'>" + val.Projeto + " - " + val.Disciplina + " <span class='pull-right badge bg-blue'>" + val.Horas + "</span></a></li>");
                    }
                });
                if (capAtual > 8) {
                    $(item).find('.widget-user-header').addClass("bg-red");
                } else {
                    if (capAtual == 8) {
                        $(item).find('.widget-user-header').addClass("bg-blue");
                    } else {
                        $(item).find('.widget-user-header').addClass("bg-yellow");
                    }
                }

                //Append to the source
                if (capAtual > 0) {

                    if (qtdColab == 0) {
                        $('#colaboradores').append($divIni)
                        $('#colabRow' + totalRow).append(item);
                        qtdColab++;
                    } else {
                        if (qtdColab == 3) {
                            $('#colabRow' + totalRow).append(item);
                            $('#colabRow' + totalRow).append($divfim);
                            qtdColab = 0;
                            totalRow++;
                        } else {
                            $('#colabRow' + totalRow).append(item);
                            qtdColab++;
                        }
                    }
                    //$('#colaboradores').append(item)
                }
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
})

