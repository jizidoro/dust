function GetProjetos(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Projeto/ListarProjetos",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetProjeto(callback, projetoId) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Projeto/GetProjeto",
        data: { id: projetoId },
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function clearDropwDown(dropDown) {
    $(dropDown).empty();
    $(dropDown).append($('<option></option>').val("").html(""));
    $(dropDown).removeAttr("disabled");
}

function GetUsuarios(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Usuario/GetUsuarios",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}