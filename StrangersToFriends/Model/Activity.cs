using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace StrangersToFriends.Model
{
    public class Activity : ObservableObject
    {
		private string _id;
        public string ID
        {
            get => _id;
            set
            {
                _id = value;
				RaisePropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
				RaisePropertyChanged();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
				RaisePropertyChanged();
            }
        }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
				RaisePropertyChanged();
            }
        }

        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                _location = value;
				RaisePropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
				RaisePropertyChanged();
            }
        }

        private int _participants;
        public int Participants
        {
            get => _participants;
            set
            {
                _participants = value;
				RaisePropertyChanged();
            }
        }

        private string _numberOfParticipants;
        public string NumberOfParticipants
        {
            get => _numberOfParticipants;
            set
            {
                _numberOfParticipants = value;
                RaisePropertyChanged();
            }
        }

        private string _createdBy;
        public string CreatedBy
        {
            get => _createdBy;
            set
            {
                _createdBy = value;
				RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> JoinedBy { get; set; }
        
		public Activity()
		{
			JoinedBy = new ObservableCollection<string>();
		}
    }
}
