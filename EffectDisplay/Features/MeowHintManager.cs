namespace EffectDisplay.Features
{
    using System;
    using System.Reflection;
    using System.Linq;

    using Exiled.Loader;
    using EffectDisplay.Features.Sereliazer;

    /// <summary>
    /// Provides work with the service by providing hints for multi-transmissions
    /// </summary>
    public class MeowHintManager
    {
        private static Assembly meowhints;
        private Type Hint;
        private Type HorizantalAligmentType;
        private Type VerticalAligmentType;
        private object HintInstance;
        /// <summary>
        /// Creates an instance of a class without any default settings.
        /// </summary>
        public MeowHintManager()
        {
            if (meowhints == null)
            {
                meowhints = Loader.Plugins.Where(x => x.Name == "HintServiceMeow").FirstOrDefault().Assembly;
            }
            if (meowhints == null) throw new Exception("Assembly HintServiceMeow not loaded in Exiled plugins");
            Hint = meowhints.GetType("HintServiceMeow.Core.Models.Hints.Hint", false);
            if (Hint == null) throw new Exception("Type Hint not found");
            HintInstance = Activator.CreateInstance(Hint);
            HorizantalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintAlignment");
            if (HorizantalAligmentType == null | !HorizantalAligmentType.IsEnum) throw new Exception("HintAligment not found or is not Enum type");
            VerticalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintVerticalAlign");
            if (VerticalAligmentType == null | !VerticalAligmentType.IsEnum) throw new Exception("HintVerticalAligment not found or is not Enum type");
        }
        /// <summary>
        /// Creates an instance of a class with preset parameters that can be retrieved without extra methods
        /// </summary>
        /// <param name="text">Hint text</param>
        /// <param name="fontsize">Text font size</param>
        /// <param name="valign">Vertical aligment (Left, Center, Right)</param>
        /// <param name="halign">Horizontal aligment (Top, Middle, Bottom)</param>
        /// <param name="y">Y Coordinate position (0 : 1080)</param>
        /// <param name="x">X Coordinate position (-1200 : 1200)</param>
        /// <param name="id">Hint work id</param>
        public MeowHintManager(string text, int fontsize = 16, string valign = "Center", string halign = "Middle", int y = 0, int x = 0, string id = "HintGeneration")
        {
            if (meowhints == null)
            {
                meowhints = Loader.Plugins.Where(xp => xp.Name == "HintServiceMeow").FirstOrDefault().Assembly;
            }
            if (meowhints == null) throw new Exception("Assembly HintServiceMeow not loaded in Exiled plugins");
            Hint = meowhints.GetType("HintServiceMeow.Core.Models.Hints.Hint", false);
            if (Hint == null) throw new Exception("Type Hint not found");
            HintInstance = Activator.CreateInstance(Hint);
            HorizantalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintAlignment");
            if (HorizantalAligmentType == null | !HorizantalAligmentType.IsEnum) throw new Exception("HintAligment not found or is not Enum type");
            VerticalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintVerticalAlign");
            if (VerticalAligmentType == null | !VerticalAligmentType.IsEnum) throw new Exception("HintVerticalAligment not found or is not Enum type");
            SetText(text);
            SetFont(fontsize);
            SetVerticalAligment(valign);
            SetHorizontalAligment(halign);
            SetId(id);
            SetXCoordinate(x);
            SetYCoordinates(y);
        }
        /// <summary>
        /// Creates an instance of a class with predefined parameters from <see cref="MeowHintSettings"/>
        /// </summary>
        /// <param name="text">Hint text</param>
        /// <param name="id">Hint work id</param>
        /// <param name="settings">Class providing settings</param>
        public MeowHintManager(string text, string id, MeowHintSettings settings)
        {
            if (meowhints == null)
            {
                meowhints = Loader.Plugins.Where(xp => xp.Name == "HintServiceMeow").FirstOrDefault().Assembly;
            }
            if (meowhints == null) throw new Exception("Assembly HintServiceMeow not loaded in Exiled plugins");
            Hint = meowhints.GetType("HintServiceMeow.Core.Models.Hints.Hint", false);
            if (Hint == null) throw new Exception("Type Hint not found");
            HintInstance = Activator.CreateInstance(Hint);
            HorizantalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintAlignment");
            if (HorizantalAligmentType == null | !HorizantalAligmentType.IsEnum) throw new Exception("HintAligment not found or is not Enum type");
            VerticalAligmentType = meowhints.GetType("HintServiceMeow.Core.Enum.HintVerticalAlign");
            if (VerticalAligmentType == null | !VerticalAligmentType.IsEnum) throw new Exception("HintVerticalAligment not found or is not Enum type");
            SetText(text);
            SetFont(settings.FontSize);
            SetVerticalAligment(settings.VerticalAligment);
            SetHorizontalAligment(settings.Aligment);
            SetId(id);
            SetXCoordinate(settings.XCoordinate);
            SetYCoordinates(settings.YCoordinate);
        }
        /// <summary>
        /// Sets the text size parameter in the working class hint
        /// </summary>
        public void SetFont(int FontSize)
        {
            try
            { 
                SetProperty("FontSize", FontSize); 
            }
            catch 
            { 

            }
        }
        /// <summary>
        /// Sets the text parameter that will be displayed
        /// </summary>
        public void SetText(string Text)
        {
            try
            {
                SetProperty("Text", Text);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Sets horizontal alignment
        /// </summary>
        /// <param name="aligment">Left, Center, Right</param>
        public void SetHorizontalAligment(string aligment)
        {
            try
            {
                object value = Enum.Parse(HorizantalAligmentType, aligment);
                SetProperty("Alignment", value);
            }
            catch { }
        }
        /// <summary>
        /// Sets vertical alignment
        /// </summary>
        /// <param name="aligment">Top, Bottom, Middle</param>
        public void SetVerticalAligment(string aligment)
        {
            try
            {
                object value = Enum.Parse(VerticalAligmentType, aligment);
                SetProperty("YCoordinateAlign", value);
            }
            catch { }
        }
        /// <summary>
        /// Takes a position in relation to Y
        /// </summary>
        /// <param name="y">0 -> 1080</param>
        public void SetYCoordinates(int y)
        {
            try
            {
                SetProperty("YCoordinate", y);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Takes a position in relation to X
        /// </summary>
        /// <param name="x">-1200 -> 1200</param>
        public void SetXCoordinate(int x)
        {
            try
            {
                SetProperty("XCoordinate", x);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Sets a unique identifier for the displayed hint message.
        /// </summary>
        public void SetId(string id)
        {
            try
            {
                SetProperty("Id", id);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Gets the resulting created Hint-based object type from HintServiceMeow
        /// </summary>
        /// <returns>type of object with set parameters at the time of operation</returns>
        public object GetHintProcessionObject()
        {
            return HintInstance;
        }

        private void SetProperty(string name, object value)
        {
            if (HintInstance == null) return;
            HintInstance.GetType().GetProperty(name)?.SetValue(HintInstance, value, null);
        }
        private object GetPropertyValue(string name)
        {
            if (HintInstance == null) return null;
            return HintInstance.GetType().GetProperty(name)?.GetValue(HintInstance, null);
        }
        /// <summary>
        /// uses the extension method from HintServiceMeow to add an object Hint"/>
        /// </summary>
        /// <param name="Player">the player for whom this method will be applied</param>
        /// <param name="MeowHint">object <see cref="MeowHintManager.GetHintProcessionObject"/> to add</param>
        public static void AddHint(object Player, object MeowHint)
        {
            meowhints.GetType("HintServiceMeow.Core.Extension.ExiledPlayerExtension").GetMethod("AddHint", BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[] { Player, MeowHint });
        }
        /// <summary>
        /// uses the extension method from HintServiceMeow to remove an object Hint"/>
        /// </summary>
        /// <param name="Player">the player for whom this method will be removed</param>
        /// <param name="MeowHint">object <see cref="MeowHintManager.GetHintProcessionObject"/> to remove</param>
        public static void RemoveHint(object Player, object MeowHint)
        {
            meowhints.GetType("HintServiceMeow.Core.Extension.ExiledPlayerExtension").GetMethod("RemoveHint", BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[] { Player, MeowHint });
        }
    }
}
