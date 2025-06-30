using System;
using tyme.jd;
using tyme.util;

namespace tyme.culture.phenology
{
    /// <summary>
    /// 候
    /// </summary>
    public class Phenology : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "蚯蚓结", "麋角解", "水泉动", "雁北乡", "鹊始巢", "雉始雊", "鸡始乳", "征鸟厉疾", "水泽腹坚", "东风解冻", "蛰虫始振", "鱼陟负冰", "獭祭鱼", "候雁北", "草木萌动", "桃始华", "仓庚鸣", "鹰化为鸠", "玄鸟至", "雷乃发声", "始电", "桐始华", "田鼠化为鴽", "虹始见", "萍始生", "鸣鸠拂其羽", "戴胜降于桑", "蝼蝈鸣", "蚯蚓出", "王瓜生", "苦菜秀", "靡草死", "麦秋至", "螳螂生", "鵙始鸣", "反舌无声", "鹿角解", "蜩始鸣", "半夏生", "温风至", "蟋蟀居壁", "鹰始挚", "腐草为萤", "土润溽暑", "大雨行时", "凉风至", "白露降", "寒蝉鸣", "鹰乃祭鸟", "天地始肃", "禾乃登", "鸿雁来", "玄鸟归", "群鸟养羞", "雷始收声", "蛰虫坯户", "水始涸", "鸿雁来宾", "雀入大水为蛤", "菊有黄花", "豺乃祭兽", "草木黄落", "蛰虫咸俯", "水始冰", "地始冻", "雉入大水为蜃", "虹藏不见", "天气上升地气下降", "闭塞而成冬", "鹖鴠不鸣", "虎始交", "荔挺出"
        };
        
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        public Phenology(int year, int index) : base(Names, index)
        {
            var size = Size;
            Year = (year * size + Index) / size;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="name">名称</param>
        public Phenology(int year, string name) : base(Names, name)
        {
            Year = year;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        public static Phenology FromIndex(int year, int index)
        {
            return new Phenology(year, index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="name">名称</param>
        public static Phenology FromName(int year, string name)
        {
            return new Phenology(year, name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的候</returns>
        public new Phenology Next(int n)
        {
            var size = Size;
            var i = Index + n;
            return FromIndex((Year * size + i) / size, IndexOf(i));
        }

        /// <summary>
        /// 三候
        /// </summary>
        public ThreePhenology ThreePhenology => ThreePhenology.FromIndex(Index % 3);

        /// <summary>
        /// 儒略日
        /// </summary>
        public JulianDay JulianDay
        {
            get
            {
                var t = ShouXingUtil.SaLonT((Year - 2000 + (Index - 18) * 5.0 / 360 + 1) * 2 * Math.PI);
                return JulianDay.FromJulianDay(t * 36525 + JulianDay.J2000 + 8.0 / 24 - ShouXingUtil.DtT(t * 36525));
            }
        }
    }
}