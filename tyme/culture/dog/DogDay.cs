namespace tyme.culture.dog
{
    /// <summary>
    /// 三伏天
    /// </summary>
    public class DogDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dog">三伏</param>
        /// <param name="dayIndex">天索引</param>
        public DogDay(Dog dog, int dayIndex) : base(dog, dayIndex)
        {
        }

        /// <summary>
        /// 三伏
        /// </summary>
        public Dog Dog => (Dog)culture;
    }
}