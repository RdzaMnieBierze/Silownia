using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Ciezarki.MVVM.Model;
using Ciezarki.Core;
using System.Windows.Input;
using SQLitePCL;


namespace Ciezarki.MVVM.Viewmodel
{
    class MainVM : BaseVM
    {
        public RelayCommand exit => new RelayCommand(execute => ExitButton());
        private void ExitButton()
        {
            Application.Current.Shutdown();
        }
        private readonly Core.NavigationService _navigationService;
        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get => _currentVM;
            set
            {
                _currentVM = value;
                OnPropertyChanged(nameof(CurrentVM));
            }
        }

        public ICommand NavigateAddWorkout { get; }
        public ICommand NavigateAddExercise { get; }
        public MainVM(Core.NavigationService navigationService)
        {
            
            _navigationService = navigationService;
            _navigationService.SetNavigator(vm => CurrentVM = vm);

            BaseVM addWorkoutVM = new AddWorkoutVM();
            BaseVM addExerciseVM = new AddExerciseVM();

            NavigateAddWorkout = new RelayCommand(_ => _navigationService.NavigateTo(addWorkoutVM), _ => true);
            NavigateAddExercise = new RelayCommand(_ => _navigationService.NavigateTo(addExerciseVM), _ => true);

            CurrentVM = addWorkoutVM;

            using var dbContext = new AppDbContext();
            MessageBox.Show("Database initialized successfully!");
            dbContext.Database.EnsureCreated();
            dbContext.SaveChanges();
        }
    }
}
