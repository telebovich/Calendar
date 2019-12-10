$(function () {
    let db;

    let request = window.indexedDB.open('events_db', 2);

    request.onerror = function () {
        console.log('Database failed to open');
    };

    request.onsuccess = function () {
        console.log('Database opened successfully');

        db = request.result;
    };

    request.onupgradeneeded = function (e) {
        db = e.target.result;

        let objectStore = db.createObjectStore('events_os', { keyPath: 'id', autoIncrement: true });

        // Define what data items the objectStore will contain
        objectStore.createIndex('event_start', 'event_start', { unique: false });
        objectStore.createIndex('event_end', 'event_end', { unique: false });

        console.log('Database setup complete');
    };

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

    $("#submit").click(function () {
        var event_start = new Date($("#event_start").val());
        event_start.setHours($('#event_time_hrs').val());
        event_start.setMinutes($('#event_time_min').val());

        let newItem = { event_start };
        let transaction = db.transaction(['events_os'], 'readwrite');
        let objectStore = transaction.objectStore('events_os');
        let request = objectStore.add(newItem);
        request.onsuccess = function () {
            console.log('Data added successfully');
        };

        request.onerror = function () {
            console.log('Failure adding data');
        };

        request.oncomplete = function () {
            console.log('Transaction completed: database modification finished.');
        };

    });
});
