﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookMeMobile.Resources {
    using System;
    using System.Reflection;
    
    
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
    internal class AlertMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AlertMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BookMeMobile.Resources.AlertMessages", typeof(AlertMessages).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Ошибка.
        /// </summary>
        internal static string ErrorHeader {
            get {
                return ResourceManager.GetString("ErrorHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заполните пустое поле.
        /// </summary>
        internal static string FieldIsEmpty {
            get {
                return ResourceManager.GetString("FieldIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ok.
        /// </summary>
        internal static string InfoAlertCanelText {
            get {
                return ResourceManager.GetString("InfoAlertCanelText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нет подключения к интернету.
        /// </summary>
        internal static string NoInternetConnection {
            get {
                return ResourceManager.GetString("NoInternetConnection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка на стороне сервера.
        /// </summary>
        internal static string ServerError {
            get {
                return ResourceManager.GetString("ServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Действие выполнено успешно.
        /// </summary>
        internal static string SuccessBody {
            get {
                return ResourceManager.GetString("SuccessBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Успешно.
        /// </summary>
        internal static string SuccessHeader {
            get {
                return ResourceManager.GetString("SuccessHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нельзя забронировать комнату на прошлое время..
        /// </summary>
        internal static string WrongIntervalInThePast {
            get {
                return ResourceManager.GetString("WrongIntervalInThePast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбран неверный интервал.
        /// </summary>
        internal static string WrongIntervalTime {
            get {
                return ResourceManager.GetString("WrongIntervalTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Логин или пароль введены неверно.
        /// </summary>
        internal static string WrongLoginOrPassword {
            get {
                return ResourceManager.GetString("WrongLoginOrPassword", resourceCulture);
            }
        }
    }
}
