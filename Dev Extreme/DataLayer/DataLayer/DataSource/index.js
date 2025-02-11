$(function () {
    // Creating a new DataSource instance using the OData protocol to fetch orders
    var dataSource = new DevExpress.data.DataSource({
        // Configuring the OData store with the required URL and version details
        store: {
            type: 'odata',  // Data source type (OData)
            url: "https://services.odata.org/V4/Northwind/Northwind.svc/Orders", // URL for the OData service
            key: "OrderID", // The key field to uniquely identify each order
            version: 4, // Using OData v4 protocol
            withCredentials: false, // Disables sending credentials with the request
            jsonp: false, // Disables JSONP support
            countOnly: true, // Ensures only the count of records is returned, optimizing queries
            beforeSend: function (options) {
                // Attach the $count parameter to the request to fetch the total count of orders
                console.log("Preparing to send the request...");
                options.params.$count = true;
                // Clean up incompatible parameters for OData v4
                if (options.params.$inlinecount) {
                    delete options.params.$inlinecount;
                }
                // Format expand parameter properly
                if (options.params.$expand) {
                    options.params.$expand = 'Customer';
                }
            }
        },

        // Filtering orders with OrderID greater than 10250 to limit the data range
        filter: [["OrderID", ">", 10250]],

        // Expanding the related Customer entity to include customer data along with orders
        expand: 'Customer',

        // Configure pagination to load 10 records per page
        pageSize: 10,
        paginate: true,

        // Sorting the orders by OrderDate in descending order to get the latest orders first
        sort: [{
            selector: 'OrderDate',
            desc: true
        }],

        // Handling load errors by displaying a user-friendly notification
        onLoadError: function (error) {
            console.error('Loading error:', error);
            DevExpress.ui.notify({
                message: 'Failed to load data: ' + error.message,
                type: 'error',
                displayTime: 3000
            });
        },

        // Logging loading state changes for debugging purposes
        onLoadingChanged: function (isLoading) {
            if (isLoading) {
                console.log("Loading data...");
            } else {
                console.log("Loading complete");
            }
        },

        // Mapping function to transform data before it's rendered or used
        map: function (item) {
            if (!item) return {};  // Handling null or undefined items
            return {
                ...item,
                orderSummary: `Order ${item.OrderID} by ${item.CustomerID}` // Adding a custom property 'orderSummary'
            };
        }
    });

    // Loading the data using the configured data source
    console.log("Loading data from the OData service...");
    var initialLoadPromise = dataSource.load()
        .done(function (data) {
            // Handle successful data loading
            console.log("Data loaded successfully:", data);
        })
        .fail(function (error) {
            // Handle failed data loading
            console.error("Failed to load data:", error);
        });

    // Example of paging: Check if the current page is the last page
    let isLastPage = dataSource.isLastPage();
    console.log("Is this the last page?", isLastPage);

    // Example of checking if the data has been fully loaded
    let isLoaded = dataSource.isLoaded();
    console.log("Is the data fully loaded?", isLoaded);

    // Example of checking if the data is still loading
    let isLoading = dataSource.isLoading();
    console.log("Is the data currently loading?", isLoading);

    // Retrieving the data items currently loaded on the current page
    var dataItems = dataSource.items();
    console.log("Data items on the current page:", dataItems);

    // Fetching the key of the data source (e.g., OrderID for orders)
    var key = dataSource.key();
    console.log("Key field for the data source:", key);

    // Reloading the data source to fetch fresh data
    console.log("Reloading data...");
    var reloadPromise = dataSource.reload()
        .done(function (data) {
            console.log("Data reloaded:", data);
        })
        .fail(function (error) {
            console.error("Failed to reload data:", error);
        });

    // Accessing and setting the 'requireTotalCount' property to fetch the total number of records
    var requireTotalCount = dataSource.requireTotalCount(); // Gets the value
    console.log("Current requireTotalCount value:", requireTotalCount);
    dataSource.requireTotalCount(true); // Sets it to true to ensure the total count is fetched
    console.log("Updated requireTotalCount to true");

    // Searching for orders with the customer name "Jo"
    console.log("Searching for orders with CustomerID containing 'Jo'...");
    dataSource.searchExpr("CustomerID");
    dataSource.searchOperation("contains");
    dataSource.searchValue("Jo");

    // Selecting specific fields
    console.log("Selecting specific fields for orders...");
    var selectExpr = dataSource.select(); // Retrieve selected fields
    dataSource.select(["CustomerID", "OrderDate", "ShipCountry"]);

    // Sorting orders by the 'Discount' field in descending order
    console.log("Sorting orders by Discount in descending order...");
    dataSource.sort({ selector: "OrderDate", desc: true });

    // Retrieving the store instance used by the data source
    var store = dataSource.store();
    console.log("Data store instance:", store);

    // Getting the total count of orders from the data source
    var count = dataSource.totalCount();
    console.log("Total count of orders:", count);

    // Example use case for handling pagination:
    if (isLastPage) {
        console.log("You have reached the last page of data.");
    } else {
        console.log("There are more pages to load.");
    }

    var store = dataSource.store();
    store.load({
        filter: ['ShipCountry', '=', "Germany"]
    })
        .done(function (result) {
            console.log("Store filtered results:", result);
        });
    // Example of canceling a specific load request if needed
    if (initialLoadPromise && initialLoadPromise.operationId) {
        dataSource.cancel(initialLoadPromise.operationId);
    }

    // Note: Move dispose to a cleanup function or event handler
    // Don't dispose immediately as it's needed for the demo
    // dataSource.dispose();
});