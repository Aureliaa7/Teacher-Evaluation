﻿function get_responses(formID, layoutID) {
    var search_details = {
        teacherId: $("#teacher-field").val(),
        formId: formID,
        taughtSubjectId: $("#selected-subject-field").val()
    };

    console.log(search_details);
    remove_tag_cloud();
    create_charts(search_details, layoutID);

    setTimeout(function () {
        create_tag_cloud(search_details, layoutID);
    }, 4000); 
}


function create_charts(search_details, layoutId) {
    // remove the old charts
    remove_charts_divs();

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/AllResponses/Charts/ViewChartsBase?handler=RetrieveResponses",

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
                        console.log("optionAnswersWithNoAnswers", Object.keys(optionAnswersWithNoAnswers));
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
                                backgroundColor: { fill: "#e9e9e9" },
                                sliceVisibilityThreshold: 0,
                            };
                            divIds[contor1] = "question" + (contor1 + 1);

                            contor1++;
                            drawCharts = "true";
                        }
                    }

                    create_new_divs(layoutId);
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

function remove_charts_divs() {
    for (var contor = 1; contor < 11; contor+=2) {
        var chartDivRow = document.getElementById("row" + contor);
        if (chartDivRow != null) {
            chartDivRow.remove();
        }
    }
}

function create_new_divs(layoutId) {
    for (var contor = 1; contor < 11; contor+=2) {
        var divRowElement = document.createElement('div');
        divRowElement.className = "row";
        divRowElement.id = "row" + contor;
        divRowElement.style.marginLeft = "300px;"
        var div1 = document.createElement('div');
        div1.id = "question" + contor;
        divRowElement.appendChild(div1);

        var div2 = document.createElement('div');
        div2.id = "question" + (contor+1);
        divRowElement.appendChild(div2);

        var mainElement = document.getElementById(layoutId);
        mainElement.appendChild(divRowElement);
    }
}

function create_tag_cloud(search_details, layoutId) {

    $.ajax({
        type: "GET",
        data: search_details,
        dataType: 'json',
        contextType: 'application/json',
        url: "/AllResponses/Charts/ViewChartsBase?handler=RetrieveTagCloud",

        success: function (result) {
            var tagDiv = document.createElement("div");
            tagDiv.id = "tag-cloud-div";
            tagDiv.className = "tag-cloud";
            tagDiv.style.marginBottom = "170px;"
            tagDiv.style.backgroundColor = "#e9e9e9";

            if (result.length > 0) {
                var h2 = document.createElement("h2");
                h2.textContent = "Word cloud";
                h2.style.color = "#6468ed";
                h2.style.fontFamily = "Brush Script MT";
                tagDiv.appendChild(h2);
            }
            for (var i = 0; i < result.length; i++) {
                if (i % 5 == 0) {
                    var br = document.createElement("br");
                    tagDiv.appendChild(br);
                }
                var tag = result[i];
                console.log("result: ", result);
                console.log("tag: ", tag);
                var spanElement = document.createElement("span");
                console.log("category: ", tag.category);
                spanElement.className = "tag kind-".concat(tag.category);
                spanElement.textContent = tag.text;
                console.log("text: ", tag.text);
                tagDiv.appendChild(spanElement);
            }
            var mainElement = document.getElementById(layoutId);
            mainElement.appendChild(tagDiv);

            var animatedProperties = {
                paddingLeft: '50px',
                paddingTop: '75px',
                paddingBottom: '125px',
                paddingRight: '150px',
                opacity: 1
            };
            $('.tag-cloud').animate(animatedProperties, 500);
        },
        error: function () {
            console.log("Error while creating the tag cloud!");
        }
    });
}

function remove_tag_cloud() {
    var tagDiv = document.getElementById("tag-cloud-div");
    if (tagDiv != null) {
        tagDiv.remove();
    }
}

function update() {
    remove_charts_divs();
    remove_tag_cloud();
    $("#selected-subject-field").val("default");
    get_taught_subjects_by_teacher_id();
}