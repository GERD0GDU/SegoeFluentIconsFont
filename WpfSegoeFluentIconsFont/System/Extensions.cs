//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Windows;

namespace System
{
    public static class Extensions
    {
        /// <summary>
        /// Tests whether an enumerable object is undefined or empty.
        /// </summary>
        /// <param name="items">An object of type 'System.Collections.IEnumerable'.</param>
        /// <returns>Returns 'true' if the object is 'null' or 'empty'.</returns>
        public static bool IsNullOrEmpty(this IEnumerable items)
        {
            return (items == null) || !items.GetEnumerator().MoveNext();
        }

        public static string AddBackSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            return path.Last() == '\\'
                ? path
                : path + '\\';
        }

        public static string RemoveBackSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            return path.Last() == '\\'
                ? path.Left(path.Length - 1)
                : path;
        }

        public static bool IsEmpty(this DirectoryInfo directory)
        {
            return directory != null
                && directory.Exists
                && !directory.EnumerateFileSystemInfos().Any();
        }

        public static T SafeClone<T>(this T obj)
        {
            return ((obj == null) || !(obj is ICloneable cloneable))
                ? default
                : (T)cloneable.Clone();
        }

        public static void SafeDispose<T>(this T obj)
            where T : IDisposable
        {
            try
            {
                if (obj is IDisposable disposableObj)
                {
                    disposableObj.Dispose();
                }
            }
            catch
            { }
        }

        public static T ChangeType<T>(this object o)
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public static string GetExceptionMessages(this Exception error, int maxCount = 2)
        {
            if (error is null)
            {
                return string.Empty;
            }

            Exception item = error;
            List<string> messages = new List<string>();
            for (int i = 0; i < maxCount && item != null; i++, item = item.InnerException)
            {
                messages.Insert(0, item.Message);
            }

            return string.Join("\r\n", messages);
        }

        public static bool IncludeExceptionType(this Exception exception, Type exceptionType)
        {
            for (Exception item = exception; item != null; item = item.InnerException)
            {
                if (item.GetType() == exceptionType)
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<DictionaryEntry> GetResources(this ResourceDictionary resource)
        {
            List<DictionaryEntry> result = new List<DictionaryEntry>();

            foreach (DictionaryEntry item in resource)
            {
                result.Add(item);
            }

            foreach (ResourceDictionary resource2 in resource.MergedDictionaries)
            {
                foreach (DictionaryEntry item2 in resource2.GetResources())
                {
                    result.Add(item2);
                }
            }

            return result;
        }
    }
}
