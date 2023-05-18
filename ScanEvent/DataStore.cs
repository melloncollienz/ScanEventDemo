using NLog;
using ScanEventDemo.Models.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScanEventDemo
{
	public class DataStore
	{
        private string saveFileName = "scanevents.json";
        private string lastEventFileName = "lastEvent.txt";
		private static Logger logger = LogManager.GetCurrentClassLogger();

        //Depending on the business logic handling of the exceptions, rethrow as necessary
		public DataStore()
        {
            
        }

        public void SaveLastEventId(int eventId)
        {
            try
            {
				File.WriteAllText(lastEventFileName, eventId.ToString());
			} 
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
        }

        public int GetLastEventId()
        {
            try
            {
                var result = 0;
                if (File.Exists(lastEventFileName))
                {


                    var eventId = File.ReadAllText(lastEventFileName);
                    if (!int.TryParse(eventId, out result))
                    {
                        result = 0;
                    }
                }
				return result;

			}
			catch (Exception ex) 
            { 
                logger.Error(ex);
                return 0;
            }


        }

        public void SaveEvents(List<ScanEventData> events)
        {
            try
            {
                var currentData = new List<ScanEventData>();
                if (File.Exists(saveFileName)) {
					var content = File.ReadAllText(saveFileName);
                    if (!string.IsNullOrEmpty(content))
                    {
                        var deserialized = JsonSerializer.Deserialize<List<ScanEventData>>(content);
                        if (deserialized != null)
                        {
                            currentData.AddRange(deserialized);
                        }

                    }
				}

                currentData.AddRange(events);
                var serialized = JsonSerializer.Serialize(currentData);
                File.WriteAllText(saveFileName, serialized);    


            } catch (Exception e) 
            { 
                logger.Error(e);
            }
        }

        public List<ScanEventData> GetEvents() 
        { 
            try
            {
                if (!File.Exists(saveFileName)) 
                {
                    return new List<ScanEventData>();
                }

                var content = File.ReadAllText(saveFileName);
                return JsonSerializer.Deserialize<List<ScanEventData>>(content) ?? new List<ScanEventData>();


            }
            catch (Exception e)
            {
                logger.Error(e);
				return new List<ScanEventData>();
			}
        }
    }
}
