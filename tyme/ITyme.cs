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
        /// <param name="n">推移的步数，正数顺推，负数逆推</param>
        /// <returns>推移后的Tyme</returns>
        ITyme Next(int n);
    }
}