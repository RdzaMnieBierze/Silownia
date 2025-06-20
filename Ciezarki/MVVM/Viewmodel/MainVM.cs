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
        public ICommand NavigateShowHistory { get; }

        public ICommand NavigateAddProgressLog { get; }
        public ICommand SignInCommand { get; }
        public ICommand CreateAccountCommand { get; }

        private AddProgressLogVM _addProgressLogVM = new AddProgressLogVM();

        public MainVM(Core.NavigationService navigationService)
        {

            _navigationService = navigationService;
            _navigationService.SetNavigator(vm => CurrentVM = vm);

            BaseVM addWorkoutVM = new AddWorkoutVM();
            BaseVM addExerciseVM = new AddExerciseVM();
            BaseVM addProgressLogVM = new AddProgressLogVM();
            BaseVM showHistoryVM = new ShowHistoryVM();



            NavigateAddWorkout = new RelayCommand(_ => _navigationService.NavigateTo(addWorkoutVM), _ => true);
            NavigateAddExercise = new RelayCommand(_ => _navigationService.NavigateTo(addExerciseVM), _ => true);
            NavigateAddProgressLog = new RelayCommand(_ =>
            {
                _navigationService.NavigateTo(_addProgressLogVM);
                _addProgressLogVM.LoadData(); 
            }, _ => true);
            NavigateShowHistory = new RelayCommand(_ => _navigationService.NavigateTo(showHistoryVM), _ => true); 

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

            SignInCommand = new RelayCommand(_ => SignIn(), _ => true);
            CreateAccountCommand = new RelayCommand(_ => CreateAccount(), _ => true);


        }
        private bool _isLoginPanelVisible = true;//zmień na false żeby ukryć panel logowania
        private string _usernameRegistration;
        private string _emailRegistration;
        private string _passwordRegistration;
        private string _repeatedPasswordRegistration;
        private string _usernameSignIn;
        private string _passwordSignIn;

        public bool IsLoginPanelVisible
        {
            get => _isLoginPanelVisible;
            set
            {
                if (_isLoginPanelVisible != value)
                {
                    _isLoginPanelVisible = value;
                    OnPropertyChanged(nameof(IsLoginPanelVisible));
                }
            }
        }
        public string UsernameRegistration
        {
            get => _usernameRegistration;
            set
            {
                _usernameRegistration = value;
                OnPropertyChanged(nameof(UsernameRegistration));
            }
        }
        public string EmailRegistration
        {
            get => _emailRegistration;
            set
            {
                _emailRegistration = value;
                OnPropertyChanged(nameof(EmailRegistration));
            }
        }
        public string PasswordRegistration
        {
            get => _passwordRegistration;
            set
            {
                _passwordRegistration = value;
                OnPropertyChanged(nameof(PasswordRegistration));
            }
        }
        public string RepeatedPasswordRegistration
        {
            get => _repeatedPasswordRegistration;
            set
            {
                _repeatedPasswordRegistration = value;
                OnPropertyChanged(nameof(RepeatedPasswordRegistration));
            }
        }
        public string UsernameSignIn
        {
            get => _usernameSignIn;
            set
            {
                _usernameSignIn = value;
                OnPropertyChanged(nameof(UsernameSignIn));
            }
        }
        public string PasswordSignIn
        {
            get => _passwordSignIn;
            set
            {
                _passwordSignIn = value;
                OnPropertyChanged(nameof(PasswordSignIn));
            }
        }

        public void SignIn()
        {
            if (string.IsNullOrEmpty(UsernameSignIn) || string.IsNullOrEmpty(PasswordSignIn))
            {
                MessageBox.Show("Należy uzupełnić wszystkie pola, aby się zalogować.", "Błędne dane", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool userExist;
            bool isPasswordCorrect;
            using (var context = new AppDbContext())
            {
                userExist = context.Users.Any(u => u.Username == UsernameSignIn);
                isPasswordCorrect = context.Users.Any(u => u.Username == UsernameSignIn && u.Password == PasswordSignIn);
            }
            if (userExist)
            {
                if (isPasswordCorrect)
                {
                    using (var context = new AppDbContext())
                    {
                        var user = context.Users
                            .FirstOrDefault(u => u.Username == UsernameSignIn && u.Password == PasswordSignIn);

                        if (user != null)
                        {
                            DbData.SetUserId(user.Id);
                        }

                    }
                    _isLoginPanelVisible = !IsLoginPanelVisible;
                    OnPropertyChanged(nameof(IsLoginPanelVisible));
                    return;
                    
                }else
                {
                    MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło.", "Błędne dane", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }else
            {
                MessageBox.Show("Podany użytkownik nie istnieje. Spróbuj jeszcze raz lub zarejestruj się.", "Błędne dane", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
        public void CreateAccount()
        {
            if (string.IsNullOrEmpty(UsernameRegistration) || string.IsNullOrEmpty(EmailRegistration) || string.IsNullOrEmpty(PasswordRegistration) || string.IsNullOrEmpty(RepeatedPasswordRegistration))
            {
                MessageBox.Show("Należy wypełnić wszystkie pola, aby się zarejestrować.","Błędne dane",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }
            bool userExist;
            using (var context = new AppDbContext())
            {
                userExist = context.Users.Any(u => u.Username == UsernameRegistration);
            }
            if (userExist)
            {
                MessageBox.Show("Podana nazwa użytkownika jest już zajęta.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (PasswordRegistration != RepeatedPasswordRegistration)
            {
                {
                    MessageBox.Show("Podane hasła nie są identyczne. Spróbuj jeszcze raz.", "Błędne dane", MessageBoxButton.OK,MessageBoxImage.Warning);
                    return;
                }
            }
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var u = new User();
                u.Username = UsernameRegistration;
                u.Password = PasswordRegistration;
                u.Email = EmailRegistration;
                u.CreatedAt = DateTime.Now;

                context.Users.Add(u);
                context.SaveChanges();
            }
            MessageBox.Show("Poprawnie utworzono użytkownika. Zaloguj się, aby przejść dalej.");

            UsernameRegistration = "";
            EmailRegistration = "";
            PasswordRegistration = "";
            RepeatedPasswordRegistration = "";
        }
    }
}
