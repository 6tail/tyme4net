using tyme.culture;
using tyme.culture.pengzu;
using tyme.culture.star.ten;
using tyme.enums;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 天干
    /// </summary>
    public class HeavenStem : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public HeavenStem(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public HeavenStem(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static HeavenStem FromIndex(int index)
        {
            return new HeavenStem(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static HeavenStem FromName(string name)
        {
            return new HeavenStem(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的天干</returns>
        public new HeavenStem Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 五行
        /// </summary>
        public Element Element => Element.FromIndex(Index / 2);

        /// <summary>
        /// 阴阳
        /// </summary>
        public YinYang YinYang => Index % 2 == 0 ? YinYang.Yang : YinYang.Yin;

        /// <summary>
        /// 十神（生我者，正印偏印。我生者，伤官食神。克我者，正官七杀。我克者，正财偏财。同我者，劫财比肩。）
        /// </summary>
        /// <param name="target">天干</param>
        /// <returns>十神</returns>
        public TenStar GetTenStar(HeavenStem target)
        {
            if (null == target)
            {
                return null;
            }

            var guestElement = target.Element;
            var offset = 0;
            var sameYinYang = YinYang.Equals(target.YinYang);
            if (Element.GetReinforce().Equals(guestElement))
            {
                offset = 1;
            }
            else if (Element.GetRestrain().Equals(guestElement))
            {
                offset = 2;
            }
            else if (guestElement.GetRestrain().Equals(Element))
            {
                offset = 3;
            }
            else if (guestElement.GetReinforce().Equals(Element))
            {
                offset = 4;
            }

            return TenStar.FromIndex(offset * 2 + (sameYinYang ? 0 : 1));
        }

        /// <summary>
        /// 方位
        /// </summary>
        public Direction Direction => Direction.FromIndex(new[] { 2, 8, 4, 6, 0 }[Index / 2]);

        /// <summary>
        /// 喜神方位（《喜神方位歌》甲己在艮乙庚乾，丙辛坤位喜神安。丁壬只在离宫坐，戊癸原在在巽间。）
        /// </summary>
        public Direction JoyDirection => Direction.FromIndex(new[] { 7, 5, 1, 8, 3 }[Index % 5]);

        /// <summary>
        /// 阳贵神方位（《阳贵神歌》甲戊坤艮位，乙己是坤坎，庚辛居离艮，丙丁兑与乾，震巽属何日，壬癸贵神安。）
        /// </summary>
        public Direction YangDirection => Direction.FromIndex(new[] { 1, 1, 6, 5, 7, 0, 8, 7, 2, 3 }[Index]);

        /// <summary>
        /// 阴贵神方位（《阴贵神歌》甲戊见牛羊，乙己鼠猴乡，丙丁猪鸡位，壬癸蛇兔藏，庚辛逢虎马，此是贵神方。）
        /// </summary>
        public Direction YinDirection => Direction.FromIndex(new[] { 7, 0, 5, 6, 1, 1, 7, 8, 3, 2 }[Index]);

        /// <summary>
        /// 财神方位（《财神方位歌》甲乙东北是财神，丙丁向在西南寻，戊己正北坐方位，庚辛正东去安身，壬癸原来正南坐，便是财神方位真。）
        /// </summary>
        public Direction WealthDirection => Direction.FromIndex(new[] { 7, 1, 0, 2, 8 }[Index / 2]);

        /// <summary>
        /// 福神方位（《福神方位歌》甲乙东南是福神，丙丁正东是堪宜，戊北己南庚辛坤，壬在乾方癸在西。）
        /// </summary>
        public Direction MascotDirection => Direction.FromIndex(new[] { 3, 3, 2, 2, 0, 8, 1, 1, 5, 6 }[Index]);

        /// <summary>
        /// 天干彭祖百忌
        /// </summary>
        public PengZuHeavenStem PengZuHeavenStem => PengZuHeavenStem.FromIndex(Index);

        /// <summary>
        /// 地势(长生十二神)
        /// </summary>
        /// <param name="earthBranch">地支</param>
        /// <returns>地势(长生十二神)</returns>
        public Terrain GetTerrain(EarthBranch earthBranch)
        {
            return Terrain.FromIndex(new[] { 1, 6, 10, 9, 10, 9, 7, 0, 4, 3 }[Index] +
                                     (YinYang.Yang == YinYang ? earthBranch.Index : -earthBranch.Index));
        }
    }
}