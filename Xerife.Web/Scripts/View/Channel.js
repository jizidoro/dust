function GetProjetosChannel(callback, gerente) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Channel/GetProjetos",
        dataType: "json",
        data: { gerente: gerente },
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetIteracoesChannel(callback, gerente) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Channel/GetIteracoes",
        dataType: "json",
        data: { gerente: gerente },
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetTarefasProjeto(callback, projetoId) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Channel/GetTarefasProjeto",
        dataType: "json",
        data: { projetoId: projetoId },
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}