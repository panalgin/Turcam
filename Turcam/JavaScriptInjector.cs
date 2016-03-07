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
            Disconnected
        }

        public static Dictionary<ScriptAction, string> ScriptPaths = new Dictionary<ScriptAction, string>() 
        {
            { ScriptAction.CommandSent, "View\\js\\async\\command-sent.js" },
            { ScriptAction.CommandFailed, "View\\js\\async\\command-failed.js" },
            { ScriptAction.CommandReceived, "View\\js\\async\\command-received.js" },
            { ScriptAction.Connected, "View\\js\\async\\connected.js" },
            { ScriptAction.Disconnected, "View\\js\\async\\disconnected.js" }
        };

        public static void Run(ScriptAction action, params string[] parameters)
        {
            var item = ScriptPaths[action];

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow m_Window = Application.Current.MainWindow as MainWindow;
                string m_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item);
                string m_Script = string.Empty;

                if (File.Exists(m_Path))
                {
                    using (StreamReader reader = new StreamReader(m_Path))
                    {
                        m_Script = reader.ReadToEnd();
                    }

                    m_Window.Browser.ExecuteScriptAsync(string.Format(m_Script, parameters));
                }
                else
                {
                    throw new FileNotFoundException(m_Path + " bulunamadı.");
                }
            }));
        }
    }
}
