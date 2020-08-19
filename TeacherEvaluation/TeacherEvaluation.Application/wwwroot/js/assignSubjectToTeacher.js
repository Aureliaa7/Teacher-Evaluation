function get_info() {
    var teacher_id = { id: $("#teacher-field").val() };
    var subject_id = { id: $("#subject-field").val() };
    var subject_type = { id: $("#type-field") };

    var subject_assignment_details = {
        teacherId: $("#teacher-field").val(),
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };
    console.log(subject_assignment_details);
    $.ajax({
        type: "GET",
        data: subject_assignment_details,
        url: "../TaughtSubjects/Create?handler=UpdateInfoField",
 
        success: function (result) {
            $("#info-field").val(result);
            console.log(result);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}