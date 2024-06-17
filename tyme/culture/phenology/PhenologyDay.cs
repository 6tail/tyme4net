namespace tyme.culture.phenology
{
    /// <summary>
    /// 七十二候
    /// </summary>
    public class PhenologyDay : AbstractCultureDay
    {
        /// <summary>
        /// 七十二候
        /// </summary>
        /// <param name="phenology">候</param>
        /// <param name="dayIndex">天索引</param>
        public PhenologyDay(Phenology phenology, int dayIndex) : base(phenology, dayIndex)
        {
        }

        /// <summary>
        /// 候
        /// </summary>
        public Phenology Phenology => (Phenology)culture;
    }
}