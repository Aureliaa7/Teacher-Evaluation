function get_taught_subject_types() {
    var search_details = {
        subjectId: $("#subject").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Attendances/Create?handler=ReturnTaughtSubjectTypes",

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

function get_enrolled_students() {
    var search_details = {
        subjectId: $("#subject").val(),
        type: $("#type").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Attendances/Create?handler=ReturnEnrolledStudents",

        success: function (result) {
            $("#student option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("student: " + item);
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