function check_enrollment_existence() {
    var student_id = { id: $("#student-field").val() };
    var subject_id = { id: $("#subject-field").val() };
    var teacher_id = { id: $("#teacher-field").val() };
    var subject_type = { id: $("#type-field") };

    var enrollment_details = {
        studentId: $("#student-field").val(),
        subjectId: $("#subject-field").val(),
        teacherId: $("#teacher-field").val(),
        type: $("#type-field").val(),
    };
    console.log(enrollment_details);
    $.ajax({
        type: "GET",
        data: enrollment_details,
        url: "../Enrollments/Create?handler=CheckEnrollmentExistence",

        success: function (result) {
            $("#info-field").val(result);
            console.log(result);
            if (result == "The enrollment already exists") {
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

function get_teachers() {
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
        dataType: 'json',
        contextType: 'application/json',
        url: "../Enrollments/Create?handler=ReturnTeachers",

        success: function (result) {
            console.log(result);
            $.each(result, function (index, item) {
                console.log(item.id);
                var fullNameArray = [item.user.firstName , item.user.fathersInitial , item.user.lastName];
                var fullName = fullNameArray.join(" ");
                console.log(fullName);
                $("#teacher-field").append('<option value="' + item.id + '">' + fullName + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function update_data() {
    get_teachers();
    check_enrollment_existence();
}