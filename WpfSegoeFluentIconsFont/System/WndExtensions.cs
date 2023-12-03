//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.Windows;

namespace System
{
    public static class WndExtensions
    {
        public static void SetAndRestoreBounds(this Window form, Rect bounds)
        {
            Size screenSize = new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
            Rect result = bounds;
            Size minSize = new Size(form.MinWidth, form.MinHeight);

            if (result.X < 0) { result.X = 0; }
            if (result.Y < 0) { result.Y = 0; }
            if (result.Right > screenSize.Width)
            {
                result.X = screenSize.Width - result.Width;
                if (result.X < 0) { result.X = 0; }
            }
            if (result.Bottom > screenSize.Height)
            {
                result.Y = screenSize.Height - result.Height;
                if (result.Y < 0) { result.Y = 0; }
            }

            if (result.Width <= 0)
            {
                result.Width = form.Width; // default
            }
            if (result.Height <= 0)
            {
                result.Height = form.Height; // default
            }

            result.Width = result.Width.Range(minSize.Width, screenSize.Width);
            result.Height = result.Height.Range(minSize.Height, screenSize.Height);

            if (result.X < 0)
            {
                result.X = (screenSize.Width - result.Width) / 2; // Center of the screen
            }
            if (result.Y < 0)
            {
                result.Y = (screenSize.Height - result.Height) / 2; // Center of the screen
            }

            form.Left = result.X;
            form.Top = result.Y;
            form.Width = result.Width;
            form.Height = result.Height;
        }

        public static void SetAndRestoreBounds(this Window form, Point location, Size size)
        {
            SetAndRestoreBounds(form, new Rect(location, size));
        }

        public static void SetAndRestoreBounds(this Window form, System.Drawing.Point location, System.Drawing.Size size)
        {
            SetAndRestoreBounds(form, new Rect(location.X, location.Y, size.Width, size.Height));
        }

        public static void ForceShow(this Window form)
        {
            if (!form.IsVisible)
            {
                form.Show();
            }

            if (form.WindowState == WindowState.Minimized)
            {
                form.WindowState = WindowState.Normal;
            }

            form.Activate();
            form.Topmost = true;  // important
            form.Topmost = false; // important
            form.Focus();         // important
        }
    }
}
