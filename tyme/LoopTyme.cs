using System;

namespace tyme
{
    /// <summary>
    /// 可轮回的Tyme
    /// </summary>
    public abstract class LoopTyme : AbstractTyme
    {
        /// <summary>
        /// 名称列表
        /// </summary>
        protected readonly string[] names;

        /// <summary>
        /// 索引，从0开始
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="names">名称列表</param>
        /// <param name="index">索引值</param>
        protected LoopTyme(string[] names, int index)
        {
            this.names = names;
            Index = IndexOf(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="names">名称列表</param>
        /// <param name="name">名称</param>
        protected LoopTyme(string[] names, string name)
        {
            this.names = names;
            Index = IndexOf(name);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return names[Index];
        }

        /// <summary>
        /// 名称长度
        /// </summary>
        public int Size => names.Length;

        /// <summary>
        /// 根据名称获取索引值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>索引值</returns>
        /// <exception cref="ArgumentException"></exception>
        protected int IndexOf(string name)
        {
            for (var i = 0; i < Size; i++)
            {
                if (names[i].Equals(name))
                {
                    return i;
                }
            }

            throw new ArgumentException($"illegal name: {GetName()}");
        }

        /// <summary>
        /// 获取任意索引值
        /// </summary>
        /// <param name="n">索引值</param>
        /// <returns>索引值</returns>
        protected int IndexOf(int n)
        {
            return IndexOf(n, Size);
        }

        /// <summary>
        /// 获取推移后的索引值
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>索引值</returns>
        protected int NextIndex(int n)
        {
            return IndexOf(Index + n);
        }

        /// <summary>
        /// 到目标索引的步数
        /// </summary>
        /// <param name="targetIndex">目标索引</param>
        /// <returns>步数</returns>
        public int StepsTo(int targetIndex)
        {
            return IndexOf(targetIndex - Index);
        }
    }
}