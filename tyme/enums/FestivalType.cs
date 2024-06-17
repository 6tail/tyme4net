using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 节日类型
    /// </summary>
    public enum FestivalType
    {
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")] Day = 0,

        /// <summary>
        /// 节气
        /// </summary>
        [Description("节气")] Term = 1,

        /// <summary>
        /// 除夕
        /// </summary>
        [Description("除夕")] Eve = 2
    }

    /// <summary>
    /// 节日类型扩展
    /// </summary>
    public static class FestivalTypeExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="festivalType">节日类型</param>
        /// <returns>代码</returns>
        public static int GetCode(this FestivalType festivalType)
        {
            return (int)festivalType;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="festivalType">节日类型</param>
        /// <returns>名称</returns>
        public static string GetName(this FestivalType festivalType)
        {
            return ((DescriptionAttribute)festivalType.GetType().GetMember(festivalType.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="festivalType">节日类型</param>
        /// <returns>描述</returns>
        public static string ToString(this FestivalType festivalType)
        {
            return festivalType.GetName();
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="festivalType">本节日类型</param>
        /// <param name="target">其他节日类型</param>
        /// <returns>True/False</returns>
        public static bool Equals(this FestivalType festivalType, FestivalType target)
        {
            return festivalType == target;
        }
    }
}