using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exiled.API.Features;
using Exiled.Loader;

namespace EffectDisplay.Extension
{
    public class RueIHints
    {
        public static bool IsEnabled { get; private set; } = false;

        public static void ShowHint(ReferenceHub hub, string content, TimeSpan duration, float position = 0)
        {
            if (IsEnabled) { _shower(hub, content, position, duration); }
            else
            {
                Log.Debug("RueI not inited");
                return;
            }
        }
        public static void ShowHint(Player player, string content, TimeSpan duration, float position = 0)
        {
            if (IsEnabled) { _shower(player.ReferenceHub, content, position, duration); }
            else
            {
                Log.Debug("RueI not inited");
                return;
            }
        }

        public static void InitRue()
        {
            IsEnabled = false;
            _shower = null;
            Assembly ruei = Loader.Dependencies.FirstOrDefault(x => x.GetName().Name == "RueI");
            if (ruei == null) { Log.Debug("RueI not found in dependencies"); return; }
            else
            {
                MethodInfo els = ruei.GetType("RueI.Extensions.ReflectionHelpers").GetMethod("GetElementShower");
                object action = els.Invoke(null, new object[] { });
                if (action is Action<ReferenceHub, string, float, TimeSpan> elst)
                {
                    MethodInfo init = ruei.GetType("RueI.RueIMain").GetMethod("EnsureInit");
                    if (init == null) return;
                    else
                    {
                        init.Invoke(null, new object[] { });
                        _shower = elst;
                        IsEnabled = true;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private static Action<ReferenceHub, string, float, TimeSpan> _shower;
    }
}
