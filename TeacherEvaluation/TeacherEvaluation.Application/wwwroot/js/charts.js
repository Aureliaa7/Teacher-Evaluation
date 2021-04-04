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
            google.charts.load('current', {
                callback: function () {
                    var data = [];
                    var options = [];
                    var divIds = [];

                    // prepare the data for charts
                    var contor = 0;
                    for (var key in result) {
                        data[contor] = new google.visualization.DataTable();
                        data[contor].addColumn('string', 'question');
                        data[contor].addColumn('number', 'number of answers');
                        data[contor].addRows(6);

                        var contor2 = 0;
                        var dictionary = result[key]
                        for (var key2 in dictionary) {
                            var numberOfAnswers = dictionary[key2];
                            data[contor].setCell(contor2, 0, key2);
                            data[contor].setCell(contor2, 1, numberOfAnswers);
                            contor2++;
                        }

                        options[contor] = {
                            title: key, // this is the question
                            is3D: true,
                        };
                        divIds[contor] = "question" + (contor + 1);

                        contor++;

                    }

                    // draw the charts
                    for (var contor = 0; contor < 10; contor++) {
                        var chart = new google.visualization.PieChart(document.getElementById(divIds[contor]));
                        chart.draw(data[contor], options[contor]);
                    }
                },
                packages: ['corechart']
            });

            console.log("*** responses: ", result);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}
