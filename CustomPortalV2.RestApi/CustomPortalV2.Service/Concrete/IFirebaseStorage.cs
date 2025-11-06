using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFirebaseStorage
    {

        Task<string> SaveFileToStorageAsync(string folder, string fileName, Stream fileStream);
        Task<string> DeleteFileToStorageAsync(string folder, string fileName);

        Task<FirebaseAuthLink> Save();
    }
}
