﻿using DbMon.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HLVRLauncher.Utilities
{
    class SingleProcessEnforcer
    {
        private readonly string mutexEventName = "HalfLifeVirtualRealityLauncherMutexEventLaliludgnskdagjfgbs";
        private readonly string mutexName = "HalfLifeVirtualRealityLauncherMutexLaliludgnskdagjfgbs";

        private EventWaitHandle mutexEventWaitHandle;
        private Mutex mutex;
        private bool isDisposed = false;

        public void ForceSingleProcess()
        {
            Boolean mutexCreated;
            mutex = new Mutex(true, mutexName, out mutexCreated);
            mutexEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, mutexEventName);
            if (!mutexCreated)
            {
                mutexEventWaitHandle.Set();
                throw new CancelAndTerminateAppException();
            }
            else
            {
                var thread = new Thread(() =>
                {
                    while (!isDisposed)
                    {
                        if (mutexEventWaitHandle.WaitOne(1000))
                        {
                            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() => ((MainWindow)System.Windows.Application.Current.MainWindow).BringToForeground()));
                        }
                    }
                })
                {
                    IsBackground = true
                };
                thread.Start();
            }
        }

        public void Dispose()
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Close();
                mutex = null;
            }
            isDisposed = true;
        }

    }
}
