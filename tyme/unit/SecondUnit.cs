using System;

namespace tyme.unit
{
    /// <summary>
    /// 秒
    /// </summary>
    public abstract class SecondUnit : DayUnit
    {
        /// <summary>
        /// 时
        /// </summary>
        public int Hour {get; protected set;}
        
        /// <summary>
        /// 分
        /// </summary>
        public int Minute {get; protected set;}
        
        /// <summary>
        /// 秒
        /// </summary>
        public int Second {get; protected set;}

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int hour, int minute, int second)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("illegal hour: " + hour);
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("illegal minute: " + minute);
            }

            if (second < 0 || second > 59)
            {
                throw new ArgumentException("illegal second: " + second);
            }
        }
    }
}