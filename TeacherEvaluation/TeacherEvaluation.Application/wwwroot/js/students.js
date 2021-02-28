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
        url: "/Students/Index?handler=ReturnStudents",

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