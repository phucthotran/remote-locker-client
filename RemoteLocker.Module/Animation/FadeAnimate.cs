using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;

namespace RemoteLocker.Module.Animation
{
    /// <summary>
    /// Fade animation
    /// </summary>
    public class FadeAnimate : DoubleAnimation
    {
        /// <summary>
        /// Create animation with duration
        /// </summary>
        /// <param name="Duration">Duration of animation</param>
        public FadeAnimate(System.Windows.Duration Duration)
            : base()
        {
            this.From = From;
            this.To = To;
            this.Duration = Duration;           
        }

        /// <summary>
        /// From 'Visible' status to 'Hidden' status
        /// </summary>
        /// <returns></returns>
        public FadeAnimate FadeIn()
        {
            this.From = 0;
            this.To = 1.0;

            return this;
        }

        /// <summary>
        /// From 'Hidden' status to 'Visible' status
        /// </summary>
        /// <returns></returns>
        public FadeAnimate FadeOut()
        {
            this.From = 1.0;
            this.To = 0;

            return this;
        }
    }
}