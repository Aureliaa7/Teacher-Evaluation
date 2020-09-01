var seconds = 3; // seconds for HTML
var foo; // variable for clearInterval() function

function redirect() {
    document.location.href = '../Account/Login/';
}

function updateSecs() {
    document.getElementById("seconds").innerHTML = seconds;
    seconds--;
    if (seconds == -1) {
        clearInterval(foo);
        redirect();
    }
}

function countdownTimer() {
    foo = setInterval(function () {
        updateSecs()
    }, 1000);
}