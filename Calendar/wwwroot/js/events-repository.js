class EventsRepository {
    constructor() {
        this.db = null;
        var fakethis = this;

        let request = window.indexedDB.open('events_db', 2);

        request.onerror = function () {
            console.log('Database failed to open');
        };

        request.onsuccess = function () {
            console.log('Database opened successfully');

            fakethis.db = request.result;
        };

        request.onupgradeneeded = function (e) {
            fakethis.db = e.target.result;

            let objectStore = fakethis.db.createObjectStore('events_os', { keyPath: 'id', autoIncrement: true });

            // Define what data items the objectStore will contain
            objectStore.createIndex('event_start', 'event_start', { unique: false });
            objectStore.createIndex('event_end', 'event_end', { unique: false });

            console.log('Database setup complete');
        };
    }

    add(event) {
        let transaction = this.db.transaction(['events_os'], 'readwrite');
        let objectStore = transaction.objectStore('events_os');
        let request = objectStore.add(event);

        request.onsuccess = function () {
            console.log('Data added successfully');
        };

        request.onerror = function () {
            console.log('Failure adding data');
        };

        request.oncomplete = function () {
            console.log('Transaction completed: database modification finished.');
        };
    }
}