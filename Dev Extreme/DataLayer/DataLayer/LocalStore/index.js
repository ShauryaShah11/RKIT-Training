var states = [
    { id: 1, state: "Alabama", capital: "Montgomery" },
    { id: 2, state: "Alaska", capital: "Juneau" },
    { id: 3, state: "Arizona", capital: "Phoenix" },
    // Add more states as needed
];

$(function () {
    // Define a LocalStore
    var store = new DevExpress.data.DataSource({
        store: {
            type: 'local', // Specify the store type as 'local'
            key: 'id', // Specify the key field
            data: states, // Provide the initial data
            name: 'myLocalData' // Optional: Name of the local store
        },
        errorHandler: function (error) {
            // Handle errors
            console.log('Error:', error.message);
        },
        immediate: false, // Optional: Set to true to save changes immediately
        flushInterval: 3000, // Optional: Interval to save changes (in milliseconds)
        key: ['id'], // Specify the key field(s)
        name: 'data', // Optional: Name of the data source
    });

    // Load data from the store
    store.load().done(function (data) {
        console.log('Data loaded:', data);
    }).fail(function (error) {
        console.log('Error loading data:', error);
    });

    // Get a single item by key
    store.byKey(2)
        .done(function (data) {
            console.log('Item with key 2:', data);
        })
        .fail(function (error) {
            console.log('Error getting item by key:', error);
        });

    // Insert a new item
    store.store().insert({ id: 4, state: "California", capital: "Sacramento" })
        .done(function () {
            console.log('Item inserted');
            store.load(); // Reload data to reflect changes
        })
        .fail(function (error) {
            console.log('Error inserting item:', error);
        });

    // Update an existing item
    store.store().update(1, { capital: "Montgomery Updated" })
        .done(function () {
            console.log('Item updated');
            store.load(); // Reload data to reflect changes
        })
        .fail(function (error) {
            console.log('Error updating item:', error);
        });

    // Remove an item
    store.store().remove(3)
        .done(function () {
            console.log('Item removed');
            store.load(); // Reload data to reflect changes
        })
        .fail(function (error) {
            console.log('Error removing item:', error);
        });

    // Dynamic property getter
    function getProperty(key, property) {
        store.byKey(key).done(function (item) {
            console.log('Property value:', item[property]);
        }).fail(function (error) {
            console.log('Error getting property:', error);
        });
    }

    // Dynamic property setter
    function setProperty(key, property, value) {
        store.byKey(key).done(function (item) {
            item[property] = value;
            store.store().update(key, item).done(function () {
                console.log('Property set successfully');
                store.load(); // Reload data to reflect changes
            }).fail(function (error) {
                console.log('Error setting property:', error);
            });
        }).fail(function (error) {
            console.log('Error getting item for setting property:', error);
        });
    }

    // Example usage of dynamic property getter and setter
    getProperty(1, 'state'); // Get the 'state' property of the item with key 1
    setProperty(1, 'capital', 'New Montgomery'); // Set the 'capital' property of the item with key 1 to 'New Montgomery'
});