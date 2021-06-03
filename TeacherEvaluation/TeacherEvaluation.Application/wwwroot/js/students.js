function get_students_by_specialization_and_study_year() {
    var search_details = {
        specializationId: $("#specialization").val(),
        studyYear: $("#study-year").val(),
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/Students/Index?handler=ReturnStudents",

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