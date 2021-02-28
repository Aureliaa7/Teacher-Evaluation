﻿function get_study_programmes() {
    $.ajax({
        type: "GET",
        dataType: 'json',
        contextType: 'application/json',
        url: "../StudyProgrammes/Index",

        success: function (result) {
            $("#study-programme option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("study-programme: " + item);
                $("#study-programme").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}