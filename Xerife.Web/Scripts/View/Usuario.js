function ConsultarUsuarios(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Usuario/ConsultarUsuarios",
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

function GetPerfis(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Perfil/GetPerfis",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#idUsuario').val("");
    $('#ddUsuario').val("");
    $('#ddPerfil').val("");
    $('#ddUsuario').show();
    $('#login').hide();
    $('#ddUsuario').removeAttr("disabled");
    $('#btnAtualizar').hide();
    $('#btnIncluir').show();
}


//Function for getting the Data Based upon Employee ID  
function EditUsuario(id) {

    $('#ddUsuario').css('border-color', 'lightgrey');
    $('#ddPerfil').css('border-color', 'lightgrey');
    $('#ddUsuario').attr('disabled', 'disabled');
    $('#ddUsuario').hide();
    $('#login').show();

    $.ajax({
        url: "Usuario/GetUsuario/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#idUsuario').val(result.Id);
            $('#login').val(result.Login);
            $('#ddPerfil').val(result.IdPerfil);
            $('#ddPerfil').trigger("chosen:updated");

            $('#myModal').modal('show');
            $('#btnAtualizar').show();
            $('#btnIncluir').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

$("button#btnFormInclusao").on('click', function () {
    $("select#ddUsuario").chosen({ width: '100%' });
    GetListaUsuariosAD(function (usuarioList) {
        $.each(usuarioList, function (i, val) {
            $("select#ddUsuario").append($('<option></option>').val(val.Login).html(val.Nome));
        });
        $("select#ddUsuario").trigger("chosen:updated");
    });
});

$("button#btnIncluir").on('click', function () {
    var usuario = {
        'Id': $("#idUsuario").val(),
        'Login': $("#ddUsuario").val(),
        'Perfil.Id': $("#ddPerfil").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "Usuario/IncluirUsuario",
        data: { usuario: usuario },
        success: function (data) {
            if (data.status) {
                $('#myModal').modal('hide');
                window.location.href = data.redirectUrl;
            } else {
                alert("Erro ao executar operação");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
    return false;
});

$("button#btnAtualizar").on('click', function () {
    var idUsuario = $("#idUsuario").val();
    var idPerfil = $("#ddPerfil").val();

    $.ajax({
        type: "POST",
        url: window.urlBase + "Usuario/AlterarPerfilUsuario",
        data: { idUsuario: idUsuario, idPerfil: idPerfil },
        success: function (data) {
            if (data.status) {
                $('#myModal').modal('hide');
                window.location.href = data.redirectUrl;
            } else {
                alert("Erro ao executar operação");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
    return false;
});

//Function for getting the Data Based upon Employee ID  
function ExcluirUsuario(id) {
    $.ajax({
        url: "Usuario/ExcluirUsuario/" + id,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data.status) {
                window.location.href = data.redirectUrl;
            } else {
                alert("Erro ao executar operação");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}