# DevExtreme DataLayer Documentation

## Index
1. [Introduction](#introduction)
2. [Core Components](#core-components)
3. [ArrayStore](#arraystore)
4. [CustomStore](#customstore)
5. [DataSource](#datasource)
6. [LocalStore](#localstore)
7. [Query Module](#query)
8. [Comparison of DataLayer Types](#comparison-of-datalayer-types)

## Overview

The **DevExtreme DataLayer** is a foundational component that facilitates seamless data handling in DevExtreme applications. It provides an abstraction for managing data operations such as fetching, transforming, and persisting data from various sources, including remote services, local storage, and in-memory collections.

## Key Features

- **Unified Data Handling**: Provides a common interface for interacting with different data sources.
- **Asynchronous Operations**: Supports promises for smooth integration with async workflows.
- **Data Transformation**: Enables filtering, sorting, and data shaping before rendering.
- **Extensibility**: Allows customization and extension to support complex business logic.

## Core Components

### 1. Data Management

The DataLayer handles data retrieval and transformation efficiently. It abstracts low-level data-fetching logic, allowing seamless communication with APIs and databases.

Example:

```jsx
const dataLayer = new DevExpress.data.DataLayer();

```

### 2. Asynchronous Data Handling

The DataLayer uses promises to manage data retrieval asynchronously, ensuring non-blocking operations.

Example:

```jsx
dataLayer.load().then(data => {
    console.log("Data Loaded:", data);
}).catch(error => {
    console.error("Data Loading Error:", error);
});

```

### 3. Data Transformation

The DataLayer allows filtering, sorting, and data shaping before it is bound to UI components.

Example:

```jsx
dataLayer.filter(["age", ">", 25]).sort("name").load().then(data => {
    console.log("Filtered Data:", data);
});

```

### 4. CRUD Operations

The DataLayer supports Create, Read, Update, and Delete (CRUD) operations for seamless data manipulation.

Example:

```jsx
dataLayer.insert({ id: 5, name: "Alice", age: 30 });
dataLayer.update(5, { age: 31 });
dataLayer.remove(5);
```

## ArrayStore

## Introduction

`ArrayStore` is a client-side in-memory data store that provides CRUD operations on an array of objects. It is commonly used in DevExtreme applications to manage data collections efficiently without requiring a server.

## Features

- **Client-side data management**
- **Filtering, sorting, and paging**
- **CRUD operations (Create, Read, Update, Delete)**
- **Event handling for data modifications**
- **Integration with DevExtreme UI components**

## Initialization

To create an `ArrayStore`, provide an array of objects and specify a unique key:

```jsx
var states = [
    { id: 1, state: "Alabama", capital: "Montgomery" },
    { id: 2, state: "Alaska", capital: "Juneau" },
    { id: 3, state: "Arizona", capital: "Phoenix" }
];

var store = new DevExpress.data.ArrayStore({
    key: "id",
    data: states
});

```

## CRUD Operations

### **Loading Data**

To load data from the store:

```jsx
store.load().done(function(data) {
    console.log("Loaded data:", data);
});

```

### **Inserting Data**

To add a new record:

```jsx
store.insert({ id: 4, state: "California", capital: "Sacramento" })
    .done(function(dataObj, key) {
        console.log("Inserted new item with key:", key);
    })
    .fail(function(error) {
        console.log("Insert failed:", error);
    });

```

### **Updating Data**

To update an existing record:

```jsx
store.update(1, { state: "Alabama", capital: "Montgomery Updated" })
    .done(function() {
        console.log("Updated state with id 1");
    })
    .fail(function(error) {
        console.log("Error updating:", error);
    });

```

### **Removing Data**

To remove a record:

```jsx
store.remove(2)
    .done(function() {
        console.log("Item with key 2 removed");
    })
    .fail(function(error) {
        console.log("Error removing item:", error);
    });

```

## Advanced Data Operations

### **Filtering Data**

Filters can be applied when loading data:

```jsx
var loadOptions = {
    filter: ["state", "startswith", "A"],
    sort: [{ selector: "state", desc: false }],
    skip: 0, // Start from first record
    take: 2  // Load only 2 records
};

store.load(loadOptions).done(function(data) {
    console.log("Filtered data:", data);
});

```

Other filter conditions:

```jsx
["capital", "=", "Juneau"]
["id", "!=", 2]
["id", ">", 2]
[["id", "<", 2], "or", ["capital", "=", "Juneau"]]

```

### **Dynamic Property Access**

### **Getting a Property**

```jsx
store.byKey(1).done(function(item) {
    console.log("Property value:", item["state"]);
}).fail(function(error) {
    console.log("Error getting property:", error);
});

```

### **Setting a Property**

```jsx
store.byKey(1).done(function(item) {
    item.capital = "New Montgomery";
    store.update(1, item).done(function() {
        console.log("Property updated successfully");
    }).fail(function(error) {
        console.log("Error updating property:", error);
    });
});

```

## Event Handling

`ArrayStore` provides hooks for various operations:

```jsx
var store = new DevExpress.data.ArrayStore({
    key: "id",
    data: states,
    onLoading: function(loadOptions) {
        console.log("Loading data with options:", loadOptions);
    },
    onInserting: function(values) {
        console.log("Inserting:", values);
    },
    onInserted: function(values, key) {
        console.log("Inserted:", values, "with key:", key);
    },
    onModifying: function() {
        console.log("Data is being modified");
    },
    onModified: function(values, key) {
        console.log("Modified:", values, "with key:", key);
    },
    onRemoving: function(key) {
        console.log("Removing item with key:", key);
    },
    onRemoved: function(key) {
        console.log("Item removed with key:", key);
    }
});

```

## Best Practices

- **Use `key` to uniquely identify records** to avoid duplication issues.
- **Apply filters before loading data** to optimize performance.
- **Use event handlers** to monitor and debug operations.
- **Batch operations for large datasets** can improve efficiency.
- **Integrate with DevExtreme UI components** for seamless data binding.

## Summary

The `ArrayStore` is a powerful client-side storage solution for managing structured data in DevExtreme applications. It supports filtering, sorting, CRUD operations, and event handling, making it a versatile choice for handling local data collections.

---

# CustomStore

## Introduction

The `CustomStore` in DevExtreme provides a way to load, modify, and save data dynamically using custom logic. It is useful when working with REST APIs, databases, or any external data sources that do not fit into predefined stores like `ArrayStore` or `LocalStore`.

## Key Features

- Fetch data from remote APIs
- Apply filtering, sorting, paging, and grouping
- Perform CRUD (Create, Read, Update, Delete) operations
- Supports both synchronous and asynchronous data loading

## Creating a CustomStore

The `CustomStore` requires implementing several functions to define how data should be loaded, inserted, updated, and removed.

### Example Implementation

```jsx
var customStore = new DevExpress.data.CustomStore({
    key: "id", // Primary key field
    load: function(loadOptions) {
        return fetch("https://api.example.com/data")
            .then(response => response.json())
            .catch(error => { throw new Error("Data Loading Failed: " + error.message); });
    },
    byKey: function(key) {
        return fetch(`https://api.example.com/data/${key}`)
            .then(response => response.json());
    },
    insert: function(values) {
        return fetch("https://api.example.com/data", {
            method: "POST",
            body: JSON.stringify(values),
            headers: { "Content-Type": "application/json" }
        }).then(response => response.json());
    },
    update: function(key, values) {
        return fetch(`https://api.example.com/data/${key}`, {
            method: "PUT",
            body: JSON.stringify(values),
            headers: { "Content-Type": "application/json" }
        }).then(response => response.json());
    },
    remove: function(key) {
        return fetch(`https://api.example.com/data/${key}`, {
            method: "DELETE"
        }).then(response => response.json());
    },
    onLoading: function(loadOptions) {
        console.log("Loading data with options:", loadOptions);
    },
    onLoaded: function(data) {
        console.log("Data loaded successfully", data);
    },
    onInserting: function(values) {
        console.log("Inserting new record", values);
    },
    onInserted: function(values, key) {
        console.log("Inserted record", values, "with key", key);
    },
    onModifying: function() {
        console.log("Modifying data");
    },
    onModified: function(values, key) {
        console.log("Modified record", values, "with key", key);
    },
    onRemoving: function(key) {
        console.log("Removing record with key", key);
    },
    onRemoved: function(key) {
        console.log("Record removed successfully", key);
    },
    errorHandler: function(error) {
        console.log("Error: ", error.message);
    }
});

```

## CustomStore Properties and Methods

### Properties

| Property | Type | Description |
| --- | --- | --- |
| `key` | String | Defines the primary key field |
| `load` | Function | Fetches data from an external source |
| `byKey` | Function | Fetches a specific item using a key |
| `insert` | Function | Inserts a new item into the data source |
| `update` | Function | Updates an existing item |
| `remove` | Function | Removes an item from the data source |
| `errorHandler` | Function | Handles errors occurring in any CRUD operation |

### Events

| Event | Description |
| --- | --- |
| `onLoading` | Triggered before data is loaded |
| `onLoaded` | Triggered after data is successfully loaded |
| `onInserting` | Triggered before a record is inserted |
| `onInserted` | Triggered after a record is inserted |
| `onModifying` | Triggered before a record is modified |
| `onModified` | Triggered after a record is modified |
| `onRemoving` | Triggered before a record is removed |
| `onRemoved` | Triggered after a record is removed |

## LoadMode & Caching Options

## LoadMode Option

Controls how data processing occurs in CustomStore.

| Value | Description |
|-------|-------------|
| `"processed"` | Server handles filtering/sorting (default) |
| `"raw"` | Client-side processing after loading |

```javascript
new DevExpress.data.CustomStore({
    loadMode: "raw",  // Client-side processing
    load: function(loadOptions) {
        return fetch("https://api.example.com/data")
            .then(response => response.json());
    }
});
```

**When to use:**
- `"processed"`: Large datasets, server-side operations available
- `"raw"`: Small datasets, offline functionality needed

## CacheEnabled Option

Controls data caching to reduce redundant server requests.

```javascript
const store = new DevExpress.data.CustomStore({
    key: "id",
    cacheEnabled: true,  // Enable caching
    load: function(loadOptions) {
        return fetchDataFromServer(loadOptions);
    }
});

// Clear cache when needed
store.clearCache();
```

**Advanced options:**
- `cacheRawData: true` - Cache unprocessed data

## Best Practices

- Always handle errors gracefully in each CRUD operation.
- Use asynchronous functions (`async/await`) for cleaner code.
- Implement caching mechanisms for optimized performance.
- Validate data before sending requests to avoid unnecessary API calls.
- Use pagination (`skip`, `take`) to improve data fetching efficiency.
- Enable for static/reference data
- Disable for frequently changing data
- Clear cache after CRUD operations

## Conclusion

The `CustomStore` provides flexibility for integrating DevExtreme components with external data sources. By properly implementing the required methods and handling events, you can efficiently manage dynamic data from APIs or databases.

## DataSource

### Overview

`DataSource` in DevExtreme is a versatile component that serves as an intermediary between UI components and various data sources. It provides powerful capabilities for data management, including sorting, filtering, grouping, paging, and data modification.

### Key Features

- **Supports Various Data Sources**: Works with arrays, remote data, and other custom stores.
- **Data Operations**: Enables filtering, sorting, grouping, and pagination.
- **Integration with UI Components**: Seamlessly integrates with DevExtreme UI components such as DataGrid, TreeList, and Charts.
- **Event Handling**: Provides events for tracking data loading, changes, and errors.

### Creating a DataSource

A `DataSource` can be created from different sources such as an array, URL, or a custom store.

### Example: Creating a DataSource from an Array

```jsx
var states = [
    { id: 1, state: "Alabama", capital: "Montgomery" },
    { id: 2, state: "Alaska", capital: "Juneau" },
    { id: 3, state: "Arizona", capital: "Phoenix" }
];

var dataSource = new DevExpress.data.DataSource({
    store: states
});

dataSource.load().done(function(data) {
    console.log("Data Loaded:", data);
});

```

### Example: Creating a DataSource from a Remote Endpoint

```jsx
var remoteDataSource = new DevExpress.data.DataSource({
    load: function(loadOptions) {
        return $.getJSON("https://api.example.com/data");
    }
});

remoteDataSource.load().done(function(data) {
    console.log("Remote Data Loaded:", data);
});

```

### Configurable Options

### `store`

Defines the source of data. Can be an array, an `ArrayStore`, `CustomStore`, or `ODataStore`.

### `filter`

Applies filtering conditions.

```jsx
dataSource.filter(["state", "startswith", "A"]);
dataSource.load();

```

### `sort`

Sorts data based on a field.

```jsx
dataSource.sort({ selector: "state", desc: false });
dataSource.load();

```

### `group`

Groups data by a field.

```jsx
dataSource.group("state");
dataSource.load();

```

### `paginate`

Enables or disables pagination.

```jsx
var pagedDataSource = new DevExpress.data.DataSource({
    store: states,
    paginate: true,
    pageSize: 2
});

```

### Methods

### `load()`

Loads data from the source.

### `reload()`

Refreshes the data from the source.

### `insert(values)`

Adds new records.

```jsx
dataSource.store().insert({ id: 4, state: "California", capital: "Sacramento" });
dataSource.load();

```

### `update(key, values)`

Modifies existing records.

```jsx
dataSource.store().update(1, { capital: "New Montgomery" });
dataSource.load();

```

### `remove(key)`

Deletes a record.

```jsx
dataSource.store().remove(2);
dataSource.load();

```

### Events

### `onLoading` (Triggered before data loads)

```jsx
var ds = new DevExpress.data.DataSource({
    store: states,
    onLoading: function(loadOptions) {
        console.log("Data is loading", loadOptions);
    }
});

```

### `onLoaded` (Triggered after data loads)

```jsx
ds.on("loaded", function(data) {
    console.log("Data loaded successfully", data);
});

```

### `onChanged` (Triggered when data changes)

```jsx
ds.on("changed", function() {
    console.log("Data has been changed");
});

```

### Best Practices

1. **Use Remote Data Efficiently**: Minimize network requests by enabling caching where possible.
2. **Utilize Filtering & Sorting**: Reduce the data load by applying filters and sorting at the server level.
3. **Paginate Large Data Sets**: When dealing with large data sets, enable pagination for better performance.
4. **Error Handling**: Implement error handling in the `load` function to manage failed API calls gracefully.

### Conclusion

The `DataSource` in DevExtreme provides a robust mechanism for managing data efficiently. Its ability to integrate with different storage options and UI components makes it a powerful tool for handling data-driven applications.

# LocalStore

## Introduction

`LocalStore` is a **client-side data store** that **persists data in the browser** using **LocalStorage or SessionStorage**. This is useful for saving small datasets that need to persist between page reloads without requiring a server.

### Key Features

- Stores data in **LocalStorage** (default) or **SessionStorage**
- Data persists even after a page reload (if using LocalStorage)
- Supports **CRUD operations (Create, Read, Update, Delete)**
- Supports **filtering, sorting, paging, and grouping**

## Basic Usage

```jsx
var store = new DevExpress.data.LocalStore({
    key: "id", // Primary key field
    name: "myLocalData", // Storage key in LocalStorage
});

```

💡 **By default, `LocalStore` uses `window.localStorage` to store data.**

## Configuration Options

### 1. `key` (Primary Key)

- Defines the unique identifier for each record.
- **Type:** `String | Array`
- **Example:**
    
    ```jsx
    key: "id" // Unique key for each item
    
    ```
    

### 2. `name` (Storage Key)

- The name of the storage entry in **LocalStorage** or **SessionStorage**.
- **Type:** `String`
- **Example:**
    
    ```jsx
    name: "myDataStore"
    
    ```
    
- Data will be saved under "myDataStore" in LocalStorage.

### 3. `immediate` (Enable Auto-Save)

- Defines whether data is saved immediately after modification.
- **Type:** `Boolean`
- **Default:** `true`
- **Example:**
    
    ```jsx
    immediate: false // Data will not be saved automatically
    
    ```
    
- If `false`, you must manually call `store.save()` to persist changes.

### 4. `storage` (LocalStorage or SessionStorage)

- Defines where data is stored (`localStorage` or `sessionStorage`).
- **Type:** `"localStorage" | "sessionStorage"`
- **Default:** `"localStorage"`
- **Example:**
    
    ```jsx
    storage: "sessionStorage" // Store data in session storage (cleared on page reload)
    
    ```
    

## CRUD Operations

### 1. Insert a New Record

```jsx
store.insert({ id: 1, name: "Alice", age: 25 })
    .done(() => console.log("Data inserted successfully"))
    .fail(error => console.log("Insert failed:", error));

```

### 2. Read All Records

```jsx
store.load()
    .done(data => console.log("Loaded Data:", data))
    .fail(error => console.log("Load failed:", error));

```

### 3. Update an Existing Record

```jsx
store.update(1, { name: "Alice Johnson", age: 26 })
    .done(() => console.log("Data updated successfully"))
    .fail(error => console.log("Update failed:", error));

```

### 4. Remove a Record

```jsx
store.remove(1)
    .done(() => console.log("Record removed successfully"))
    .fail(error => console.log("Remove failed:", error));

```

## Advanced Features

### Filtering

```jsx
store.load({ filter: ["name", "contains", "Alice"] })
    .done(data => console.log("Filtered Data:", data));

```

### Sorting

```jsx
store.load({ sort: [{ selector: "age", desc: false }] })
    .done(data => console.log("Sorted Data:", data));

```

### Paging

```jsx
store.load({ skip: 0, take: 2 }) // Load first 2 records
    .done(data => console.log("Paged Data:", data));

```

## Manually Saving Data

If `immediate: false`, you must call `save()` manually:

```jsx
store.save();

```

## Clearing the Store

```jsx
store.clear()
    .done(() => console.log("LocalStore cleared"))
    .fail(error => console.log("Clear failed:", error));

```

## Use Case Scenarios

- Storing user preferences/settings
- Caching small datasets in the browser
- Saving form data temporarily
- Offline storage for small applications

## Limitations

- **Data limit:** LocalStorage has a 5MB storage limit
- **Security:** Data is not encrypted; do not store sensitive information
- **SessionStorage clears on reload:** Use `storage: "localStorage"` for persistence

## Conclusion

`LocalStore` in **DevExtreme** is a powerful way to store and manage data on the client-side. It supports **CRUD operations, filtering, sorting, paging, and grouping**. It is ideal for small-scale data storage needs without a backend. 🚀

# Query

## Introduction

DevExtreme's `query` module provides a powerful way to perform data operations such as filtering, sorting, selecting fields, aggregation, grouping, and transformation on arrays and collections. It allows for efficient data handling, especially when dealing with large datasets.

## Basic Usage

The `query` function is used to process collections of data using method chaining. It supports various operations like filtering, sorting, grouping, and aggregation.

### Example Data

```jsx
var dataObjects = [
    { name: "Amelia", birthYear: 1991, gender: "female" },
    { name: "Benjamin", birthYear: 1983, gender: "male" },
    { name: "Daniela", birthYear: 1987, gender: "female" },
    { name: "Lee", birthYear: 1981, gender: "male" }
];

```

---

## Filtering Data

### Filtering with a Simple Condition

```jsx
var filteredData = DevExpress.data.query(dataObjects)
    .filter(["gender", "=", "female"])
    .toArray();
console.log(filteredData);

```

**Explanation:**

- Filters only records where `gender` is "female".

### Filtering with a Function

```jsx
var filteredData = DevExpress.data.query(dataObjects)
    .filter(function (dataItem) {
        return dataItem.birthYear > 1985;
    })
    .toArray();
console.log(filteredData);

```

**Explanation:**

- Filters records where `birthYear` is greater than 1985.

---

## Sorting Data

### Sorting by a Single Field

```jsx
var sortedData = DevExpress.data.query(dataObjects)
    .sortBy("birthYear")
    .toArray();
console.log(sortedData);

```

**Explanation:**

- Sorts the data in ascending order by `birthYear`.

### Sorting by Multiple Fields

```jsx
var sortedData = DevExpress.data.query(dataObjects)
    .sortBy("gender")
    .thenBy("birthYear")
    .toArray();
console.log(sortedData);

```

**Explanation:**

- First sorts by `gender`, then by `birthYear` within each gender.

---

## Selecting Specific Fields

```jsx
var selectedData = DevExpress.data.query(dataObjects)
    .select("name", "birthYear")
    .toArray();
console.log(selectedData);

```

**Explanation:**

- Extracts only `name` and `birthYear` fields from the dataset.

---

## Grouping Data

```jsx
var groupedData = DevExpress.data.query(dataObjects)
    .groupBy("gender")
    .toArray();
console.log(groupedData);

```

**Explanation:**

- Groups records by `gender`.

---

## Aggregation Operations

### Counting Items

```jsx
DevExpress.data.query([10, 20, 30, 40, 50])
    .count()
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Counts the total number of elements in the array.

### Finding Minimum and Maximum Values

```jsx
DevExpress.data.query(dataObjects)
    .max("birthYear")
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Finds the maximum `birthYear`.

```jsx
DevExpress.data.query(dataObjects)
    .min("birthYear")
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Finds the minimum `birthYear`.

### Summing Values

```jsx
DevExpress.data.query(dataObjects)
    .select("birthYear")
    .sum()
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Computes the sum of all `birthYear` values.

### Average Calculation

```jsx
DevExpress.data.query([10, 20, 30, 40, 50])
    .avg()
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Computes the average value of the array.

---

## Pagination (Slicing Data)

```jsx
var subset = DevExpress.data.query(dataObjects)
    .slice(1, 2)
    .toArray();
console.log(subset);

```

**Explanation:**

- Extracts a subset of the array, starting at index `1` and retrieving `2` items.

---

## Enumerating Items

```jsx
DevExpress.data.query(dataObjects)
    .enumerate()
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Retrieves all items from the dataset as an indexed list.

---

## Advanced Aggregation

### Custom Step and Finalize Functions

```jsx
var step = function (total, itemData) {
    return total + itemData;
};

var finalize = function (total) {
    return total / 1000;
};

DevExpress.data.query([10, 20, 30, 40, 50])
    .aggregate(0, step, finalize)
    .done(function (result) {
        console.log(result);
    });

```

**Explanation:**

- Custom aggregation where `step` accumulates values and `finalize` normalizes the total.

---

## Summary

The `query` module in DevExtreme provides a robust and flexible way to manipulate data collections. With its rich API for filtering, sorting, selecting, grouping, aggregating, and transforming data, it is an essential tool for efficiently managing data operations.

## Comparison Of DataLayer Types

| **DataLayer Type** | **Description** | **Use Case** |
| --- | --- | --- |
| **ArrayStore** | Stores data in an in-memory array. Supports CRUD operations on client-side data. | Ideal for small datasets that don't require server interaction. |
| **LocalStore** | Similar to `ArrayStore` but persists data in `localStorage` or `sessionStorage`. | Used when data should be retained across page reloads without a backend. |
| **CustomStore** | Allows custom implementation of data loading, updating, inserting, and deleting by making AJAX requests to a server. | Best for working with APIs, databases, or remote services. |
| **ODataStore** | Supports interaction with OData services, handling filtering, paging, and CRUD operations automatically. | Used for applications working with OData-based RESTful APIs. |
| **DataSource** | Acts as a wrapper around any of the above stores, adding features like data shaping, caching, and custom loading logic. | Used when working with multiple data providers or implementing complex data manipulations. |