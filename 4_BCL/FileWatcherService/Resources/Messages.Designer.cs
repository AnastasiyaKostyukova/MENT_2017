﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileWatcherService.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FileWatcherService.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Found new configurate element:.
        /// </summary>
        internal static string CustomSectionElement {
            get {
                return ResourceManager.GetString("CustomSectionElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Watching of the directory is stopped. Reason:.
        /// </summary>
        internal static string ExceptionMessage {
            get {
                return ResourceManager.GetString("ExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File found:.
        /// </summary>
        internal static string FileFound {
            get {
                return ResourceManager.GetString("FileFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File replaced:.
        /// </summary>
        internal static string FileReplaced {
            get {
                return ResourceManager.GetString("FileReplaced", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press CTRL+C or CTRL-Break to interrupt the watching:.
        /// </summary>
        internal static string FinishMessage {
            get {
                return ResourceManager.GetString("FinishMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rule found:.
        /// </summary>
        internal static string RuleFound {
            get {
                return ResourceManager.GetString("RuleFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No rule found.
        /// </summary>
        internal static string RuleNotFound {
            get {
                return ResourceManager.GetString("RuleNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Same file deleted from destination folder..
        /// </summary>
        internal static string SameFileDeleting {
            get {
                return ResourceManager.GetString("SameFileDeleting", resourceCulture);
            }
        }
    }
}