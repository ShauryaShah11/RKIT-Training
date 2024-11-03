function asyncTask() {
    var deferred = $.Deferred();

    setTimeout(function () {
        var success = true; // Simulate a successful operation
        if (success) {
            deferred.resolve("Data loaded successfully!");
        } else {
            deferred.reject("Failed to load data.");
        }
    }, 2000); // 2 seconds delay to mimic async behavior

    return deferred.promise(); // Return the promise object
}

// Calling the asyncTask and attaching callback handlers
asyncTask().then(
    function (data) {
        console.log("Success: " + data); // Called on resolve
    },
    function (error) {
        console.log("Error: " + error); // Called on reject
    }
);
