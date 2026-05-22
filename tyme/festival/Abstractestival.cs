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
        /// <param name="index">索引</param>
        /// <param name="e">事件</param>
        /// <param name="day">日</param>
        public AbstractFestival(int index, Event e, DayUnit day)
        {
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