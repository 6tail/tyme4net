using System.ComponentModel;

namespace tyme.enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")] Woman = 0,
        
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")] Man = 1
    }

    /// <summary>
    /// 性别扩展
    /// </summary>
    public static class GenderExtension
    {
        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="gender">性别</param>
        /// <returns>代码</returns>
        public static int GetCode(this Gender gender)
        {
            return (int)gender;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="gender">性别</param>
        /// <returns>名称</returns>
        public static string GetName(this Gender gender)
        {
            return ((DescriptionAttribute)gender.GetType().GetMember(gender.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        /// <param name="gender">性别</param>
        /// <returns>描述</returns>
        public static string ToString(this Gender gender)
        {
            return gender.GetName();
        }
        
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="gender">本性别</param>
        /// <param name="target">其他性别</param>
        /// <returns>True/False</returns>
        public static bool Equals(this Gender gender, Gender target)
        {
            return gender == target;
        }
    }
}