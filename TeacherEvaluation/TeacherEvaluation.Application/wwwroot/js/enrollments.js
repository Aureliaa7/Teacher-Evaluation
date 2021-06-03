function get_enrolled_students() {
    var search_details = {
        subjectId: $("#subject").val(),
        type: $("#type").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Enrollments/Index?handler=ReturnEnrolledStudents",

        success: function (result) {
            $("#student option").remove();
            $.each(result, function (index, item) {
                var fullNameArray = [item.user.firstName, item.user.fathersInitial, item.user.lastName];
                var fullName = fullNameArray.join(" ");
                $("#student").append('<option value="' + item.id + '">' + fullName + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function check_enrollment_existence_by_subject_and_student() {
    var enrollment_details = {
        studentId: $("#student").val(),
        subjectId: $("#subject").val(),
        type: $("#type").val(),
    };
    console.log(enrollment_details);
    $.ajax({
        type: "GET",
        data: enrollment_details,
        url: "../Enrollments/Index?handler=CheckEnrollmentExistenceBySubjectStudent",

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

function check_enrollment_existence_by_student_subject_teacher_type() {
    var student_id = { id: $("#student").val() };
    var subject_id = { id: $("#subject-id-field").val() };
    var teacher_id = { id: $("#teacher-id-field").val() };
    var subject_type = { id: $("#type-id-field") };

    var enrollment_details = {
        studentId: $("#student").val(),
        subjectId: $("#subject-id-field").val(),
        teacherId: $("#teacher-id-field").val(),
        type: $("#type-id-field").val(),
    };
    console.log(enrollment_details);
    $.ajax({
        type: "GET",
        data: enrollment_details,
        url: "../Enrollments/Index?handler=CheckEnrollmentExistenceByStudentSubjectTeacherType",

        success: function (result) {
            $("#info-field").val(result);
            console.log(result);
            if (result == "The enrollment is not available") {
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


function update_enrollment_info() {
    get_teachers_by_subject_and_type();
    check_enrollment_existence_by_student_subject_teacher_type();
}