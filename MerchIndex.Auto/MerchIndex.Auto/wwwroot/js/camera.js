window.captureImage = {
    startCamera: function (videoElementId) {
        const video = document.getElementById(videoElementId);
        navigator.mediaDevices.getUserMedia({ video: true })
            .then((stream) => {
                video.srcObject = stream;
                video.play();
            })
            .catch((err) => {
                console.error('Error accessing the camera: ', err);
            });
    },
    captureImage: function (videoElementId, canvasElementId, hiddenInputId) {
        const video = document.getElementById(videoElementId);
        const canvas = document.getElementById(canvasElementId);
        const hiddenInput = document.getElementById(hiddenInputId);
        const context = canvas.getContext('2d');
        context.drawImage(video, 0, 0, canvas.width, canvas.height);
        hiddenInput.value = canvas.toDataURL('image/png');

        const image = document.getElementById('image');
        image.src = canvas.toDataURL('image/png');
    }
};
