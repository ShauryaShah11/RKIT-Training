$(function () {
    const apiUrl = "https://jsonplaceholder.typicode.com/posts";

    // Define a CustomStore
    const customStore = new DevExpress.data.CustomStore({
        key: "id", // Specify the key field
        load: function (loadOptions) {
            // Load data from the API
            let params = {};
            if (loadOptions.skip) {
                params.$skip = loadOptions.skip;
            }
            if (loadOptions.take) {
                params.$top = loadOptions.take;
            }
            return $.getJSON(apiUrl, params);
        },
        byKey: function (key) {
            // Get a single item by key
            var d = new $.Deferred();
            $.get(`${apiUrl}/${key}`)
                .done(function (dataItem) {
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        insert: function (values) {
            // Insert a new item
            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: values
            });
        },
        update: function (key, values) {
            // Update an existing item
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: 'PUT',
                data: values
            });
        },
        remove: function (key) {
            // Remove an item
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE"
            });
        },
        totalCount: function (loadOptions) {
            // Get the total count of items
            return $.ajax({
                url: `${apiUrl}`,
                method: "GET"
            }).then(response => {
                return response.length;
            });
        },
        onInserted: function (values, key) {
            // Notify when an item is inserted
            DevExpress.ui.notify("Record successfully added", "success", 3000);
        },
        onLoaded: function (result) {
            // Hide loading indicator and display results
            $("#loadingIndicator").hide();
            $("#resultsContent").text(JSON.stringify(result, null, 2));
        },
        onLoading: function (loadOptions) {
            // Show loading indicator
            $("#loadingIndicator").show();
        },
        onModified: function () {
            // Notify when data is modified
            DevExpress.ui.notify("Data successfully updated", "success", 3000);
        },
        onRemoved: function (key) {
            // Notify when an item is removed
            DevExpress.ui.notify("Record successfully deleted", "success", 3000);
        }
    });

    // Create Operation Buttons
    $("#loadButton").dxButton({
        text: "Load Data",
        onClick: function () {
            hideAllForms();
            customStore.load({
                skip: 0,
                take: 10
            });
        }
    });

    $("#insertButton").dxButton({
        text: "Show Insert Form",
        onClick: function () {
            hideAllForms();
            $("#insertForm").show();
        }
    });

    $("#updateButton").dxButton({
        text: "Show Update Form",
        onClick: function () {
            hideAllForms();
            $("#updateForm").show();
        }
    });

    $("#deleteButton").dxButton({
        text: "Show Delete Form",
        onClick: function () {
            hideAllForms();
            $("#deleteForm").show();
        }
    });

    // Create Submit Buttons
    $("#insertSubmitButton").dxButton({
        text: "Insert",
        onClick: function () {
            const values = {
                title: $("#insertTitle").val(),
                body: $("#insertBody").val()
            };

            if (!values.title || !values.body) {
                DevExpress.ui.notify("Title and body are required", "error", 3000);
                return;
            }

            customStore.insert(values)
                .done(function (response) {
                    $("#resultsContent").text(JSON.stringify(response, null, 2));
                    clearInsertForm();
                })
                .fail(function (error) {
                    DevExpress.ui.notify(error.message, "error", 3000);
                });
        }
    });

    $("#updateSubmitButton").dxButton({
        text: "Update",
        onClick: function () {
            const id = $("#updateId").val();
            const values = {
                title: $("#updateTitle").val(),
                body: $("#updateBody").val()
            };

            if (!id || !values.title || !values.body) {
                DevExpress.ui.notify("ID, title and body are required", "error", 3000);
                return;
            }

            customStore.update(id, values)
                .done(function (response) {
                    $("#resultsContent").text(JSON.stringify(response, null, 2));
                    clearUpdateForm();
                })
                .fail(function (error) {
                    DevExpress.ui.notify(error.message, "error", 3000);
                });
        }
    });

    $("#deleteSubmitButton").dxButton({
        text: "Delete",
        onClick: function () {
            const id = $("#deleteId").val();

            if (!id) {
                DevExpress.ui.notify("ID is required", "error", 3000);
                return;
            }

            if (confirm("Are you sure you want to delete this record?")) {
                customStore.remove(id)
                    .done(function (response) {
                        $("#resultsContent").text(JSON.stringify(response, null, 2));
                        clearDeleteForm();
                    })
                    .fail(function (error) {
                        DevExpress.ui.notify(error.message, "error", 3000);
                    });
            }
        }
    });

    // Dynamic property getter
    function getProperty(key, property) {
        customStore.byKey(key).done(function (item) {
            console.log('Property value:', item[property]);
        }).fail(function (error) {
            console.log('Error getting property:', error);
        });
    }

    // Dynamic property setter
    function setProperty(key, property, value) {
        customStore.byKey(key).done(function (item) {
            item[property] = value;
            customStore.update(key, item).done(function () {
                console.log('Property set successfully');
            }).fail(function (error) {
                console.log('Error setting property:', error);
            });
        }).fail(function (error) {
            console.log('Error getting item for setting property:', error);
        });
    }

    // Example usage of dynamic property getter and setter
    getProperty(1, 'title'); // Get the 'title' property of the item with key 1
    setProperty(1, 'body', 'Updated body content'); // Set the 'body' property of the item with key 1 to 'Updated body content'

    // Helper Functions
    function hideAllForms() {
        $("#insertForm").hide();
        $("#updateForm").hide();
        $("#deleteForm").hide();
    }

    function clearInsertForm() {
        $("#insertTitle").val("");
        $("#insertBody").val("");
        $("#insertForm").hide();
    }

    function clearUpdateForm() {
        $("#updateId").val("");
        $("#updateTitle").val("");
        $("#updateBody").val("");
        $("#updateForm").hide();
    }

    function clearDeleteForm() {
        $("#deleteId").val("");
        $("#deleteForm").hide();
    }

    // Loading Indicator
    $("#loadingIndicator").dxLoadPanel({
        visible: false,
        shading: true,
        showIndicator: true,
        showPane: true
    });
});