namespace tyme
{
    /// <summary>
    /// 传统文化抽象
    /// </summary>
    public abstract class AbstractCulture : ICulture
    {
        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return GetName();
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public abstract string GetName();

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="o">其他对象</param>
        /// <returns>True/False</returns>
        public override bool Equals(object o)
        {
            return o is ICulture && ToString().Equals(o.ToString());
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// 重载 == 运算符
        /// </summary>
        /// <param name="left">传统文化抽象1</param>
        /// <param name="right">传统文化抽象2</param>
        /// <returns>true/false</returns>
        public static bool operator ==(AbstractCulture left, AbstractCulture right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// 重载 != 运算符
        /// </summary>
        /// <param name="left">传统文化抽象1</param>
        /// <param name="right">传统文化抽象2</param>
        /// <returns>true/false</returns>
        public static bool operator !=(AbstractCulture left, AbstractCulture right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 计算索引值
        /// </summary>
        /// <param name="index">索引值</param>
        /// <param name="size">大小</param>
        /// <returns>索引值</returns>
        protected int IndexOf(int index, int size)
        {
            var i = index % size;
            if (i < 0)
            {
                i += size;
            }

            return i;
        }
    }
}