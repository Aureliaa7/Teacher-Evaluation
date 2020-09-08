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
        url: "../Register/Student?handler=ReturnDomains",

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

function get_specializations() {
    var search_details = {
        studyDomainId: $("#domain").val()
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "../Register/Student?handler=ReturnSpecializations",

        success: function (result) {
            $("#specialization option").remove();
            console.log(result);
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

function get_study_programmes() {
    $.ajax({
        type: "GET",
        dataType: 'json',
        contextType: 'application/json',
        url: "../Students/Edit?handler=ReturnStudyProgrammes",

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