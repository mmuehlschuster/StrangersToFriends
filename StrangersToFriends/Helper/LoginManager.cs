using System;
using System.Threading.Tasks;

using Firebase.Auth;

namespace StrangersToFriends.Helper
{
	public sealed class LoginManager
    {
		private const string FirebaseAppKey = "AIzaSyCnvfHpScTlaTYR1IMlyeN4o_aQ_PVjzEM";

		private static readonly LoginManager _instance = new LoginManager();
		private static FirebaseAuthProvider _authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAppKey));

		public static FirebaseAuth Auth { get; set; }

		public static LoginManager Instance 
		{
			get
			{
				return _instance;
			}
		}

		private LoginManager() {}

		public Task<FirebaseAuthLink> signupWithEmailAndPassword(string email, string password)
		{
			return _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
		}

		public Task<FirebaseAuthLink> loginWithEmailAndPassword(string email, string password)
		{
			return _authProvider.SignInWithEmailAndPasswordAsync(email, password);
		}
    }
}
