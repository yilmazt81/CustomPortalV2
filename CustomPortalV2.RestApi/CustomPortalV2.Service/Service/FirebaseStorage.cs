using CustomPortalV2.Business.Concrete;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CustomPortalV2.Business.Service
{
    public class FireStorage : IFirebaseStorage
    {
        private readonly FirebaseStorage _storage;

        public FireStorage()
        {
            string projectId = "digiform-44565";



            _storage = new FirebaseStorage(
           $"{projectId}.appspot.com",
           new FirebaseStorageOptions
           {
               AuthTokenAsyncFactory = async () =>
                    {
                        var authService = new FirebaseService();
                        var auth = await authService.LoginUserAsync("api@gmail.com", "49999f9f");
                        return auth.FirebaseToken;
                    }
           }
            );
        }

       

        public async Task<string> SaveFileToStorageAsync(string folder, string fileName, Stream fileStream)
        {
            try
            {

                var storageReference = _storage.Child(folder).Child(fileName);
                var downloadUrl = await storageReference.PutAsync(fileStream);


                return downloadUrl;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
        async Task<string> IFirebaseStorage.DeleteFileToStorageAsync(string folder, string fileName)
        {
            var storageReference = _storage.Child(folder).Child(fileName);
            await storageReference.DeleteAsync();

            return "ok";
        }

        async Task<FirebaseAuthLink> IFirebaseStorage.Save()
        {
            var authService = new FirebaseService();
            var ff = await authService.RegisterUserAsync("api@gmail.com", "49999f9f");

            return ff;
        }
    }
}
