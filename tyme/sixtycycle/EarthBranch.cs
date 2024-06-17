using tyme.culture;
using tyme.culture.pengzu;
using tyme.enums;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 地支
    /// </summary>
    public class EarthBranch : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public EarthBranch(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public EarthBranch(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static EarthBranch FromIndex(int index)
        {
            return new EarthBranch(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static EarthBranch FromName(string name)
        {
            return new EarthBranch(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的地支</returns>
        public new EarthBranch Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 五行
        /// </summary>
        public Element Element => Element.FromIndex(new[] { 4, 2, 0, 0, 2, 1, 1, 2, 3, 3, 2, 4 }[Index]);

        /// <summary>
        /// 阴阳
        /// </summary>
        public YinYang YinYang => Index % 2 == 0 ? YinYang.Yang : YinYang.Yin;

        /// <summary>
        /// 藏干之本气
        /// </summary>
        public HeavenStem HideHeavenStemMain =>
            HeavenStem.FromIndex(new[] { 9, 5, 0, 1, 4, 2, 3, 5, 6, 7, 4, 8 }[Index]);

        /// <summary>
        /// 藏干之中气，无中气为null
        /// </summary>
        public HeavenStem HideHeavenStemMiddle
        {
            get
            {
                var n = new[] { -1, 9, 2, -1, 1, 6, 5, 3, 8, -1, 7, 0 }[Index];
                return n == -1 ? null : HeavenStem.FromIndex(n);
            }
        }

        /// <summary>
        /// 藏干之余气，无余气为null
        /// </summary>
        public HeavenStem HideHeavenStemResidual
        {
            get
            {
                var n = new[] { -1, 7, 4, -1, 9, 4, -1, 1, 4, -1, 3, -1 }[Index];
                return n == -1 ? null : HeavenStem.FromIndex(n);
            }
        }

        /// <summary>
        /// 生肖
        /// </summary>
        public Zodiac GetZodiac()
        {
            return Zodiac.FromIndex(Index);
        }

        /// <summary>
        /// 方位
        /// </summary>
        public Direction Direction => Direction.FromIndex(new[] { 0, 4, 2, 2, 4, 8, 8, 4, 6, 6, 4, 0 }[Index]);

        /// <summary>
        /// 相冲的地支（子午冲，丑未冲，寅申冲，辰戌冲，卯酉冲，巳亥冲）
        /// </summary>
        public EarthBranch Opposite => Next(6);

        /// <summary>
        /// 煞（逢巳日、酉日、丑日必煞东；亥日、卯日、未日必煞西；申日、子日、辰日必煞南；寅日、午日、戌日必煞北。）
        /// </summary>
        public Direction Ominous => Direction.FromIndex(new[] { 8, 2, 0, 6 }[Index % 4]);

        /// <summary>
        /// 地支彭祖百忌
        /// </summary>
        public PengZuEarthBranch PengZuEarthBranch => PengZuEarthBranch.FromIndex(Index);
    }
}