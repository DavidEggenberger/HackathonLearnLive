using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebClient.JSInterop
{
    public class VideoJS
    {
        private IJSRuntime jsRuntime;
        public VideoJS(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }
        public ValueTask<CameraDevice[]> GetVideoDevicesAsync()
        {
            return jsRuntime?.InvokeAsync<CameraDevice[]>(
                  "getVideoDevices") ?? new ValueTask<CameraDevice[]>();
        }

        public ValueTask StartVideoAsync(string deviceId, string selector)
        {
            return jsRuntime?.InvokeVoidAsync(
                "startVideo",
                deviceId, selector) ?? new ValueTask();
        }
    }
}
