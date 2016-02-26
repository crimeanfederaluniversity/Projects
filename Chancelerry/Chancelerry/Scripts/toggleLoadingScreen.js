﻿function showLoadingScreen() {
    var over = document.createElement("div");
    over.id = "Loader";
    over.classList.add("overlay");

    var d = document.createElement("div");
    d.classList.add("loader");
    over.appendChild(d);

    document.body.appendChild(over);
};

function removeLoadingScreen() {
    var d = document.getElementById("Loader");
    document.body.removeChild(d);
}