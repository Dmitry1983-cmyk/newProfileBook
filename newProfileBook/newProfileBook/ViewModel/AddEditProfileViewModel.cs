using Acr.UserDialogs;
using newProfileBook.Services.Repository;
using newProfileBook.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace newProfileBook.ViewModel
{
    public class AddEditProfileViewModel: BindableBase
    {
        public string _title;
        public string _nickname;
        public string _name;
        public string _cdescription;
        private string imagePath;

        private readonly INavigationService _navigateService;
        private IRepository _repository;
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

        public string Description
        {
            get { return _cdescription; }
            set 
            {
               SetProperty(ref _cdescription, value);
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                SetProperty(ref imagePath, value);
            }
        }

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #region--ctor
        public AddEditProfileViewModel(INavigationService navigationService, IRepository repository)
        {
            Title = "Add Profile Page";
            _navigateService = navigationService;
            _repository = repository;
            ImagePath = "pic_profile.png";
        }
        #endregion


        #region--methods

        public ICommand AddProfile => new Command(ExecuteNavigateCommand);
        private async void ExecuteNavigateCommand()
        {
            var profile = new ProfileModel()
            {
                Nickname = Nickname,
                Name = Name,
                Description = Description,
                CreationTime = DateTime.Now
            };

            //var id = await _repository.InsertAsync(profile);
            //profile.Id = id;
            //ProfileList.Add(profile);

            await _repository.InsertAsync(profile);
            await _navigateService.GoBackAsync();
        }

        public ICommand ImageTappedCommand =>new Command(OnImageTappedCommand);
        private void OnImageTappedCommand()
        {
            
        }
        #endregion

        //---------------------удаление по клику
        private ProfileModel _selectedItem;
        public ProfileModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ICommand OnTapDeleteUser => new Command(DeleteCommand);
        async private void DeleteCommand()
        {
            if (SelectedItem != null)
            {
                //начало модалки
                var confirmConfig = new ConfirmConfig()//он возвращает true если ОК
                {
                    Message = "Are yo really want to deled this profile?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);//после того как инициализирловали ACR в MainActivity 
                if (confirm)
                {
                    await _repository.DleteAsync(SelectedItem);
                    ProfileList.Remove(SelectedItem);
                }
                //проверка и конец модалки
            }
        }
        //-------------обновление 
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(SelectedItem))
            {
                Name = SelectedItem.Name;
                Nickname = SelectedItem.Nickname;
            }
        }
        public ICommand OnTapUpdateUser => new Command(UdateCommand);
        async private void UdateCommand()
        {
            if (SelectedItem != null)
            {
                var new_profile = new ProfileModel()
                {
                    Id = SelectedItem.Id,
                    Nickname = Nickname,
                    Name = Name,
                    CreationTime = DateTime.Now
                };

                var index = ProfileList.IndexOf(SelectedItem);
                ProfileList.Remove(SelectedItem);

                await _repository.UpdateAsync(new_profile);
                ProfileList.Insert(index,new_profile);
            }
        }
        //---------------------------------------

    }
}
