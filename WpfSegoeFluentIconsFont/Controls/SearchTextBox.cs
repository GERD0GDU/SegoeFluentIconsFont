//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfSegoeFluentIconsFont.Controls
{
    public class SearchTextBox : TextBox
    {
        private Button m_clearButton = null;

        static SearchTextBox()
        {
            // Initialize as lookless control
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
        }

        public SearchTextBox()
        {
            Resources = new ResourceDictionary() { Source = new System.Uri($"pack://application:,,,/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name};component/Styles/{GetType().Name}Styles.xaml", System.UriKind.RelativeOrAbsolute) };
        }

        public override void OnApplyTemplate()
        {
            m_clearButton = Template.FindName("PART_ClearButton", this) as System.Windows.Controls.Button;
            if (m_clearButton != null)
            {
                m_clearButton.Click += new RoutedEventHandler(ClearButton_Click);
                m_clearButton.PreviewMouseDown += (s, e) =>
                {
                    Focus();
                };
            }

            base.OnApplyTemplate();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Text = null;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if ((e.Key == Key.Enter) && !AcceptsReturn)
            {
                e.Handled = true;
                if (Command != null && Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
            else if (e.Key == Key.Escape)
            {
                Text = null;
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            HasText = !string.IsNullOrEmpty(Text) && (Text.Length > 0);

            if (!HasText)
            {
                if (Command != null && Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        #region Properties

        #region HasText

        public static readonly DependencyProperty HasTextProperty =
           DependencyProperty.Register("HasText",
           typeof(bool),
           typeof(SearchTextBox),
           new FrameworkPropertyMetadata(false));

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextProperty, value); }
        }

        #endregion // HasText

        #region LabelText

        public static readonly DependencyProperty LabelTextProperty =
           DependencyProperty.Register("LabelText",
           typeof(string),
           typeof(SearchTextBox),
           new FrameworkPropertyMetadata("Search"));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        #endregion // LabelText

        #region LabelTextColor

        public static readonly DependencyProperty LabelTextColorProperty =
           DependencyProperty.Register("LabelTextColor",
           typeof(System.Windows.Media.Brush),
           typeof(SearchTextBox),
           new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Gray));

        public System.Windows.Media.Brush LabelTextColor
        {
            get { return (System.Windows.Media.Brush)GetValue(LabelTextColorProperty); }
            set { SetValue(LabelTextColorProperty, value); }
        }

        #endregion // LabelTextColor

        #region CommandParameter

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter",
            typeof(object),
            typeof(SearchTextBox),
            new FrameworkPropertyMetadata(null));

        [Bindable(true)]
        [Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion // CommandParameter

        #region Command

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
            typeof(ICommand),
            typeof(SearchTextBox),
            new FrameworkPropertyMetadata(null));

        [Bindable(true)]
        [Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        #endregion // Command

        #endregion // Properties
    }
}
