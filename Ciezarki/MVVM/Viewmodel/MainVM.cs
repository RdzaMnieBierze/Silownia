using Ciezarki.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;


namespace Ciezarki.MVVM.Viewmodel
{
    class MainVM : BaseVM
    {

        public RelayCommand exit => new RelayCommand(execute => ExitButton());
        private void ExitButton()
        {
            using var dbContext = new AppDbContext();
            dbContext.Database.ExecuteSqlRaw("PRAGMA wal_checkpoint(FULL);");

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

        public ICommand NavigateAddProgressLog { get; }
        public MainVM(Core.NavigationService navigationService)
        {

            _navigationService = navigationService;
            _navigationService.SetNavigator(vm => CurrentVM = vm);

            BaseVM addWorkoutVM = new AddWorkoutVM();
            BaseVM addExerciseVM = new AddExerciseVM();
            BaseVM addProgressLogVM = new AddProgressLogVM();

            NavigateAddWorkout = new RelayCommand(_ => _navigationService.NavigateTo(addWorkoutVM), _ => true);
            NavigateAddExercise = new RelayCommand(_ => _navigationService.NavigateTo(addExerciseVM), _ => true);
            NavigateAddProgressLog = new RelayCommand(_ => _navigationService.NavigateTo(addProgressLogVM), _ => true);

            CurrentVM = addWorkoutVM;


       
            //MessageBox.Show("Database initialized successfully!");
            

            //using (var context = new AppDbContext())
            //{
            //    context.Database.EnsureCreated();
            //    var u = new User();
            //    u.Username = "Antek";
            //    u.Password = "1234";
            //    u.Email = "antel@polsl.pl";



            //    context.Users.Add(u);   // dodaj do kontekstu
            //    context.SaveChanges();        // zapis do bazy
            //}



          

        }
    }
}
