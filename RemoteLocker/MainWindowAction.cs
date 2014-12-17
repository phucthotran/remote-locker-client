using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteLocker.Communication;
using RemoteLocker.Module;
using RemoteLocker.Common.Library.Action;
using RemoteLocker.Controller;

namespace RemoteLocker
{
    public partial class MainWindow
    {        
        #region ACTION PERFORM

        [InvokeCommand(Command.LOCK)]
        public void Lock()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Show();
            }));
        }

        [InvokeCommand(Command.UNLOCK)]
        public void Unlock()
        {
            this.Dispatcher.Invoke(new Action(() => {
                this.Hide();
            }));                        
        }

        [InvokeCommand(Command.AUTHENTICATION, HasInput = true, HasOutput = true, OnOutputTrue = Command.APPROVE, OnOutputFalse = Command.REJECT)]
        public bool Authentication(String IdentifyCode)
        {
            AccountController accController = new AccountController();

            return accController.AvailableCode(IdentifyCode);
        }

        [InvokeCommand(Command.APPROVE)]
        public void Approve()
        {
            (tcpProvider as TcpCommunication).CanReaction = true;
        }

        [InvokeCommand(Command.REJECT)]
        public void Reject()
        {
            stringCommandSender.Send(Command.REJECT);
            (tcpProvider as TcpCommunication).CanReaction = false;
            (tcpProvider as TcpCommunication).ClientDisconnect();
        }

        [InvokeCommand(Command.APP_CLOSE)]
        public void AppClose()
        {
            (tcpProvider as TcpCommunication).ClientDisconnect();

            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Close();
            }));
        }

        [InvokeCommand(Command.COMPUTER_NAME)]
        public void ComputerName()
        {
            stringCommandSender.Send(Command.COMPUTER_NAME_OUTPUT, SystemAction.GetComputerName());
            (tcpProvider as TcpCommunication).ClientDisconnect();
        }

        [InvokeCommand(Command.SHUTDOWN)]
        public void Shutdown()
        {
            SystemAction.Shutdown();
        }

        [InvokeCommand(Command.DISCONNECT)]
        public void Disconnect()
        {
            (tcpProvider as TcpCommunication).ClientDisconnect();
        }

        #endregion
    }
}
