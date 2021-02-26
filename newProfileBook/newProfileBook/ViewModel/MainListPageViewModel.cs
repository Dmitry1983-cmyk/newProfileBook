using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;
using System.Collections.ObjectModel;
using System;
using newProfileBook.Services.Repository;

namespace newProfileBook
{
    class MainListPageViewModel: BindableBase
    {
        private readonly INavigationService _navigateService;
        private IRepository _repository;

        private string _title;
        private ObservableCollection<ProfileModel> _profileList;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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

            var profileList = _repository.GetAllAsync<ProfileModel>();
            ProfileList = new ObservableCollection<ProfileModel>();
        }
        #endregion


        #region--methods
        public ICommand OnTapAddUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            //var profile = new ProfileModel()
            //{
            //    Login = Login,
            //    Password = Password,
            //    Confirm = Confirm,
            //    CreationTime =DateTime.Now
            //};
            await _navigateService.NavigateAsync($"{nameof(AddEditProfileView)}");
        }

        #endregion
    }
}

