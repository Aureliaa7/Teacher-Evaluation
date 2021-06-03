function subject_assignment_is_possible() {
    var subject_assignment_details = {
        teacherId: $("#teacher-field").val(),
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };

    $.ajax({
        type: "GET",
        data: subject_assignment_details,
        url: "../TaughtSubjects/Create?handler=UpdateSubjectAssignmentInfoField",
 
        success: function (result) {
            $("#info-field").val(result);
            if (result == "This assignment already exists") {
                $("#submit-button").attr("disabled", "disabled");
            }
            else {
                $("#submit-button").removeAttr("disabled");
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

