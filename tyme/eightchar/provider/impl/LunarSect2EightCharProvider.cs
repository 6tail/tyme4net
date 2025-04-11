using tyme.lunar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// Lunar流派2的八字计算（晚子时日柱算当天）
    /// </summary>
    public class LunarSect2EightCharProvider : IEightCharProvider
    {
        /// <inheritdoc />
        public EightChar GetEightChar(LunarHour hour)
        {
            var h = hour.GetSixtyCycleHour();
            return new EightChar(h.Year, h.Month, hour.LunarDay.SixtyCycle, hour.SixtyCycle);
        }
    }
}