using tyme.holiday;
using tyme.solar;
using Xunit.Abstractions;

namespace test;

/// <summary>
/// 法定节假日测试
/// </summary>
public class LegalHolidayTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public LegalHolidayTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test0()
    {
        var d = LegalHoliday.FromYmd(2011, 5, 1);
        Assert.NotNull(d);
        Assert.Equal("2011年5月1日 劳动节(休)", d.ToString());

        Assert.Equal("2011年5月2日 劳动节(休)", d.Next(1).ToString());
        Assert.Equal("2011年6月4日 端午节(休)", d.Next(2).ToString());
        Assert.Equal("2011年4月30日 劳动节(休)", d.Next(-1).ToString());
        Assert.Equal("2011年4月5日 清明节(休)", d.Next(-2).ToString());
    }

    [Fact]
    public void Test1()
    {
        var d = LegalHoliday.FromYmd(2010, 1, 1);
        Assert.NotNull(d);
        while (d.Day.Year < 2012)
        {
            _testOutputHelper.WriteLine(d.ToString());
            d = d.Next(1);
        }
    }

    [Fact]
    public void Test2()
    {
        var d = LegalHoliday.FromYmd(2010, 1, 1);
        Assert.NotNull(d);
        while (d.Day.Year > 2007)
        {
            _testOutputHelper.WriteLine(d.ToString());
            d = d.Next(-1);
        }
    }

    [Fact]
    public void Test3()
    {
        var d = LegalHoliday.FromYmd(2001, 12, 29);
        Assert.NotNull(d);
        Assert.Equal("2001年12月29日 元旦节(班)", d.ToString());
        Assert.Null(d.Next(-1));
    }

    [Fact]
    public void Test4()
    {
        var d = LegalHoliday.FromYmd(2022, 10, 5);
        Assert.NotNull(d);
        Assert.Equal("2022年10月5日 国庆节(休)", d.ToString());
        Assert.Equal("2022年10月4日 国庆节(休)", d.Next(-1).ToString());
        Assert.Equal("2022年10月6日 国庆节(休)", d.Next(1).ToString());
    }

    [Fact]
    public void Test5()
    {
        var d = SolarDay.FromYmd(2010, 10, 1).LegalHoliday;
        Assert.NotNull(d);
        Assert.Equal("2010年10月1日 国庆节(休)", d.ToString());
    }
}