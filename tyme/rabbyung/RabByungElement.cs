using tyme.culture;

namespace tyme.rabbyung
{
    /// <summary>
    /// 藏历五行
    /// </summary>
    public class RabByungElement : Element
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public RabByungElement(int index) : base(index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public RabByungElement(string name) : base(name.Replace("铁", "金"))
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static RabByungElement FromIndex(int index)
        {
            return new RabByungElement(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static RabByungElement FromName(string name)
        {
            return new RabByungElement(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的藏历五行</returns>
        public new RabByungElement Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 我生者
        /// </summary>
        /// <returns>藏历五行</returns>
        public RabByungElement GetReinforce()
        {
            return Next(1);
        }

        /// <summary>
        /// 我克者
        /// </summary>
        /// <returns>藏历五行</returns>
        public RabByungElement GetRestrain()
        {
            return Next(2);
        }

        /// <summary>
        /// 生我者
        /// </summary>
        /// <returns>藏历五行</returns>
        public RabByungElement GetReinforced()
        {
            return Next(-1);
        }

        /// <summary>
        /// 克我者
        /// </summary>
        /// <returns>藏历五行</returns>
        public RabByungElement GetRestrained()
        {
            return Next(-2);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return base.GetName().Replace("金", "铁");
        }
    }
}