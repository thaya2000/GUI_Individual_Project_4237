using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Desktop_Application.Data;
using Desktop_Application.Models;
using Desktop_Application.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;


namespace Desktop_Application.ViewModels
{
    public partial class StaffWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string firstName;

        [ObservableProperty]
        public string lastName;

        [ObservableProperty]
        public DateOnly dateOfBirth;

        [ObservableProperty]
        public double? gpa;

        [ObservableProperty]
        public byte[] imageData;


        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        [ObservableProperty]
        public ObservableCollection<Student> schoolStudents;

        [RelayCommand]
        public void InsertPerson()
        {
            Student p = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Gpa = Gpa,
                ImageData = ImageData
            };
            //ImageData = ReadImageDataFromFile(ImagePath)
            //// Read the image data from the selected image file
            //

            // Set the ImageData property of the student entity
            //p.ImageData = imageData;

            using (var db = new DataBaseContext())
            {
                db.Students.Add(p);
                db.SaveChanges();
            }

            LoadPerson();
        }


        public byte[] ReadImageDataFromFile(string imagePath)
        {
            byte[] imageData = null;

            try
            {
                imageData = File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during reading the image file
                Console.WriteLine("Error reading image file: " + ex.Message);
            }

            return imageData;
        }


        //[RelayCommand]
        //public void SelectImage()
        //{
        //    // Implement the logic to select an image and set the ImagePath property
        //    // You can use OpenFileDialog or any other method to allow the user to choose an image file
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string imagePath = openFileDialog.FileName;
        //        // Set the ImagePath property with the selected image path
        //        ImagePath = imagePath;
        //    }
        //}



        private Student editedStudent;
        private EditStudentWindow editWindow;

        [RelayCommand]
        public void Edit(object parameter)
        {
            if (parameter is Student student)
            {
                editedStudent = student;
                editWindow = new EditStudentWindow(editedStudent);

                Window? staffWindow = Application.Current.Windows.OfType<StaffWindow>().FirstOrDefault();
                if (staffWindow != null)
                {
                    staffWindow.Visibility = Visibility.Hidden;
                }

                editWindow.ShowDialog();

                // After the edit window is closed, check if the student was modified
                if (editWindow.IsModified)
                {
                    // Update the student in the database and reload the students
                    using (var db = new DataBaseContext())
                    {
                        var originalStudent = db.Students.FirstOrDefault(s => s.Id == editedStudent.Id);
                        if (originalStudent != null)
                        {
                            // Update the properties of the original student with the modified values
                            originalStudent.FirstName = editedStudent.FirstName;
                            originalStudent.LastName = editedStudent.LastName;
                            originalStudent.DateOfBirth = editedStudent.DateOfBirth;
                            originalStudent.Gpa = editedStudent.Gpa;
                            originalStudent.ImageData = editedStudent.ImageData;

                            db.SaveChanges();
                        }
                    }
                }
                // Reload the students
                LoadPerson();

                if (staffWindow != null)
                {
                    staffWindow.Visibility = Visibility.Visible;
                }
            }
        }


        [RelayCommand]
        public void DeletePerson(Student student)
        {
            DeleteStudentWindow deleteWindow = new DeleteStudentWindow(student);   
            deleteWindow.ShowDialog();

            if (student == null)
            {
                return;
            }
            else if (deleteWindow.IsConfirmDelete)
            {
                using (var db = new DataBaseContext())
                {
                    var originalPerson = db.Students.FirstOrDefault(p => p.Id == student.Id);
                    if (originalPerson != null)
                    {
                        db.Students.Remove(originalPerson);
                        db.SaveChanges();
                    }
                }
                SchoolStudents.Remove(student);
                LoadPerson();
            }
            
        }



        public void LoadPerson()
        {
            using (var db = new DataBaseContext())
            {
                var list = db.Students.OrderByDescending(p => p.Gpa ).ToList();
                SchoolStudents.Clear(); // Clear the collection
                foreach (var student in list)
                {
                    SchoolStudents.Add(student); // Add the loaded students
                }
            }
        }


        public StaffWindowVM()
        {
            SchoolStudents = new ObservableCollection<Student>(); // Initialize the collection
            LoadPerson();
        }

    }
}
