using System;
using GalaSoft.MvvmLight;

namespace StrangersToFriends.Model
{
    public class Activity : ObservableObject
    {
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Participants { get; set; }

        public Activity(String title, DateTime date, String description, String participants)
        {
            Title = title;
            Date = date;
            Description = description;
            Participants = participants;
        }
    }
}
