using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectDisplay.Features.Sereliazer
{
    public class NativeHintSettings
    {
        [Description("Text size")]
        public int FontSize { get; set; } = 12;
        [Description("Text aligment")]
        public string Aligment { get; set; } = "Left";
    }
}
