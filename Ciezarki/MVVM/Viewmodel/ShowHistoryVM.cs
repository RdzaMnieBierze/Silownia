using Ciezarki.MVVM.Model;
using LiveCharts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ciezarki.MVVM.Viewmodel
{
    internal class ShowHistoryVM : BaseVM
    {
        private UserWorkout _userWorkout;
        private Workout _workout;

        public List<string> SortList { get; } = new List<string>{ "Rosnąco po dacie", "Malejącio po dacie", "Rosnąco po nazwie", "Malejąco po nazwie" };
        private string sortBy { get; set; }
        private string _dateBefore;
        public string DateBefore
        {
            get => _dateBefore;
            set
            {
                _dateBefore = value;
                OnPropertyChanged(nameof(DateBefore));
            }
        }
        private string _dateAfter;
        public string DateAfter
        {
            get => _dateAfter;
            set
            {
                _dateAfter = value;
                OnPropertyChanged(nameof(DateAfter));
            }
        }
        private string _nameSearch;
        public string NameSearch
        {
            get => _nameSearch;
            set
            {
                _nameSearch = value;
                OnPropertyChanged(nameof(NameSearch));
            }
        }
        public List<UserWorkout>? ListOfWorkouts { get; set; }
        private List<WorkoutExercises>? _specificWorkoutExercises;
        public List<WorkoutExercises>? SpecificWorkoutExercises
        {
            get => _specificWorkoutExercises;
            set
            {
                _specificWorkoutExercises = value;
                OnPropertyChanged(nameof(SpecificWorkoutExercises));
            }
        }
        private ObservableCollection<WorkoutExerciseDTO> _workoutExercises;
        public ObservableCollection<WorkoutExerciseDTO> WorkoutExercises
        {
            get => _workoutExercises;
            set
            {
                _workoutExercises = value;
                OnPropertyChanged(nameof(WorkoutExercises));
            }
        }
        private UserWorkout _selectedWorkout;
        public UserWorkout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }

        private AppDbContext _dbContext;
        public ICommand LoadSelectedWorkout { get; }
        public ShowHistoryVM()
        {
            _dbContext = new AppDbContext();
            _userWorkout = new UserWorkout();
            _workout = new Workout();
            LoadSelectedWorkout = new RelayCommand(_ => LoadSpecificWorkout());
            _specificWorkoutExercises = null;

            LoadWorkouts();
        }
        private void LoadWorkouts()
        {
            ListOfWorkouts = _dbContext.UserWorkouts.Where(p => p.Id_user == DbData.UserId).ToList();
        }
        private void LoadSpecificWorkout()
        {
            int selectedId = SelectedWorkout.Id_workout;
            var query = from we in _dbContext.WorkoutExercises
                        join ex in _dbContext.Exercises on we.Id_exercise equals ex.Id
                        where we.Id_workout == selectedId
                        select new WorkoutExerciseDTO
                        {
                            Name = ex.Name,
                            Muscle = ex.Muscle,
                            Reps = we.Reps_exercise,
                            Sets = we.Sets_exercise,
                            Load = we.Load_exercise,
                            Resttime = we.Resttime_exercise
                        };

            var results = query.ToList();
            WorkoutExercises = new ObservableCollection<WorkoutExerciseDTO>(results);
        }
    }
}
