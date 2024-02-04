using Exiled.API.Interfaces;
using Exiled.Loader;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EffectDisplay.Extension
{
    // Lobby plugin conflict with EffectDisplay, added for remove it
    public class LobbyChecker
    {
        public const string Name = "Lobby";

        public const string EventHandlers = "Lobby.EventHandlers";

        public const string Parametr = "IsLobby";

        /// <summary>
        /// Read only
        /// </summary>
        public static bool IsLobby 
        { 
            get
            {
                if (lobby == null) return false;
                else
                {
                    try
                    {
                        Type eventhandler = lobby.GetType(EventHandlers);
                        return (bool)eventhandler.GetField(Parametr).GetValue(null);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        internal static Assembly lobby { get; private set; } = null;

        public static bool LoadOrRefresh()
        {
            foreach (IPlugin<IConfig> plugin in Loader.Plugins)
            {
                if (plugin.Name == Name)
                {
                    lobby = plugin.Assembly;
                    break;
                }
            }
            if (lobby == null) return false;
            else
            {
                return true;
            }
        }
    }
}
