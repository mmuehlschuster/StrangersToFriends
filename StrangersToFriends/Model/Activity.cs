using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace StrangersToFriends.Model
{
    public class Activity : ObservableObject
    {
		public string ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
		public string Category { get; set; }
		public Location Location { get; set; }
        public string Description { get; set; }
		public int Participants { get; set; }
		public string NumberOfParticipants { get; set; }
		public string CreatedBy { get; set; }
		public ObservableCollection<string> JoinedBy { get; set; }
        
		public Activity()
		{
			JoinedBy = new ObservableCollection<string>();
		}

		public Activity ShallowCopy()
		{
			return (Activity)this.MemberwiseClone();
		}
    }
}
