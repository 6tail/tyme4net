using System;
using tyme.solar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// Lunar的流派2童限计算（按分钟数计算）
    /// </summary>
    public class LunarSect2ChildLimitProvider : AbstractChildLimitProvider
    {
        /// <inheritdoc />
        public override ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term)
        {
            // 出生时刻和节令时刻相差的分钟数
            var minutes = Math.Abs(term.JulianDay.GetSolarTime().Subtract(birthTime)) / 60;
            var year = minutes / 4320;
            minutes %= 4320;
            var month = minutes / 360;
            minutes %= 360;
            var day = minutes / 12;
            minutes %= 12;
            var hour = minutes * 2;

            return Next(birthTime, year, month, day, hour, 0, 0);
        }
    }
}