//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.Threading;
using System.Diagnostics;

namespace System
{
    public class PreventMultipleInstances : IDisposable
    {
        private bool _disposed = false;

        private Mutex g_mutex = null;

        public PreventMultipleInstances(string name)
        {
#if DEBUG
            Debug.Assert(!string.IsNullOrEmpty(name), "Invalid 'name' parameter.");
#endif

            Name = name;

            if (!string.IsNullOrEmpty(name))
            {
                bool created = false;
                g_mutex = new Mutex(false, name, out created);
                IsMultibleInstance = !created;
            }
        }

        #region <Dispose Methods>
        ~PreventMultipleInstances()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                // Free any other managed objects here. 
                //
                if (g_mutex != null)
                {
                    g_mutex.Dispose();
                    g_mutex = null;
                }
            }
            // Free any unmanaged objects here. 
            //

            _disposed = true;
        }
        #endregion // </Dispose Methods>

        public static implicit operator bool(PreventMultipleInstances obj)
        {
            return obj != null && obj.IsMultibleInstance;
        }

        public string Name { get; private set; }
        public bool IsMultibleInstance { get; private set; }
    }
}
