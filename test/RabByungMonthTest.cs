using tyme.rabbyung;

namespace test;

/// <summary>
/// 藏历月测试
/// </summary>
public class RabByungMonthTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("第十六饶迥铁虎年十二月", RabByungMonth.FromYm(1950, 12).ToString());
    }
}