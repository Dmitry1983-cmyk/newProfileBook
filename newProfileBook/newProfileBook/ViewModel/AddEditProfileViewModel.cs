﻿using newProfileBook.Services.Repository;
using newProfileBook.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            //var id=await _repository.InsertAsync(profile);
            //profile.Id = id;
            //ProfileList.Add(profile);
            await _repository.InsertAsync(profile);

        }

        #endregion

    }
}
