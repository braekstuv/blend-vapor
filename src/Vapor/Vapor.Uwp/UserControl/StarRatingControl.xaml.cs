using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Vapor.UWP.UserControls
{
    public sealed partial class StarRatingControl : UserControl
    {
        private readonly IEnumerable<ToggleButton> _stars;

        public int Value { get; private set; }
        public StarRatingControl()
        {
            this.InitializeComponent();
            _stars = StarContainer.Children.OfType<ToggleButton>();

        }

        private void ToggleButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var senderStarValue = int.Parse(((ToggleButton)sender).Tag.ToString());

            foreach (var star in _stars)
            {
                var starValue = int.Parse(star.Tag.ToString());
                if (starValue <= senderStarValue)
                    VisualStateManager.GoToState(star, "PointerOver", true);
                else
                    VisualStateManager.GoToState(star, "Normal", true);
            }

        }

        private void ToggleButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            foreach (var star in _stars)
            {
                if (int.Parse(star.Tag.ToString()) <= Value)
                    VisualStateManager.GoToState(star, "PointerOver", true);
                else
                    VisualStateManager.GoToState(star, "Normal", true);
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Value = int.Parse(((ToggleButton)sender).Tag.ToString());
            foreach (var star in _stars)
            {
                star.IsChecked = int.Parse(star.Tag.ToString()) <= Value;
            }
        }
    }
}
