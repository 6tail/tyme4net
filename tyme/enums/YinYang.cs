using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 阴阳
    /// </summary>
    public enum YinYang
    {
        /// <summary>
        /// 阴
        /// </summary>
        [Description("阴")] Yin = 0,
        
        /// <summary>
        /// 阳
        /// </summary>
        [Description("阳")] Yang = 1
    }

    /// <summary>
    /// 阴阳扩展
    /// </summary>
    public static class YinYangExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="yinYang">阴阳</param>
        /// <returns>代码</returns>
        public static int GetCode(this YinYang yinYang)
        {
            return (int)yinYang;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="yinYang">阴阳</param>
        /// <returns>名称</returns>
        public static string GetName(this YinYang yinYang)
        {
            return ((DescriptionAttribute)yinYang.GetType().GetMember(yinYang.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="yinYang">阴阳</param>
        /// <returns>描述</returns>
        public static string ToString(this YinYang yinYang)
        {
            return yinYang.GetName();
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="yinYang">本阴阳</param>
        /// <param name="target">其他阴阳</param>
        /// <returns>True/False</returns>
        public static bool Equals(this YinYang yinYang, YinYang target)
        {
            return yinYang == target;
        }
    }
}