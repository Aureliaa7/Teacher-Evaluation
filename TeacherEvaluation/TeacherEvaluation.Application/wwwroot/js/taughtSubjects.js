// this is used for a logged in teacher
function get_taught_subject_types_by_subject() {
    var search_details = {
        subjectId: $("#subject").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../TaughtSubjects/TaughtSubjectTypeRetrieval",

        success: function (result) {
            $("#type option").remove();
            $.each(result, function (index, item) {
                var text = "";
                if (item.type == '0') {
                    text = "Course"
                }
                else {
                    text = "Laboratory"
                }
                $("#type").append('<option value="' + item.type + '">' + text + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_taught_subjects_by_teacher_id() {
    var search_details = {
        teacherId: $("#teacher-field").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/TaughtSubjects/TaughtSubjectsByTeacherId",

        success: function (result) {
            $("#selected-subject-field option").remove();
            $("#selected-subject-field").append('<option value="' + "default" + '">' + "Please select" + '</option>');
            $("#selected-subject-field").append('<option value="' + "All" + '">' + "All" + '</option>');
            $.each(result, function (index, item) {
                $("#selected-subject-field").append('<option value="' + index + '">' + item + '</option>');
            });
            $("#selected-subject-field").val("default");
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_assigned_subjects_by_teacher_id() {
    var search_details = {
        teacherId: $("#teacher-field").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/TaughtSubjects/Index?handler=ReturnAssignedSubjectsByTeacherId",

        success: function (result) {
            $("#subject option").remove();
            $("#subject").append('<option value="' + "default" + '">' + "Please select" + '</option>');
            $.each(result, function (index, item) {
                $("#subject").append('<option value="' + index + '">' + item + '</option>');
            });
            $("#subject").val("default");
        },
        error: function () {
            console.log("Couldn't get the assigned subjects..");
        }
    });
}

function get_taught_subject_types_by_teacher_and_subject() {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        subjectId: $("#subject").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../TaughtSubjects/TaughtSubjectTypeRetrieval?handler=ReturnTaughtSubjectTypes",

        success: function (result) {
            $("#type option").remove();
            $.each(result, function (index, item) {
                var text = "";
                if (item == '0') {
                    text = "Course"
                }
                else {
                    text = "Laboratory"
                }
                $("#type").append('<option value="' + item + '">' + text + '</option>');
            });
        },
        error: function () {
            console.log("Could not retrieve the taught subject types...");
        }
    });
}
