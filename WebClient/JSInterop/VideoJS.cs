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

        public ValueTask<bool> CreateOrJoinRoomAsync(
            string roomName,
            string token)
        {
            return jsRuntime?.InvokeAsync<bool>(
                "createOrJoinRoom",
                roomName, token) ?? new ValueTask<bool>(false);
        }

        public ValueTask LeaveRoomAsync()
        {
            return jsRuntime?.InvokeVoidAsync(
                "leaveRoom") ?? new ValueTask();
        }
    }
}
