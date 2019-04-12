var quantTarefas = 0;

function gerarIteracoes(alterando) {
    $('select[id^="ddIteration"]').each(function () { this.remove(); });

    var tfs = $("select#ddTfs").val();
    var collection = $("select#ddCollection").val();
    var project = $("select#ddProject").val();
    var team = $("select#ddTeam").val();
    var iteracoes = "";

    GetTeamIterations(function (iterationList) {
        $.each(iterationList, function () {
            iteracoes += "<option value='" + this.path + "'>" + this.name + "</option>";
        });

        if (iteracoes != "") {
            var sel = 0;
            for (var i = 0; i < quantTarefas; i++) {
                $('#tar_' + (i + 1)).append("<div class='col-md-6'><select id= 'ddIteration_" + (i + 1) +
                    "' class='form-control' style='width: 35%;'>" + iteracoes + "</select></div>");

                if (alterando) {
                    var chk = $('#chkTar_' + (i + 1)).find('input[type=checkbox]');

                    if (chk.is(':checked')) {
                        sel++;
                        var id = chk.prop('id');
                        var indice = 0;
                        var encontrou = false;

                        while (!encontrou && indice < listaIteracoesSelecionadas.length) {
                            debugger;
                            if (id == listaIteracoesSelecionadas[indice].IdTarefaChannel) {
                                $('#ddIteration_' + (i + 1)).val(listaIteracoesSelecionadas[indice].IdIterationTfs);
                                encontrou = true;
                            } else {
                                indice += 1;
                            }
                        }
                    }
                }

                $("select#ddIteration_" + (i + 1)).trigger("chosen:updated");
            }
        }
    }, tfs, collection, project, team);
}

function gerarTarefas(projeto, alterando, tarefasSelecionadas) {
    var cont = 0;

    GetTarefasProjeto(function (tarefaList) {
        $("#tarefas").empty();
        $.each(tarefaList, function () {
            cont++;

            var i = 0;
            var encontrou = false;
            var id = this.id;
            var checked = "";
            if (alterando) {
                if (tarefasSelecionadas != null) {
                    while (!encontrou && i < tarefasSelecionadas.length) {
                        if (tarefasSelecionadas[i] == id) {
                            encontrou = true;
                            checked = " checked";
                        }
                        else {
                            i++;
                        }
                    }
                }


                    $("#tarefas").append("<div id='tar_" + cont + "' class='row'><div class='col-md-5'><div id='chkTar_" + cont + "' class='checkbox' > <label><input type='checkbox' id='" + this.id + "'" + checked + " > " + this.nome + "</label></div></div></div>");

            } else {

                    $("#tarefas").append("<div id='tar_" + cont + "' class='row'><div class='col-md-5'><div id='chkTar_" + cont + "' class='checkbox'> <label><input type='checkbox' id='" + this.id + "' >" + this.nome + "</label></div></div></div>");

            }
        });

        quantTarefas = cont;
        if (alterando) {
            gerarIteracoes(alterando);
        }
    }, projeto);
}

$("select#ddProjeto").on('change', function () {
    var projeto = $("select#ddProjeto").val();

    gerarTarefas(projeto, false, tarefasSelecionadas);
});

$("select#ddTfs").on('change', function () {
    var tfs = $("select#ddTfs").val();
    GetCollections(function (collectionList) {
        clearDropwDown("select#ddCollection");
        clearDropwDown("select#ddProject");
        clearDropwDown("select#ddTeam");
        clearDropwDown("select#ddAreaPath");
        $.each(collectionList, function () {
            $("select#ddCollection").append($('<option></option>').val(this.name).html(this.name));
        });
        $("select#ddCollection").trigger("chosen:updated");
        $("select#ddProject").trigger("chosen:updated");
        $("select#ddTeam").trigger("chosen:updated");
        $("select#ddAreaPath").trigger("chosen:updated");
    }, tfs);
});

$("select#ddCollection").on('change', function () {
    var tfs = $("select#ddTfs").val();
    var collection = $("select#ddCollection").val();
    GetProjects(function (projectList) {
        clearDropwDown("select#ddProject");
        clearDropwDown("select#ddTeam");
        clearDropwDown("select#ddAreaPath");
        $.each(projectList, function () {
            $("select#ddProject").append($('<option></option>').val(this.name).html(this.name));
        });
        $("select#ddProject").trigger("chosen:updated");
        $("select#ddTeam").trigger("chosen:updated");
        $("select#ddAreaPath").trigger("chosen:updated");
    }, tfs, collection);
});

$("select#ddProject").on('change', function () {
    var tfs = $("select#ddTfs").val();
    var collection = $("select#ddCollection").val();
    var project = $("select#ddProject").val();

    GetTeams(function (teamList) {
        clearDropwDown("select#ddTeam");
        $.each(teamList, function () {
            $("select#ddTeam").append($('<option></option>').val(this.name).html(this.name));
        });
        $("select#ddTeam").trigger("chosen:updated");
    }, tfs, collection, project);

    GetAreaPathTree(function (areaList) {
        clearDropwDown("select#ddAreaPath");
        $.each(areaList, function () {
            $("select#ddAreaPath").append($('<option></option>').val(this.name).html(this.name));
        });
        $("select#ddAreaPath").trigger("chosen:updated");
    }, tfs, collection, project);
});

$("select#ddTeam").on('change', function () {
    gerarIteracoes(false);
});

$("button#btIncluir").on('click', function () {
    if (validarCampos()) {
        debugger;
        var idTarefas = [];
        var idIteracoes = [];
        $('#tarefas input:checked').each(function () {
            var divChk = $(this).closest('div');
            var splitID = divChk.attr('id').split('_');
            var idDiv = splitID[1];

            var idIteracao = $('#ddIteration_' + idDiv).val();

            idTarefas.push({ IdTarefaChannel: this.id, IdIterationTfs: idIteracao });
        });
        var projeto = {
            'Id': idProjeto,
            'IdChannel': $("select#ddProjeto").val(),
            'Nome': $("select#ddProjeto option:selected").text(),
            'TfsUrl': $("select#ddTfs").val(),
            'TfsCollection': $("select#ddCollection").val(),
            'TfsProject': $("select#ddProject").val(),
            'TfsTeam': $("select#ddTeam").val(),
            'Status': "true",
            'TarefaChannel': idTarefas,
            'TfsAreaPath': $("select#ddAreaPath").val()
        }

        $.ajax({
            type: "POST",
            url: window.urlBase + "Projeto/Incluir",
            data: { projeto: projeto },
            success: function (data) {
                if (data.status) {
                    window.location.href = data.redirectUrl;
                } else {
                    alert("Erro ao executar operação");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert(textStatus + " " + errorThrown);
            }
        });
    }
    return false;
});

function validarCampos() {
    var msgErro = '';

    if ($("select#ddProjeto").val() == '') {
        msgErro = 'O campo Projeto no Channel é obrigatório.';
    } else if ($("select#ddTfs").val() == '') {
        msgErro = 'O campo TFS é obrigatório.';
    } else if ($("select#ddCollection").val() == '') {
        msgErro = 'O campo Unidade é obrigatório.';
    } else if ($("select#ddProject").val() == '') {
        msgErro = 'O campo Contrato é obrigatório.';
    } else if ($("select#ddTeam").val() == '') {
        msgErro = 'O campo Equipe é obrigatório.';
    } else if ($("select#ddAreaPath").val() == '') {
        msgErro = 'O campo Projeto no TFS é obrigatório.';
    } else {
        var existeTarefaSelecionada = false;
        var existeIteracao = false;

        $('#tarefas input:checked').each(function () {
            existeTarefaSelecionada = true;

            var divChk = $(this).closest('div');
            var splitID = divChk.attr('id').split('_');
            var idDiv = splitID[1];

            existeIteracao = ($('#ddIteration_' + idDiv).val() != null);
        });
        if (!existeTarefaSelecionada) {
            msgErro = 'Selecione pelo menos uma tarefa.';
        } else {
            if (!existeIteracao) {
                msgErro = 'Sem iterações para a equipe selecionada.';
            }
        }
    }

    if (msgErro != '') {
        alert(msgErro);
        return false;
    } else {
        return true;
    }
}