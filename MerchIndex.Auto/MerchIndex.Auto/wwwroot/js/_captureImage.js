var video = document.getElementById('video');
var canvas = document.getElementById('canvas');
var context = canvas.getContext('2d');

navigator.mediaDevices.getUserMedia({ video: true })
    .then(function (stream) {
        video.srcObject = stream;
        video.play();
    })
    .catch(function (err) {
        console.log("An error occurred: " + err);
    });

document.getElementById("captureButton").addEventListener("click", function () {
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    var image = canvas.toDataURL("image/png");
    uploadImage(image);
}

function uploadImage(image) {
        fetch("/api/uploadimage", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ image: image })
        })
            .then(function (response) {
                return response.json();
            }).then(function (data) {
                console.log(data);
            })
            .catch(function (err) {
                console.log("An error occurred: " + err);
            });
    }