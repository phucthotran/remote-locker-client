using System;
using RemoteLocker.Common.Model;
using RemoteLocker.Common.Library.DataAccess;
using RemoteLocker.Common.Library.Encryption;
using RemoteLocker.Common.Global;
using System.IO;

namespace RemoteLocker.Common.Library.Action
{
    public class AppAction
    {
        public static void InitData()
        {
            if (!File.Exists(CommonConstant.REMOTE_LOCKER_DATA))
            {
                PlainTextData.SaveTo(
                    CommonConstant.REMOTE_LOCKER_DATA, ';',
                    "admin",
                    "admin",
                    Md5Sha1Encrypt.MD5Hashing("RemoteLocker.Default.IdentifyCode")
                );
            }
        }
    }
}
