using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace RegIN_Kylosov.Classes
{
    public class Tools
    {
        public static void AnimateImageChange(Image imageControl, ImageSource newSource, double durationSeconds = 0.6)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(durationSeconds)
            };

            fadeOutAnimation.Completed += (s, e) =>
            {
                imageControl.Source = newSource;

                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(durationSeconds * 2)
                };
                imageControl.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
            };

            imageControl.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
        }

        public static void SetNotification(Label label, string Message, SolidColorBrush _Color)
        {
            label.Content = Message;
            label.Foreground = _Color;
        }

        public static bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"(?=.*[0-9])(?=.*[!@#$%^&?*\-_=])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&?*\-_=]{10,}");
            return regex.IsMatch(password);
        }
    }
}
