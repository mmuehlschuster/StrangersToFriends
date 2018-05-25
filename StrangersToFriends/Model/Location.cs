using System;
using GalaSoft.MvvmLight;

namespace StrangersToFriends.Model
{
	public class Location : ObservableObject
    {
		private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

		private Position _position;
		public Position Position
		{
			get => _position;
			set
			{
				_position = value;
				RaisePropertyChanged();
			}
		}

		public Location(string name, Position position)
        {
			Name = name;
			Position = position;
        }

		public Distance DistanceTo(Position position)
		{
			double radians = Math.Acos(
				Math.Sin(ToRadians(_position.Latitude)) * Math.Sin(ToRadians(position.Latitude)) + 
				Math.Cos(ToRadians(_position.Latitude)) * Math.Cos(ToRadians(position.Latitude)) * 
				Math.Cos(ToRadians(_position.Longitude - position.Longitude)));

			return Distance.FromKilometers(6371 * radians);
		}

		private double ToRadians(double angle)
		{
			return Math.PI * angle / 180;
		}
    }

	public class Position : ObservableObject
	{
		private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                RaisePropertyChanged();
            }
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                RaisePropertyChanged();
            }
        }

		
		public Position(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
	}

	public class Distance : ObservableObject 
	{
		private const double MetersPerMile = 1609.344;
        private const double MetersPerKilometer = 1000.0;

		private double _meters;
		public double Meters 
		{
			get => _meters;
			set
			{
				_meters = value;
				RaisePropertyChanged();
			}
		}

		private double _kilometers;
		public double Kilometers
		{
			get => _kilometers;
			set
			{
				_kilometers = value;
				RaisePropertyChanged();
			}
		}

		private double _miles;
        public double Miles
        {
			get => _miles;
            set
            {
				_miles = value;
                RaisePropertyChanged();
            }
        }

		public Distance(double meters) {
			Meters = meters;
			Kilometers = meters / MetersPerKilometer;
			Miles = meters / MetersPerMile;
		}

		public static Distance FromMeter(double meters) 
		{
			return new Distance(meters);
		}

		public static Distance FromKilometers(double kilometers)
		{
			return new Distance(kilometers * MetersPerKilometer);
		}

		public static Distance FromMiles(double miles)
		{
			return new Distance(miles * MetersPerMile);
		}
	}
}
