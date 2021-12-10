let _videoTrack = null;
let _activeRoom = null;
let _participants = new Map();
let _dominantSpeaker = null;

async function getVideoDevices() {
    try {
        let devices = await navigator.mediaDevices.enumerateDevices();
        if (devices.every(d => !d.label)) {
            await navigator.mediaDevices.getUserMedia({
                video: true
            });
        }

        devices = await navigator.mediaDevices.enumerateDevices();
        if (devices && devices.length) {
            const deviceResults = [];
            devices.filter(device => device.kind === 'videoinput')
                .forEach(device => {
                    const { deviceId, label } = device;
                    deviceResults.push({ deviceId, label });
                });

            return deviceResults;
        }
    } catch (error) {
        console.log(error);
    }

    return [];
}

async function startVideo(deviceId, selector) {
    const cameraContainer = document.querySelector(selector);
    if (!cameraContainer) {
        return;
    }

    try {
        if (_videoTrack) {
            _videoTrack.detach().forEach(element => element.remove());
        }

        _videoTrack = await Twilio.Video.createLocalVideoTrack({ deviceId });
        const videoEl = _videoTrack.attach();
        cameraContainer.append(videoEl);

    } catch (error) {
        console.log(error);
    }
}

window.onload = function () {
    navigator.mediaDevices.addEventListener('devicechange', function (event) {
        DotNet.invokeMethodAsync('WebClient', 'UpdateDevices');
    });
}