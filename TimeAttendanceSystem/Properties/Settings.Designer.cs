﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeAttendanceSystem.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=AWAIS-PC;Initial Catalog=TASDesktop;Persist Security Info=True;User I" +
            "D=sa;Password=Cns123;MultipleActiveResultSets=True;Application Name=EntityFramew" +
            "ork")]
        public string TASDesktopConnectionString {
            get {
                return ((string)(this["TASDesktopConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=AWAIS-PC;Initial Catalog=TASDesktop;Persist Security Info=True;User I" +
            "D=sa;Password=Cns123")]
        public string TASDesktopConnectionString1 {
            get {
                return ((string)(this["TASDesktopConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("E:\\\\Projects\\\\inven Projects\\\\TimeAttendanceSystem\\\\TimeAttendanceSystem\\\\Reports" +
            "\\\\RDLC\\\\")]
        public string ReportPath {
            get {
                return ((string)(this["ReportPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=AHSIN-PC;Initial Catalog=TASDesktop;Persist Security Info=True;User I" +
            "D=sa;Password=Cns123")]
        public string TASDesktopConnectionString2 {
            get {
                return ((string)(this["TASDesktopConnectionString2"]));
            }
        }
    }
}
