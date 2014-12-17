using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteLocker.Common.Library.Encryption;

namespace RemoteLocker.Communication
{
    public class Command
    {
        public const string UNLOCK = "de4981e4f0c745b679098a6ab2452911fe87aaa6"; //sha1: remotelocker.unlock
        public const string LOCK = "a908add964c7ac5c4623b005953f95279086705b"; //sha1: remotelocker.lock
        public const string APPROVE = "f053b07828e30f4f34d7f55982820f6fb4e807e2"; //sha1: remotelocker.approve
        public const string REJECT = "f25e541e5dce5e84cf83c24e1798689d375f15d1"; //sha1: remotelocker.reject
        public const string AUTHENTICATION = "4eb7df3a02821648c4ee8c056ecfebb8828eedab"; //sha1: remotelocker.authentication
        public const string SHUTDOWN = "f863e9679f0b8e8d9e2767f3cea488879ed59823"; //sha1: remotelocker.shutdown
        public const string DISCONNECT = "26465459408fd3282e63aee07039c5ac17a8e960"; //sha1: remotelocker.disconnect
        public const string UNLOCK_REQUEST = "e48a53e31e8a570d2ba72c22f698de35d39b2bb2"; //sha1: remotelocker.unlock_request
        public const string APP_CLOSE = "7e57f5d4f99ae1890c866e3e33e556d677f5f272"; //sha1: remotelocker.app_close
        public const string COMPUTER_NAME = "cbe612ae6dbcbf2ef49410a176781530890b0d05"; //sha1: remotelocker.computer_name
        public const string COMPUTER_NAME_OUTPUT = "cebdb21d7d39cb1e4c9709b42c3349c9b43eedca"; //sha1: remotelocker.computer_name_output
        public const string NULL = "null";

        private String value;

        public String Value
        {
            get { return this.value; }
        }

        public Command(String Value)
        {
            this.value = Value;
        }
    }
}
