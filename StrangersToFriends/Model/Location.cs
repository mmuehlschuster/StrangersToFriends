using System;
namespace StrangersToFriends.Model
{
    public class Location
    {
		public string Name { get; set; }
		public int Latitude { get; set; }
		public int Longitude { get; set; }

        public Location(string name, int latitude, int longitude)
        {
			Name = name;
			Latitude = latitude;
			Longitude = longitude;
        }
    }
}
