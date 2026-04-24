using System;
using tyme.enums;
using tyme.evt;
using tyme.unit;

namespace tyme.festival
{
    /// <summary>
    /// 节日抽象
    /// </summary>
    public abstract class AbstractFestival : AbstractTyme
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Obsolete("已过时，可能在后续版本中删除。")]
        public FestivalType Type { get; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 日
        /// </summary>
        public DayUnit Day { get; }

        /// <summary>
        /// 事件
        /// </summary>
        public Event Event { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">节日类型</param>
        /// <param name="index">索引</param>
        /// <param name="e">事件</param>
        /// <param name="day">日</param>
        public AbstractFestival(FestivalType type, int index, Event e, DayUnit day)
        {
            Type = type;
            Index = index;
            Event = e;
            Day = day;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Event.GetName();
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{Day} {GetName()}";
        }
    }
}