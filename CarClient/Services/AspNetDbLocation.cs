using System.Threading.Tasks;

namespace CarClient.Services
{
    // This class is used by the application to get the location of the AspNet.db used for authntication of users.
    public class AspNetDbLocation : IAspNetDbLocation
    {
	    public async Task<string> GetAspNetDbAsync()
	    {
			return await Utils.Get<string>("api/AspNetDb");
		}
	}
}
