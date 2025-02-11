var states = [
    { id: 1, state: "Alabama", capital: "Montgomery" },
    { id: 2, state: "Alaska", capital: "Juneau" },
    { id: 3, state: "Arizona", capital: "Phoenix" },
];

$(function () {
    var store = new DevExpress.data.ArrayStore({
        key: "id",
        data: states,
        errorHandler: function (error) {
            console.log('Error: ' + error.message);
        },
        onLoading: function (loadOptions) {
            // You can modify the loadOptions to add your custom logic
            loadOptions.filter = ["state", "startswith", "A"]; // Example: filter states starting with "A"
            // loadOptions.filter = ["state", "contains", "a"];
            // loadOptions.filter = ["state", "endswith", "a"];
            // loadOptions.filter = ["capital", "=", "Juneau"];
            // loadOptions.filter = ["capital", "!=", "Juneau"];
            // loadOptions.filter = ["id", ">", 2];
            // loadOptions.filter = ["id", "<", 3];
            // loadOptions.filter = ["id", ">=", 2];
            // loadOptions.filter = ["id", "<=", 2];
            // loadOptions.filter = [["id", ">", 1], "and", ["state", "contains", "a"]];
            // loadOptions.filter = [["id", "<", 2], "or", ["capital", "=", "Juneau"]];
            // loadOptions.filter = ["date", ">", new Date(2024, 0, 1)];
            // loadOptions.filter = ["id", "in", [1, 2, 3]];    
            console.log('Loading data with options:', loadOptions);
        },
        onInserting: function (values) {
            // Prevent inserting a duplicate state
            var isDuplicate = false;
            store.byKey(values.id)
                .done(function (existingItem) {
                    if (existingItem) {
                        isDuplicate = true;
                        console.log('Duplicate ID, cannot insert');
                    }
                })
                .fail(function () {
                    // If no item is found, we can insert
                    console.log('Inserting:', values);
                });

            return !isDuplicate; // Return false to prevent insertion if it's a duplicate
        },
        onInserted: function (values, key) {
            console.log('Inserted item with key:', key, 'and values:', values);
        },
        onModifying: function () {
            console.log('Data is being modified');
        },
        onRemoved: function (key) {
            console.log('Item removed with key:', key);
        }
    });

    store.load().done(function (data) {
        console.log('Loaded data:', data);
        data.forEach(function (item) {
            console.log('State:', item.state);
        });
    });

    // Insert a new state
    store.insert({ id: 4, state: "California", capital: "Sacramento" })
        .done(function (dataObj, key) {
            console.log('Inserted new item with key:', key);
        })
        .fail(function (error) {
            console.log('Insert failed:', error);
        });

    // Update state with id 1
    store.update(1, { state: "Alabama", capital: "Montgomery Updated" })
        .done(function () {
            console.log('Updated state with id 1');
        })
        .fail(function (error) {
            console.log('Error updating:', error);
        });

    // Remove an item
    store.remove(2)  // Remove by key (ID)
        .done(function () {
            console.log('Item with key 2 removed');
        })
        .fail(function (error) {
            console.log('Error removing item:', error);
        });

    var keyProps = store.key(); // Get the key field
    console.log('Key properties:', keyProps);

    // // Composite key store

    // var compositeKeyStore = new DevExpress.data.ArrayStore({
    //     key: ["state", "capital"],
    //     data: states
    // });

    // // Fetch by composite key
    // compositeKeyStore.byKey({ state: "Arizona", capital: "Phoenix" })
    //     .done(function (dataItem) {
    //         console.log('Found item:', dataItem);
    //     })
    //     .fail(function (error) {
    //         console.log('Error fetching item:', error);
    //     });

    // compositeKeyStore.clear();
});
