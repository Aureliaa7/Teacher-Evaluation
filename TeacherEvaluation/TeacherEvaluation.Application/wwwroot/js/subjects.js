function get_subjects_by_student() {
    var search_details = {
        studentId: $("#student").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Subjects/Index?handler=ReturnSubjectsByStudent",

        success: function (result) {
            $("#subject option").remove();
            $.each(result, function (index, item) {
                $("#subject").append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_subjects_by_specialization_and_study_year() {
    var search_details = {
        specializationId: $("#specialization").val(),
        studyYear: $("#study-year").val(),
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Subjects/Index?handler=ReturnSubjectsBySpecializationAndStudyYear",

        success: function (result) {
            $("#subject-id-field option").remove();
            $.each(result, function (index, item) {
                $("#subject-id-field").append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}