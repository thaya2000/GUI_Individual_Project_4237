using Desktop_Application.Models;
using Desktop_Application.ViewModels;
using Desktop_Application.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Desktop_Application.Views
{
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
        {
            InitializeComponent();
            DataContext = new StaffWindowVM();
        }

        //private void addstudent_close_Click(object sender, RoutedEventArgs e)
        //{
        //    var staffWindow = new StaffWindow();
        //    staffWindow.Show();
        //    this.Close();
        //}

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void textFirstNme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFirstNme.Focus();
        }

        private void txtFirstNme_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstNme.Text) && txtFirstNme.Text.Length > 0)
            {
                textFirstNme.Visibility = Visibility.Collapsed;
            }
            else
            {
                textFirstNme.Visibility = Visibility.Visible;
            }
        }

        private void textLastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLastName.Focus();
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length > 0)
            {
                textLastName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textLastName.Visibility = Visibility.Visible;
            }
        }

        private void textDoB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDoB.Focus();
        }

        private void txtDoB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDoB.Text) && txtDoB.Text.Length > 0)
            {
                textDoB.Visibility = Visibility.Collapsed;
            }
            else
            {
                textDoB.Visibility = Visibility.Visible;
            }
        }

        private void textGPA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtGPA.Focus();
        }

        private void txtGPA_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGPA.Text) && txtGPA.Text.Length > 0)
            {
                textGPA.Visibility = Visibility.Collapsed;
            }
            else
            {
                textGPA.Visibility = Visibility.Visible;
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            StaffWindowVM viewModel = new StaffWindowVM();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected image file path
                string imagePath = openFileDialog.FileName;

                // Set the ImagePath property in the view model
                ((StaffWindowVM)DataContext).ImagePath = imagePath;
                ((StaffWindowVM)DataContext).ImageData = viewModel.ReadImageDataFromFile(imagePath);
            }
        }


        private void AddStudent_Create_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StaffWindowVM viewModel)
            {
                viewModel.InsertPersonCommand.Execute(this);
            }

            var staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
