function get_top_teachers() {
    var search_details = {
        questionId: $("#question-field").val(),
        rankingType: $("#ranking-type-field").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/Ranking/View?handler=ReturnTopTeachers",

        success: function (result) {
            console.log("*** top teachers: ", result);
            //TODO display the top teachers
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}
