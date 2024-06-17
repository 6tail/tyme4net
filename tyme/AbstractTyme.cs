namespace tyme
{
    /// <summary>
    /// 抽象Tyme
    /// </summary>
    public abstract class AbstractTyme : AbstractCulture, ITyme
    {
        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的Tyme</returns>
        public ITyme Next(int n)
        {
            throw new System.NotImplementedException();
        }
    }
}