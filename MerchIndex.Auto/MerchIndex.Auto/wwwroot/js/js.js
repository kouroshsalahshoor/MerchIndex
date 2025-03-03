//close navbar when click on nav - link
window.CloseMenu = () => {
    const menuToggle = document.getElementById('navbarSupportedContent');
    const bsCollapse = new bootstrap.Collapse(menuToggle, { toggle: false });
    bsCollapse.toggle();
}

//window.CloseMenu = () => {
//    const navLinks = document.querySelectorAll('.nav-item')
//    const menuToggle = document.getElementById('navbarSupportedContent')
//    const bsCollapse = new bootstrap.Collapse(menuToggle, { toggle: false })
//    navLinks.forEach((l) => {
//        l.addEventListener('click', () => { bsCollapse.toggle() })
//    })
//}

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

//https://stackoverflow.com/questions/55186784/scroll-to-specified-part-of-page-when-clicking-top-navigation-link-in-blazor
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

//scroll button
//https://mdbootstrap.com/snippets/standard/mdbootstrap/2964350#js-tab-view
let mybutton = document.getElementById("btn-back-to-top");

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (
        document.body.scrollTop > 200 ||
        document.documentElement.scrollTop > 200
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}