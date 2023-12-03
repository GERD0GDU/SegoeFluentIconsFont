//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class EnumExtensions
    {
        public static ulong ToUInt64(this Enum en)
        {
            return (ulong)Convert.ChangeType(en, typeof(ulong));
        }

        public static long ToInt64(this Enum en)
        {
            return (long)en.ToUInt64();
        }

        public static int ToInt32(this Enum en)
        {
            ulong ui64 = en.ToUInt64();
            return ((ui64 % 0xFFFFFFFF00000000) == 0x0)
                ? 0
                : (int)(ui64 & 0xFFFFFFFF);
        }

        public static uint ToUInt32(this Enum en)
        {
            ulong ui64 = en.ToUInt64();
            return ((ui64 % 0xFFFFFFFF00000000) == 0x0)
                ? 0
                : (uint)(ui64 & 0xFFFFFFFF);
        }

        #region TryToEnum helper methods

        public static bool TryToEnum<TEnum>(string s, bool ignoreCase, out object result)
            where TEnum : struct
        {
            Type genericType = typeof(TEnum);
            if (!genericType.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            result = genericType.CreateDefault();
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            try
            {
                if (Enum.TryParse(s, ignoreCase, out TEnum tResult))
                {
                    result = tResult;
                    return true;
                }
            }
            catch { }

            List<string> values = new List<string>(s.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Trim()).ToList();

            int retval = 0;
            Array aryEnums = Enum.GetValues(genericType);
            foreach (TEnum enumItem in aryEnums)
            {
                if (values.Exists(x =>
                    (0 == string.Compare(enumItem.ToString(), x, ignoreCase)) ||
                    (0 == string.Compare((enumItem as Enum).ToDescription(), x, ignoreCase))))
                {
                    retval |= (int)Convert.ChangeType(enumItem, typeof(int));
                }
            }

            if (retval != 0)
            {
                try
                {
                    result = (TEnum)Enum.Parse(genericType, retval.ToString());
                    return true;
                }
                catch (Exception)
                {
                }
            }

            return false;
        }

        public static bool TryToEnum<TEnum>(string s, out object result)
            where TEnum : struct
        {
            return TryToEnum<TEnum>(s, true, out result);
        }

        public static bool TryToEnum(string s, Type enumType, bool ignoreCase, out object result)
        {
            object[] parameters = new object[] { s, ignoreCase, enumType.CreateDefault() };
            bool retVal = (bool)typeof(EnumExtensions)
                .GetMethod("TryToEnum", new Type[] { typeof(string), typeof(bool), typeof(object).MakeByRefType() })
                .MakeGenericMethod(enumType)
                .Invoke(null, parameters);

            result = parameters[2];
            return retVal;
        }

        public static bool TryToEnum(string s, Type enumType, out object result)
        {
            return TryToEnum(s, enumType, true, out result);
        }

        public static bool TryToEnum(object o, Type enumType, out object result)
        {
            result = enumType.CreateDefault();

            if (o is null)
            {
                return false;
            }

            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    try
                    {
                        result = Enum.Parse(enumType, o.ChangeType<ulong>().ToString());
                        return true;
                    }
                    catch (Exception)
                    { }
                    break;
                case TypeCode.String:
                    return TryToEnum((string)o, enumType, out result);
            }

            return false;
        }

        public static bool TryToEnum<TEnum>(object o, out object result)
            where TEnum : struct
        {
            result = typeof(TEnum).CreateDefault();
            if (!TryToEnum(o, typeof(TEnum), out object objResult))
            {
                return false;
            }

            result = objResult;
            return true;
        }

        #endregion // TryToEnum helper methods

        #region Enum extension methods

        public static TEnum ToEnum<TEnum>(this string s, bool ignoreCase)
            where TEnum : struct
        {
            if (TryToEnum<TEnum>(s, ignoreCase, out object result))
            {
                return (TEnum)result;
            }
            return (TEnum)typeof(TEnum).CreateDefault();
        }

        public static TEnum ToEnum<TEnum>(this string s)
            where TEnum : struct
        {
            return s.ToEnum<TEnum>(true);
        }

        public static object ToEnum(this string s, Type enumType, bool ignoreCase)
        {
            // get 'object Extensions::ToEnum<TEnum>(this string s, bool ignoreCase)'
            return typeof(Extensions)
                .GetMethod("ToEnum", new Type[] { typeof(string), typeof(bool) })
                .MakeGenericMethod(enumType)
                .Invoke(null, new object[] { s, ignoreCase });
        }

        public static object ToEnum(this string s, Type enumType)
        {
            return s.ToEnum(enumType, true);
        }

        public static object ToEnum(this object o, Type enumType)
        {
            return TryToEnum(o, enumType, out object result)
                ? result
                : enumType.CreateDefault();
        }

        public static TEnum ToEnum<TEnum>(this object o)
            where TEnum : struct
        {
            return (TEnum)o.ToEnum(typeof(TEnum));
        }

        public static string ToDescription(this Enum en)
        {
            Type genericType = en.GetType();
            FlagsAttribute flagAttr = genericType.GetCustomAttribute<FlagsAttribute>();
            List<string> retVal = new List<string>();

            if (flagAttr is null)
            {
                MemberInfo[] memInfo = genericType.GetMember(en.ToString());

                if (memInfo != null && memInfo.Length > 0)
                {
                    DescriptionAttribute attr = memInfo[0].GetCustomAttribute<DescriptionAttribute>();
                    if (attr != null)
                    {
                        retVal.Add(attr.Description);
                    }
                }

                if (retVal.Count == 0)
                {
                    retVal.Add(en.ToString());
                }
            }
            else
            {
                string memberName = string.Empty;
                Array aryEnums = Enum.GetValues(genericType);
                foreach (Enum enumItem in aryEnums)
                {
                    if (enumItem.ToInt32() == en.ToInt32())
                    {
                        MemberInfo[] memInfo = genericType.GetMember(enumItem.ToString());

                        if (memInfo != null && memInfo.Length > 0)
                        {
                            DescriptionAttribute attr = memInfo[0].GetCustomAttribute<DescriptionAttribute>();
                            if (attr != null)
                            {
                                memberName = attr.Description;
                            }
                        }

                        if (string.IsNullOrEmpty(memberName))
                        {
                            memberName = enumItem.ToString();
                        }

                        break;
                    }
                }

                if (!string.IsNullOrEmpty(memberName))
                {
                    retVal.Add(memberName);
                }
                else
                {
                    foreach (Enum enumItem in aryEnums)
                    {
                        if ((enumItem.ToInt32() != 0) && en.HasFlag(enumItem))
                        {
                            memberName = string.Empty;
                            MemberInfo[] memInfo = genericType.GetMember(enumItem.ToString());

                            if (memInfo != null && memInfo.Length > 0)
                            {
                                DescriptionAttribute attr = memInfo[0].GetCustomAttribute<DescriptionAttribute>();
                                if (attr != null)
                                {
                                    memberName = attr.Description;
                                }
                            }

                            if (string.IsNullOrEmpty(memberName))
                            {
                                memberName = enumItem.ToString();
                            }

                            retVal.Add(memberName);
                        }
                    }
                }

                if (retVal.Count == 0)
                {
                    retVal.Add(((Enum)aryEnums.GetValue(0)).ToDescription());
                }
            }

            return string.Join(" | ", retVal);
        }

        #endregion // Enum extension methods
    }
}
