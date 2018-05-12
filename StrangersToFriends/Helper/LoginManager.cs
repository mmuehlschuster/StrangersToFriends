using System;
using System.Threading.Tasks;

using Firebase.Auth;

namespace StrangersToFriends.Helper
{
	public class LoginManager
    {
		public const string FirebaseAppKey = "AIzaSyCnvfHpScTlaTYR1IMlyeN4o_aQ_PVjzEM";
		public const string FirebaseAppUri = "https://<YOUR_FIREBASE_APP>.firebaseio.com/";

		private static FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAppKey));
		private static LoginManager shared = new LoginManager();

		public static LoginManager Shared => shared;

		private LoginManager() {}

		public async Task<FirebaseAuthLink> LoginWithEmailAndPassword(String email, String password) 
		{
			return await authProvider.SignInWithEmailAndPasswordAsync(email, password);
		}

		public async Task<FirebaseAuthLink> SignUpWithEmailAndPassword(String email, String password)
        {
			return await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
        }
    }
}
