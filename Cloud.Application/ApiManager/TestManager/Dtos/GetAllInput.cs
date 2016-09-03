using Cloud.Framework;

namespace Cloud.ApiManager.TestManager.Dtos
{
    public class GetAllInput : PageIndex
    {
        public string Id { get; set; }

        public bool State { get; set; }
    }
}