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
            if (result == "This assignment already exists") {
                $("#submit-button").attr("disabled", "disabled");
            }
            else {
                $("#submit-button").removeAttr("disabled");
            }
            console.log(result);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_teachers_by_department() {
    var search_details = {
        department: $("#department-field").val(),
    };
    console.log(search_details);
    $.ajax({
        type: "GET",
        data: search_details,
        url: "../TaughtSubjects/Create?handler=ReturnTeachersByDepartment",

        success: function (result) {
            $("#teacher-field option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("teacher: " + item);
                var fullNameArray = [item.user.firstName, item.user.fathersInitial, item.user.lastName];
                var fullName = fullNameArray.join(" ");
                $("#teacher-field").append('<option value="' + item.id + '">' + fullName + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    })
}