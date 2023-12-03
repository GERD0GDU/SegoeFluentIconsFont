//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------

namespace System.Reflection
{
    public static class DefaultValueExtensions
    {
        private class DefaultValueReflection
        {
            public object GetDefaultFromType(Type t)
            {
                return GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
            }

            public T GetDefaultGeneric<T>()
            {
                return default;
            }
        }

        public static object CreateDefault(this Type objType)
        {
            if (objType is null)
            {
                throw new ArgumentNullException("objType");
            }

            return new DefaultValueReflection().GetDefaultFromType(objType);
        }

        public static T CreateDefault<T>()
        {
            return new DefaultValueReflection().GetDefaultGeneric<T>();
        }
    }
}
