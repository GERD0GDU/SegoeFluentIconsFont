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
    //     Specifies which email the assembly supports.
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ComVisible(true)]
    public sealed class AssemblyEmailAttribute : Attribute
    {
        // Summary:
        //     Initializes a new instance of the System.Reflection.AssemblyEmailAttribute
        //     class with the email supported by the assembly being attributed.
        //
        // Parameters:
        //   culture:
        //     The email supported by the attributed assembly.
        public AssemblyEmailAttribute(string email)
        {
            this.Email = email;
        }

        // Summary:
        //     Gets the supported email of the attributed assembly.
        //
        // Returns:
        //     A string containing the name of the supported email.
        public string Email { get; private set; }
    }
}
