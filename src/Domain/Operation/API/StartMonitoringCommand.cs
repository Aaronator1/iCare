namespace Domain
{
    #region << Using >>

    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using Incoding.Block.Logging;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class StartMonitoringCommand : CommandBase
    {
        #region Properties

        public string ActionPlanId { get; set; }

        #endregion

        public override void Execute()
        {
            Action<string> put = message =>
                                     {
                                         try
                                         {
                                             string url = "http://192.168.0.100/api/newdeveloper/lights/2/state";

                                             var request = WebRequest.CreateHttp(url);
                                             request.Method = "PUT";
                                             request.AllowWriteStreamBuffering = false;
                                             request.ContentType = "application/json";
                                             request.Accept = "Accept=application/json";
                                             request.SendChunked = false;
                                             request.ContentLength = message.Length;
                                             using (var writer = new StreamWriter(request.GetRequestStream()))
                                                 writer.Write(message);
                                             var response = request.GetResponse() as HttpWebResponse;
                                             var res = response.GetResponseStream();
                                         }
                                         catch (Exception ex)
                                         {
                                             LoggingFactory.Instance.LogException(LogType.Debug, ex);
                                         }
                                     };

            put(@"{""on"":true, ""sat"":254, ""bri"":4,""hue"":25718}");


            bool isBrokenHeatRate = false;
            while (true)
            {Thread.Sleep(1.Seconds());
                isBrokenHeatRate =
                    Repository.Query<Notification>()
                        .Any(r => r.ActionPlan.Id == ActionPlanId && r.Type == Monitoring.MonitorOfType.HeartRate);
                if (isBrokenHeatRate)
                {

                    put(@"{""on"":true, ""sat"":253, ""bri"":255,""hue"":65527}");
                    Thread.Sleep(1.Seconds());
                    put(@"{""on"":true, ""sat"":253, ""bri"":255,""hue"":15000}");
                }
            }
        }

            
        }
    
}