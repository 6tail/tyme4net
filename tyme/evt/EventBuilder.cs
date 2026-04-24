using tyme.enums;

namespace tyme.evt
{
    /// <summary>
    /// 事件构建器
    /// </summary>
    public class EventBuilder
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        protected string name;

        /// <summary>
        /// 事件数据
        /// </summary>
        protected readonly char[] Data = { '@', '_', '_', '_', '_', '_', '0', '0', '0' };

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="s">名称</param>
        /// <returns>事件构造器</returns>
        public EventBuilder Name(string s)
        {
            name = s;
            return this;
        }
        
        /// <summary>
        /// 获取指定下标的字符
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>字符</returns>
        protected char GetChar(int index)
        {
            return EventManager.Chars[index];
        }
        
        /// <summary>
        /// 设置数值
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="n">数值</param>
        /// <returns>事件构造器</returns>
        protected EventBuilder SetValue(int index, int n) {
            Data[index] = GetChar(31 + n);
            return this;
        }

        /// <summary>
        /// 数据内容
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="a">参数1</param>
        /// <param name="b">参数2</param>
        /// <param name="c">参数3</param>
        /// <returns>事件构造器</returns>
        protected EventBuilder Content(EventType type, int a, int b, int c)
        {
            Data[1] = GetChar(type.GetCode());
            return SetValue(2, a).SetValue(3, b).SetValue(4, c);
        }

        /// <summary>
        /// 公历日
        /// </summary>
        /// <param name="solarMonth">公历月（1至12）</param>
        /// <param name="solarDay">公历日（1至31）</param>
        /// <param name="delayDays">顺延天数，例如生日在2月29，非闰年没有2月29，是+1天，还是-1天（最远支持-31至31天）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder SolarDay(int solarMonth, int solarDay, int delayDays)
        {
            return Content(EventType.SolarDay, solarMonth, solarDay, delayDays);
        }

        /// <summary>
        /// 农历日
        /// </summary>
        /// <param name="lunarMonth">农历月（-12至-1，1至12，闰月为负）</param>
        /// <param name="lunarDay">农历日（1至30）</param>
        /// <param name="delayDays">顺延天数，例如生日在某月的三十，但下一年当月可能只有29天，是+1天，还是-1天（最远支持-31至31天）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder LunarDay(int lunarMonth, int lunarDay, int delayDays)
        {
            return Content(EventType.LunarDay, lunarMonth, lunarDay, delayDays);
        }

        /// <summary>
        /// 公历第几个星期几
        /// </summary>
        /// <param name="solarMonth">公历月（1至12）</param>
        /// <param name="weekIndex">第几个星期（1为第1个星期，-1为倒数第1个星期）</param>
        /// <param name="week">星期几（0至6，0代表星期天，1代表星期一）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder SolarWeek(int solarMonth, int weekIndex, int week)
        {
            return Content(EventType.SolarWeek, solarMonth, weekIndex, week);
        }

        /// <summary>
        /// 节气
        /// </summary>
        /// <param name="termIndex">节气索引（0至23）</param>
        /// <param name="delayDays">顺延天数（最远支持-31至31天）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder TermDay(int termIndex, int delayDays)
        {
            return Content(EventType.TermDay, termIndex, 0, delayDays);
        }

        /// <summary>
        /// 节气天干
        /// </summary>
        /// <param name="termIndex">节气索引（0至23）</param>
        /// <param name="heavenStemIndex">天干索引（0至9）</param>
        /// <param name="delayDays">顺延天数（最远支持-31至31天）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder TermHeavenStem(int termIndex, int heavenStemIndex, int delayDays)
        {
            return Content(EventType.TermHs, termIndex, heavenStemIndex, delayDays);
        }

        /// <summary>
        /// 节气地支
        /// </summary>
        /// <param name="termIndex">节气索引（0至23）</param>
        /// <param name="earthBranchIndex">地支索引（0至11）</param>
        /// <param name="delayDays">顺延天数（最远支持-31至31天）</param>
        /// <returns>事件构建器</returns>
        public EventBuilder TermEarthBranch(int termIndex, int earthBranchIndex, int delayDays)
        {
            return Content(EventType.TermEb, termIndex, earthBranchIndex, delayDays);
        }

        /// <summary>
        /// 起始年
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>事件构造器</returns>
        public EventBuilder StartYear(int year)
        {
            var size = EventManager.Chars.Length;
            var n = year;
            for (var i = 0; i < 3; i++)
            {
                Data[8 - i] = GetChar(n % size);
                n /= size;
            }

            return this;
        }

        /// <summary>
        /// 偏移天数
        /// </summary>
        /// <param name="days">天数（最远支持-31至31天）</param>
        /// <returns>事件构造器</returns>
        public EventBuilder Offset(int days)
        {
            return SetValue(5, days);
        }

        /// <summary>
        /// 生成事件
        /// </summary>
        /// <returns>事件</returns>
        public Event Build()
        {
            return new Event(name, new string(Data));
        }
    }
}