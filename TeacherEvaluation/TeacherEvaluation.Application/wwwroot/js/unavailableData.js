function display_unavailable_data_message(layoutId) {
    var h4 = document.createElement("h4");
    h4.innerText = "No data is available";
    h4.id = "noDataAvailableH4";
    h4.setAttribute("style", "margin-top: 100px; margin-left: 435px;");
    var mainElement = document.getElementById(layoutId);
    mainElement.appendChild(h4); 
}

function remove_unavailable_data_message() {
    var noDataAvailableHeader = document.getElementById("noDataAvailableH4");
    if (noDataAvailableHeader != null) {
        noDataAvailableHeader.remove();
    }
}