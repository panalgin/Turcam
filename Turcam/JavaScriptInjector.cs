using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Turcam
{
    public static class JavascriptInjector
    {
        public enum ScriptAction
        {
            CommandSent,
            CommandFailed,
            CommandReceived,
            Connected,
            Disconnected,
            PositionChanged
        }

        private static Dictionary<ScriptAction, ScriptInfo> ScriptEntities = new Dictionary<ScriptAction, ScriptInfo>() 
        {
            { ScriptAction.CommandSent, new ScriptInfo("View\\js\\async\\command-sent.js", false) },
            { ScriptAction.CommandFailed, new ScriptInfo("View\\js\\async\\command-failed.js", false) },
            { ScriptAction.CommandReceived, new ScriptInfo("View\\js\\async\\command-received.js", false) },
            { ScriptAction.Connected, new ScriptInfo("View\\js\\async\\connected.js", false) },
            { ScriptAction.Disconnected, new ScriptInfo("View\\js\\async\\disconnected.js", false) },
            { ScriptAction.PositionChanged, new ScriptInfo("View\\js\\cached\\position-changed.js", false) }
        };

        public static void Run(ScriptAction action, params object[] parameters)
        {
            var entity = ScriptEntities[action];

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow m_Window = Application.Current.MainWindow as MainWindow;
                string m_Script = string.Empty;

                if (entity.Exists)
                {
                    if (entity.Cacheable == false)
                    {
                        using (StreamReader reader = new StreamReader(entity.FileLocation))
                        {
                            m_Script = reader.ReadToEnd();
                        }
                    }
                    else
                        m_Script = entity.CachedData;

                    m_Window.Browser.ExecuteScriptAsync(string.Format(m_Script, parameters));
                }
                else
                {
                    throw new FileNotFoundException(entity.FileLocation + " bulunamadı.");
                }
            }));
        }

        private class ScriptInfo
        {
            public string FileLocation { get; private set; }
            public string CachedData { get; private set; }
            public bool Cacheable { get; private set; }

            public bool Exists { get; private set; }

            public ScriptInfo(string path, bool cacheable)
            {
                this.FileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                this.Cacheable = cacheable;

                if (File.Exists(this.FileLocation))
                {
                    this.Exists = true;

                    using (StreamReader reader = new StreamReader(this.FileLocation))
                    {
                        this.CachedData = reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
