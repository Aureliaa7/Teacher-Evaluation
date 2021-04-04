function get_responses(formID) {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        formId: formID
    };
    console.log(search_details);

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/EvaluationForms/ViewResponses?handler=RetrieveResponses",

        success: function (result) {
            google.charts.load("current", { packages: ["corechart"] });
            var contor = 1;
            for (var key in result) {
                google.charts.setOnLoadCallback(function () { draw_charts(key, result[key], contor); });
                contor++;
            }

            console.log("*** responses: ", result);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function draw_charts(questionText, dictionary, contor) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'question');
    data.addColumn('number', 'number of answers');
    data.addRows(6);
   
    var contor = 0; 
    for (var key in dictionary) {
        var numberOfAnswers = dictionary[key];
        data.setCell(contor, 0, key);
        data.setCell(contor, 1, numberOfAnswers);
        contor++;
    }

    var options = {
        title: questionText,
        is3D: true,
    };

    var id = 'question'.concat(contor.toString());
    console.log("id: ", id);
    var chart = new google.visualization.PieChart(document.getElementById(id));
    chart.draw(data, options);
}