function GetUsuarioVpn(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "UsuarioVpns/GetUsuarioVpns",
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetListaUsuariosAD(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "UsuarioVpns/GetListaUsuariosAD",
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}


function GetUsuarioVpnDetail(callback, usuarioId) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "UsuarioVpns/GetUsuarioVpnDetail",
        data: { id: usuarioId },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

$("button#btIncluir").on('click', function () {
    var usuario = {
        'Id': 0,
        'Login': $("select#ddUsuario").val(),
        'Nome': $("select#ddUsuario option:selected").text(),
        'Fim': $("input#dataFim").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "UsuarioVpns/ConcederAcesso",
        async: false,
        data: {
            usuarioViewModel: usuario,
            justificativa: $("textarea#taJustificativa").val()
        },
        success: function (data) {
            if (data.mensagem == "") {
                alert("Usuário inserido com sucesso!");
                window.location.href = data.redirectUrl;
            } else {
                alert(data.mensagem);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });

    return false;
})

//Function for getting the Data Based upon Employee ID  
function ProlongarAcesso(id) {


    $.ajax({
        url: "UsuarioVpns/GetUsuarioVpnDetail/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.mensagem == "") {
                $('#idUsuario').val(result.data.Id);
                $('#login').val(result.data.Login);
                $('#dataFim').val(result.data.DataFim);

                $('#myModal').modal('show');
                $('#btnProlongarAcesso').show();
            } else {
                alert(result.mensagem);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

$("button#btnProlongarAcesso").on('click', function () {
    var usuario = {
        'Id': $("input#idUsuario").val(),
        'Fim': $("input#dataFim").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "UsuarioVpns/ProlongarAcesso",
        async: false,
        data: {
            usuarioViewModel: usuario,
            justificativa: $("textarea#taJustificativa").val()
        },
        success: function (data) {
            if (data.mensagem == "") {
                alert("Acesso prolongado com sucesso!");
                window.location.href = data.redirectUrl;
            } else {
                alert(data.mensagem);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });

    return false;
})