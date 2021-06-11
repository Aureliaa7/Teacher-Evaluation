$(document).ready(function () {
    $('#taught-subjects-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 6 }]
    });
});