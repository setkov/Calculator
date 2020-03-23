using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _ = textBlockIn.Focus();
        }

        private void Root_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    Button_Clear_Click(sender, e);
                    break;

                case Key.Back:
                    Button_Backspace_Click(sender, e);
                    break;

                case Key.Enter:
                    Button_Enter_Click(sender, e);
                    break;

                default:
                    break;
            }
        }

        private void Root_TextInput(object sender, TextCompositionEventArgs e)
        {
            var text = e.Text;
            if (".1234567890()/*-+".Contains(text))
            {
                textBlockIn.Text += text;
            }
        }

        private void Pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Topmost = !this.Topmost;

            var toolTip = new ToolTip();
            if (this.Topmost)
            {
                imagePin.Source = new BitmapImage(new Uri("Resources/icon-pined.png", UriKind.Relative));
                toolTip.Content = "UnPin from top window";
            }
            else
            {
                imagePin.Source = new BitmapImage(new Uri("Resources/icon-unpined.png", UriKind.Relative));
                toolTip.Content = "Pin as top window";
            }
            imagePin.ToolTip = toolTip;
        }

        private void Info_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string msg =    "Calculator | v1.0.0" + Environment.NewLine +
                            "© 2020 Setkov" + Environment.NewLine + 
                            "All Rights Reserved" + Environment.NewLine +
                            "For more information, visit: https://github.com/setkov/Calculator";
            if (MessageBox.Show(msg, string.Empty, MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                var targetURL = "https://github.com/setkov/Calculator";
                var psi = new ProcessStartInfo
                {
                    FileName = targetURL,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = (string)((Button)e.OriginalSource).Content;
            textBlockIn.Text += text;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            textBlockIn.Text = string.Empty;
            textBlockOut.Text = string.Empty;
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockIn.Text.Length > 0)
            {
                textBlockIn.Text = textBlockIn.Text.Remove(textBlockIn.Text.Length - 1);
            }
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockIn.Text.Length > 0)
            {
                var computer = new Computer(textBlockIn.Text);
                if (computer.Parser.Error != string.Empty)
                {
                    textBlockOut.Foreground = Brushes.Red;
                    textBlockOut.Text = textBlockIn.Text + " " + computer.Parser.Error;
                    textBlockIn.Text = string.Empty;
                }
                else
                {
                    textBlockOut.Foreground = Brushes.Black;
                    textBlockOut.Text = textBlockIn.Text + "=";
                    textBlockIn.Text = computer.Calc().ToString().Replace(',', '.');
                }
            }
        }
    }

}
