function get_teacher() {
    var subject_id = { id: $("#subject-field").val() };
    var subject_type = { id: $("#type-field") };

    var search_details = {
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };
    console.log(search_details);
    $.ajax({
        type: "GET",
        data: search_details,
        url: "/EvaluationForms/EvaluateTeacher?handler=ReturnTeacher",

        success: function (result) {
            console.log(result);
            var fullNameArray = [result.user.firstName, result.user.fathersInitial, result.user.lastName];
            var fullName = fullNameArray.join(" ");
            $("#teacher-field").val(fullName);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function disable_or_enable_btn() {
    var subject_id = { id: $("#subject-field").val() };
    var subject_type = { id: $("#type-field") };

    var search_details = {
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };
    console.log(search_details);
    $.ajax({
        type: "GET",
        data: search_details,
        url: "/EvaluationForms/EvaluateTeacher?handler=EnableOrDisableSubmitBtn",

        success: function (result) {
            console.log(result);
            if (result == "disable") {
                $("#teacher-field").val('');
                $("#submit-form-btn").attr("disabled", "disabled");
                $('#myModal').modal('show');
            }
            else {
                $("#submit-form-btn").removeAttr("disabled");
                get_teacher();
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}
