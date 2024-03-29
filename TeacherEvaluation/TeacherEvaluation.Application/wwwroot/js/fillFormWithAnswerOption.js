﻿function get_teacher() {
    var search_details = {
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };

    $.ajax({
        type: "GET",
        data: search_details,
        url: "/EvaluationForms/EvaluateTeacher?handler=ReturnTeacher",

        success: function (result) {
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
    var search_details = {
        subjectId: $("#subject-field").val(),
        type: $("#type-field").val(),
    };

    $.ajax({
        type: "GET",
        data: search_details,
        url: "/EvaluationForms/EvaluateTeacher?handler=EnableOrDisableSubmitBtn",

        success: function (result) {
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