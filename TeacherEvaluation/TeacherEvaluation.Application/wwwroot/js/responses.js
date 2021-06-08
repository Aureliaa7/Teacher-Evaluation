function get_responses(formID, layoutID) {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        formId: formID,
        taughtSubjectId: $("#selected-subject-field").val()
    };

    create_responses_table(search_details, layoutID);
}

function update_subjects_drop_down() {
    var subjectsDropDown = document.getElementById("selected-subject-field");
    var selectedValue = subjectsDropDown.options[subjectsDropDown.selectedIndex].value;
    if (selectedValue == "Please select") {
        get_taught_subjects_by_teacher_id();
    }
}

function create_responses_table(search_details, layoutId) {
    clear();

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/AllResponses/Responses/ViewResponsesBase?handler=RetrieveResponses",

        success: function (result) {
            if (Object.keys(result).length > 0) {
                create_empty_table(layoutId);
                var dataSet = create_dataset(result, search_details.formId);

                $('#responses').DataTable({
                    data: dataSet,
                    columns: [
                        { title: "Response" },
                        { title: "Attendances" },
                        { title: "Grade" }
                    ]
                });
            }
            else {
                display_unavailable_data_message(layoutId);
            }
        },
        error: function () {
            console.log("Could not create the responses table...");
        }
    });
}

function update() {
    clear();
    $("#selected-subject-field").val("default");
    get_taught_subjects_by_teacher_id();
}

function clear() {
    var oldTable = document.getElementById("responses-div");
    if (oldTable != null) {
        oldTable.remove();
    }

    remove_unavailable_data_message();
}

function get_attendances_interval(maxNoAttendances, noAttendances) {
    var percentage = noAttendances / maxNoAttendances * 100;
    if (percentage >= 25 && percentage < 50) {
        return "25-50%";
    }
    else if (percentage >= 50 && percentage < 75) {
        return "50-75%";
    }
    else if (percentage >= 75 && percentage <= 100) {
        return "75-100%";
    }
    return "0-25%";
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

function create_dataset(result, formId) {
    var dataset = [];
    var i = 0;
    Object.entries(result).forEach(([key, value]) => {
        var text = "Response " + (i + 1);
        var responseLink = '<a href="./OneResponse?enrollmentId=' + value.enrollmentId + "&formId=" + formId +'">' + text + '</a>';
        var noAttendances = get_attendances_interval(value.maxNoAttendances, value.noAttendances);
        var grade = get_grade_interval(value.grade);

        dataset[i] = [responseLink, noAttendances, grade];
        i++;
    });

    return dataset;
}

function create_empty_table(layoutId) {
    var divRowElement = document.createElement('div');
    divRowElement.className = "row";
    divRowElement.id = "responses-div";
    var tableElement = document.createElement("table");
    tableElement.className = "table table-dark table-striped table-bordered";
    tableElement.id = "responses";
    tableElement.style.width = "750px";
    tableElement.style.marginLeft = "190px";
    divRowElement.appendChild(tableElement);

    var mainElement = document.getElementById(layoutId);
    mainElement.appendChild(divRowElement);
}