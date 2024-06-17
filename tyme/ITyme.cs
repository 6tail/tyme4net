namespace tyme
{
    /// <summary>
    /// Tyme
    /// </summary>
    public interface ITyme: ICulture
    {
        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的Tyme</returns>
        ITyme Next(int n);
    }
}