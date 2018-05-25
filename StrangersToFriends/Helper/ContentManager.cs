using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Firebase.Database;
using Firebase.Database.Query;

using StrangersToFriends.Model;
using StrangersToFriends.Constants;
using Newtonsoft.Json;

namespace StrangersToFriends.Helper
{
	public class ContentManager
    {
		private FirebaseClient _firebase;

		public ContentManager() 
		{
			_firebase = new FirebaseClient(Constant.FirebaseAppUri, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(LoginManager.Auth.FirebaseToken)
            });
		}

		public Task<IReadOnlyCollection<FirebaseObject<Activity>>> GetActivities() 
		{
			return _firebase.Child("activities").OnceAsync<Activity>();
		}

		public Task<FirebaseObject<string>> AddActivity(Activity activity)
		{
			string json = JsonConvert.SerializeObject(activity);
		    return _firebase.Child("activities").PostAsync(json);

		}

		public Task DeleteActivity(Activity activity)
		{
			return _firebase.Child("activities").Child(activity.ID).DeleteAsync();
		}

		public Task UpdateActivity(Activity activity)
		{
			string json = JsonConvert.SerializeObject(activity);
			return _firebase.Child("activities").Child(activity.ID).PutAsync(json);
		}
    }
}
