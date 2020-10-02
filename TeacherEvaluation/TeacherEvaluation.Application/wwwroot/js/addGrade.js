function get_subjects() {
    var search_details = {
        studentId: $("#student").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Grades/Edit?handler=ReturnSubjects",

        success: function (result) {
            $("#subject option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("subject id: " + item.id);
                console.log(item.name);
                $("#subject").append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function enrollment_exists() {
    var enrollment_details = {
        studentId: $("#student").val(),
        subjectId: $("#subject").val(),
        type: $("#type").val(),
    };
    console.log(enrollment_details);
    $.ajax({
        type: "GET",
        data: enrollment_details,
        url: "../Grades/Edit?handler=CheckEnrollment",

        success: function (result) {
            console.log(result);
            if (result == "The enrollment does not exist") {
                $("#grade-button").attr("disabled", "disabled");
            }
            else {
                $("#grade-button").removeAttr("disabled");
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

