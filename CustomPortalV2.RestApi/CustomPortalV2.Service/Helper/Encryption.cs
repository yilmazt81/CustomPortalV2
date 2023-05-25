using System.IO;

using System;
using System.Security.Cryptography;
using System.Text;

///<summary />
public class Encryption
{
    ///<summary />
    // private const string Key = "BackAOT5!6!2012";
    private readonly string _key = string.Empty;

    public Encryption(string passwordKey)
    {
        _key = passwordKey;
    }

    ///<summary />
    ///<param name="toEncrypt" />
    ///<returns />
    public string Encrypt(string toEncrypt)
    {
        return Encrypt(toEncrypt, true);
    }

    public string Decrypt(string cipherString)
    {
        if (string.IsNullOrEmpty(cipherString))
            return string.Empty;

        return Decrypt(cipherString, true);
    }

    public string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(cipherString);
        if (useHashing)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_key));
            hashmd5.Clear();
        }
        else
        {
            keyArray = Encoding.UTF8.GetBytes(_key);
        }
        var tdes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
            toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }


    ///<summary />
    ///<param name="toEncrypt" />
    ///<param name="useHashing" />
    ///<returns />
    public string Encrypt(string toEncrypt, bool useHashing)
    {
        try
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_key));
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(_key);
            }

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        catch
        {
            return toEncrypt;
        }
    }


    //private string Base64Encode(string encrString)
    //{
    //    try
    //    {
    //        var b = Encoding.UTF8.GetBytes(encrString);
    //        var encryptedConnectionString = Convert.ToBase64String(b);

    //        return encryptedConnectionString;
    //    }
    //    catch
    //    {
    //        return encrString;
    //    }
    //}

    private byte[] ConvertFileToByteArray(string fileFolder, string fileName)
    {
        var path = Path.Combine(fileFolder, fileName);
        return File.ReadAllBytes(path);
    }


    private void SaveNewFile(string cryptedFile, string newFolder, string fileName)
    {
        var fileBytes = Convert.FromBase64String(cryptedFile);
        var path = Path.Combine(newFolder, fileName);

        DirectoryInfo di = new DirectoryInfo(newFolder);
        if (!di.Exists)
        {
            di.Create();
        }
        File.WriteAllBytes(path, fileBytes);
    }

    private void SaveNewFile(string cryptedFile, string path)
    {
        var fileBytes = Convert.FromBase64String(cryptedFile);
        File.WriteAllBytes(path, fileBytes);
    }




    /// <summary>
    /// Dokümanı şifreleyip, lokale kaydeder.
    /// </summary>
    /// <param name="toEncrypt">Şifrelenecek Dosya</param>
    /// <param name="fileName">Lokalde oluşturulacak dosya</param>
    /// <returns></returns>
    public void EncryptFile(byte[] toEncrypt, string fileName)
    {
        string cryptedFile = Encrypt(Convert.ToBase64String(toEncrypt), true);
        SaveNewFile(cryptedFile, fileName);
    }

    /// <summary>
    /// Dokümanı şifreleyip, aynı isimle verilen yola kaydeder.
    /// </summary>
    /// <param name="fileFolder">Şifrelenecek Dosyanın Bulunduğu Klasör</param>
    /// <param name="fileName">Şifrelenecek Dosyanın Adı</param>
    /// <param name="newFolder">Dosyanın lokalde kaydedileceği klasör</param>
    /// <returns></returns>
    public void EncryptFile(string fileFolder, string fileName, string newFolder)
    {
        var bytes = ConvertFileToByteArray(fileFolder, fileName);
        EncryptFile(bytes, fileName, newFolder);
    }

    /// <summary>
    /// Dokümanı şifreleyip, lokale kaydeder.
    /// </summary>
    /// <param name="toEncrypt">Şifrelenecek Dosya</param>
    /// <param name="fileName">Şifrelenecek Dosyanın Adı</param>
    /// <param name="newFolder">Dosyanın lokalde kaydedileceği klasör</param>
    /// <returns></returns>
    public void EncryptFile(byte[] toEncrypt, string fileName, string newFolder)
    {
        string cryptedFile = Encrypt(Convert.ToBase64String(toEncrypt), true);
        SaveNewFile(cryptedFile, newFolder, fileName);
    }

    /// <summary>
    /// Dokümanı şifreleyip byte[] döndürür.
    /// </summary>
    /// <param name="fileName">Şifrelenecek Dosya Yolu</param>
    /// <returns>Şifrelenmiş Dosya</returns>
    public byte[] EncryptFile(string fileName)
    {
        byte[] bytes = File.ReadAllBytes(fileName);
        return EncryptFile(bytes);
    }

    /// <summary>
    /// Dokümanı şifreleyip, byte[] döndürür.
    /// </summary>
    /// <param name="toEncrypt">Şifrelenecek Dosya</param>
    /// <returns>Şifrelenmiş Dosya</returns>
    public byte[] EncryptFile(byte[] toEncrypt)
    {
        string cryptedFile = Encrypt(Convert.ToBase64String(toEncrypt), true);
        return Convert.FromBase64String(cryptedFile);
    }


    /// <summary>
    /// Dokümanı şifreleyip, byte[] döndürür.
    /// </summary>
    /// <param name="fileFolder">Şifrelenecek Dosyanın Bulunduğu Klasör</param>
    /// <param name="fileName">Şifrelenecek Dosyanın Adı</param>
    /// <returns>Şifrelenmiş Dosya</returns>
    public byte[] EncryptFile(string fileFolder, string fileName)
    {
        var bytes = ConvertFileToByteArray(fileFolder, fileName);
        return EncryptFile(bytes);
    }



    /// <summary>
    /// Şifrelenmiş dokümanı çözüp, lokale kaydeder.
    /// </summary>
    /// <param name="toDecrypt">Şifrelenmiş Dosya</param>
    /// <param name="fileName">Lokalde oluşturulacak dosya</param>
    /// <returns></returns>
    public void DecryptFile(byte[] toDecrypt, string fileName)
    {
        string cryptedFile = Decrypt(Convert.ToBase64String(toDecrypt), true);
        SaveNewFile(cryptedFile, fileName);
    }

    /// <summary>
    /// Şifrelenmiş dokümanı çözüp, aynı isimle verilen yola kaydeder.
    /// </summary>
    /// <param name="fileFolder">Şifrelenmiş Dosyanın Bulunduğu Klasör</param>
    /// <param name="fileName">Şifrelenmiş Dosyanın Adı</param>
    /// <param name="newFolder">Dosyanın lokalde kaydedileceği klasör</param>
    /// <returns></returns>
    public void DecryptFile(string fileFolder, string fileName, string newFolder)
    {
        byte[] bytes = ConvertFileToByteArray(fileFolder, fileName);
        DecryptFile(bytes, fileName, newFolder);
    }

    /// <summary>
    /// Şifrelenmiş dokümanı çözüp, lokale kaydeder.
    /// </summary>
    /// <param name="toDecrypt">Şifrelenmiş Dosya</param>
    /// <param name="fileName">Şifrelenmiş Dosyanın Adı</param>
    /// <param name="newFolder">Dosyanın lokalde kaydedileceği klasör</param>
    /// <returns></returns>
    public void DecryptFile(byte[] toDecrypt, string fileName, string newFolder)
    {
        string cryptedFile = Decrypt(Convert.ToBase64String(toDecrypt), true);
        SaveNewFile(cryptedFile, newFolder, fileName);
    }

    /// <summary>
    /// Şifrelenmiş dokümanı çözüp byte[] döndürür.
    /// </summary>
    /// <param name="fileName">Şifrelenmiş Dosya Yolu</param>
    /// <returns>Çözülmüş Dosya</returns>
    public byte[] DecryptFile(string fileName)
    {
        byte[] bytes = File.ReadAllBytes(fileName);
        return DecryptFile(bytes);
    }

    /// <summary>
    /// Şifrelenmiş dokümanı çözüp, byte[] döndürür.
    /// </summary>
    /// <param name="toDecrypt">Şifrelenmiş Dosya</param>
    /// <returns></returns>
    public byte[] DecryptFile(byte[] toDecrypt)
    {
        string cryptedFile = Decrypt(Convert.ToBase64String(toDecrypt), true);
        return Convert.FromBase64String(cryptedFile);
    }

    /// <summary>
    /// Şifrelenmiş dokümanı çözüp, byte[] döndürür.
    /// </summary>
    /// <param name="fileFolder">Şifrelenmiş Dosyanın Bulunduğu Klasör</param>
    /// <param name="fileName">Şifrelenmiş Dosyanın Adı</param>
    /// <returns></returns>
    public byte[] DecryptFile(string fileFolder, string fileName)
    {
        var bytes = ConvertFileToByteArray(fileFolder, fileName);
        return DecryptFile(bytes);
    }
}

