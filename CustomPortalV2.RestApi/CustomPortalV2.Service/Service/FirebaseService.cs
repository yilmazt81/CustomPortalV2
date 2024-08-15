using Firebase.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FirebaseService
    {
        private readonly FirebaseAuthProvider _authProvider;
        public FirebaseService() {

            string ApiKey = "AIzaSyCfRAsxaGptI-WCKZbhQFXZskT2YqarmBk";

            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        }
        public async Task<FirebaseAuthLink> LoginUserAsync(string email, string password)
        {
            try
            {
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
                return auth; // Kullanıcının kimlik doğrulama bilgilerini döndürür
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in: {ex.Message}");
                return null;
            }
        }

        public async Task<FirebaseAuthLink> RegisterUserAsync(string email, string password)
        {
            try
            {
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                return auth; // Kullanıcının kimlik doğrulama bilgilerini döndürür
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                return null;
            }
        }
    }
}
