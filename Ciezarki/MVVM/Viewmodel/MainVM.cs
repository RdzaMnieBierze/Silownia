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
        public MainVM(Core.NavigationService navigationService)
        {

            _navigationService = navigationService;
            _navigationService.SetNavigator(vm => CurrentVM = vm);

            BaseVM addVM = new AddProgressLogVM();

            NavigateAddWorkout = new RelayCommand(_ => _navigationService.NavigateTo(addVM), _ => true);

            CurrentVM = addVM;


       
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
