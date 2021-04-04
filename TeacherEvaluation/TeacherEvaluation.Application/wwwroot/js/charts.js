function get_responses(formID) {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        formId: formID
    };
    console.log(search_details);

    // remove the old charts
    removeChartsDivs();

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
                    var drawCharts = "false";

                    // prepare the data for charts
                    var contor1 = 0;
                    for (var question in result) {
                        data[contor1] = new google.visualization.DataTable();
                        data[contor1].addColumn('string', 'question');
                        data[contor1].addColumn('number', 'number of answers');
                        data[contor1].addRows(10);

                        var contor2 = 0;
                        var optionAnswersWithNoAnswers = result[question];
                        console.log("count: ", Object.keys(optionAnswersWithNoAnswers).length);
                        if (Object.keys(optionAnswersWithNoAnswers).length > 0) {
                            for (var optionAnswer in optionAnswersWithNoAnswers) {
                                var numberOfAnswers = optionAnswersWithNoAnswers[optionAnswer];
                                data[contor1].setCell(contor2, 0, optionAnswer);
                                data[contor1].setCell(contor2, 1, numberOfAnswers);
                                contor2++;
                            }

                            options[contor1] = {
                                title: question,
                                is3D: true,
                            };
                            divIds[contor1] = "question" + (contor1 + 1);

                            contor1++;
                            drawCharts = "true";
                        }
                    }

                    createNewDivs();
                    if (drawCharts == "true") {
                        // draw the charts
                        for (var contor = 0; contor < 10; contor++) {
                            var div = document.getElementById(divIds[contor]);
                            div.setAttribute("style", "width: 420px; height: 420px;");
                            var chart = new google.visualization.PieChart(div);
                            chart.draw(data[contor], options[contor]);
                        }
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

function removeChartsDivs() {
    for (var contor = 1; contor < 11; contor+=2) {
        var chartDivRow = document.getElementById("row" + contor);
        chartDivRow.remove();
    }
}

function createNewDivs() {
    for (var contor = 1; contor < 11; contor+=2) {
        var divRowElement = document.createElement('div');
        divRowElement.className = "row";
        divRowElement.id = "row" + contor;
        var div1 = document.createElement('div');
        div1.id = "question" + contor;
        divRowElement.appendChild(div1);

        var div2 = document.createElement('div');
        div2.id = "question" + (contor+1);
        divRowElement.appendChild(div2);

        var mainElement = document.getElementById('mainElementDeanLayout');
        mainElement.appendChild(divRowElement);
    }
}