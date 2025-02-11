$(function () {
    const apiUrl = "https://jsonplaceholder.typicode.com/posts";

    const customStore = new DevExpress.data.CustomStore({
        key: "id",
        load: function (loadOptions) {
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
            var d = new $.Deferred();
            $.get(`${apiUrl}/${key}`)
                .done(function (dataItem) {
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        insert: function (values) {
            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: values
            })
        },
        update: function (key, values) {
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: 'PUT',
                data: values
            })
        },
        remove: function (key) {
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE"
            });
        },
        totalCount: function (loadOptions) {
            return $.ajax({
                url: `${apiUrl}`,
                method: "GET"
            }).then(response => {
                return response.length;
            });
        },
        onInserted: function (values, key) {
            DevExpress.ui.notify("Record successfully added", "success", 3000);
        },
        onLoaded: function (result) {
            $("#loadingIndicator").hide();
            $("#resultsContent").text(JSON.stringify(result, null, 2));
        },
        onLoading: function (loadOptions) {
            $("#loadingIndicator").show();
        },
        onModified: function () {
            DevExpress.ui.notify("Data successfully updated", "success", 3000);
        },
        onRemoved: function (key) {
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