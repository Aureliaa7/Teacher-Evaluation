function check_enrollment_existence() {
    var student_id = { id: $("#student-id-field").val() };
    var subject_id = { id: $("#subject-id-field").val() };
    var teacher_id = { id: $("#teacher-id-field").val() };
    var subject_type = { id: $("#type-id-field") };

    var enrollment_details = {
        studentId: $("#student-id-field").val(),
        subjectId: $("#subject-id-field").val(),
        teacherId: $("#teacher-id-field").val(),
        type: $("#type-id-field").val(),
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
                $("#enroll-button").attr("disabled", "disabled");
            }
            else {
                $("#enroll-button").removeAttr("disabled");
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_teachers() {
    var subject_id = { id: $("#subject-id-field").val() };
    var subject_type = { id: $("#type-id-field") };

    var search_details = {
        subjectId: $("#subject-id-field").val(),
        type: $("#type-id-field").val(),
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Enrollments/Create?handler=ReturnTeachers",

        success: function (result) {
            $("#teacher-id-field option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log(item.id);
                var fullNameArray = [item.user.firstName , item.user.fathersInitial , item.user.lastName];
                var fullName = fullNameArray.join(" ");
                console.log(fullName);
                $("#teacher-id-field").append('<option value="' + item.id + '">' + fullName + '</option>');
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