//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.Runtime.InteropServices;

namespace System.Reflection
{
    // Summary:
    //     Specifies which author the assembly supports.
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ComVisible(true)]
    public sealed class AssemblyAuthorAttribute : Attribute
    {
        // Summary:
        //     Initializes a new instance of the System.Reflection.AssemblyAuthorAttribute
        //     class with the author supported by the assembly being attributed.
        //
        // Parameters:
        //   culture:
        //     The author supported by the attributed assembly.
        public AssemblyAuthorAttribute(string author)
        {
            this.Author = author;
        }

        // Summary:
        //     Gets the supported author of the attributed assembly.
        //
        // Returns:
        //     A string containing the name of the supported author.
        public string Author { get; private set; }
    }
}
