function showLoadingScreen() {
    window.scrollTo(0, 0);  //
    var over = document.createElement("div");
    over.id = "Loader";
    over.classList.add("overlay");

    var d = document.createElement("div");
    d.classList.add("loader");
    over.appendChild(d);

    document.body.appendChild(over);

    // lock scroll
    document.body.style['overflow'] = 'hidden';
    document.body.style['height'] = '100%';
};

function removeLoadingScreen() {
    var d = document.getElementById("Loader");
    document.body.removeChild(d);

    // unlock scroll

    $('html, body').css({
        'overflow': 'auto',
        'height': 'auto'
    });
}