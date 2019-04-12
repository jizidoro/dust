function GetParametroSistema(callback) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "ParametroSistema/ConsultarParametroSistema",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function EditParametro(id) {
    $('#nome').css('border-color', 'lightgrey');
    $('#descricao').css('border-color', 'lightgrey');
    $('#perfil').css('border-color', 'lightgrey');
    $('#sigla').css('border-color', 'lightgrey');
    $('#valor').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ParametroSistema/GetById/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#idParametro').val(result.Id);
            $('#nome').val(result.Nome);
            $('#descricao').val(result.Descricao);
            $('#perfil').val(result.NomePerfil);
            $('#sigla').val(result.Sigla);
            $('#valor').val(result.Valor);

            $('#myModal').modal('show');
            $('#btnAtualizar').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

$("button#btnAtualizar").on('click', function () {
    var parametro = {
        'Id': $("#idParametro").val(),
        'Nome': $("#nome").val(),
        'Descricao': $("#descricao").val(),
        'Valor': $("#valor").val()
    }

    $.ajax({
        type: "POST",
        url: window.urlBase + "ParametroSistema/AlterarParametroSistema",
        data: { parametro: parametro },
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