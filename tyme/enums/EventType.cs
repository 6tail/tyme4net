using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 公历日期
        /// </summary>
        [Description("公历日期")] SolarDay = 0,

        /// <summary>
        /// 几月第几个星期几
        /// </summary>
        [Description("几月第几个星期几")] SolarWeek = 1,

        /// <summary>
        /// 农历日期
        /// </summary>
        [Description("农历日期")] LunarDay = 2,

        /// <summary>
        /// 节气日期
        /// </summary>
        [Description("节气日期")] TermDay = 3,

        /// <summary>
        /// 节气天干
        /// </summary>
        [Description("节气天干")] TermHs = 4,

        /// <summary>
        /// 节气地支
        /// </summary>
        [Description("节气地支")] TermEb = 5
    }

    /// <summary>
    /// 事件类型扩展
    /// </summary>
    public static class EventTypeExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="t">事件类型</param>
        /// <returns>代码</returns>
        public static int GetCode(this EventType t)
        {
            return (int)t;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="t">事件类型</param>
        /// <returns>名称</returns>
        public static string GetName(this EventType t)
        {
            return ((DescriptionAttribute)t.GetType().GetMember(t.ToString())[0].GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="t">事件类型</param>
        /// <returns>描述</returns>
        public static string ToString(this EventType t)
        {
            return t.GetName();
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="t">本事件类型</param>
        /// <param name="target">其他事件类型</param>
        /// <returns>True/False</returns>
        public static bool Equals(this EventType t, EventType target)
        {
            return t == target;
        }
    }
}