window.onload = function () {
    store_selected_values();
    set_selected_values();
};

function set_selected_values() {
    let selectedTeacher = sessionStorage.getItem("selectedTeacherId");
    let selectedTaughtSubject = sessionStorage.getItem("selectedTaughtSubjectId");

    if (selectedTeacher != null && selectedTaughtSubject != null) {
        $("#teacher-field").val(selectedTeacher);
        get_taught_subjects_by_teacher_id();
        $("#selected-subject-field").val(selectedTaughtSubject);
    }
}

// store the selected teacher id and the taught subject id so that when the user clicks the Back link from 
// OneResponse page, he/she will be redirected to ViewAsDean/ViewAsTeacher and the dropdowns will have the 
// same selected values as before accessing the OneResponse page
function store_selected_values() {
    let teachersDropdown = document.getElementById("teacher-field");
    let taughtSubjectsDropdown = document.getElementById("selected-subject-field");

    teachersDropdown.addEventListener("change", function () {
        if ($("#teacher-field").val() != null) {
            sessionStorage.setItem('selectedTeacherId', $("#teacher-field").val());
        }
    });

    taughtSubjectsDropdown.addEventListener("change", function () {
        if ($("#selected-subject-field").val() != null) {
            sessionStorage.setItem('selectedTaughtSubjectId', $("#selected-subject-field").val());
        }
    });
}

