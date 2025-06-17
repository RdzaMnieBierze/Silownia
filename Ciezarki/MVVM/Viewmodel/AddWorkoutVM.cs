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
        private UserWorkout _editUserWorkout;
        public UserWorkout EditUserWorkout
        {
            set {  _editUserWorkout = value; }
            get { return _editUserWorkout; }
        }
        private Workout _workout;
        public ICommand AddExerciseCommand { get; }
        public ICommand LoadExerciseCommand { get; }
        public ICommand EditExerciseCommand { get; }
        public ICommand DeleteExerciseCommand { get; }
        public ICommand SaveWorkoutCommand { get; }

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
            get => _selectedWorkoutExercise;
        }

        public ObservableCollection<WorkoutExercises> WorkoutsExercisess { get; set; }


        private AppDbContext _dbContext;

        public DateTime Date {  get; set; } 
        public string? Name
        {
            get;
            set;
        }
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

        public int SelectedWorkoutExercisesIndex { get; set; }
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
            Exercises = new ObservableCollection<Exercise>();
            _editUserWorkout = new UserWorkout();
            _editWorkoutExercise = new WorkoutExercises();
            _selectedWorkoutExercise = new WorkoutExercises();
            _selectedExercise = new Exercise();
            _workout = new Workout();
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated();

            var exerc = from exercise in _dbContext.Exercises
                            select exercise;

            foreach (var exercise in exerc)
            {
                Exercises.Add(exercise);
            }
            //MessageBox.Show(Exercises.Count().ToString());

            Date = DateTime.Now;

            LoadExerciseCommand = new RelayCommand(_ => LoadExercise(), _ => true);
            EditExerciseCommand = new RelayCommand(_ => EditExercise(), _ => true);
            DeleteExerciseCommand = new RelayCommand(_ => DeleteExercise(), _ => true);
            AddExerciseCommand = new RelayCommand(_ => AddExercise(), _ => true);
            SaveWorkoutCommand = new RelayCommand(_ => SaveWorkout(), _ => true);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void DeleteExercise() {
            OnPropertyChanged(nameof(WorkoutsExercisess));
            WorkoutsExercisess.RemoveAt(SelectedWorkoutExercisesIndex);
        }

        private void SaveWorkout()
        {
<<<<<<< Updated upstream

=======
            _workout.Create_date = DateTime.Now;
            _workout.Name = Name;
            _workout.Notes = Notes;
            _dbContext.Workouts.Add(_workout);
            _dbContext.SaveChanges();
            _editUserWorkout.Name = Name;
            _editUserWorkout.Id_user = DbData.UserId;
            _editUserWorkout.Id_workout = _workout.Id;
            _editUserWorkout.Create_date = DateTime.Now;
            _editUserWorkout.Plan_date = Date;
            _dbContext.UserWorkouts.Add( _editUserWorkout );
            _dbContext.SaveChanges();

            for (int i = 0; i < WorkoutsExercisess.Count; i++)
            {
                var newWorkoutExercise = WorkoutsExercisess[i] as WorkoutExercises;
                newWorkoutExercise.Id_workout = EditUserWorkout.Id;
                _dbContext.Add( newWorkoutExercise );
                MessageBox.Show(newWorkoutExercise.ToString());
            }
            _dbContext.SaveChanges();

            MessageBox.Show(_editUserWorkout.ToString());
            ClearAll();
        }

        private void ClearAll()
        {
            _workout = new Workout();
            _editUserWorkout = new UserWorkout();
            _editWorkoutExercise = new WorkoutExercises();
            _selectedWorkoutExercise = null;
            _selectedExercise = null;
            Name = "";
            Notes = "";
            Date = DateTime.Now;
            WorkoutsExercisess.Clear();
>>>>>>> Stashed changes
        }
        private void EditExercise() {
            
            if (SelectedWorkoutExercisesIndex >= 0)
            {
                WorkoutsExercisess.Insert(SelectedWorkoutExercisesIndex, EditWorkoutExercise);
                WorkoutsExercisess.RemoveAt(SelectedWorkoutExercisesIndex);
            }
        }
        private void LoadExercise() {
            MessageBox.Show("SSSSs");
            EditWorkoutExercise = new WorkoutExercises();
            EditWorkoutExercise.Load_exercise = SelectedWorkoutExercise.Load_exercise;
            EditWorkoutExercise.Reps_exercise = SelectedWorkoutExercise.Reps_exercise;
            EditWorkoutExercise.Sets_exercise = SelectedWorkoutExercise.Sets_exercise;
            EditWorkoutExercise.Resttime_exercise = SelectedWorkoutExercise.Resttime_exercise;
            OnPropertyChanged(nameof(EditWorkoutExercise));
            SelectedExercise = SelectedWorkoutExercise.Exercise;


        }
        public void AddExercise()
        {
            if(SelectedExercise.Id  != null)
            {
                var newExercise = new WorkoutExercises
                {
                    Id_exercise = SelectedExercise.Id,
                    Exercise = SelectedExercise,
                    Load_exercise = EditWorkoutExercise.Load_exercise,
                    Reps_exercise = EditWorkoutExercise.Reps_exercise,
                    Sets_exercise = EditWorkoutExercise.Sets_exercise,
                    Resttime_exercise = EditWorkoutExercise.Resttime_exercise
                };

                WorkoutsExercisess.Add(newExercise);
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


