//close navbar when click on nav - link
window.CloseMenu = () => {
    const menuToggle = document.getElementById('navbarSupportedContent');
    const bsCollapse = new bootstrap.Collapse(menuToggle, { toggle: false });
    bsCollapse.hide();
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

//scroll button
//https://mdbootstrap.com/snippets/standard/mdbootstrap/2964350#js-tab-view

//Get the button
let mybutton = document.getElementById("btn-back-to-top");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
    if (
        document.body.scrollTop > 400 ||
        document.documentElement.scrollTop > 400
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
};

//// When the user clicks on the button, scroll to the top of the document
mybutton.addEventListener("click", backToTop);

window.ScrollToTop = function () {
    backToTop();
}
function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

//Offcanvas
window.openOffCanvas = function () {
    var offcanvasNavbar = document.getElementById('offcanvasNavbar')
    var bsOffcanvas = new bootstrap.Offcanvas(offcanvasNavbar)
    bsOffcanvas.show()
}

window.closeOffCanvas = function () {
    var offcanvasNavbar = document.getElementById('offcanvasNavbar')
    var bsOffcanvas = new bootstrap.Offcanvas(offcanvasNavbar)
    bsOffcanvas.hide()
}