using tyme.lunar;
using tyme.sixtycycle;

namespace tyme.culture
{
    /// <summary>
    /// 灶马头
    /// </summary>
    public class KitchenGodSteed : AbstractCulture
    {
        /// <summary>
        /// 数字
        /// </summary>
        public static readonly string[] Numbers = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };

        /// <summary>
        /// 正月初一的干支
        /// </summary>
        protected SixtyCycle FirstDaySixtyCycle { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        public KitchenGodSteed(int lunarYear)
        {
            FirstDaySixtyCycle = LunarDay.FromYmd(lunarYear, 1, 1).SixtyCycle;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        /// <returns>灶马头</returns>
        public static KitchenGodSteed FromLunarYear(int lunarYear)
        {
            return new KitchenGodSteed(lunarYear);
        }

        /// <summary>
        /// 通过天干获取对应数字
        /// </summary>
        /// <param name="n">步数</param>
        /// <returns>数字</returns>
        protected string ByHeavenStem(int n)
        {
            return Numbers[FirstDaySixtyCycle.HeavenStem.StepsTo(n)];
        }

        /// <summary>
        /// 通过地支获取对应数字
        /// </summary>
        /// <param name="n">步数</param>
        /// <returns>数字</returns>
        protected string ByEarthBranch(int n)
        {
            return Numbers[FirstDaySixtyCycle.EarthBranch.StepsTo(n)];
        }

        /// <summary>
        /// 几鼠偷粮
        /// </summary>
        public string Mouse => $"{ByEarthBranch(0)}鼠偷粮";

        /// <summary>
        /// 草子几分
        /// </summary>
        public string Grass => $"草子{ByEarthBranch(0)}分";

        /// <summary>
        /// 几牛耕田
        /// </summary>
        public string Cattle => $"{ByEarthBranch(1)}牛耕田";

        /// <summary>
        /// 花收几分
        /// </summary>
        public string Flower => $"花收{ByEarthBranch(3)}分";

        /// <summary>
        /// 几龙治水
        /// </summary>
        public string Dragon => $"{ByEarthBranch(4)}龙治水";

        /// <summary>
        /// 几马驮谷
        /// </summary>
        public string Horse => $"{ByEarthBranch(6)}马驮谷";

        /// <summary>
        /// 几鸡抢米
        /// </summary>
        public string Chicken => $"{ByEarthBranch(9)}鸡抢米";

        /// <summary>
        /// 几姑看蚕
        /// </summary>
        public string Silkworm => $"{ByEarthBranch(9)}姑看蚕";

        /// <summary>
        /// 几猪屠共
        /// </summary>
        public string Pig => $"{ByEarthBranch(11)}屠共猪";

        /// <summary>
        /// 甲田几分
        /// </summary>
        public string Field => $"甲田{ByHeavenStem(0)}分";

        /// <summary>
        /// 几人分饼
        /// </summary>
        public string Cake => $"{ByHeavenStem(2)}人分饼";

        /// <summary>
        /// 几日得金
        /// </summary>
        public string Gold => $"{ByHeavenStem(7)}日得金";

        /// <summary>
        /// 几人几丙
        /// </summary>
        public string PeopleCakes => $"{ByEarthBranch(2)}人{ByHeavenStem(2)}丙";

        /// <summary>
        /// 几人几锄
        /// </summary>
        public string PeopleHoes => $"{ByEarthBranch(2)}人{ByHeavenStem(3)}锄";

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "灶马头";
        }
    }
}