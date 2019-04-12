$("select#ddProjeto").on('change', function () {
    var projeto = $("select#ddProjeto").val();
    GetTeamIterationsByProject(function (iterationList) {
        clearDropwDown("select#ddIteration");
        $.each(iterationList, function () {
            $("select#ddIteration").append($('<option></option>').val(this.path).html(this.name));
        });
        $("select#ddIteration").trigger("chosen:updated");
    }, projeto);
    GetTfsRootFolder(function (pathList) {
        clearDropwDown("select#ddFolderPath");
        $.each(pathList.value, function () {
            $("select#ddFolderPath").append($('<option></option>').val(this.path).html(this.path));
        });
        $("select#ddFolderPath").trigger("chosen:updated");
    }, projeto);

    setIdentificadorBaseline();
});

function setIdentificadorBaseline() {
    $("input#identificacao").val('');
    var it = $("select#ddIteration").val();
    var pr = $("select#ddProjeto").val();
    var fp = $("select#ddFolderPath").val();
    GetProjeto(function (projeto) {
        $("input#identificacao").val((projeto.TfsProject + '.[PRODUTO].' + projeto.TfsTeam + '.[MARCO]').toUpperCase().replace('-', '').replace(/\s/g, '').replace(/ +(?= )/g, ''));
        if (!(it === "")) {
            $("input#identificacao").val($("input#identificacao").val().replace('[MARCO]', it.split('\\').pop()).toUpperCase().replace('-', '').replace(/\s/g, '').replace(/ +(?= )/g,''));
        }
        if (!(fp === "")) {
            $("input#identificacao").val($("input#identificacao").val().replace('[PRODUTO]', fp.split('/').pop()).toUpperCase().replace('-', '').replace(/\s/g, '').replace(/ +(?= )/g,''));
        }
    }, pr);
}

$("select#ddIteration").on('change', function () {
    setIdentificadorBaseline();
});

$("select#ddFolderPath").on('change', function () {
    var projeto = $("select#ddProjeto").val();
    var folderPath = $("select#ddFolderPath").val();
    clearDropwDown("select#ddBranch");
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetBranchFromPath",
        data: { projetoId: projeto, folderPath: folderPath },
        success: function (data) {
            if (data.count == 0) {
                $("select#ddBranch").append($('<option selected></option>').val("N/A").html("N/A"));
            } else {
                $.each(data.value, function () {
                    $("select#ddBranch").append($('<option></option>').val(this.path).html(this.path));
                });
            }
            $("select#ddBranch").trigger("chosen:updated");
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });

    setIdentificadorBaseline();
});

$("button#btCheck").on('click', function () {
    var baselineGeracao = {
        'Identificacao': $("input#identificacao").val(),
        'AnalistaConfiguracao': $("input#analistaConfiguracao").val(),
        'Iteracao': $("select#ddIteration").val(),
        'ProjetoId': $("select#ddProjeto").val(),
        'DataGeracao': $("input#dataGeracao").val(),
        'NomenclaturaArtefatos': false,
        'RepositorioArtefatos': false,
        'WorkItemResolved': false,
        'ChangesetArtefato': false,
        'ArtefatoWorkItem': false,
        'CommitBranch': false,
        'BranchPathtfs': $("select#ddBranch").val(),
        'FolderPathTfs': $("select#ddFolderPath").val()
    }

    GerarChecklistGeracaoBaseline(function () {
    }, baselineGeracao);
});


function GerarChecklistGeracaoBaseline(callback, baselineGeracao) {

    $.ajax({
        type: "POST",
        url: window.urlBase + "Baseline/GerarBaselineProjeto",
        data: { baselineGeracao: baselineGeracao },
        success: function (data) {
            window.location = window.urlBase + 'Baseline/Download?fileGuid=' + data.FileGuid
                              + '&filename=' + data.FileName;
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
    //return false;

    //$.ajax({
    //    type: "GET",
    //    url: "/Baseline/GerarBaselineProjeto",
    //    data: { baselineGeracao: baselineGeracao },
    //    dataType: "json",
    //    success: callback,
    //    error: function (xhr, textStatus, errorThrown) {
    //        alert(textStatus + " " + errorThrown);
    //    }
    //});
}