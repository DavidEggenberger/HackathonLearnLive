using Microsoft.JSInterop;

namespace WebClient.JSInterop
{
    public class VideoJS
    {
        private IJSRuntime jsRuntime;
        public VideoJS(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

    }
}
