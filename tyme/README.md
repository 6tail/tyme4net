# Tyme [![License](https://img.shields.io/badge/license-MIT-4EB1BA.svg?style=flat-square)](https://github.com/6tail/tyme4j/blob/master/LICENSE)

Tyme是一个非常强大的日历工具库，可以看作 [Lunar](https://6tail.cn/calendar/api.html "https://6tail.cn/calendar/api.html") 的升级版，拥有更优的设计和扩展性，支持公历和农历、星座、干支、生肖、节气、法定假日等。

> 基于netstandard2.0、C#7.3

## 示例

    using System;
    using tyme.solar;

    namespace demo
    {
        class Program
        {
            static void Main(string[] args)
            {
                // 公历日
                var solarDay = SolarDay.FromYmd(1986, 5, 29);
                 
                // 转农历日
                var lunarDay = solarDay.GetLunarDay();
                 
                Console.WriteLine(lunarDay.ToString());
                Console.WriteLine(solarDay.ToString());
            }
        }
    }

## 文档

请移步至 [https://6tail.cn/tyme.html](https://6tail.cn/tyme.html "https://6tail.cn/tyme.html")
