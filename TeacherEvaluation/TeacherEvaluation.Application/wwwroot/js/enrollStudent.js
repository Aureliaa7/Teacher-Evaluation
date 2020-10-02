﻿function check_enrollment_availability() {
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
        url: "../Enrollments/Create?handler=CheckEnrollmentAvailability",

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
    check_enrollment_availability();
}

function get_students_by_specialization_and_study_year() {
    var specialization_id = { id: $("#specialization").val() };
    var study_year = { id: $("#study-year") };

    var search_details = {
        specializationId: $("#specialization").val(),
        studyYear: $("#study-year").val(),
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/Enrollments/Create?handler=ReturnStudents",

        success: function (result) {
            $("#student option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log(item.id);
                var fullNameArray = [item.user.firstName, item.user.fathersInitial, item.user.lastName];
                var fullName = fullNameArray.join(" ");
                console.log(fullName);
                $("#student").append('<option value="' + item.id + '">' + fullName + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}