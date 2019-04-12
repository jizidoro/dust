function GetCollections(callback, tfs) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetCollections",
        dataType: "json",
        data: { tfs: tfs },
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetProjects(callback, tfs, collection) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetProjects",
        data: { tfs: tfs, collection: collection },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetTeams(callback, tfs, collection, project) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetTeams",
        data: { tfs: tfs, collection: collection, project: project },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetAreaPathTree(callback, tfs, collection, project) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetAreaPathTree",
        data: { tfs: tfs, collection: collection, project: project },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetTeamIterations(callback, tfs, collection, project, team) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetTeamIterations",
        data: { tfs: tfs, collection: collection, project: project, team: team },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetTeamIterationsByProject(callback, projetoId) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetTeamIterationsByProject",
        data: { projetoId: projetoId },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetWIQLByIteration(callback, tfs, collection, project, iteration) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetWIQLbyIteration",
        data: { tfs: tfs, collection: collection, project: project, iteration: iteration },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

//function GetTfsRootFolder(callback, tfs, collection, project) {
//    $.ajax({
//        type: "GET",
//        url: "/Tfs/GetTfsRootFolder",
//        data: { tfs: tfs, collection: collection, project: project },
//        dataType: "json",
//        success: callback,
//        error: function (xhr, textStatus, errorThrown) {
//            alert(textStatus + " " + errorThrown);
//        }
//    });
//}

function GetTfsRootFolder(callback, projetoId) {
    $.ajax({
        type: "GET",
        url: window.urlBase + "Tfs/GetTfsRootFolder",
        data: { projetoId: projetoId },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}

function GetBranchByRoot(callback, projetoId) {
    $.ajax({
        type: "GET",
        url: "/Tfs/GetBranchByRoot",
        data: { projetoId: projetoId },
        dataType: "json",
        success: callback,
        error: function (xhr, textStatus, errorThrown) {
            alert(textStatus + " " + errorThrown);
        }
    });
}