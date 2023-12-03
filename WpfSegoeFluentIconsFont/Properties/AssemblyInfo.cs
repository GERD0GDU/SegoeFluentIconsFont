using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Segoe Fluent Icons Font")]
[assembly: AssemblyDescription("This application provides developer guidelines for using the Segoe Fluent Icons font and lists each icon along with its Unicode value and descriptive name.")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Handeck")]
[assembly: AssemblyProduct("Sample")]
[assembly: AssemblyCopyright("Copyright © 2023 Handeck. All right reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyAuthor("Gökhan Erdoğdu")]
[assembly: AssemblyEmail("gokhan_erdogdu@yahoo.com")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]



namespace WpfSegoeFluentIconsFont
{
    /// <summary>
    /// Gets the values from the AssemblyInfo.cs file for the current executing assembly
    /// </summary>
    /// <example>        
    /// string company = AssemblyInfo.Company;
    /// string product = AssemblyInfo.Product;
    /// string copyright = AssemblyInfo.Copyright;
    /// string trademark = AssemblyInfo.Trademark;
    /// string title = AssemblyInfo.Title;
    /// string description = AssemblyInfo.Description;
    /// string configuration = AssemblyInfo.Configuration;
    /// string fileversion = AssemblyInfo.FileVersion;
    /// Version version = AssemblyInfo.Version;
    /// string versionFull = AssemblyInfo.VersionFull;
    /// string versionMajor = AssemblyInfo.VersionMajor;
    /// string versionMinor = AssemblyInfo.VersionMinor;
    /// string versionBuild = AssemblyInfo.VersionBuild;
    /// string versionRevision = AssemblyInfo.VersionRevision;
    /// </example>
    internal static class AssemblyInfo
    {
        public static string Company => GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
        public static string Product => GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product);
        public static string Copyright => GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        public static string Trademark => GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
        public static string Title => GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
        public static string Description => GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public static string Configuration => GetExecutingAssemblyAttribute<AssemblyConfigurationAttribute>(a => a.Configuration);
        public static string FileVersion => GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);
        public static string Author => GetExecutingAssemblyAttribute<AssemblyAuthorAttribute>(a => a.Author);
        public static string Email => GetExecutingAssemblyAttribute<AssemblyEmailAttribute>(a => a.Email);

        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public static string VersionFull => Version.ToString();
        public static string VersionMajor => Version.Major.ToString();
        public static string VersionMinor => Version.Minor.ToString();
        public static string VersionBuild => Version.Build.ToString();
        public static string VersionRevision => Version.Revision.ToString();

        private static string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }


    /// <summary>
    /// Gets the values from the AssemblyInfo.cs file for the previous assembly
    /// </summary>
    /// <example>
    /// AssemblyInfoCalling assembly = new AssemblyInfoCalling();
    /// string company1 = assembly.Company;
    /// string product1 = assembly.Product;
    /// string copyright1 = assembly.Copyright;
    /// string trademark1 = assembly.Trademark;
    /// string title1 = assembly.Title;
    /// string description1 = assembly.Description;
    /// string configuration1 = assembly.Configuration;
    /// string fileversion1 = assembly.FileVersion;
    /// Version version1 = assembly.Version;
    /// string versionFull1 = assembly.VersionFull;
    /// string versionMajor1 = assembly.VersionMajor;
    /// string versionMinor1 = assembly.VersionMinor;
    /// string versionBuild1 = assembly.VersionBuild;
    /// string versionRevision1 = assembly.VersionRevision;
    /// </example>
    internal class AssemblyInfoCalling
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoCalling"/> class.
        /// </summary>
        /// <param name="traceLevel">The trace level needed to get correct assembly 
        /// - will need to adjust based on where you put these classes in your project(s).</param>
        public AssemblyInfoCalling(int traceLevel = 4)
        {
            //----------------------------------------------------------------------
            // Default to "3" as the number of levels back in the stack trace to get the 
            //  correct assembly for "calling" assembly
            //----------------------------------------------------------------------
            StackTraceLevel = traceLevel;
        }

        //----------------------------------------------------------------------
        // Standard assembly attributes
        //----------------------------------------------------------------------
        public string Company => GetCallingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
        public string Product => GetCallingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product);
        public string Copyright => GetCallingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        public string Trademark => GetCallingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
        public string Title => GetCallingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
        public string Description => GetCallingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public string Configuration => GetCallingAssemblyAttribute<AssemblyConfigurationAttribute>(a => a.Configuration);
        public string FileVersion => GetCallingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);
        public string Author => GetCallingAssemblyAttribute<AssemblyAuthorAttribute>(a => a.Author);
        public string Email => GetCallingAssemblyAttribute<AssemblyEmailAttribute>(a => a.Email);

        //----------------------------------------------------------------------
        // Version attributes
        //----------------------------------------------------------------------
        public static Version Version
        {
            get
            {
                //----------------------------------------------------------------------
                // Get the assembly, return empty if null
                //----------------------------------------------------------------------
                Assembly assembly = GetAssembly(StackTraceLevel);
                return assembly == null ? new Version() : assembly.GetName().Version;
            }
        }
        public string VersionFull => Version.ToString();
        public string VersionMajor => Version.Major.ToString();
        public string VersionMinor => Version.Minor.ToString();
        public string VersionBuild => Version.Build.ToString();
        public string VersionRevision => Version.Revision.ToString();

        //----------------------------------------------------------------------
        // Set how deep in the stack trace we're looking - allows for customized changes
        //----------------------------------------------------------------------
        public static int StackTraceLevel { get; set; }

        /// <summary>
        /// Gets the calling assembly attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <example>return GetCallingAssemblyAttribute&lt;AssemblyCompanyAttribute&gt;(a => a.Company);</example>
        /// <returns></returns>
        private string GetCallingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            //----------------------------------------------------------------------
            // Get the assembly, return empty if null
            //----------------------------------------------------------------------
            Assembly assembly = GetAssembly(StackTraceLevel);
            if (assembly == null)
            {
                return string.Empty;
            }

            //----------------------------------------------------------------------
            // Get the attribute value
            //----------------------------------------------------------------------
            T attribute = (T)Attribute.GetCustomAttribute(assembly, typeof(T));
            return value.Invoke(attribute);
        }

        /// <summary>
        /// Go through the stack and gets the assembly
        /// </summary>
        /// <param name="stackTraceLevel">The stack trace level.</param>
        /// <returns></returns>
        private static Assembly GetAssembly(int stackTraceLevel)
        {
            //----------------------------------------------------------------------
            // Get the stack frame, returning null if none
            //----------------------------------------------------------------------
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            if (stackFrames == null)
            {
                return null;
            }

            //----------------------------------------------------------------------
            // Get the declaring type from the associated stack frame, returning null if nonw
            //----------------------------------------------------------------------
            Type declaringType = stackFrames[stackTraceLevel].GetMethod().DeclaringType;
            if (declaringType == null)
            {
                return null;
            }

            //----------------------------------------------------------------------
            // Return the assembly
            //----------------------------------------------------------------------
            Assembly assembly = declaringType.Assembly;
            return assembly;
        }
    }
}
