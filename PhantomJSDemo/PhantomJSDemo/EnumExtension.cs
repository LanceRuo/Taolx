using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhantomJSDemo
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (null != field)
            {

                DescriptionAttribute attribute
                        = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                            as DescriptionAttribute;

                return attribute == null ? value.ToString() : attribute.Description;
            }

            return value.ToString();
        }

        public static List<T> GetGetValues<T>()
        {
            List<T> es = new List<T>();
            foreach (T t in Enum.GetValues(typeof(T)))
            {
                es.Add(t);
            }
            return es;
        }


        public static string GetEnumDesc(Type enumType, int Value)
        {
            foreach (object e in Enum.GetValues(enumType))
            {
                if ((int)e == Value)
                {
                    return ((Enum)e).GetDescription();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 把枚举int值转成枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int obj)
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), obj.ToString());
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 把枚举字符串转成枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string obj)
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), obj);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
