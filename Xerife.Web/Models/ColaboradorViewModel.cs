using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    /// <summary>
    /// ViewModel de colaborador
    /// </summary>
    public class ColaboradorViewModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Url da foto
        /// </summary>
        public string UrlFoto { get; set; }

        /// <summary>
        /// Capacidade
        /// </summary>
        public List<CapacidadeViewModel> Capacidade { get; set; }
    }

    /// <summary>
    /// ViewModel de capacidade
    /// </summary>
    public class CapacidadeViewModel
    {
        /// <summary>
        /// Disciplina
        /// </summary>
        public string Disciplina { get; set; }

        /// <summary>
        /// Horas
        /// </summary>
        public double Horas { get; set; }

        /// <summary>
        /// Projeto
        /// </summary>
        public string Projeto { get; set; }
    }
}