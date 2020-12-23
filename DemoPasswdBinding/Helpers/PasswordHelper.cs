using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DemoPasswdBinding.Helpers
{
    public static class PasswordHelper
    {
        #region IsUpdating DependencyProperty
        private static readonly DependencyProperty IsUpdatingProperty;
        private static bool GetIsUpdating(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsUpdatingProperty, value);
        }
        #endregion

        #region IsAttached DependencyProperty
        private static readonly DependencyProperty IsAttachedProperty;

        public static bool GetIsAttached(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsAttachedProperty);
        }

        public static void SetIsAttached(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsAttachedProperty, value);
        }

        private static void OnIsAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is PasswordBox passwordBox)
            {
                if(e.OldValue is bool oldValue && oldValue && e.NewValue is bool newValue && !newValue)
                {
                    passwordBox.PasswordChanged -= PasswordChanged;
                }

                if (e.NewValue is bool b && b)
                {
                    passwordBox.PasswordChanged += PasswordChanged;
                }
            }
        }
        #endregion

        #region Password DependencyProperty
        private static readonly DependencyProperty PasswordProperty;
        public static string GetPassword(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if (!GetIsUpdating(passwordBox))
                {
                    passwordBox.PasswordChanged -= PasswordChanged;
                    passwordBox.Password = (string)e.NewValue;
                    passwordBox.PasswordChanged += PasswordChanged;
                }
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if (!GetIsUpdating(passwordBox))
                {
                    SetIsUpdating(passwordBox, true);
                    SetPassword(passwordBox, passwordBox.Password);
                    SetIsUpdating(passwordBox, false);
                }
            }
        } 
        #endregion


        static PasswordHelper()
        {
            PasswordProperty = DependencyProperty.RegisterAttached("Password",
                                                                   typeof(string),
                                                                   typeof(PasswordHelper),
                                                                   new FrameworkPropertyMetadata(defaultValue: string.Empty, flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnPasswordChanged));
            IsAttachedProperty = DependencyProperty.RegisterAttached("IsAttached", 
                                                                     typeof(bool), 
                                                                     typeof(PasswordHelper),
                                                                     new PropertyMetadata(defaultValue:false, propertyChangedCallback:OnIsAttachedChanged));
            IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", 
                                                                     typeof(bool), 
                                                                     typeof(PasswordHelper), 
                                                                     new PropertyMetadata(defaultValue:false));
        }

        
    }
}
