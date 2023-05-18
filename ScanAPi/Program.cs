namespace ScanAPi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var app = builder.Build();

			app.MapGet("/v1/scans/scanevents", () => File.ReadAllText("scanevent.json"));

			app.Run();
		}
	}
}