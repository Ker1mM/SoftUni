// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#partsTable').DataTable();
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#playerTable').DataTable();
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#tracksTable').DataTable();
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#attempts').DataTable();
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#equipParts').DataTable({
        columnDefs: [{
            orderable: false,
            targets: 6
        }]
    });
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#storeTable').DataTable({
        columnDefs: [{
            orderable: false,
            targets: 8
        }]
    });
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#raceCenter').DataTable({
        columnDefs: [{
            orderable: false,
            targets: 6
        }]
    });
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#inventory').DataTable({
        columnDefs: [{
            orderable: false,
            targets: 7
        }]
    });
    $('.dataTables_length').addClass('bs-select');
});

$(document).ready(function () {
    $('#equippedParts').DataTable({
        columnDefs: [{
            orderable: false,
            targets: 6
        }]
    });
    $('.dataTables_length').addClass('bs-select');
});

$('.sev_check').click(function () {
    $('.sev_check').not(this).prop('checked', false);
});




