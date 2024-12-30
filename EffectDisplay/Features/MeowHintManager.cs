namespace EffectDisplay.Features
{
    using System;
    using System.Reflection;
    using System.Linq;

    using Exiled.Loader;

    public class MeowHintManager
    {
        private static Assembly meowhints;
        private Type Hint;
        private Type HorizantalAligmentType;
        private Type VerticalAligmentType;
        private object HintInstance;
        /// <summary>
        /// Creates all the necessary types to work with HintServiceMeow
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
        /// </summary>
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
        /// </summary>
        /// <param name="aligment"></param>
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
        /// </summary>
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
        /// Exposes a parameter to the main Hint instance with the appropriate name
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
