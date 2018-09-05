using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace CustomGestures
{

    public class TwoFigureTapGestureRecognizer
    {
        private List<uint> tapPointerIds = new List<uint>();
        private TimeSpan maxIntervalBetweenTaps = TimeSpan.FromMilliseconds(200);
        private const int MaxTapCount = 2;
        private DateTime tapStartTime;
        private bool isTwoFingerTap;

        public TwoFigureTapGestureRecognizer(UIElement element)
        {
            element.PointerPressed += Element_PointerPressed;
            element.PointerReleased += Element_PointerReleased;
        }

        private void Element_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;            
            tapPointerIds.Remove(e.Pointer.PointerId);
            if (tapPointerIds.Count == 0 && isTwoFingerTap)
            {
                // verify that taps are within allowed max interval to be a 2 finger tap
                var tapEndTime = DateTime.Now;
                var elapsedTime = (tapEndTime - tapStartTime);
                if (elapsedTime <= maxIntervalBetweenTaps)
                {
                    // is a two finger tap, raise gesture event

                }  
            }
        }

        private void Element_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            var pointerId = e.Pointer.PointerId;            
            tapPointerIds.Add(pointerId);

            if (tapPointerIds.Count == 1)
            {
                tapStartTime = DateTime.Now;
            }

            isTwoFingerTap = tapPointerIds.Count == MaxTapCount;

        }
            
    }
}
