using CustomPortalV2.Business.Concrete;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FireStorage : IFirebaseStorage
    {
        private readonly FirebaseStorage _storage;

        public FireStorage()
        {
            string projectId = "digiform-e2926";



            _storage = new FirebaseStorage($"{projectId}.firebasestorage.app",
           new FirebaseStorageOptions
           {
               AuthTokenAsyncFactory = async () =>
                    {
                        var authService = new FirebaseService();
                        var auth = await authService.LoginUserAsync("api@gmail.com", "49999f9f");
                        return auth.FirebaseToken;
                    },
           }
            );
            //gs://digiform-e2926.firebasestorage.app
            var bucket = _storage.StorageBucket;//= "digiform-e2926.firebasestorage.app";


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
