$("button#btnPesquisar").on('click', function () {

    var filtro = {
        'Usuario': $("select#ddUsuario").val(),
        'DataInicio': $("input#dataInicio").text(),
        'DataFim': $("input#dataFim").text(),
        'Responsavel': $("select#ddResponsavel").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "VpnHistorico/ConsultarHistoricoVpn",
        //async: false,
        data: {
            filtro: filtro
        },
        success: function (data) {
            $("table#tbHistorico>tbody").empty();
            $.each(data, function (i, val) {
                var newRow = "<tr>";
                newRow = newRow + "<td>" + val.Usuario + "</td>";
                newRow = newRow + "<td>" + val.DataAcao + "</td>";
                newRow = newRow + "<td>" + val.Responsavel + "</td>";
                newRow = newRow + "<td>" + val.Acao + "</td>";
                newRow = newRow + "<td>" + val.Inicio + "</td>";
                newRow = newRow + "<td>" + val.Fim + "</td>";
                newRow = newRow + "<td>" + val.Justificativa + "</td>";
                newRow = newRow + "</tr>";
                $("table#tbHistorico>tbody").append(newRow);
            });


        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
})

$("button#btnExcel").on('click', function () {
    $("#tbHistorico").table2excel({
        exclude: ".excludeThisClass",
        name: "HistoricoVPN",
        filename: "HistoricoVPN"
    });
});