namespace Gizmo.DAL
{
    /// <summary>
    /// Import options.
    /// </summary>
    public class ImportOptions
    {
        #region PROPERTIES
        
        /// <summary>
        /// Gets or sets if time values should be treated as minutes.
        /// Default value is true.
        /// </summary>
        public bool TreatTimeAsMinutes
        {
            get; set;
        } = true;

        #endregion
    }
}
