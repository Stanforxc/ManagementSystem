using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace encryption
{

    [Guid("0A2ECA2B-3AB1-4B59-AE1A-65088D9BD2B6"),
       InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface encryption_Events {

    }

    [Guid("E19B362A-3D38-4FAC-9A65-45520672E7DA")]
    public interface encryption_Interface
    {
        [DispId(1)]
        string encrypt(string origin_pwd, string origin_username);

        Boolean match(string origin_pwd,  string pwd, string username);
    }

    [Guid("FF40923F-7EC1-40CF-9A74-864AF9430C5A"),
         ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(encryption_Events))]

    public class MD5encryptor : encryption_Interface
    {

        public MD5 md = MD5.Create();

        public string charset = "ABCDEFGHIGKLMNOPQRSTUVWXYZzbcdefghigklmnopqrstuvwx+=#:%$*!@";

        public string encrypt(string origin_pwd, string origin_username)
        {

            string combine_str = origin_pwd + origin_username;
            byte[] bytes = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combine_str));

            string ret_str = "";

            foreach (byte b in bytes)
            {
                ret_str += charset[b%charset.Length];
            }

            return ret_str;

        }

        public Boolean match(string origin_pwd, string pwd, string username) {

            string new_str = encrypt(pwd, username);

            if (0 == new_str.CompareTo(origin_pwd))
            {
                return true;
            }
            else {
                return false;
            }
        }

    }
}
