using tyme.culture;
using tyme.culture.pengzu;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 六十甲子(六十干支周)
    /// </summary>
    public class SixtyCycle : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉", "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛巳",
            "壬午", "癸未", "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳", "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥",
            "庚子", "辛丑", "壬寅", "癸卯", "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑", "甲寅", "乙卯", "丙辰", "丁巳",
            "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public SixtyCycle(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public SixtyCycle(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static SixtyCycle FromIndex(int index)
        {
            return new SixtyCycle(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static SixtyCycle FromName(string name)
        {
            return new SixtyCycle(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的干支</returns>
        public new SixtyCycle Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 天干
        /// </summary>
        public HeavenStem HeavenStem => HeavenStem.FromIndex(Index % HeavenStem.Names.Length);

        /// <summary>
        /// 地支
        /// </summary>
        public EarthBranch EarthBranch => EarthBranch.FromIndex(Index % EarthBranch.Names.Length);

        /// <summary>
        /// 纳音
        /// </summary>
        public Sound Sound => Sound.FromIndex(Index / 2);

        /// <summary>
        /// 纳音
        /// </summary>
        public PengZu PengZu => PengZu.FromSixtyCycle(this);

        /// <summary>
        /// 旬
        /// </summary>
        public Ten Ten => Ten.FromIndex((HeavenStem.Index - EarthBranch.Index) / 2);

        /// <summary>
        /// 旬空(空亡)，因地支比天干多2个，旬空则为每一轮干支一一配对后多出来的2个地支
        /// </summary>
        public EarthBranch[] ExtraEarthBranches
        {
            get
            {
                EarthBranch[] l = new EarthBranch[2];
                l[0] = EarthBranch.FromIndex(10 + EarthBranch.Index - HeavenStem.Index);
                l[1] = l[0].Next(1);
                return l;
            }
        }
    }
}