using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 藏干类型
    /// </summary>
    public enum HideHeavenStemType
    {
        /// <summary>
        /// 余气
        /// </summary>
        [Description("余气")] Residual = 0,

        /// <summary>
        /// 中气
        /// </summary>
        [Description("中气")] Middle = 1,

        /// <summary>
        /// 本气
        /// </summary>
        [Description("本气")] Main = 2
    }

    /// <summary>
    /// 藏干类型扩展
    /// </summary>
    public static class HideHeavenStemTypeExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="hideHeavenStemType">藏干类型</param>
        /// <returns>代码</returns>
        public static int GetCode(this HideHeavenStemType hideHeavenStemType)
        {
            return (int)hideHeavenStemType;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="hideHeavenStemType">藏干类型</param>
        /// <returns>名称</returns>
        public static string GetName(this HideHeavenStemType hideHeavenStemType)
        {
            return ((DescriptionAttribute)hideHeavenStemType.GetType().GetMember(hideHeavenStemType.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="hideHeavenStemType">藏干类型</param>
        /// <returns>描述</returns>
        public static string ToString(this HideHeavenStemType hideHeavenStemType)
        {
            return hideHeavenStemType.GetName();
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="hideHeavenStemType">本藏干类型</param>
        /// <param name="target">其他藏干类型</param>
        /// <returns>True/False</returns>
        public static bool Equals(this HideHeavenStemType hideHeavenStemType, HideHeavenStemType target)
        {
            return hideHeavenStemType == target;
        }
    }
}