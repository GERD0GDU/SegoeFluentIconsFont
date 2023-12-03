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

namespace System
{
    public class ArgumentWrapper
    {
        private Dictionary<string, string> m_args;

        private static ArgumentWrapper _defaultInstance = new ArgumentWrapper();
        public static ArgumentWrapper Default
        {
            get { return _defaultInstance; }
            set { _defaultInstance = value is null ? new ArgumentWrapper() : value; }
        }

        private ArgumentWrapper()
        {
            m_args = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public ArgumentWrapper(IEnumerable<string> args)
        {
            m_args = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            string sName = "";
            foreach (string arg in args)
            {
                if (string.IsNullOrWhiteSpace(arg))
                    continue;

                if ((arg[0] == '-') || (arg[0] == '/'))
                {
                    sName = arg.Substring(1);
                    m_args[sName] = "";
                }
                else
                {
                    m_args[sName] = arg;
                    sName = "";
                }
            }
        }

        public int Count { get { return m_args.Count; } }

        public string[] Keys
        {
            get { return m_args.Keys.Cast<string>().ToArray(); }
        }

        public string[] Values
        {
            get { return m_args.Values.Cast<string>().ToArray(); }
        }

        public string this[string key]
        {
            get { return m_args.ContainsKey(key) ? m_args[key] : ""; }
        }

        public bool ContainsKey(string key)
        {
            return m_args.ContainsKey(key);
        }

        public string FileName
        {
            get
            {
                return m_args.ContainsKey("")
                    ? m_args[""]
                    : "";
            }
        }

        public bool IsExit { get { return this.ContainsKey("exit"); } }
        public bool IsInstall { get { return this.ContainsKey("install"); } }
        public bool IsUninstall { get { return this.ContainsKey("uninstall"); } }
        public bool IsStart { get { return this.ContainsKey("start"); } }
        public bool IsStop { get { return this.ContainsKey("stop"); } }
        public bool IsConfig { get { return this.ContainsKey("config"); } }
        public bool IsConsole { get { return this.ContainsKey("console"); } }
        public bool IsGUI { get { return this.ContainsKey("gui"); } }
        public bool IsScript { get { return this.ContainsKey("script"); } }
    }
}
