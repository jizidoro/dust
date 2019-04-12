$("button#btnPesquisar").on('click', function () {

    var filtro = {
        'Projeto': $("select#ddProjetos").val(),
        'Status': $("select#ddStatus").val(),
        'DataInicio': $("input#dataInicio").val(),
        'DataFim': $("input#dataFim").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "LogIntegracao/ConsultarlogIntegracao",
        //async: false,
        data: {
            filtro: filtro
        },
        success: function (data) {
            $("table#tbHistorico>tbody").empty();
            $.each(data, function (i, val) {
                var newRow = "<tr>";
                newRow = newRow + "<td>" + val.Projeto + "</td>";
                newRow = newRow + "<td>" + val.Status + "</td>";
                newRow = newRow + "<td>" + val.Data + "</td>";
                newRow = newRow + "<td>" + val.Registro + "</td>";
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
        name: "LogIntegracaoChannelTfs",
        filename: "LogIntegracaoChannelTfs"
    });
});