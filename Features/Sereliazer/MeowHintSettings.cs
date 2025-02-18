using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectDisplay.Features.Sereliazer
{
    public class MeowHintSettings
    {
        [Description("Text size")]
        public int FontSize { get; set; } = 16;
        [Description("Position Y Horizontal coordinate 0 -> 1080")]
        public int YCoordinate { get; set; } = 900;
        [Description("Position X Vertical coordinate -1200 -> 1200")]
        public int XCoordinate { get; set; } = -1200;
        [Description("Hint aligment (Left, Right, Center)")]
        public string Aligment { get; set; } = "Left";
        [Description("Hint vertical aligment (Top, Bottom, Middle)")]
        public string VerticalAligment { get; set; } = "Bottom";
    }
}