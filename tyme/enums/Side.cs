using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 内外
    /// </summary>
    public enum Side
    {
        /// <summary>
        /// 内
        /// </summary>
        [Description("内")] In = 0,
        
        /// <summary>
        /// 外
        /// </summary>
        [Description("外")] Out = 1
    }

    /// <summary>
    /// 内外扩展
    /// </summary>
    public static class SideExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="side">内外</param>
        /// <returns>代码</returns>
        public static int GetCode(this Side side)
        {
            return (int)side;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="side">内外</param>
        /// <returns>名称</returns>
        public static string GetName(this Side side)
        {
            return ((DescriptionAttribute)side.GetType().GetMember(side.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="side">内外</param>
        /// <returns>描述</returns>
        public static string ToString(this Side side)
        {
            return side.GetName();
        }
        
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="side">本内外</param>
        /// <param name="target">其他内外</param>
        /// <returns>True/False</returns>
        public static bool Equals(this Side side, Side target)
        {
            return side == target;
        }
    }
}