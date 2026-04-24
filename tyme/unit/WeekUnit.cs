using System;

namespace tyme.unit
{
    /// <summary>
    /// 周
    /// </summary>
    public abstract class WeekUnit : MonthUnit
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "第一周", "第二周", "第三周", "第四周", "第五周", "第六周" };
        
        /// <summary>
        /// 索引，0-5
        /// </summary>
        public int Index {get; protected set;}

        /// <summary>
        /// 起始星期，1234560分别代表星期一至星期天
        /// </summary>
        public int Start {get; protected set;}
        
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="index">索引，0-5</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int index, int start)
        {
            if (index < 0 || index > 5)
            {
                throw new ArgumentException("illegal week index: " + index);
            }

            if (start < 0 || start > 6)
            {
                throw new ArgumentException("illegal week start: " + start);
            }
        }
    }
}