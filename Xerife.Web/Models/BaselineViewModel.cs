using System;

namespace Xerife.Web.Models
{
    /// <summary>
    /// ViewModel de BaselineGeracao
    /// </summary>
    public class BaselineGeracaoViewModel
    {
        /// <summary>
        /// Identificacao
        /// </summary>
        public string Identificacao { get; set; }
        /// <summary>
        /// AnalistaConfiguracao
        /// </summary>
        public string AnalistaConfiguracao { get; set; }
        /// <summary>
        /// Iteracao
        /// </summary>
        public string Iteracao { get; set; }
        /// <summary>
        /// ProjetoId
        /// </summary>
        public int ProjetoId { get; set; }
        /// <summary>
        /// Data Geracao
        /// </summary>
        public string DataGeracao { get; set; }
        /// <summary>
        /// Nomenclatura Artefatos
        /// </summary>
        public bool NomenclaturaArtefatosAssert { get; set; }
        public string NomenclaturaArtefatosComentario { get; set; }
        /// <summary>
        /// Repositorio Artefatos
        /// </summary>
        public bool RepositorioArtefatosAssert { get; set; }
        public string RepositorioArtefatosComentario { get; set; }
        /// <summary>
        /// WorkItem Resolved
        /// </summary>
        public bool WorkItemUnclosedAssert { get; set; }
        public string WorkItemUnclosedComentario { get; set; }
        /// <summary>
        /// ChangesetArtefato
        /// </summary>
        public bool ChangesetArtefatoAssert { get; set; }
        public string ChangesetArtefatoComentario { get; set; }
        /// <summary>
        /// ArtefatoWorkItem
        /// </summary>
        public bool ArtefatoWorkItemAssert { get; set; }
        public string ArtefatoWorkItemComentario { get; set; }
        /// <summary>
        /// CommitBranch
        /// </summary>
        public bool CommitBranchAssert { get; set; }
        public string CommitBranchComentario { get; set; }
        /// <summary>
        /// Diretorio do tfs
        /// </summary>
        public string FolderPathTfs { get; set; }
        /// <summary>
        /// Diretorio da branch
        /// </summary>
        public string BranchPathtfs { get; set; }
    }

    /// <summary>
    /// ViewModel de Baseline Auditoria
    /// </summary>
    public class BaselineAuditoriaViewModel
    {

    }
}