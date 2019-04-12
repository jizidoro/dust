namespace Xerife.Web.Models
{
    /// <summary>
    /// ViewModel de calendário
    /// </summary>
    public class CalendarioViewModel
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Start
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// BackgroundColor
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Bodercolor
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// AllDay
        /// </summary>
        public string AllDay { get; set; }
    }
}