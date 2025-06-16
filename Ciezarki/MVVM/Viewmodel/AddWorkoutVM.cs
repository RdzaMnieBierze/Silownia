using Ciezarki.MVVM.Model;
using Ciezarki.MVVM.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Ciezarki.MVVM.Viewmodel
{
    class AddWorkoutVM : BaseVM
    {

        private Workout _workout;
        public ICommand AddExerciseCommand { get; }
        public ICommand LoadExerciseCommand { get; }
        public ICommand EditExerciseCommand { get; }
        public ICommand DeleteExerciseCommand { get; }

       

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

        private WorkoutExercises _editWorkoutExercise;
        public WorkoutExercises EditWorkoutExercise
        {
            set { _editWorkoutExercise = value;
                OnPropertyChanged(nameof(EditWorkoutExercise));
            }
            get { return _editWorkoutExercise; }    
        }
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

        
        public string? Notes
        {
            get => _workout.Notes.ToString();
            set
            {
                _workout.Notes = value;
                OnPropertyChanged(nameof(Notes));

            }
        }

        public string? Reps
        {
            get => _editWorkoutExercise.Reps_exercise.ToString();
            set
            {

                if(int.TryParse(value, out int reps))
                {
                    _editWorkoutExercise.Reps_exercise = reps;
                    OnPropertyChanged(nameof(Reps));
                }

            }
        }
        public string? Sets
        {
            get => _editWorkoutExercise.Sets_exercise.ToString();
            set
            {

                if (int.TryParse(value, out int sets))
                {
                    _editWorkoutExercise.Sets_exercise = sets;
                    OnPropertyChanged(nameof(Sets));
                }

            }
        }
        public string? Load
        {
            get => _editWorkoutExercise.Load_exercise.ToString();
            set
            {
                if(double.TryParse(value,out double load))
                {
                    _editWorkoutExercise.Load_exercise = load;
                    OnPropertyChanged(nameof(Load));
                }
            }
        }
        public string? Rest_time
        {
            get => _editWorkoutExercise.Resttime_exercise.ToString();
            set
            {

                if (int.TryParse(value, out int rest_time))
                {
                    _editWorkoutExercise.Resttime_exercise = rest_time;
                    OnPropertyChanged(nameof(Rest_time));
                }

            }
        }
        public AddWorkoutVM()
        {
            WorkoutsExercisess = new ObservableCollection<WorkoutExercises>();
            _editWorkoutExercise = new WorkoutExercises();
            _selectedWorkoutExercise = new WorkoutExercises();
            _selectedExercise = new Exercise();
            _workout = new Workout();
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



            LoadExerciseCommand = new RelayCommand(_ => LoadExercise(), _ => true);
            EditExerciseCommand = new RelayCommand(_ => EditExercise(), _ => true);
            DeleteExerciseCommand = new RelayCommand(_ => DeleteExercise(), _ => true);
            AddExerciseCommand = new RelayCommand(_ => AddExercise(), _ => true);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void DeleteExercise() {
            WorkoutsExercisess.Remove(SelectedWorkoutExercise);
        }
        private void EditExercise() {
        
        }
        private void LoadExercise() {
            MessageBox.Show("SSSSs");
            
            Load = SelectedWorkoutExercise.Load_exercise.ToString();
            
            Reps = SelectedWorkoutExercise.Reps_exercise.ToString();
            Sets = SelectedWorkoutExercise.Sets_exercise.ToString();
            Rest_time = SelectedWorkoutExercise.Resttime_exercise.ToString();
            SelectedExercise = SelectedWorkoutExercise.Exercise;

        }
        public void AddExercise()
        {
            if(SelectedExercise.Id  != null)
            {
                EditWorkoutExercise.Id_exercise = SelectedExercise.Id;
                EditWorkoutExercise.Exercise = SelectedExercise;
                WorkoutsExercisess.Add(EditWorkoutExercise);
            }

        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();
        }


    }
}


