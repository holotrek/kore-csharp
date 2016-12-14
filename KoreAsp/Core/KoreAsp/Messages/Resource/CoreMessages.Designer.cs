﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KoreAsp.Messages.Resource {
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
    internal class CoreMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CoreMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KoreAsp.Messages.Resource.CoreMessages", typeof(CoreMessages).Assembly);
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
        ///   Looks up a localized string similar to {0} was unable to be added..
        /// </summary>
        internal static string FailedAdd {
            get {
                return ResourceManager.GetString("FailedAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was unabled to be changed..
        /// </summary>
        internal static string FailedAddOrUpdate {
            get {
                return ResourceManager.GetString("FailedAddOrUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was unable to be deleted..
        /// </summary>
        internal static string FailedDelete {
            get {
                return ResourceManager.GetString("FailedDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was unable to be updated..
        /// </summary>
        internal static string FailedUpdate {
            get {
                return ResourceManager.GetString("FailedUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unauthorized access to resource: {0}.
        /// </summary>
        internal static string HttpStatusCode401 {
            get {
                return ResourceManager.GetString("HttpStatusCode401", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forbidden to access resource: {0}.
        /// </summary>
        internal static string HttpStatusCode403 {
            get {
                return ResourceManager.GetString("HttpStatusCode403", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to find resource: {0}.
        /// </summary>
        internal static string HttpStatusCode404 {
            get {
                return ResourceManager.GetString("HttpStatusCode404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Server Error encountered in resource: {0}.
        /// </summary>
        internal static string HttpStatusCode500 {
            get {
                return ResourceManager.GetString("HttpStatusCode500", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The request passed, but no changes were detected to be saved..
        /// </summary>
        internal static string NoRecordsSaved {
            get {
                return ResourceManager.GetString("NoRecordsSaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A fatal application error occurred. Please use Reference ID {0} when contacting support..
        /// </summary>
        internal static string ProductionMessageForExceptions {
            get {
                return ResourceManager.GetString("ProductionMessageForExceptions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} successfully added..
        /// </summary>
        internal static string SuccessfulAdd {
            get {
                return ResourceManager.GetString("SuccessfulAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} successfully changed..
        /// </summary>
        internal static string SuccessfulAddOrUpdate {
            get {
                return ResourceManager.GetString("SuccessfulAddOrUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} successfully deleted..
        /// </summary>
        internal static string SuccessfulDelete {
            get {
                return ResourceManager.GetString("SuccessfulDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} successfully updated..
        /// </summary>
        internal static string SuccessfulUpdate {
            get {
                return ResourceManager.GetString("SuccessfulUpdate", resourceCulture);
            }
        }
    }
}