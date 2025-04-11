using tyme.enums;
using tyme.lunar;
using tyme.sixtycycle;

namespace tyme.culture.fetus
{
    /// <summary>
    /// 逐日胎神
    /// </summary>
    public class FetusDay : AbstractCulture
    {
        /// <summary>
        /// 天干六甲胎神
        /// </summary>
        public FetusHeavenStem FetusHeavenStem { get; }

        /// <summary>
        /// 地支六甲胎神
        /// </summary>
        public FetusEarthBranch FetusEarthBranch { get; }

        /// <summary>
        /// 内外
        /// </summary>
        public Side Side { get; }

        /// <summary>
        /// 方位
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sixtyCycle">干支</param>
        public FetusDay(SixtyCycle sixtyCycle)
        {
            FetusHeavenStem = new FetusHeavenStem(sixtyCycle.HeavenStem.Index % 5);
            FetusEarthBranch = new FetusEarthBranch(sixtyCycle.EarthBranch.Index % 6);
            var index = new[]{3, 3, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 1, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, -9, -9, -9, -9, -9, -5, -5, -1, -1, -1, -3, -7, -7, -7, -7, -5, 7, 7, 7, 7, 7, 7, 2, 2, 2, 2, 2, 3, 3, 3, 3}[sixtyCycle.Index];
            Side = index < 0 ? Side.In : Side.Out;
            Direction = Direction.FromIndex(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarDay">农历日</param>
        /// <returns>逐日胎神</returns>
        public static FetusDay FromLunarDay(LunarDay lunarDay)
        {
            return new FetusDay(lunarDay.SixtyCycle);
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sixtyCycleDay">干支日</param>
        /// <returns>逐日胎神</returns>
        public static FetusDay FromSixtyCycleDay(SixtyCycleDay sixtyCycleDay)
        {
            return new FetusDay(sixtyCycleDay.SixtyCycle);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            var s = FetusHeavenStem.GetName() + FetusEarthBranch.GetName();
            switch (s)
            {
                case "门门":
                    s = "占大门";
                    break;
                case "碓磨碓":
                    s = "占碓磨";
                    break;
                case "房床床":
                    s = "占房床";
                    break;
                default:
                {
                    if (s.StartsWith("门"))
                    {
                        s = "占" + s;
                    }

                    break;
                }
            }

            s += " ";

            var directionName = Direction.GetName();
            if (Side.In == Side)
            {
                s += "房";
            }

            s += Side.GetName();

            if (Side.Out == Side && "北南西东".Contains(directionName))
            {
                s += "正";
            }

            s += directionName;
            return s;
        }
    }
}