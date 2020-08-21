function get_subjects() {
    var search_details = {
        studentId: $("#student-field").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Grades/Edit?handler=ReturnSubjects",

        success: function (result) {
            console.log(result);
            $.each(result, function (index, item) {
                console.log("enrollment id: " + item.id);
                console.log("subject id: " + item.taughtSubject.subject.id);
                console.log(item.taughtSubject.subject.name);
                $("#subject-field").append('<option value="' + item.taughtSubject.subject.id + '">' + item.taughtSubject.subject.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function check_enrollment_existence() {
    var enrollment_details = {
        studentId: $("#student-field").val(),
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };
    console.log(enrollment_details);
    $.ajax({
        type: "GET",
        data: enrollment_details,
        url: "../Grades/Edit?handler=CheckEnrollmentExistence",

        success: function (result) {
            console.log(result);
            if (result == "The enrollment exists") {
                $("#submit-button").removeAttr("disabled");
            }
            else {
                $("#submit-button").attr("disabled", "disabled");
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}