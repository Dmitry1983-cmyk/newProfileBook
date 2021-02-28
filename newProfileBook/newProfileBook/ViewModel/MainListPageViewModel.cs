using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;
using System.Collections.ObjectModel;
using newProfileBook.Services.Repository;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Acr.UserDialogs;

namespace newProfileBook
{
    class MainListPageViewModel : BindableBase, IInitializeAsync
    {
        private readonly INavigationService _navigateService;
        private IRepository _repository;
        private IUserDialogs _userDialogs;

        private string _title;
        public string _nickname;
        public string _name;
        private ObservableCollection<ProfileModel> _profileList;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Nickname
        {
            get { return _nickname; }
            set { SetProperty(ref _nickname, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }


        #region --ctor
        public MainListPageViewModel(INavigationService navigationService, IRepository repository)
        {
            Title = "Main List Page";
            _navigateService = navigationService;
            _repository = repository;

        }
        //public MainListPageViewModel(INavigationService navigationService, IRepository repository, IUserDialogs userDialogs)
        //{
        //    Title = "Main List Page";
        //    _navigateService = navigationService;
        //    _repository = repository;

        //    _userDialogs = userDialogs;
        //}
        #endregion


        #region--methods
        public ICommand OnTapAddUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            await _navigateService.NavigateAsync($"{nameof(AddEditProfileView)}");
        }


        public ICommand LogOutTappedCommand =>new Command(OnLogOutCommandAsync);
        private async void OnLogOutCommandAsync()
        {
            //придумать как сделать logout
            await _navigateService.NavigateAsync("/NavigationPage/MainPage");
        }

        public ICommand SettingsCommand => new Command(OnSettingsTappedCommandAsync);
        private async void OnSettingsTappedCommandAsync()
        {
            await _navigateService.NavigateAsync(nameof(SettingsPage));
        }
        #endregion


        #region --- show data users from db

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();
            ProfileList = new ObservableCollection<ProfileModel>(profileList);
            
        }

        #endregion

    }
}

