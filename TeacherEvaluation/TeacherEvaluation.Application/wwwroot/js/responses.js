function get_responses(formID, layoutID) {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        formId: formID,
        taughtSubjectId: $("#selected-subject-field").val()
    };
    console.log(search_details);

    create_table(search_details, layoutID);
}

function update_subjects_drop_down() {
    var subjectsDropDown = document.getElementById("selected-subject-field");
    var selectedValue = subjectsDropDown.options[subjectsDropDown.selectedIndex].value;
    if (selectedValue == "Please select") {
        get_taught_subjects_by_teacher_id();
    }
}

function create_table(search_details, layoutId) {
    remove_table();

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/AllResponses/Responses/ViewResponsesBase?handler=RetrieveResponses",

        success: function (result) {
            console.log("*** responses: ", result);
            if (Object.keys(result).length > 0) {
                var divRowElement = document.createElement('div');
                divRowElement.className = "row";
                divRowElement.id = "responses-table";
                var tableElement = document.createElement("table");
                tableElement.className = "table table-dark table-striped table-bordered";
                tableElement.style.width = "650px";
                tableElement.style.marginLeft = "180px";
                tableElement.style.marginTop = "40px";
                var thead = document.createElement("thead");
                var tr = document.createElement("tr");
                var th1 = document.createElement("th");
                var th2 = document.createElement("th");
                tr.appendChild(th1);
                tr.appendChild(th2);
                thead.appendChild(tr);
                tableElement.appendChild(thead);

                var tbody = document.createElement("tbody");

                Object.entries(result).forEach(([key, value]) => {
                    console.log(key, value);
                    var newTr = document.createElement("tr");
                    var newTd1 = document.createElement("td");
                    newTd1.innerHTML = key;

                    var newTd2 = document.createElement("td");
                    var anchor = document.createElement("a");
                    var href = "./OneResponse?enrollmentId=" + value + "&formId=" + search_details.formId;
                    anchor.href = href;
                    anchor.setAttribute("asp-route-enrollmentId", value);
                    anchor.setAttribute("asp-route-formId", search_details.formId);
                    anchor.text = "View";
                    newTd2.appendChild(anchor);
                    newTr.appendChild(newTd1);
                    newTr.appendChild(newTd2);
                    tbody.appendChild(newTr);
                });

                tableElement.appendChild(tbody);
                divRowElement.appendChild(tableElement);

                var mainElement = document.getElementById(layoutId);
                mainElement.appendChild(divRowElement);
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function update() {
    remove_table();
    $("#selected-subject-field").val("default");
    get_taught_subjects_by_teacher_id();
}

function remove_table() {
    var oldTable = document.getElementById("responses-table");
    if (oldTable != null) {
        oldTable.remove();
    }
}