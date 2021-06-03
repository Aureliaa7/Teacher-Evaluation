function get_specializations() {
    var search_details = {
        studyDomainId: $("#domain").val()
    };

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/Specializations/Index",

        success: function (result) {
            $("#specialization option").remove();

            $.each(result, function (index, item) {
                console.log("specialization: " + item);
                $("#specialization").append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}