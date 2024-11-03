var deferred = $.Deferred();

var promise = deferred.promise(); // Getting the promise object

promise
    .done(function (message) {
        console.log("Done: " + message); // Success callback
    })
    .fail(function (error) {
        console.log("Fail: " + error); // Failure callback
    });

setTimeout(function () {
    deferred.resolve("Task completed successfully!");
}, 2000);
