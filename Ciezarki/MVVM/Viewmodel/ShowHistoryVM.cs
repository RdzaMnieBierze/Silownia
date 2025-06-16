using Ciezarki.MVVM.Model;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private AppDbContext _dbContext;
        public ShowHistoryVM()
        {
            _dbContext = new AppDbContext();
            _userWorkout = new UserWorkout();
            _workout = new Workout();

            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            ListOfWorkouts = _dbContext.UserWorkouts.Where(p => p.Id_user == DbData.UserId).ToList();
        }
    }
}
