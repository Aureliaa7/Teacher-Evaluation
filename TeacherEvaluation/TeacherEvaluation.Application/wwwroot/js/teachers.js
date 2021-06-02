function get_teachers_by_department() {
    var search_details = {
        department: $("#department-field").val(),
    };
    console.log(search_details);
    $.ajax({
        type: "GET",
        data: search_details,
        url: "../Teachers/Index?handler=ReturnTeachersByDepartment",

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

function get_teachers_by_subject_and_type() {
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
        url: "../Teachers/Index?handler=ReturnTeachersBySubjectAndType",

        success: function (result) {
            $("#teacher-id-field option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log(item.id);
                var fullNameArray = [item.user.firstName, item.user.fathersInitial, item.user.lastName];
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