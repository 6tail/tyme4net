using System.Text.RegularExpressions;

namespace tyme.evt
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// 有效字符
        /// </summary>
        public const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTU_VWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 全量事件数据，@[1] 事件类型[1] 内容[3] 偏移天数(-31到31)[1] 起始年[3] 名称[n]
        /// <h3>内容</h3>
        /// <ul>
        ///   <li>0.SOLAR_DAY： 月(1到12，大于12时往后推到明年1月)[1] 日(1到31)[1] 顺延天数(-31到31)[1]</li>
        ///   <li>1.SOLAR_WEEK：月(1到12，大于12时往后推到明年1月)[1] 第几个(-6到-1，1到6)[1] 星期几(0到6)[1]</li>
        ///   <li>2.LUNAR_DAY： 月(-12到-1，1到12，大于12时往后推到明年1月)[1] 日(1到30)[1] 顺延天数(-31到31)[1]</li>
        ///   <li>3.TERM_DAY：节气索引(0-23)[1] 保留[1] 偏移天数(-31到31)[1]</li>
        ///   <li>4.TERM_HS：节气索引(0-23)[1] 天干索引(0-9)[1] 偏移天数(-31到31)[1]</li>
        ///   <li>5.TERM_EB：节气索引(0-23)[1] 地支索引(0-11)[1] 偏移天数(-31到31)[1]</li>
        /// </ul>
        /// </summary>
        public static string Data = "";

        /// <summary>
        /// 数据匹配的正则表达式
        /// </summary>
        public const string RegexExp = "(@[0-9A-Za-z_]{8})";

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="name">名称</param>
        public static void Remove(string name)
        {
            Data = Regex.Replace(Data, RegexExp + name, "");
        }

        /// <summary>
        /// 新增或更新事件
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="data">事件数据</param>
        protected static void SaveOrUpdate(string name, string data)
        {
            var o = RegexExp + name;
            var matcher = Regex.Match(Data, o);
            if (matcher.Success)
            {
                Data = Regex.Replace(Data, o, data);
            }
            else
            {
                Data += data;
            }
        }

        /// <summary>
        /// 新增或更新事件
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="e">事件</param>
        public static void Update(string name, Event e)
        {
            SaveOrUpdate(name, e.Data + (string.IsNullOrEmpty(e.GetName()) ? name : e.GetName()));
        }

        /// <summary>
        /// 新增或更新事件
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="data">事件数据</param>
        public static void UpdateData(string name, string data)
        {
            Event.Validate(data);
            SaveOrUpdate(name, data);
        }
    }
}