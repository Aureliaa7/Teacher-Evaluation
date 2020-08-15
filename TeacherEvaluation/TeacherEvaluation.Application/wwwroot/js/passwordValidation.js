var myInput = document.getElementById("psw");
var letter = document.getElementById("letter");
var capital = document.getElementById("capital");
var number = document.getElementById("number");
var specialChar = document.getElementById("specialChar");
var length = document.getElementById("length");

// When the user clicks on the password field, show the message box
myInput.onfocus = function () {
    document.getElementById("message").style.display = "block";
}

// When the user clicks outside of the password field, hide the message box
myInput.onblur = function () {
    document.getElementById("message").style.display = "none";
}

// When the user starts to type something inside the password field
myInput.onkeyup = function () {
    // Validate lowercase letters
    var lowerCaseLetters = /[a-z]/g;
    if (myInput.value.match(lowerCaseLetters)) {
        letter.classList.remove("invalid");
        letter.classList.add("validField");
    } else {
        letter.classList.remove("validField");
        letter.classList.add("invalid");
    }

    // Validate capital letters
    var upperCaseLetters = /[A-Z]/g;
    if (myInput.value.match(upperCaseLetters)) {
        capital.classList.remove("invalid");
        capital.classList.add("validField");
    } else {
        capital.classList.remove("validField");
        capital.classList.add("invalid");
    }

    // Validate numbers
    var numbers = /[0-9]/g;
    if (myInput.value.match(numbers)) {
        number.classList.remove("invalid");
        number.classList.add("validField");
    } else {
        number.classList.remove("validField");
        number.classList.add("invalid");
    }

    //Validate special characters
    var specialChars = /[#?!@$%^&*-]/g;
    if (myInput.value.match(specialChars)) {
        specialChar.classList.remove("invalid");
        specialChar.classList.add("validField");
    } else {
        specialChar.classList.remove("validField");
        specialChar.classList.add("invalid");
    }

    // Validate length
    if (myInput.value.length >= 8) {
        length.classList.remove("invalid");
        length.classList.add("validField");
    } else {
        length.classList.remove("validField");
        length.classList.add("invalid");
    }
}