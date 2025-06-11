using Ciezarki.MVVM.Viewmodel;

namespace Ciezarki.Core
{
    public class NavigationService
    {
        private Action<BaseVM> _navigate;
        public void SetNavigator(Action<BaseVM> navigate)
        {
            _navigate = navigate;
        }
        public void NavigateTo(BaseVM vm)
        {
            _navigate?.Invoke(vm);
        }
    }
}
