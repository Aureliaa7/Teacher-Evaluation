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
    clear();

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/AllResponses/Responses/ViewResponsesBase?handler=RetrieveResponses",

        success: function (result) {
            console.log("*** responses: ", result);
            if (Object.keys(result).length > 0) {

                console.log("if");

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
                th1.innerHTML = "Response";
                var th2 = document.createElement("th");
                th2.innerHTML = "View";
                var th3 = document.createElement("th");
                th3.innerHTML = "No. Attendances";
                var th4 = document.createElement("th");
                th4.innerHTML = "Grade";
                tr.appendChild(th1);
                tr.appendChild(th2);
                tr.appendChild(th3);
                tr.appendChild(th4);
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
                    var href = "./OneResponse?enrollmentId=" + value.enrollmentId + "&formId=" + search_details.formId;
                    anchor.href = href;
                    anchor.setAttribute("asp-route-enrollmentId", value.enrollmentId);
                    anchor.setAttribute("asp-route-formId", search_details.formId);
                    anchor.text = "View";
                    newTd2.appendChild(anchor);

                    var newTd3 = document.createElement("td");
                    newTd3.innerHTML = get_attendances_interval();

                    var newTd4 = document.createElement("td");
                    newTd4.innerHTML = get_grade_interval(value.grade);

                    newTr.appendChild(newTd1);
                    newTr.appendChild(newTd2);
                    newTr.appendChild(newTd3);
                    newTr.appendChild(newTd4);
                    tbody.appendChild(newTr);
                });

                tableElement.appendChild(tbody);
                divRowElement.appendChild(tableElement);

                var mainElement = document.getElementById(layoutId);
                mainElement.appendChild(divRowElement);
            }
            else {
                console.log("else");

                var h4 = document.createElement("h4");
                h4.innerText = "No data is available";
                h4.id = "noDataAvailableH4";
                h4.setAttribute("style", "margin-top: 100px; margin-left: 100px;");
                var mainElement = document.getElementById(layoutId);
                mainElement.appendChild(h4); 
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function update() {
    clear();
    $("#selected-subject-field").val("default");
    get_taught_subjects_by_teacher_id();
}

function clear() {
    var oldTable = document.getElementById("responses-table");
    if (oldTable != null) {
        oldTable.remove();
    }
    var noDataAvailableHeader = document.getElementById("noDataAvailableH4");
    if (noDataAvailableHeader != null) {
        noDataAvailableHeader.remove();
    }
}


//how do i know the total number of attendances for each subject?
// maybe when adding a new subject, the admin should also provide the total number of attendances
// because for instance, some laboratories are held once at every 2 weeks
//TODO this approach needs validation. Until then return a dummy string
function get_attendances_interval() {
    return "dummy";
}

function get_grade_interval(grade) {
    if (grade == 5 || grade == 6) {
        return "5-6";
    }
    else if (grade == 7 || grade == 8) {
        return "7-8";
    }
    else if (grade == 9 || grade == 10) {
        return "9-10";
    }
    return "<5";
}