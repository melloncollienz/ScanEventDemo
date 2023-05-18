using Microsoft.AspNetCore.WebUtilities;
using NLog;
using NLog.LayoutRenderers.Wrappers;
using ScanEventDemo.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScanEventDemo
{
	internal class ApiClient
	{
        private static string scanEventUrl = "https://localhost:7117/v1/scans/scanevents";
        private HttpClient client = new HttpClient();
        private static Logger logger = LogManager.GetCurrentClassLogger();

		public ApiClient()
        {
            //todo: initialization stuff for the apiclient

        }

        public async Task<List<ParcelEvent>> GetScanEvents(int fromEvent = 1, int limit = 100)
        {
            try
            {
				Dictionary<string, string> parameters = new Dictionary<string, string>
				{
					{ "fromEvent", fromEvent.ToString() },
					{ "limit", limit.ToString() }
				};

				var response = await Get<ScanEventsResponse>(scanEventUrl, parameters);
                return response.ScanEvents ?? new List<ParcelEvent>();
			}
            catch (Exception ex) 
            {
                //todo: is the payload data safe to log?
                logger.Error(ex, $"Exception in {nameof(GetScanEvents)}");
            }
            return new List<ParcelEvent>();

        }

        public async Task<TResult> Get<TResult>(string uri, Dictionary<string, string> parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(uri)) 
                {
                    throw new ArgumentNullException("uri");
                }

				if (parameters != null)
				{
					uri = QueryHelpers.AddQueryString(uri, parameters);
				}
				//var result = await client.GetAsync(uri);
								
                var result = await client.GetFromJsonAsync<TResult>(uri);
				return result;
			} 
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

    }

}
