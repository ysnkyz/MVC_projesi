using MVC_projesi.Tasks.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_projesi.Tasks.Triggers
{
    public class CezaArttirmaDusurmeTrigger
    {
        public static void Baslat()
        {
            // Zamanlayıcıyı oluşturuyoruz.
            IScheduler zamanlayici = StdSchedulerFactory.GetDefaultScheduler();
            // Zamanlayıcıyı çalıştırıyoruz.
            if (!zamanlayici.IsStarted)
                zamanlayici.Start();
            // Tetiklenecek görevi belirtiyoruz.
            IJobDetail gorev = JobBuilder.Create<CezaArttirmaDusurmeJob>().Build();

            // Tetikleyici oluşturuyoruz.
            ICronTrigger tetikleyici = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("CezaArttirmaDusurmeJob", "null") // Çalıştırılacak görevin adını giriyoruz.
                .WithCronSchedule("0 0 21 * * ? *") // Günün hangi saatinde çalışacağını belirtiyoruz.
                .Build(); // Tetikleyiciyi aktif ediyoruz.

            // Zamanlayıcıya; görevi ve tetikleyiciyi tanıtıyoruz.
            zamanlayici.ScheduleJob(gorev, tetikleyici);


        }
    }
}