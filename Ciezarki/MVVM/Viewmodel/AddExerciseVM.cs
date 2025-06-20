using Ciezarki.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ciezarki.MVVM.Viewmodel
{
    class AddExerciseVM : BaseVM
    {
        public AddExerciseVM()
        {
            _exercise = new Exercise();
        }

        private Exercise _exercise;

        public String Name
        {
            get => _exercise.Name;
            set
            {
                _exercise.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public String Description
        {
            get => _exercise.Description;
            set
            {
                _exercise.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

     public string Muscle
        {
            get => _exercise.Muscle;
            set
            {
                _exercise.Muscle = value;
                OnPropertyChanged(nameof(Muscle));
            }
        }

        public ICommand SaveExerciseCommand => new RelayCommand(_=>Save());

        public void Save()
        {
            if (string.IsNullOrEmpty(Muscle) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Należy wypełnić wszystkie pola przed dodaniem ćwiczenia!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (var context = new AppDbContext())
                {
                    if (context.Exercises.Any(e => e.Name == Name))
                    {
                        MessageBox.Show("Ćwiczenie o tej nazwie już istnieje!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    context.Exercises.Add(_exercise);
                    context.SaveChanges();
                }
                MessageBox.Show("Ćwiczenie zostało dodane pomyślnie!", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
        }
    }
}
