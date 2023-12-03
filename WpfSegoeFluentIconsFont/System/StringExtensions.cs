//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string Left(this string s, int length)
        {
            return string.IsNullOrEmpty(s)
                ? s
                : s.Substring(0, length.Range(0, s.Length));
        }

        public static string Right(this string s, int length)
        {
            return string.IsNullOrEmpty(s)
                ? s
                : s.Substring(s.Length - length.Range(0, s.Length));
        }

        public static string RemoveDiacritics(this string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                Globalization.UnicodeCategory unicodeCategory = Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        #region Format Methods

        public static string Format(this string format, object arg0)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(format, arg0);
        }

        public static string Format(this string format, object arg0, object arg1)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(format, arg0, arg1, arg2);
        }

        public static string Format(this string format, params object[] args)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(format, args);
        }

        public static string Format(this string format, IFormatProvider provider, object arg0)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(provider, format, arg0);
        }

        public static string Format(this string format, IFormatProvider provider, object arg0, object arg1)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(provider, format, arg0, arg1);
        }

        public static string Format(this string format, IFormatProvider provider, object arg0, object arg1, object arg2)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(provider, format, arg0, arg1, arg2);
        }

        public static string Format(this string format, IFormatProvider provider, params object[] args)
        {
            return string.IsNullOrEmpty(format)
                ? format
                : string.Format(provider, format, args);
        }

        #endregion // Format Methods
    }
}
