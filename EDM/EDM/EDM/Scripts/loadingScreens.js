// SIMPLE LOADING SCREEN

function showSimpleLoadingScreen() {
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

function removeSimpleLoadingScreen() {
    var d = document.getElementById("Loader");
    document.body.removeChild(d);

    // unlock scroll
    document.body.style['overflow'] = 'auto';
    document.body.style['height'] = 'auto';
}


// LOADING SCREEN WITH TEXT

function showLoadingScreenWithText(text) {
    var over = document.createElement("div");
    over.id = "TextedLoader";
    over.classList.add("overlay");

    var loading_container = document.createElement("div");
    over.appendChild(loading_container);
    loading_container.classList.add("row");

    var d = document.createElement("div");
    d.classList.add("loader-with-text");
    loading_container.appendChild(d);

    var span = document.createElement("span");
    span.innerHTML = text;
    loading_container.appendChild(span);

    document.body.appendChild(over);

    document.body.style['overflow'] = 'hidden';
    document.body.style['height'] = '100%';
}

function removeLoadingSreenWithText() {
    var d = document.getElementById("TextedLoader");
    document.body.removeChild(d);

    document.body.style['overflow'] = 'auto';
    document.body.style['height'] = 'auto';
}