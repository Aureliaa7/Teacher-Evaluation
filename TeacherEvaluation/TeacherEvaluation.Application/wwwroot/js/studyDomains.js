function get_domains() {
    var search_details = {
        studyProgramme: $("#study-programme").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../StudyDomains/Index",

        success: function (result) {
            $("#domain option").remove();
            console.log(result);
            $.each(result, function (index, item) {
                console.log("domain: " + item);
                $("#domain").append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}