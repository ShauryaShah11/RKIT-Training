$(function () {
    // Sample data array containing objects with name, birthYear, and gender
    var dataObjects = [
        { name: "Amelia", birthYear: 1991, gender: "female" },
        { name: "Benjamin", birthYear: 1983, gender: "male" },
        { name: "Daniela", birthYear: 1987, gender: "female" },
        { name: "Lee", birthYear: 1981, gender: "male" }
    ];

    // Filtering data to get only female users, sorting them by birthYear, 
    // selecting name and birthYear fields, and converting to an array
    var processedArray = DevExpress.data.query(dataObjects)
        .filter(["gender", "=", "female"]) // Filters to keep only "female" entries
        .sortBy("birthYear") // Sorts by birthYear in ascending order
        .select("name", "birthYear") // Selects only the name and birthYear fields
        .toArray(); // Converts the result into an array

    console.log(processedArray); // Logs the filtered and sorted array to the console

    // Example of aggregate operation with custom step and finalize functions
    var step = function (total, itemData) {
        // "total" is the accumulator value (initially 0)
        // "itemData" is each item in the array (in this case numbers like 10, 20, 30, etc.)
        return total + itemData; // Add itemData to the accumulator
    };

    var finalize = function (total) {
        // The "total" is the final result of the accumulation
        // Divide the total by 1000 to get the final result
        return total / 1000;
    };

    // Perform aggregate operation with a custom step and finalize
    DevExpress.data.query([10, 20, 30, 40, 50])
        .aggregate(0, step, finalize) // Starts with 0, applies the step function, then finalizes with the finalize function
        .done(function (result) {
            console.log(result); // Logs the final result (output 0.15)
        });

    // Perform aggregate with just the step function (no finalize function)
    DevExpress.data.query([10, 20, 30, 40, 50])
        .aggregate(step) // Uses the default finalize (no operation)
        .done(function (result) {
            console.log(result); // Logs the result (output will vary)
        });

    // Calculate the average of the numbers
    DevExpress.data.query([10, 20, 30, 40, 50])
        .avg() // Calculates the average
        .done(function (result) {
            console.log(result); // Logs the average result
        });

    // Count the number of items in the array
    DevExpress.data.query([10, 20, 30, 40, 50])
        .count() // Counts the number of items
        .done(function (result) {
            console.log(result); // Logs the count (5)
        });

    // Enumerate through the items in the array (provides the index and the value)
    DevExpress.data.query(dataObjects)
        .enumerate() // Enumerates through the data (returns index and value)
        .done(function (result) {
            console.log(result); // Logs each item's index and value
        });

    // Filter data based on birthYear being greater than 1985
    var result = DevExpress.data.query(dataObjects)
        .filter(['birthYear', '>', '1985']) // Filters for birthYear greater than 1985
        .toArray(); // Converts the result into an array
    console.log(result); // Logs the filtered result

    // Another way of filtering data using a function
    var result1 = DevExpress.data.query(dataObjects)
        .filter(function (dataItem) {
            return dataItem.birthYear < 1985; // Filters for birthYear less than 1985
        })
        .toArray(); // Converts the result into an array
    console.log(result1); // Logs the filtered result

    // Group data by gender
    var groupedData = DevExpress.data.query(dataObjects)
        .groupBy("gender") // Groups the data by gender
        .toArray(); // Converts the result into an array
    console.log(groupedData); // Logs the grouped result

    // Find the maximum value of birthYear
    DevExpress.data.query(dataObjects)
        .select('birthYear') // Selects the birthYear field
        .max() // Finds the maximum value in the birthYear field
        .done(function (result) {
            console.log(result); // Logs the maximum value of birthYear
        });

    // Find the minimum value of birthYear
    DevExpress.data.query(dataObjects)
        .min('birthYear') // Finds the minimum value in the birthYear field
        .done(function (result) {
            console.log(result); // Logs the minimum value of birthYear
        });

    // Slice a portion of the array (getting the 2nd and 3rd items)
    var subset = DevExpress.data.query(dataObjects)
        .slice(1, 2) // Starts at index 1, takes 2 items
        .toArray(); // Converts the result into an array
    console.log(subset); // Logs the subset of data

    // Sort the data by birthYear in descending order
    var sortedData = DevExpress.data.query(dataObjects)
        .sortBy("birthYear", true) // Sorts by birthYear in descending order (true for descending)
        .toArray(); // Converts the result into an array
    console.log(sortedData); // Logs the sorted data

    // Sum the birthYear values
    DevExpress.data.query(dataObjects)
        .select("birthYear") // Selects only the birthYear field
        .sum() // Calculates the sum of all birthYear values
        .done(function (result) {
            console.log(result); // Logs the sum of birthYear values
        });

    // Sort the data first by gender, then by birthYear
    sortedData = DevExpress.data.query(dataObjects)
        .sortBy("gender") // Sorts by gender
        .thenBy("birthYear") // Sorts by birthYear (if genders are the same)
        .toArray(); // Converts the result into an array
    console.log(sortedData); // Logs the sorted data
});
