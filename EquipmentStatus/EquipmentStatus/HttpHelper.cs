using EFmodel;
using Newtonsoft.Json;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Monitor.Alarm;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EquipmentStatus
{
    public class HttpHelper
    {
        public static HttpClient _httpClient;
        internal static readonly string serverurl = ConfigurationManager.AppSettings["ServerUrl"];

        public HttpHelper()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }
        /// <summary>
        /// 获取资料库报警主机信息
        /// </summary>
        /// <returns></returns>
        public  List<AlarmHost> GetAlarmHosts()
        {


            string url = $"{serverurl}api/AlarmHost/GetAlarmHosts";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<AlarmHost>>(data);

            return requst;
        }


        /// <summary>
        /// 依据主机IP地址查询门磁表
        /// </summary>
        /// <param name="AlarmHostIP"></param>
        /// <returns></returns>
        public List<Alarm> GetAlarmsByHostIP(string AlarmHostIP)
        {
            string url = $"{serverurl}api/Alarm/GetAlarmsByHostIP?Ip={AlarmHostIP}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<Alarm>>(data);
            return requst;

        }
    
        /// <summary>
        /// 依据Alarm_Id查询Manage
        /// </summary>
        /// <returns></returns>
        public List<AlarmManage> GetAlarmManage(string AlarmID)
        {
            string url = $"{serverurl}api/AlarmManage/GetAlarmManagesByAlarmId?AlarmId={AlarmID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<AlarmManage>>(data);
            return requst;

        }

        /// <summary>
        /// 新增AlarmManageState
        /// </summary>
        /// <param name="stateDto"></param>
        /// <returns></returns>
        public string AddAlarmManage(AlarmManage alarmManage)
        {

            string url = $"{serverurl}api/AlarmManage/add";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Entity", alarmManage);
            string input= Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(input);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PostAsync(url, content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }
        /// <summary>
        /// 修改AlarmManage
        /// </summary>
        /// <param name="stateDto"></param>
        /// <returns></returns>
        public string UpdateAlarmManage(AlarmManage alarmManage)
        {

            string url = $"{serverurl}api/AlarmManage/Edit";



            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Entity", alarmManage);

            string input = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(input);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PutAsync(url, content).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }





        /// <summary>
        /// 获取资料库全部主机信息
        /// </summary>
        /// <returns></returns>
        public List<DVRs> GetDVRs()
        {


            string url = $"{serverurl}api/DVR/GetDVRs";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<DVRs>>(data);

            return requst;

        }


        /// <summary>
        /// 新增AlarmManageState
        /// </summary>
        /// <param name="stateDto"></param>
        /// <returns></returns>
        public string AddDVRInfoCheck(DVRInfoChecks dVRInfo)
        {

            string url = $"{serverurl}api/DVRInfoCheck/Add";

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Entity", dVRInfo);
            string input = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(input);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PostAsync(url, content).Result;
            if (response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return "ok";
            }
            else
            {
                return "no";
            }

           
        }

    }
}
