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

        public List<string> SortList { get; } = new List<string>
        {
            "Od najstarszego",
            "Od najnowszego",
            "Alfabetycznie",
            "Niealfabetycznie"
        };
        private string _sortBy;
        public string SortBy
        {
            get => _sortBy;
            set
            {
                _sortBy = value;
                OnPropertyChanged(nameof(SortBy));
                FilterAndSortWorkouts();
            }
        }
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }


        private string _dateBefore;
        public string DateBefore
        {
            get => _dateBefore;
            set
            {
                _dateBefore = value;
                OnPropertyChanged(nameof(DateBefore));
                FilterAndSortWorkouts();
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
                FilterAndSortWorkouts();
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
                FilterAndSortWorkouts();
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
        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
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
            LoadSelectedWorkout = new RelayCommand(_ => LoadSpecificWorkout(), _ => SelectedWorkout != null);
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

            Notes = _dbContext.Workouts
                            .Where(wor => wor.Id == selectedId)
                            .Select(wor => wor.Notes)
                            .FirstOrDefault();
        }

        private void FilterAndSortWorkouts()
        {
            var query = _dbContext.UserWorkouts
                .Include(uw => uw.Workout)
                .Where(uw => uw.Id_user == DbData.UserId);

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                query = query.Where(uw => uw.Workout.Name.Contains(NameSearch));
            }

            if (!string.IsNullOrWhiteSpace(DateAfter))
            {
                if (DateTime.TryParse(DateAfter, out DateTime afterDate))
                {
                    query = query.Where(uw => uw.Plan_date >= afterDate);
                }
                else
                {
                    MessageBox.Show("Nieprawidłowy format daty 'Od'. Użyj formatu: dd.mm.rrrr", "Błąd daty", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(DateBefore))
            {
                if (DateTime.TryParse(DateBefore, out DateTime beforeDate))
                {
                    query = query.Where(uw => uw.Plan_date <= beforeDate);
                }
                else
                {
                    MessageBox.Show("Nieprawidłowy format daty 'Do'. Użyj formatu: dd.mm.rrrr", "Błąd daty", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Sortowanie
            switch (SortBy)
            {
                case "Od najstarszego":
                    query = query.OrderBy(uw => uw.Plan_date);
                    break;
                case "Od najnowszego":
                    query = query.OrderByDescending(uw => uw.Plan_date);
                    break;
                case "Alfabetycznie":
                    query = query.OrderBy(uw => uw.Workout.Name.ToLower());
                    break;
                case "Niealfabetycznie":
                    query = query.OrderByDescending(uw => uw.Workout.Name.ToLower());
                    break;
                default:
                    query = query.OrderByDescending(uw => uw.Plan_date);
                    break;
            }


            ListOfWorkouts = query.ToList();
            OnPropertyChanged(nameof(ListOfWorkouts));
        }



    }
}
