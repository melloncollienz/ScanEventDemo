using NLog;
using ScanEventDemo.Models.Api;
using ScanEventDemo.Models.Datastore;

namespace ScanEventDemo
{
	internal class Program
	{

		//How far should I be going for this, as I would have put in dependency injection, which also allows
		//much simpler unit testing as well
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private static DataStore _store = new DataStore();
		private static ApiClient _client = new ApiClient();
		private static int _lastEventId = -1;	

        public Program()
        {
			    
        }

		//This hasn't been plumbed through properly as based on the question, it seems like this is supposed
		//to be a windows service, and for simplicity, I've used a console app but have the relevant functions
		//that should be called from the relevant windows service events
		//There should also be logging that goes into the windows event log
        static void Main(string[] args)
		{
			OnStart();
			ServiceEvent().Wait();
			OnStop();
			
		}

		public static void OnStart()
		{
			logger.Info("Service Starting");
			_lastEventId = _store.GetLastEventId();
		}

		public static void OnStop()
		{
			logger.Info("Service Stopping");
			_store.SaveLastEventId(_lastEventId);
		}

		public static async Task ServiceEvent() 
		{
			logger.Info("Service event triggered");
			var events = await _client.GetScanEvents(_lastEventId);
			_store.SaveEvents(events.Select(t => ToScanEventData(t)).ToList());
			_lastEventId = events.Max(t => t.EventId);
		}

		//ideally this should be an extension method
		public static ScanEventData ToScanEventData(ParcelEvent parcelEvent)
		{
			return new ScanEventData()
			{
				EventId = parcelEvent.EventId,
				ParcelId = parcelEvent.ParcelId,
				CreatedDateTimeUtc = parcelEvent.CreatedDateTimeUtc,
				RunId = parcelEvent.User.RunId,
				StatusCode = parcelEvent.StatusCode,
				Type = parcelEvent.Type
			};
		}
	}
}