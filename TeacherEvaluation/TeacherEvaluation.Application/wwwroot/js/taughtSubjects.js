function get_taught_subject_types_by_subject() {
    var search_details = {
        subjectId: $("#subject").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../TaughtSubjects/TaughtSubjectTypeRetrieval",

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