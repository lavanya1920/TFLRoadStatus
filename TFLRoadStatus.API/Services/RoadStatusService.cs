using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TFLRoadStatus.API.Interfaces;
using TFLRoadStatus.API.Models;

namespace TFLRoadStatus.API.Service
{
    public class RoadStatusService : IRoadStatusService
    {
        public readonly TFLAPIDetails _apiConfigDetails;
        private static readonly HttpClient _client = new HttpClient();
        public RoadStatusService(TFLAPIDetails apiConfigDetails)
        {
            _apiConfigDetails = apiConfigDetails;
        }

        /// <summary>
        /// Get Road status from TFL API with RoadId
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public async Task<RoadStatus> GetRoadStatus(string roadId)
        {
            RoadStatus roadStatus = new RoadStatus();
            string apiClientUrl = GetTFLAPIClientURL(roadId);
            var response = await _client.GetAsync(apiClientUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                var roadResponse= JsonConvert.DeserializeObject<List<RoadStatus>>(responseContent);
               
                // considering only one record
                if (roadResponse.Count > 0)
                {
                    roadStatus = roadResponse.First();
                    roadStatus.IsSuccess = response.IsSuccessStatusCode;
                }
            }
            else
            {
                roadStatus.StatusSeverityDescription = response.ReasonPhrase;
                roadStatus.IsSuccess  = false;
            }
            return roadStatus;
            
        }

        /// <summary>
        /// Method to form TFL API url which works without API key and API Id
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        private string GetTFLAPIClientURL(string roadId)
        {
            string apiClientURL = _apiConfigDetails.Url + $"/Road/{roadId}";
            if (!(string.IsNullOrEmpty(_apiConfigDetails.AppId) && string.IsNullOrEmpty(_apiConfigDetails.AppKey)))
            {
                apiClientURL= _apiConfigDetails.Url + $"/Road/{roadId}?app_id={_apiConfigDetails.AppId}&app_key={_apiConfigDetails.AppKey}";

            }
            return apiClientURL;
        }
    }
}
