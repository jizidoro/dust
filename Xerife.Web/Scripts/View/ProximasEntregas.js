$(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },
        //Random default events
        //events: $.get("/Consultas/GetProximasEntregas"),
        events: "@Url.Action("GetCalendarEvents/")",
        editable: false,
        droppable: false
    });

});