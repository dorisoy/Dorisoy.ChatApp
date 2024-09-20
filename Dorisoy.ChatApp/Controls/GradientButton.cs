using System;
using System.Collections.Generic;
using System.Text;

namespace Dorisoy.ChatApp.Controls
{
    public class GradientButton : Button
    {
        public enum GradientOrientation
        {
            Vertical,
            Horizontal
        }

        public Color StartColor
        {
            get; set;
        }

        public Color EndColor
        {
            get; set;
        }

        public GradientOrientation GradientColorOrientation
        {
            get; set;
        }
    }
}
