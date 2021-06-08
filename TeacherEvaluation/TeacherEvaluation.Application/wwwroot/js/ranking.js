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
            remove_column_chart();
            if (Object.keys(result).length > 0) {
                draw_column_chart(result);
            }
            else {
                var div = create_div();
                var h4 = document.createElement("h4");
                h4.innerText = "No data is available";
                div.appendChild(h4);
            }
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function remove_column_chart() {
    var rankingDiv = document.getElementById("ranking-div");
    if (rankingDiv != null) {
        rankingDiv.remove();
    }
}

class TeacherVm {
    constructor(name, department, degree) {
        this.name = name;
        this.department = department;
        this.degree = degree;
    }
}

// used to deserialize the teachers details 
TeacherVm.fromJson = function (jsonData) {
    var data = JSON.parse(jsonData); 
    return new TeacherVm(data.Name, data.Department, data.Degree);
};

function draw_column_chart(result) {
    google.charts.load('current', {
        callback: function () {
            // prepare the data for chart
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'teacherVm');
            data.addColumn('number', 'score');
            data.addColumn({ type: 'string', role: 'tooltip' });
            data.addRows(Object.keys(result).length);
            var contor1 = 0;
            var max = 0;

            for (var key in result) {
                console.log("draw_column_chart result: ", result);

                var teacherVm = TeacherVm.fromJson(key);
                data.setCell(contor1, 0, teacherVm.name);
                var score = result[key];
                max = score > max ? score : max;
                data.setCell(contor1, 1, score);
                var teacherDetails = "Department: " + teacherVm.department + ", Degree: " + teacherVm.degree;
                data.setCell(contor1, 2, teacherDetails);
                contor1++;
            }

            var view = new google.visualization.DataView(data);
            view.setColumns([0, 1,
                {
                    calc: "stringify",
                    sourceColumn: 2,
                    type: "string",
                    role: "tooltip"
                }]);

            var options = {
                title: "Top teachers",
                width: 600,
                height: 400,
                bar: { groupWidth: "50" },
                backgroundColor: { fill: "#e9e9e9" },
                vAxis: {
                    gridlines: { count: max }, 
                    viewWindow: {
                        min: 0,
                        max: max
                    },
                }
            };

            var div = create_div();
            var chart = new google.visualization.ColumnChart(div);
            chart.draw(view, options);
        },
        packages: ['corechart']
    });
}

function create_div() {
    var div = document.createElement('div');
    div.id = "ranking-div";
    var mainElement = document.getElementById("mainElementDeanLayout");
    div.setAttribute("style", "width: 420px; height: 420px; margin-left: 60px; margin-top: 100px;");
    mainElement.appendChild(div);
    return div;
}

function reset() {
    remove_column_chart();
    $("#ranking-type-field").val("Please select the taught subject type");
}