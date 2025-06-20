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
        public ICommand ClearWorkoutCommand { get; }

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
            get => _editWorkoutExercise;
            set
            {
                _editWorkoutExercise = value;
                OnPropertyChanged(nameof(EditWorkoutExercise));
                OnPropertyChanged(nameof(Load));
                OnPropertyChanged(nameof(Reps));
                OnPropertyChanged(nameof(Sets));
                OnPropertyChanged(nameof(Rest_time));
            }
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
        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }


        private string? _notes;
        public string? Notes
        {
            get => _notes;
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
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
            ClearWorkoutCommand  = new RelayCommand(_ => ClearAll(), _ => true);
        }


        private void DeleteExercise() {
            OnPropertyChanged(nameof(WorkoutsExercisess));
            WorkoutsExercisess.RemoveAt(SelectedWorkoutExercisesIndex);
        }


        private void SaveWorkout()
        {
            if (Name != null & WorkoutsExercisess.Count > 0)
            {
                _workout.Create_date = DateTime.Now;
                _workout.Name = Name;
                _workout.Notes = Notes;
                var _addworkout = new Workout();
                _addworkout.Name = _workout.Name;
                _addworkout.Notes = _workout.Notes;
                _dbContext.Workouts.Add(_addworkout);
                _dbContext.SaveChanges();

                _editUserWorkout.Name = Name;
                _editUserWorkout.Id_user = DbData.UserId;
                _editUserWorkout.Id_workout = _addworkout.Id;
                _editUserWorkout.Create_date = DateTime.Now;
                _editUserWorkout.Plan_date = Date;
                _dbContext.UserWorkouts.Add(_editUserWorkout);
                _dbContext.SaveChanges();

                for (int i = 0; i < WorkoutsExercisess.Count; i++)
                {
                    var newWorkoutExercise = WorkoutsExercisess[i] as WorkoutExercises;
                    newWorkoutExercise.Id_workout = EditUserWorkout.Id;
                    _dbContext.Add(newWorkoutExercise);
                }
                _dbContext.SaveChanges();
                MessageBox.Show("Dodano trening " + Name + "!");
                ClearAll();
            }
            else MessageBox.Show("Proszę wypełnić nazwę treningu i zaznaczyć przynamniej 1 ćwiczenie");

        }

        private void ClearAll()
        {
            _workout = new Workout();
            _editUserWorkout = new UserWorkout();
            _editWorkoutExercise = new WorkoutExercises();
            _selectedWorkoutExercise = null;
            SelectedExercise = null;
            Date = DateTime.Now;
            WorkoutsExercisess.Clear();
            Name = null;
            OnPropertyChanged(nameof(Name));
            Notes = null;
            OnPropertyChanged(nameof(Notes));

        }
        private void EditExercise() {
            
            if (SelectedWorkoutExercisesIndex >= 0)
            {
                WorkoutsExercisess.Insert(SelectedWorkoutExercisesIndex, EditWorkoutExercise);
                WorkoutsExercisess.RemoveAt(SelectedWorkoutExercisesIndex);
            }
        }
        private void LoadExercise() {

            if (SelectedWorkoutExercise == null)
            {
                MessageBox.Show("Nie wybrano ćwiczenia.");
                return;
            }

            // Nowy obiekt z danymi
            var loadedworkoutexercise = new WorkoutExercises
            {
                Load_exercise = SelectedWorkoutExercise.Load_exercise,
                Reps_exercise = SelectedWorkoutExercise.Reps_exercise,
                Sets_exercise = SelectedWorkoutExercise.Sets_exercise,
                Resttime_exercise = SelectedWorkoutExercise.Resttime_exercise,
                Exercise = SelectedWorkoutExercise.Exercise,
                Id_exercise = SelectedWorkoutExercise.Id_exercise
            };
            EditWorkoutExercise = loadedworkoutexercise;
            OnPropertyChanged(nameof(EditWorkoutExercise));
            // Ustawienie wybranego ćwiczenia dla ComboBoxa
            SelectedExercise = SelectedWorkoutExercise.Exercise;

        }
        public void AddExercise()
        {
            try
            {
                if (SelectedExercise.Id >0)
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
            catch { }
            
        }


        public void AddWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();

        }


    }
}


