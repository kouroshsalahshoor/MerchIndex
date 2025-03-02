window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message);
    }
    if (type === "error") {
        toastr.error(message);
    }
    if (type === "warning") {
        toastr.warning(message);
    }
    if (type === "info") {
        toastr.error(message);
    }
}

function ScrollToTop() {
    var element = document.getElementById("top");
    element.scrollIntoView({
        behavior: 'smooth'
    });
}
function ScrollTo(elementId) {
    var element = document.getElementById(elementId);
    element.scrollIntoView({
        behavior: 'smooth'
    });
}