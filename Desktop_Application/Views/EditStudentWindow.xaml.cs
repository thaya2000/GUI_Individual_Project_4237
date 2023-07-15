using Desktop_Application.Models;
using Desktop_Application.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
  
    public partial class EditStudentWindow : Window
    {
        public EditStudentWindow(Student student)
        {
            InitializeComponent();
            DataContext = student;
        }

        public bool IsModified { get; private set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            IsModified = true;
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {
            StaffWindowVM viewModel = new StaffWindowVM();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                // Update the ImageData property of the Student object with the new image

                ((Student)DataContext).ImageData = viewModel.ReadImageDataFromFile(imagePath);

                // Update the image preview in the UI
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(((Student)DataContext).ImageData);
                bitmapImage.EndInit();
                ImageDataPreview.Source = bitmapImage;


            }

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //var staffWindow = new StaffWindow();
            //staffWindow.Show();
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

    }
}
