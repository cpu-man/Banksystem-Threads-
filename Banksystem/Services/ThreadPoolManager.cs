using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banksystem.Services
{
    public class ThreadPoolManager
    {
        //private static ThreadPoolManager? _instance;
        //public static ThreadPoolManager Instance => _instance ??= new ThreadPoolManager();

        //private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3);

        //public void QueueWork(Action work)
        //{
        //    Task.Run(async () =>
        //    {
        //        await _semaphore.WaitAsync();
        //        try
        //        {
        //            Thread.Sleep(500);
        //            work();
        //        }
        //        finally
        //        {
        //            _semaphore.Release();
        //        }
        //    });
        //}
    }
}