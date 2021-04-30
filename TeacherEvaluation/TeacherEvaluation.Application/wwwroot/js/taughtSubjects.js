function get_taught_subject_types_by_subject() {
    var search_details = {
        subjectId: $("#subject").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../TaughtSubjects/TaughtSubjectTypeRetrieval",

        success: function (result) {
            $("#type option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("type: " + item);
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

    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/TaughtSubjects/TaughtSubjectsByTeacherId",

        success: function (result) {
            $("#selected-subject-field option").remove();
            console.log(result);
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