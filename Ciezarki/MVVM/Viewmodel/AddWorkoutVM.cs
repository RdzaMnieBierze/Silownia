using Ciezarki.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Ciezarki.MVVM.Viewmodel
{
    class AddWorkoutVM : BaseVM
    {

        private Exercise _selectedExercise;
        public Exercise SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                if (_selectedExercise != value)
                {
                    _selectedExercise = value;
                    OnPropertyChanged(nameof(SelectedExercise));
                }
            }

        }


        public ObservableCollection<Exercise> Exercises { get; set; }

        private WorkoutExercises _selectedWorkoutExercise;

        public WorkoutExercises SelectedWorkoutExercise
        {
            set { 
                _selectedWorkoutExercise = value;
                OnPropertyChanged(nameof(SelectedWorkoutExercise));

            }
            get { return _selectedWorkoutExercise;
            }
        }

        public ObservableCollection<WorkoutExercises> WorkoutsExercisess { get; set; }


        private AppDbContext _dbContext;

        public AddWorkoutVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated();

            var exerc = from exercise in _dbContext.Exercises
                            select exercise;

            foreach (var exercise in exerc)
            {
                Exercises.Add(exercise);
            }
            MessageBox.Show(Exercises.Count().ToString());
            

            

            //Workout w = new Workout();
            //Exercise e = new Exercise();
            //AddWorkout(w);
            //AddExercise(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();
        }

        public void AddExercise(Exercise exercise)
        {
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

        }
    }
}


