using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;

namespace newProfileBook
{
    public class MainPageViewModel: BindableBase
    {
        private readonly INavigationService _navigateService;

        public string _title;
        public string _login;
        public string _password;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Main Page";
            _navigateService = navigationService;
        }

        public ICommand OnTapSignUpPage => new Command(ExecuteNavigateCommand);

        async private void ExecuteNavigateCommand()
        {
            await _navigateService.NavigateAsync($"{nameof(RegisterPageView)}");
        }

        public ICommand OnTapLogin => new Command(ExecuteNavigateCommand_MainList);

        async private void ExecuteNavigateCommand_MainList()
        {
            await _navigateService.NavigateAsync($"{nameof(MainListPageView)}");
        }

    }
}
