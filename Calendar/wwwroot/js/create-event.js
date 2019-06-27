$(function () {
    $(".datepicker").datepicker({});

    $("#show_end_date_panel").change(function () {
        if ($("#show_end_date_panel").is(":checked")) {
            $("#event_end_panel").removeClass("d-none");
            $("#event_end_panel").addClass("d-block");
        } else {
            $("#event_end_panel").removeClass("d-block");
            $("#event_end_panel").addClass("d-none");
        }
    });
});
