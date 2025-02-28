# DevExtreme DataGrid Documentation

## Index
1. [Introduction](#introduction)
2. [Installation & Configuration](#installation--configuration)
3. [Data Binding](#data-binding)
   - [Binding with a Simple Array](#binding-with-a-simple-array)
   - [Binding with an AJAX Request](#binding-with-an-ajax-request)
4. [Paging and Scrolling](#paging-and-scrolling)
   - [Record Paging](#record-paging)
   - [Virtual Scrolling](#virtual-scrolling)
   - [Infinite Scrolling](#infinite-scrolling)
   - [Differences Between Virtual Scrolling & Infinite Scrolling](#differences-between-virtual-scrolling--infinite-scrolling)
5. [DataGrid Editing](#datagrid-editing)
   - [Row Editing & Editing Events](#1-row-editing--editing-events)
   - [Batch Editing](#2-batch-editing)
   - [Cell Editing](#3-cell-editing)
   - [Form Editing](#4-form-editing)
   - [Popup Editing](#5-popup-editing)
6. [Data Validation](#data-validation)
   - [Cascading Lookups](#cascading-lookups)
7. [Grouping](#grouping)
8. [Filtering](#filtering)
   - [Implementing Filtering](#implementing-filtering-in-devextreme-datagrid)
   - [Types of Filtering](#types-of-filtering-in-devextreme-datagrid)
9. [Sorting](#sorting)
   - [Multiple Sorting](#multiple-sorting)
   - [Initial and Runtime Sorting](#initial-and-runtime-sorting)
   - [Custom Sorting](#custom-sorting)
10. [Selection](#selection)
11. [Columns](#columns)
    - [Column Customization](#1-column-customization)
    - [Columns Based on a Data Source](#2-columns-based-on-a-data-source)
    - [Multi-Row Headers](#3-multi-row-headers)
    - [Column Resizing](#4-column-resizing)
    - [Command Column Customization](#5-command-column-customization)
12. [State Persitence](#state-persistence)
13. [Appearance](#appearance)
    - [Built-in Themes](#built-in-themes)
    - [Grid Borders and Row Alternation](#grid-borders-and-row-alternation)
    - [Cell Styling using CSS](#cell-styling-using-css)
    - [Customizing Row and Cell Styles](#customizing-row-and-cell-styles)
    - [Column Header Customization](#column-header-customization)
    - [Custom Icons in Command Columns](#custom-icons-in-command-columns)
14. [Template](#template)
    - [Column Template](#column-template)
    - [Row Template](#row-template)
15. [Toolbar Customization](#toolbar-customization)
16. [Data Summaries](#data-summaries)
    - [Grid Summaries](#1-grid-summaries)
    - [Group Summaries](#2-group-summaries)
    - [Custom Summaries](#3-custom-summaries)
17. [Master-Detail View](#master-detail-view)
18. [Export](#export)
    - [Required Packages for .NET Framework](#required-packages-for-net-framework)
    - [Required CDN Links](#required-cdn-links)
    - [Adding Export Button in Toolbar](#adding-export-button-in-toolbar)
    - [Export to Excel](#export-to-excel)
    - [Export to Csv](#export-to-csv)
    - [Export to Pdf](#export-to-pdf)
19. [Adaptability](#adaptability)
    - [Grid Adaptability Overview](#grid-adaptability-overview)
    - [Grid Columns Hiding Priority](#grid-columns-hiding-priority)

---

## Introduction

DevExtreme DataGrid is a powerful UI component that allows displaying, managing, and manipulating tabular data in web applications. It supports various features such as paging, sorting, filtering, and virtual scrolling.

## Installation & Configuration

To use DevExtreme DataGrid in an ASP.NET application, install the necessary NuGet package:

```jsx
Install-Package DevExtreme.AspNet.Data
```

Include the required JavaScript and CSS files in your HTML or layout file:

```html
<!-- CSS -->
<link rel="stylesheet" href="https://cdn3.devexpress.com/jslib/latest/css/dx.light.css" />

<!-- JS -->
<script src="https://cdn3.devexpress.com/jslib/latest/js/dx.all.js"></script>

```

## Data Binding

### Binding with a Simple Array

```jsx
$(function () {
    const employees = [
        { ID: 1, Name: "John Doe", Position: "Manager", Age: 35 },
        { ID: 2, Name: "Jane Smith", Position: "Developer", Age: 28 },
        { ID: 3, Name: "Sam Johnson", Position: "Designer", Age: 42 }
    ];

    $('#dataGridContainer1').dxDataGrid({
        dataSource: employees,
        columns: [
            { dataField: 'ID', caption: 'ID', width: 50 },
            'Name',
            'Position',
            { dataField: 'Age', caption: 'Age (Years)' }
        ],
        showBorders: true,
    });
});

```

### Binding with an AJAX Request

```jsx
$(function () {
    let store = new DevExpress.data.CustomStore({
        load: async function () {
            let data = await $.get("https://dummyjson.com/users");
            return data.users;
        }
    });
\
    $('#dataGridContainer2').dxDataGrid({
        dataSource: store,
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 20, 30],
            showInfo: true,
            displayMode: 'adaptive',
            infoText: 'Page {0} of {1} ({2} items)',
            showNavigationButtons: true,
            visible: 'auto'
        }
    });
});

```

## Paging and Scrolling

### Record Paging

Record paging divides data into discrete pages, allowing users to navigate between them. This is useful when handling a large dataset efficiently without overloading the UI.

```jsx
$(function () {
    $('#dataGridContainer3').dxDataGrid({
        dataSource: store,
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 20, 30],
            showInfo: true,
            displayMode: 'adaptive',
            infoText: 'Page {0} of {1} ({2} items)',
            showNavigationButtons: true,
            visible: 'auto'
        }
    });
});

```

### Virtual Scrolling

Virtual scrolling loads only a small portion of data at a time while the user scrolls, significantly improving performance when dealing with large datasets.

```jsx
$(function () {
    let virtualStore = new DevExpress.data.CustomStore({
        load: async function () {
            let data = await $.get("https://dummyjson.com/users?limit=1000&skip=0&select=id,firstName,lastName,age,username,email");
            return data.users;
        }
    });

    $('#dataGridContainer4').dxDataGrid({
        dataSource: virtualStore,
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'
        }
    });
});

```

### Infinite Scrolling

Infinite scrolling continuously loads new data as the user scrolls down, eliminating the need for pagination.

```jsx
$(function () {
    $('#dataGridContainer5').dxDataGrid({
        dataSource: virtualStore,
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'infinite'
        }
    });
});

```

## Differences Between Virtual Scrolling & Infinite Scrolling

| Feature | Virtual Scrolling | Infinite Scrolling |
| --- | --- | --- |
| Data Loading | Loads a small portion at a time, replacing off-screen data | Continuously loads new data as user scrolls |
| Performance | Efficient for large datasets | Can become slow with very large datasets |
| User Experience | Keeps a fixed grid height and replaces data | Extends grid height dynamically |
| Best Use Case | When navigating large datasets while maintaining UI consistency | When an endless scrolling experience is required |

# Editing

DevExtreme DataGrid provides multiple editing modes that allow users to modify data directly within the grid. These include **row editing, batch editing, cell editing, form editing, popup editing**, and support for **data validation and cascading lookups**.

---

## 1. Row Editing & Editing Events

Row editing allows users to edit data row by row. Users can edit a row and save changes before moving to another row.

### Implementation:

```jsx
$(function () {
    $('#dataGridRowEditing').dxDataGrid({
        dataSource: employees,
        columns: ['ID', 'Name', 'Position', 'Age'],
        editing: {
            mode: 'row',  // Enable row editing
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },
        onRowUpdating: function (e) {
            console.log("Row Updated", e.newData);
        },
        onRowRemoving: function (e) {
            console.log("Row Deleted", e.data);
        }
    });
});
```

## **Editing Events**

## 1. Row Editing & Editing Events

### **onRowUpdating**

- **Called When:** A row update is triggered.
- **Usage:** Validate data before updating.
- **Example:**
    
    ```jsx
    onRowUpdating: function (e) {
        console.log('Row updating:', e.oldData, '=>', e.newData);
    }
    ```
    

### **onRowUpdated**

- **Called When:** A row update is completed.
- **Usage:** Handle post-update actions.
- **Example:**
    
    ```jsx
    onRowUpdated: function (e) {
        console.log('Row updated:', e.data);
    }
    ```
    

### **onRowInserted**

- **Called When:** A new row is successfully inserted.
- **Usage:** Log new records or show a success message.
- **Example:**
    
    ```jsx
    onRowInserted: function (e) {
        console.log('Row inserted:', e.data);
    }
    
    ```
    

### **onRowRemoving**

- **Called When:** A row is about to be removed.
- **Usage:** Confirm before deleting a row.
- **Example:**
    
    ```jsx
    onRowRemoving: function (e) {
        if (!confirm('Are you sure you want to delete this row?')) {
            e.cancel = true;
        }
    }
    ```
    

### **onRowRemoved**

- **Called When:** A row is deleted.
- **Usage:** Log deleted records or audit deletion.
- **Example:**
    
    ```jsx
    onRowRemoved: function (e) {
        console.log('Row removed:', e.data);
    }
    ```
    

### **onEditingStart**

- **Called When:** A row enters edit mode.
- **Usage:** Customize editing behavior.
- **Example:**
    
    ```jsx
    onEditingStart: function (e) {
        console.log('Editing started:', e);
    }
    
    ```
    

### **onSaving**

- **Called When:** Data is being saved in batch mode.
- **Usage:** Validate batch edits before saving.
- **Example:**
    
    ```jsx
    onSaving: function (e) {
        console.log('Saving changes:', e);
    }
    
    ```
    

### **onSaved**

- **Called When:** Data changes are successfully saved.
- **Usage:** Notify users after batch save.
- **Example:**
    
    ```jsx
    onSaved: function (e) {
        console.log('Changes saved:', e);
    }
    
    ```
    

### **onCellClick**

- **Called When:** A cell is clicked.
- **Usage:** Capture clicked cell data.
- **Example:**
    
    ```jsx
    onCellClick: function (e) {
        console.log(e.rowIndex, e.column, e.value, e.data);
    }
    
    ```
    

### **onCellDblClick**

- **Called When:** A cell is double-clicked.
- **Usage:** Open detailed views on double-click.
- **Example:**
    
    ```jsx
    onCellDblClick: function (e) {
        console.log(e.rowIndex, e.data);
    }
    
    ```
    

### **onEditorPreparing**

- **Called When:** An editor is being prepared.
- **Usage:** Modify the editor before rendering.
- **Example:**
    
    ```jsx
    onEditorPreparing: function (e) {
        console.log('Preparing editor for:', e);
    }
    
    ```
    

### **onEditorPrepared**

- **Called When:** An editor is rendered.
- **Usage:** Customize appearance or behavior.
- **Example:**
    
    ```jsx
    onEditorPrepared: function (e) {
        console.log('Editor prepared for:', e);
    }
    
    ```
    

### **onEditCanceled**

- **Called When:** Edit is canceled.
- **Usage:** Notify users about canceled edits.
- **Example:**
    
    ```jsx
    onEditCanceled: function (e) {
        console.log('Edit canceled for row:', e.rowIndex, e.data);
    }
    ```
    

### **onEditCanceling**

- **Called When:** An edit cancellation is attempted.
- **Usage:** Confirm before canceling an edit.
- **Example:**
    
    ```jsx
    onEditCanceling: function (e) {
        if (!confirm('Are you sure you want to cancel the edit?')) {
            e.cancel = true;
        }
    }
    
    ```
    

### **onDataErrorOccurred**

- **Called When:** A data validation error occurs.
- **Usage:** Notify users about errors.
- **Example:**
    
    ```jsx
    onDataErrorOccurred: function (e) {
        console.error('Data Error:', e.error.message);
    }
    ```
    

### **onSelectionChanged**

- **Called When:** A selection is changed.
- **Usage:** Update dependent dropdowns dynamically.
- **Example:**
    
    ```jsx
    onSelectionChanged: function (e) {
        console.log('Selection changed:', e);
    }
    ```
    

This document provides an overview of DevExtreme DataGrid editing events with their respective usage and examples.

---

## 2. Batch Editing

Batch editing allows users to modify multiple cells without submitting each change individually. Changes are submitted in bulk.

### Implementation:

```jsx
$(function () {
    $('#dataGridBatchEditing').dxDataGrid({
        dataSource: employees,
        columns: ['ID', 'Name', 'Position', 'Age'],
        editing: {
            mode: 'batch',  // Enable batch editing
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true
        }
    });
});

```

---

## 3. Cell Editing

Cell editing mode allows users to edit individual cells without affecting other cells in the row.

### Implementation:

```jsx
$(function () {
    $('#dataGridCellEditing').dxDataGrid({
        dataSource: employees,
        columns: ['ID', 'Name', 'Position', 'Age'],
        editing: {
            mode: 'cell',  // Enable cell editing
            allowUpdating: true
        }
    });
});

```

---

## 4. Form Editing

Form editing mode allows users to edit rows using a form layout instead of inline editing.

### Implementation:

```jsx
$(function () {
    $('#dataGridFormEditing').dxDataGrid({
        dataSource: employees,
        columns: ['ID', 'Name', 'Position', 'Age'],
        editing: {
            mode: 'form',  // Enable form editing
            allowUpdating: true,
            allowAdding: true
        }
    });
});

```

---

## 5. Popup Editing

Popup editing opens a modal popup for data modification.

### Implementation:

```jsx
$(function () {
    $('#dataGridPopupEditing').dxDataGrid({
        dataSource: employees,
        columns: ['ID', 'Name', 'Position', 'Age'],
        editing: {
            mode: 'popup',  // Enable popup editing
            allowUpdating: true,
            allowAdding: true,
            popup: {
                title: 'Edit Employee',
                showTitle: true
            }
        }
    });
});

```

---

## 6. Data Validation

Data validation ensures that users enter correct data before submission.

### Implementation:

```jsx
$(function () {
    $('#dataGridValidation').dxDataGrid({
        dataSource: employees,
        columns: [
            { dataField: 'ID', caption: 'ID', allowEditing: false },
            {
                dataField: 'Name',
                validationRules: [{ type: 'required' }]
            },
            {
                dataField: 'Age',
                validationRules: [{ type: 'range', min: 18, max: 60 }]
            }
        ],
        editing: {
            mode: 'cell',
            allowUpdating: true
        }
    });
});
```

---

## 7. Cascading Lookups

Cascading lookups allow dependent dropdowns where selecting a value in one column filters the available options in another.

### Implementation:

```jsx
$(function () {
    $('#dataGridCascadingLookups').dxDataGrid({
        dataSource: employees,
        columns: [
            { dataField: 'Department', lookup: { dataSource: departments, displayExpr: 'name', valueExpr: 'id' } },
            {
                dataField: 'Position',
                lookup: {
                    dataSource: function (options) {
                        return positions.filter(pos => pos.departmentId === options.data.Department);
                    },
                    displayExpr: 'name',
                    valueExpr: 'id'
                }
            }
        ],
        editing: {
            mode: 'row',
            allowUpdating: true
        }
    });
});

```

---

## Grouping

Grouping in DevExtreme DataGrid allows users to organize data by categorizing rows based on shared values in a particular column. This feature enhances data visualization by providing collapsible groups for better analysis and readability.

## **1. Enabling Grouping in DataGrid**

To enable grouping, set the `grouping` and `groupPanel` options in the DataGrid configuration.

### **Example:**

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: employees,
    keyExpr: "ID",
    grouping: {
        autoExpandAll: false // Determines if groups should be expanded by default
    },
    groupPanel: {
        visible: true // Enables the group panel for drag-and-drop grouping
    },
    allowColumnReordering: true,
    columns: [
        { dataField: "Department", groupIndex: 0 },
        { dataField: "Position" },
        { dataField: "Name" },
        { dataField: "Salary", format: "currency" }
    ]
});

```

## **2. Configuring Grouping Behavior**

### **2.1 groupIndex Property**

- Defines the order in which columns are grouped.
- A lower index groups data first.
- `groupIndex: 0` means the column is grouped at the top level.

### **2.2 autoExpandAll Property**

- Controls whether groups are expanded or collapsed by default.
- `true` expands all groups initially, `false` keeps them collapsed.

### **2.3 showGroupedColumns Property**

- Determines whether grouped columns are displayed in the grid.
- `true`: Grouped columns remain visible.
- `false`: Grouped columns are hidden from the grid.

```jsx
grouping: {
    autoExpandAll: true, // Expands all groups initially
},
columnChooser: {
    enabled: true // Allows users to toggle column visibility
}

```

## **3. Customizing Group Row Display**

### **3.1 Customize Group Summary**

Summaries provide aggregate values such as count, sum, or average.

```jsx
summary: {
    groupItems: [
        { column: "Salary", summaryType: "sum", displayFormat: "Total: {0}" },
        { column: "ID", summaryType: "count", displayFormat: "Count: {0}" }
    ]
}

```

### **3.2 Custom Group Cell Template**

Customizing the appearance of group headers.

```jsx
columns: [{
    dataField: "Department",
    groupIndex: 0,
    groupCellTemplate: function (element, options) {
        element.text(options.value + " (" + options.data.items.length + ")");
    }
}]

```

## **4. Handling Grouping Events**

### **onOptionChanged**

Triggers when a grouping-related setting changes.

```jsx
onOptionChanged: function (e) {
    if (e.name === "grouping") {
        console.log("Grouping changed", e);
    }
}

```

### **onContentReady**

Executes after data is loaded and grouped.

```jsx
onContentReady: function (e) {
    console.log("Grid ready with grouping", e);
}

```

## **5. Advanced Grouping Techniques**

### **5.1 Multiple Column Grouping**

```jsx
columns: [
    { dataField: "Department", groupIndex: 0 },
    { dataField: "Position", groupIndex: 1 }
]

```

### **5.2 Custom Group Sorting**

Sorting grouped columns by a specific order.

```jsx
columns: [{
    dataField: "Department",
    groupIndex: 0,
    sortOrder: "desc"
}]

```

## Filtering

Filtering in DevExtreme DataGrid allows users to refine data visibility by applying conditions on one or multiple columns. DevExtreme provides multiple filtering options such as the filter row, header filters, search panel, and a dedicated filter panel. The goal of filtering is to help users quickly find relevant records in large datasets.

---

### Implementing Filtering in DevExtreme DataGrid

The following JavaScript code demonstrates how to implement filtering in a DevExtreme DataGrid using various filtering techniques.

### **Dataset Definition**

We begin by defining a sample dataset that includes company details such as `Company Name`, `City`, `Phone`, `Industry`, `Employees`, `Revenue`, and `Founded Year`.

```jsx
const data = [
    { ID: 1, CompanyName: 'Microsoft', City: 'Redmond', Phone: '+1-800-555-0101', Industry: 'Technology', Employees: 144000, Revenue: 168000, Founded: 1975 },
    { ID: 2, CompanyName: 'Google', City: 'Mountain View', Phone: '+1-800-555-0102', Industry: 'Technology', Employees: 156500, Revenue: 258000, Founded: 1998 },
    { ID: 3, CompanyName: 'Amazon', City: 'Seattle', Phone: '+1-800-555-0103', Industry: 'E-Commerce', Employees: 1298000, Revenue: 469800, Founded: 1994 },
    { ID: 4, CompanyName: 'Apple', City: 'Cupertino', Phone: '+1-800-555-0104', Industry: 'Technology', Employees: 147000, Revenue: 274500, Founded: 1976 }
];
```

### **DataGrid Initialization with Filtering Options**

We configure a DataGrid component to enable various filtering features:

```jsx
$('#gridContainer').dxDataGrid({
    dataSource: data,
    keyExpr: 'ID',
    showBorders: true,
    columnAutoWidth: true,

    filterRow: {
        visible: true,
        applyFilter: 'auto'
    },
    headerFilter: {
        visible: true,
        allowSearch: true
    },
    searchPanel: {
        visible: true
    },
    filterSyncEnabled: false,
    filterPanel: { visible: true },

    columns: [
        { dataField: 'CompanyName', caption: 'Company Name', allowFiltering: false, allowHeaderFiltering: true },
        { dataField: 'City', caption: 'City', headerFilter: { filterType: "exclude" } },
        { dataField: 'Phone', caption: 'Phone', selectedFilterOperation: "contains", filterValue: "800" },
        { dataField: 'Industry', caption: 'Industry', filterOperations: ['contains', '='] },
        { dataField: 'Employees', caption: 'Employees', filterOperations: ['=', '<>', '>', '<', '>=', '<='] },
        { dataField: 'Founded', caption: 'Founded Year', filterOperations: ['=', '>', '<', '>=', '<='] }
    ],

    paging: {
        pageSize: 10
    }
});

```

---

### **Types of Filtering in DevExtreme DataGrid**

### **1. Filter Row**

- The filter row appears at the top of the grid and allows users to enter values to filter each column.
- It can be configured using:

```jsx
filterRow: {
    visible: true, // Enables the filter row
    applyFilter: 'auto' // Applies filtering automatically when typing
}
```

### **2. Header Filter**

- The header filter provides dropdown menus in column headers, allowing users to filter values.
- Search within the header filter can be enabled:

```jsx
headerFilter: {
    visible: true,
    allowSearch: true
}

```

### **3. Search Panel**

- A search panel allows users to filter records globally using a text input box.

```jsx
searchPanel: {
    visible: true
}

```

### **4. Filter Panel**

- The filter panel displays the applied filter criteria in a user-friendly way.

```jsx
filterPanel: { visible: true }

```

### **5. Custom Filtering with Filter Builder**

- Users can define advanced custom filters:

```jsx
filterBuilder: {
    customOperations: [{
        name: "isZero",
        caption: "Is Zero",
        dataTypes: ["number"],
        hasValue: false,
        calculateFilterExpression: function(filterValue, field) {
            return [field.dataField, "=", 0];
        }
    }]
}
```

---

### **Column-Specific Filtering**

Each column in the grid can have its own filtering logic:

- **`City` Column** (Excluding specific values):

```jsx
headerFilter: { filterType: "exclude" }

```

- **`Phone` Column** (Filters using "contains"):

```jsx
selectedFilterOperation: "contains",
filterValue: "800"

```

- **`Revenue` Column** (Range-based filter options):

```jsx
headerFilter: {
    dataSource: [
        { text: 'Less than $30000', value: ['Revenue', '<', 30000] },
        { text: '$30000 - $50000', value: [['Revenue', '>=', 30000], ['Revenue', '<', 50000]] },
        { text: '$50000 - $100000', value: [['Revenue', '>=', 50000], ['Revenue', '<', 100000]] },
        { text: '$100000 - $200000', value: [['Revenue', '>=', 100000], ['Revenue', '<', 200000]] },
        { text: 'Greater than $200000', value: ['Revenue', '>=', 200000] }
    ]
}

```

---

### **Additional Enhancements**

---

1. Implement custom filter logic for specific business cases.
2. Combine filtering with sorting and grouping for better data organization.
3. Save and restore filter states for persistent user experience.

---

## Sorting

Sorting is an essential feature in `DevExtreme DataGrid`, which allows users to arrange data in a meaningful order. You can implement sorting by column, use multiple sorting levels, customize sorting behavior, and apply sorting dynamically. This document covers multiple sorting, API sorting, and custom sorting examples.

---

## **Multiple Sorting**

Multiple sorting allows users to sort data by more than one column simultaneously. In DevExtreme, this can be done by holding the `Shift` key and clicking on column headers. This ensures the sorting of one column doesn’t remove the sorting settings of other columns.

### **Example**

```jsx
$('#gridContainer1').dxDataGrid({
    dataSource: virtualStore,
    columns: [
        { dataField: 'id', caption: 'ID', width: 50 },
        { dataField: 'firstName', caption: 'First Name', allowSorting: false },
        { dataField: 'username', caption: 'Username' },
        { dataField: 'email', caption: 'Email' },
        { type: 'buttons', width: 110, buttons: ['edit', 'delete'] },
    ],
    sorting: {
        mode: "multiple"  // Enables multiple column sorting
    },
    paging: { pageSize: 10 },
    editing: {
        mode: 'cell',
        allowUpdating: true,
        allowDeleting: true,
        allowAdding: true
    }
});
```

### **Key Points**

- The sorting `mode: "multiple"` allows multiple column sorting.
- The user can hold `Shift` and click the column header to sort by multiple columns.
- The `allowSorting: false` disables sorting for specific columns.

---

## **Initial and Runtime Sorting**

You can set initial sorting and modify sorting behavior during runtime. This can be done by specifying the `sortIndex` and `sortOrder` properties.

### **Example**

```jsx

let gridContainer2 = $('#gridContainer2').dxDataGrid({
    dataSource: virtualStore,
    columns: [
        { dataField: 'id', caption: 'ID', width: 50 },
        { dataField: 'firstName', caption: 'First Name', sortIndex: 1, sortOrder: 'asc' },
        { dataField: 'username', caption: 'Username', sortIndex: 0, sortOrder: 'desc' },
        { dataField: 'email', caption: 'Email' },
        { type: 'buttons', width: 110, buttons: ['edit', 'delete'] },
    ],
    sorting: {
        mode: "multiple"  // Allows multiple sorting
    },
    paging: { pageSize: 10 },
    editing: {
        mode: 'cell',
        allowUpdating: true,
        allowDeleting: true,
        allowAdding: true
    }
});

```

### **Dynamic Sorting API**

You can dynamically change the sorting of a column using the `columnOption` method.

### **Example: Changing the Sort Order Dynamically**

```jsx

$('#button').on('click', function () {
    gridContainer2.columnOption("username", {
        sortIndex: 0,
        sortOrder: 'asc'
    });
});
```

### **Clearing Sorting**

To clear the sorting for a column, use the `columnOption` method to set the `sortOrder` to `"none"`.

```jsx

$('#btnClearSort').on('click', function () {
    gridContainer2.columnOption("firstName", {
        sortOrder: "none",  // Removes the sort order
        sortIndex: undefined
    });
});
```

---

## **Custom Sorting**

Custom sorting allows sorting based on complex criteria, such as multiple fields or conditions. In this example, employees on vacation are sorted after non-vacationing employees.

### **Example**

```jsx

let data = [
    { id: 1, firstName: "John", lastName: "Doe", position: "Manager", isOnVacation: false },
    { id: 2, firstName: "Jane", lastName: "Smith", position: "Developer", isOnVacation: true },
    // More data...
];

let gridContainer3 = $("#gridContainer3").dxDataGrid({
    dataSource: data,
    columns: [
        { dataField: "id", caption: "ID", width: 50 },
        { dataField: "firstName", caption: "First Name" },
        { dataField: "lastName", caption: "Last Name" },
        {
            dataField: "position",
            sortOrder: "asc", // Default sorting order
            calculateSortValue: function (rowData) {
                if (rowData.isOnVacation) {
                    return "zzz_" + rowData.position;  // Vacationing employees sorted last
                }
                return "aaa_" + rowData.position;  // Non-vacationing employees sorted first
            }
        }
    ],
    sorting: {
        mode: "multiple"
    },
    paging: { pageSize: 5 }
});
```

### **Key Points**

- **`calculateSortValue`**: Used for custom sorting logic.
- Employees who are on vacation are sorted last by modifying the sort value dynamically.
- This sorting logic is applied to the `position` column, but you can adjust it for other columns as needed.

---

## **Conclusion**

The DevExtreme DataGrid provides extensive features for sorting, including multiple column sorting, dynamic sorting, and custom sorting logic. You can configure sorting settings both initially and at runtime, allowing for flexibility based on the application's needs.

- **Multiple Sorting**: Allows sorting by multiple columns using the `Shift` key.
- **Runtime Sorting**: Dynamically change sorting with `columnOption`.
- **Custom Sorting**: Apply complex sorting logic with `calculateSortValue`.

## Selection

In DevExpress `dxDataGrid`, selection allows you to interact with rows, either for highlighting, modifying, or executing actions based on the rows selected. Selection can be controlled in a variety of ways, from simple row selection to complex scenarios where you manage multiple selections, track selected keys, and respond to user actions like clicking or checkbox toggling.

Here are the main properties and methods that govern selection in `dxDataGrid`, explained with examples:

---

### **Key Properties and Behaviors of Selection**

### 1. **`selection` Object**:

- **Purpose**: Defines how rows are selected and handled.
- **Properties**:
    - `mode`: Determines the selection mode.
        - **Options**: `"single"`, `"multiple"`, `"none"`.
        - `"single"` allows only one row to be selected at a time.
        - `"multiple"` allows multiple rows to be selected.
    - `selectAllMode`: Determines whether all rows on the page can be selected.
        - **Options**: `"page"` or `"all"`.
    - `allowSelectAll`: Controls whether the "select all" checkbox is available.
    - `showCheckBoxesMode`: Shows checkboxes on rows and affects multi-row selection.
        - **Options**: `"always"`, `"auto"`, `"none"`.
    - **Example**:
        
        ```jsx
        
        selection: {
            mode: "multiple",  // Allow multiple rows to be selected
            selectAllMode: "page",  // Allow select all on the current page only
            allowSelectAll: true,  // Enable select all checkbox
            showCheckBoxesMode: "always"  // Always show checkboxes
        }
        ```
        

### 2. **`selectedRowKeys`**:

- **Purpose**: Contains the keys of all currently selected rows.
- **Use**: Can be used to programmatically select rows or track the current selection.
- **Example**: Preselect specific rows by their key.
    
    ```jsx
    
    selectedRowKeys: [1, 5, 18],  // Preselect rows with IDs 1, 5, and 18
    ```
    

### 3. **`onSelectionChanged` Event**:

- **Purpose**: Fired when the selection changes (either through user interaction or programmatically).
- **Parameters**:
    - `e`: Event object containing the details of the selection change.
    - **`e.selectedRowKeys`**: Array of currently selected row keys.
    - **`e.selectedRowsData`**: Data of the currently selected rows.
    - **`e.currentSelectedRowKeys`**: Newly selected row keys.
    - **`e.currentDeselectedRowKeys`**: Row keys that were deselected.
- **Example**:
    
    ```jsx
    
    onSelectionChanged: function(e) {
        console.log("Selected row keys:", e.selectedRowKeys);
        console.log("Selected rows data:", e.selectedRowsData);
    }
    ```
    

---

### **Selection Methods**

These are the key methods to interact with selections programmatically:

### 1. **`selectRows(keys, preserve)`**:

- **Purpose**: Selects rows with specific keys.
- **Parameters**:
    - `keys`: An array of keys to select.
    - `preserve`: A boolean. If `true`, it preserves the existing selection and adds the new keys. If `false`, it replaces the existing selection.
- **Example**:
    
    ```jsx
    
    gridContainer.selectRows([1, 3, 5], true);  // Selects rows 1, 3, and 5 while preserving existing selections
    ```
    

### 2. **`deselectRows(keys)`**:

- **Purpose**: Deselects rows by their keys.
- **Parameters**:
    - `keys`: An array of row keys to deselect.
- **Example**:
    
    ```jsx
    
    gridContainer.deselectRows([1, 3]);  // Deselect rows 1 and 3
    ```
    

### 3. **`clearSelection()`**:

- **Purpose**: Clears all row selections in the grid.
- **Example**:
    
    ```jsx
    
    gridContainer.clearSelection();  // Deselect all rows
    ```
    

### 4. **`isRowSelected(key)`**:

- **Purpose**: Checks if a specific row is selected by its key.
- **Parameters**:
    - `key`: The row key you want to check.
- **Example**:
    
    ```jsx
    
    if (gridContainer.isRowSelected(1)) {
        console.log("Row 1 is selected");
    }
    ```
    

---

### **Complex Selection Scenarios**

### 1. **Handling Multiple Grids with Shared Store (Your Example)**:

In your example, you have two grids (`gridContainer1` and `gridContainer2`) sharing the same `virtualStore` for data, which means the selection in one grid can be reflected in the other. This shared data source can be used to synchronize or differentiate selections between grids.

You can monitor selection changes across grids by using the `onSelectionChanged` event, and programmatically select or deselect rows across grids using `selectRows` or `clearSelection` methods.

Example:

```jsx

onSelectionChanged: function(e) {
    console.log("Selection Changed in Grid 1", e.selectedRowKeys);
    // Apply the same selection to the second grid (if needed)
    gridContainer2.selectRows(e.selectedRowKeys, true);
}
```

### 2. **Programmatically Selecting Rows in a Grid**:

You can select or deselect rows programmatically based on certain conditions, for instance, if a particular row meets a condition.

Example: Select a row with key `1`:

```jsx
gridContainer.selectRows([1], false);  // Deselects other rows and selects row 1

```

---

### **Conclusion**

Selection in `dxDataGrid` is flexible and can be customized extensively for various use cases:

- **Single vs Multiple Selection**: You can allow single or multiple selections depending on the use case.
- **Programmatic Control**: Methods like `selectRows()`, `deselectRows()`, and `clearSelection()` give you complete control over which rows are selected.
- **Selection Change Event**: The `onSelectionChanged` event allows you to track and respond to user interactions with the grid.

--- 

## Columns

## 1. Columns Overview

Columns in `dxDataGrid` define how data is displayed and allow various customizations like formatting, resizing, and multi-row headers. Each column corresponds to a field in the data source and can be customized for appearance and functionality.

### **Basic Column Definition**

```jsx
columns: [
    {
        dataField: "ProductName",  // Field name from the data source
        caption: "Product Name"    // Display label
    },
    {
        dataField: "Price",
        caption: "Price",
        dataType: "number"         // Data type: "string", "number", "date"
    }
]

```

## 1. Column Customization

### **Width and Alignment**

- `width`: Defines column width.
- `alignment`: Sets text alignment (`left`, `center`, `right`).

```jsx
columns: [
    { dataField: "ProductName", width: 200, alignment: "center" },
    { dataField: "Price", width: 150, alignment: "right" }
]

```

### **Hiding Columns**

```jsx
columns: [ { dataField: "ProductName", visible: false } ]

```

### **Formatting Data**

```jsx
columns: [
    { dataField: "Price", format: { type: "currency", currency: "USD" } },
    { dataField: "OrderDate", format: "yyyy-MM-dd" }
]

```

## 2. Columns Based on a Data Source

### **Auto-generate Columns**

```jsx
dataSource: [ { ProductName: "Laptop", Price: 1200 } ],
columnsAutoWidth: true

```

### **Dynamic Columns**

```jsx
var data = [ { ProductName: "Laptop", Price: 1200 } ];
var columns = Object.keys(data[0]).map(field => ({ dataField: field, caption: field }));
$("#gridContainer").dxDataGrid({ dataSource: data, columns: columns });

```

## 3. Multi-Row Headers

```jsx
columns: [
    { caption: "Product Info", columns: [ { dataField: "ProductName" }, { dataField: "Price" } ] },
    { caption: "Stock Info", columns: [ { dataField: "Quantity" } ] }
]

```

## 4. Column Resizing

```jsx
columns: [
    { dataField: "ProductName", allowResizing: true, minWidth: 100, maxWidth: 300 },
    { dataField: "Price", allowResizing: true }
]

```

## 5. Command Column Customization

### **Default Command Buttons**

```jsx
columns: [ { type: "buttons", buttons: ["edit", "delete"] } ]

```

### **Custom Command Buttons**

```jsx
columns: [
    {
        type: "buttons",
        buttons: [
            "edit", "delete",
            { name: "customAction", text: "Details", onClick: (e) => alert("Details for " + e.row.data.ProductName) }
        ]
    }
]

```

## Conclusion

DevExpress `dxDataGrid` provides extensive column customization options, allowing developers to format, resize, and organize data effectively for a better user experience.

---

## State Persistence

State persistence in DevExpress `dxDataGrid` allows saving and restoring user configurations such as column order, sorting, filtering, selection, and grouping. This feature enhances the user experience by maintaining their preferred grid settings across page reloads or sessions.

---

### **1. Enabling State Persistence**

To enable state persistence, set the `stateStoring.enabled` property to `true`. The grid will then automatically save and restore its state using the specified storage method.

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: myDataSource,
    keyExpr: "id",
    stateStoring: {
        enabled: true,
        type: "localStorage",
        storageKey: "gridState"
    },
    columns: [
        { dataField: "id", caption: "ID" },
        { dataField: "name", caption: "Name" },
        { dataField: "age", caption: "Age" }
    ]
});

```

### **2. Storage Types**

DevExpress `dxDataGrid` supports multiple storage types for saving the grid state:

- **`localStorage`**: Stores state in the browser’s local storage, persisting data across sessions.
- **`sessionStorage`**: Stores state only for the current session (disappears after a page refresh).
- **`custom`**: Allows defining custom functions for saving and loading grid state.

Example using session storage:

```jsx
stateStoring: {
    enabled: true,
    type: "sessionStorage",
    storageKey: "sessionGridState"
}

```

Example using a custom function:

```jsx
stateStoring: {
    enabled: true,
    type: "custom",
    customSave: function(state) {
        myCustomSaveFunction(JSON.stringify(state));
    },
    customLoad: function() {
        return myCustomLoadFunction();
    }
}

```

### **3. Restoring and Resetting Grid State**

If you need to manually load or reset the state, you can use:

```jsx
var grid = $("#gridContainer").dxDataGrid("instance");

grid.state({ /* JSON state object */ }); // Restore a specific state

console.log(grid.state()); // Retrieve current state

grid.state(null); // Reset grid to default state

```

### **4. Supported State Properties**

The following grid settings can be persisted:

- **Column configuration** (visibility, order, width, sorting, filtering)
- **Selected rows**
- **Grouping and grouping order**
- **Paging information** (current page, page size)
- **Sorting order**

### **5. Use Cases and Best Practices**

- **User Preferences**: Retain grid configurations between user visits.
- **Admin Dashboards**: Ensure consistent layouts for large data tables.
- **Data Analysis Tools**: Save customized views for specific reports.
- **Security Considerations**: Avoid storing sensitive user data in `localStorage`.

---

State persistence is a powerful feature in DevExpress `dxDataGrid` that improves usability by maintaining user settings. Proper implementation ensures a seamless experience, especially for applications with complex data grids.

---

## Appearence

The appearance of the **dxDataGrid** component in DevExpress can be customized extensively using built-in settings, CSS, themes, and styling options. This document covers various aspects of appearance customization.

## 1. Built-in Themes

DevExpress provides several built-in themes that can be applied to **dxDataGrid** to control its appearance.

### Setting a Theme

To apply a DevExpress theme, include the corresponding CSS file:

```html
<link rel="stylesheet" href="https://cdn3.devexpress.com/jslib/latest/css/dx.light.css">

```

You can also change the theme dynamically using:

```jsx
DevExpress.ui.themes.current("material.blue.light");
```

## 2. Grid Borders and Row Alternation

You can enable or disable grid borders and alternate row shading for better visibility.

```jsx
$("#gridContainer").dxDataGrid({
    showBorders: true,   // Enables borders around grid cells
    rowAlternationEnabled: true,  // Alternating row background color
});
```

## 3. Cell Styling using CSS

You can style individual cells using the **cellTemplate** property:

```jsx
$("#gridContainer").dxDataGrid({
    columns: [
        {
            dataField: "status",
            caption: "Status",
            cellTemplate: function (container, options) {
                let color = options.value === "Active" ? "green" : "red";
                $("<span>")
                    .text(options.value)
                    .css("color", color)
                    .appendTo(container);
            }
        }
    ]
});

```

## 4. Customizing Row and Cell Styles

You can use **rowTemplate** and **onCellPrepared** for further customization.

### Styling Rows

```jsx
$("#gridContainer").dxDataGrid({
    onRowPrepared: function(e) {
        if (e.rowType === "data" && e.data.priority === "High") {
            e.rowElement.css("background-color", "#f8d7da"); // Light red background
        }
    }
});

```

### Styling Cells

```jsx
$("#gridContainer").dxDataGrid({
    onCellPrepared: function(e) {
        if (e.rowType === "data" && e.column.dataField === "price") {
            e.cellElement.css("font-weight", "bold");
        }
    }
});

```

## 5. Column Header Customization

Modify the style of column headers for better visibility:

```jsx
$("#gridContainer").dxDataGrid({
    columns: [
        {
            dataField: "product",
            caption: "Product Name",
            headerCellTemplate: function(container) {
                $("<b>").text("Custom Header").css("color", "blue").appendTo(container);
            }
        }
    ]
});

```

## 6. Custom Icons in Command Columns

Customize command column icons for **edit, delete, and add** buttons.

```jsx
$("#gridContainer").dxDataGrid({
    editing: {
        mode: "row",
        allowUpdating: true,
        allowDeleting: true,
        allowAdding: true
    },
    columns: [
        {
            type: "buttons",
            buttons: [
                {
                    hint: "Edit",
                    icon: "edit",
                    onClick: function(e) {
                        alert("Editing Row: " + e.row.data.id);
                    }
                },
                {
                    hint: "Delete",
                    icon: "trash",
                    onClick: function(e) {
                        alert("Deleting Row: " + e.row.data.id);
                    }
                }
            ]
        }
    ]
});

```

---

## Template

This document provides detailed information on the row and column templates used in the DevExtreme DataGrid. Templates allow customization of how data is displayed in the grid, enabling features such as formatted text, images, and interactive elements.

---

### **Column Template**

Column templates allow customizing how individual column data is displayed in the grid.

### **Profile Picture Column**

- **Purpose**: Displays the user’s profile picture in a circular format.
- **Implementation**:

```jsx
{
    dataField: 'image',
    caption: 'Profile Picture',
    alignment: 'center',
    allowEditing: false,
    cellTemplate: function (container, options) {
        $('<img>')
            .attr('src', options.value)
            .css({ width: '50px', height: '50px', borderRadius: '50%' })
            .appendTo(container);
    }
}

```

- **Customization Options**:
    - Change the image size by modifying `width` and `height`.
    - Add a border by applying `border: '2px solid #000'`.

### **Full Name Column**

- **Purpose**: Concatenates `firstName` and `lastName` fields into a single column.
- **Implementation**:

```jsx
{
    caption: 'Full Name',
    cellTemplate: function (container, options) {
        let firstName = options.data.firstName;
        let lastName = options.data.lastName;
        container.text(firstName + " " + lastName);
    }
}

```

- **Customization Options**:
    - Modify the format, e.g., `lastName, firstName`.
    - Apply styling using `.css()`.

### **Age Column**

- **Purpose**: Displays age with "years old" suffix.
- **Implementation**:

```jsx
{
    dataField: 'age',
    caption: 'Age (Year)',
    cellTemplate: function (container, options) {
        container.text(options.value + " years old");
    }
}

```

### **Gender Column**

- **Purpose**: Displays gender with color coding.
- **Implementation**:

```jsx
{
    dataField: 'gender',
    caption: 'Gender',
    alignment: 'center',
    cellTemplate: function (container, options) {
        let gender = options.value;
        let color = gender === 'Male' ? '#32a838' : '#a8328f';
        $('<span>')
            .text(gender)
            .css('color', color)
            .appendTo(container);
    }
}

```

- **Customization Options**:
    - Change color codes for male and female.
    - Apply additional styling like `font-weight`.

### **Action Column (Buttons)**

- **Purpose**: Displays edit and delete buttons for each row.
- **Implementation**:

```jsx
{
    type: 'buttons',
    caption: 'Action',
    buttons: [
        { name: 'edit', icon: 'edit' },
        { name: 'delete', icon: 'remove' }
    ]
}

```

---

### **Row Template**

Row templates allow complete customization of each row layout.

### **Implementation**

```jsx
rowTemplate: function (rowElement, rowInfo) {
    let rowData = rowInfo.data;
    let $row = $('<tr>');

    $('<td>').text(rowData.id).css('text-align', 'center').appendTo($row);
    $('<td>').html('<img src="' + rowData.image + '" width="50" height="50" style="border-radius:50%">').css('text-align', 'center').appendTo($row);
    $('<td>').text(rowData.firstName + " " + rowData.lastName).appendTo($row);
    $('<td>').text(rowData.username).appendTo($row);
    $('<td>').text(rowData.email).appendTo($row);
    $('<td>').text(rowData.age + " years old").appendTo($row);
    $('<td>').text(rowData.birthDate).appendTo($row);
    $('<td>').css('color', rowData.gender === 'Male' ? 'green' : 'pink').text(rowData.gender).css('text-align', 'center').appendTo($row);

    let $actionCell = $('<td>').css('text-align', 'center');
    $('<button>')
        .addClass('dx-button dx-button-normal')
        .text('Edit')
        .click(function () {
            console.log('Edit clicked for ID:', rowData.id);
        })
        .appendTo($actionCell);
    $('<button>')
        .addClass('dx-button dx-button-danger')
        .text('Delete')
        .click(function () {
            console.log('Delete clicked for ID:', rowData.id);
        })
        .appendTo($actionCell);

    $actionCell.appendTo($row);
    rowElement.append($row);
}

```

### **Customization Options**

- Modify the row background color based on age or gender.
- Add additional data fields or styling to improve readability.
- Include tooltips for action buttons.

---

### **Conclusion**

Templates provide powerful customization options in DevExtreme DataGrid. By using column and row templates, you can enhance the user experience, making data more readable and interactive. Adjust the provided implementations to fit specific business needs and styling preferences.

---

### Toolbar Customization

In DevExtreme's `dxDataGrid`, the toolbar can be customized to improve user interaction and functionality. This guide explains how to extend the default toolbar by adding:

- An "Add Row" button
- A "Refresh Data" button
- A dropdown filter for gender selection
- A search box for filtering grid data

## Implementation

### Toolbar Customization Code

The following JavaScript code modifies the toolbar using the `onToolbarPreparing` event:

```jsx
onToolbarPreparing: function (e) {
    e.toolbarOptions.items.unshift(
        {
            location: 'before',
            widget: 'dxButton',
            options: {
                icon: 'add',
                text: 'Add Row',
                onClick: function () {
                    $("#columnTemplateContainer").dxDataGrid("instance").addRow();
                }
            }
        },
        {
            location: 'before',
            widget: 'dxButton',
            options: {
                icon: 'refresh',
                text: 'Refresh Data',
                onClick: function () {
                    $("#columnTemplateContainer").dxDataGrid("instance").refresh();
                }
            }
        },
        {
            location: 'before',
            widget: 'dxSelectBox',
            options: {
                items: ['All', 'Male', 'Female'],
                value: 'All',
                onValueChanged: function (args) {
                    let grid = $("#columnTemplateContainer").dxDataGrid("instance");

                    if (args.value === 'All') {
                        grid.clearFilter(); // Remove all filters to show all data
                    } else {
                        grid.filter(["gender", "=", args.value]);
                    }
                }
            }
        },
        {
            location: 'after',
            widget: 'dxTextBox',
            options: {
                placeholder: "Search...",
                onValueChanged: function (e) {
                    let grid = $("#columnTemplateContainer").dxDataGrid("instance");
                    grid.searchByText(e.value);
                }
            }
        }
    );
}

```

## Explanation

1. **Adding an "Add Row" Button:**
    - A `dxButton` is added to the toolbar.
    - When clicked, it adds a new row to the grid.
2. **Adding a "Refresh Data" Button:**
    - Another `dxButton` refreshes the grid when clicked.
3. **Adding a Gender Filter Dropdown:**
    - A `dxSelectBox` is used to filter data by gender.
    - The grid is filtered dynamically when the dropdown value changes.
4. **Adding a Search Box:**
    - A `dxTextBox` allows users to search data within the grid.
    - The grid updates as users type in the search box.

## Usage

Ensure that the `dxDataGrid` is properly initialized with an ID `#columnTemplateContainer` before using this customization.

### Example Grid Initialization

```jsx
$("#columnTemplateContainer").dxDataGrid({
    dataSource: employees,
    columns: ["name", "age", "gender"],
    onToolbarPreparing: onToolbarPreparing
});

```

## Conclusion

This toolbar enhancement improves usability by allowing users to add, refresh, filter, and search data in `dxDataGrid` efficiently. Adjust the toolbar items as needed to suit your application's requirements.

---

## Data Summaries

Data summaries in `dxDataGrid` allow users to display aggregate information, such as totals, averages, or counts, for columns or groups of data. DevExtreme provides three types of summaries:

1. **Grid Summaries** - Display overall summaries at the bottom of the grid.
2. **Group Summaries** - Display summaries for grouped data.
3. **Custom Summaries** - Allow implementing custom aggregation logic.

---

## 1. Grid Summaries

Grid summaries provide overall calculations, such as sum, count, or average, displayed in the grid footer.

### Implementation:

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: employees,
    columns: ["name", "age", "salary"],
    summary: {
        totalItems: [
            {
                column: "age",
                summaryType: "avg",
                displayFormat: "Average Age: {0}"
            },
            {
                column: "salary",
                summaryType: "sum",
                displayFormat: "Total Salary: {0}"
            }
        ]
    }
});

```

### Key Properties:

- `totalItems` - Defines an array of summary items.
- `summaryType` - Specifies the aggregation type (`sum`, `avg`, `min`, `max`, `count`).
- `displayFormat` - Defines how the summary is displayed.
- `valueFormat` - Formats the summary values (e.g., currency, percentage).
- `showInColumn` - Specifies which column to display the summary in.

### Example with Formatting:

```jsx
summary: {
    totalItems: [
        {
            column: "salary",
            summaryType: "sum",
            displayFormat: "Total: {0}",
            valueFormat: "currency"
        }
    ]
}

```

---

## 2. Group Summaries

Group summaries calculate aggregate values for each group within a grouped column.

### Implementation:

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: employees,
    columns: ["department", "name", "salary"],
    grouping: { autoExpandAll: true },
    groupPanel: { visible: true },
    summary: {
        groupItems: [
            {
                column: "salary",
                summaryType: "sum",
                displayFormat: "Total Salary: {0}",
                alignByColumn: true
            }
        ]
    }
});

```

### Key Properties:

- `groupItems` - Defines an array of summary items for grouped data.
- `alignByColumn` - Aligns summary values with corresponding columns.
- `summaryType` - Specifies the aggregation type.
- `showInGroupFooter` - Displays the summary in the group footer instead of the group row.

### Example with Group Footer:

```jsx
summary: {
    groupItems: [
        {
            column: "salary",
            summaryType: "sum",
            displayFormat: "Total: {0}",
            showInGroupFooter: true
        }
    ]
}

```

---

## 3. Custom Summaries

Custom summaries provide flexibility by allowing users to define their own aggregation logic.

### Implementation:

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: employees,
    columns: ["name", "salary"],
    summary: {
        totalItems: [
            {
                column: "salary",
                summaryType: "custom",
                displayFormat: "Average Above 5000: {0}",
                calculateCustomSummary: function(options) {
                    if (options.summaryProcess === "start") {
                        options.totalValue = 0;
                        options.count = 0;
                    }
                    if (options.summaryProcess === "calculate") {
                        if (options.value > 5000) {
                            options.totalValue += options.value;
                            options.count++;
                        }
                    }
                    if (options.summaryProcess === "finalize") {
                        options.totalValue = options.count ? (options.totalValue / options.count) : 0;
                    }
                }
            }
        ]
    }
});

```

### Key Properties:

- `summaryType: "custom"` - Enables custom logic.
- `calculateCustomSummary` - Defines the aggregation process.
    - `summaryProcess: "start"` - Initializes variables.
    - `summaryProcess: "calculate"` - Performs calculations.
    - `summaryProcess: "finalize"` - Finalizes the result.

### Example: Counting Employees Above Salary Threshold

```jsx
calculateCustomSummary: function(options) {
    if (options.summaryProcess === "start") {
        options.totalValue = 0;
    }
    if (options.summaryProcess === "calculate") {
        if (options.value > 6000) {
            options.totalValue++;
        }
    }
}

```

---

## Performance Considerations

- **Large Datasets**: Avoid complex custom calculations on large datasets to maintain performance.
- **Async Processing**: For heavy computations, consider asynchronous processing.
- **Use Indexed Columns**: When working with large databases, ensure that summarized columns are indexed.

---

## Conclusion

Summaries in `dxDataGrid` enhance data visualization by providing aggregate values. You can use predefined summary types for common calculations or create custom summaries for more complex aggregation logic. Proper configuration of summaries improves usability and data analysis within the grid.

---

## Master-Detail View

The Master-Detail view in `dxDataGrid` allows displaying detailed information related to a specific row within the grid. This feature is useful for scenarios where additional data needs to be shown without cluttering the main grid.

---

## 1. Basic Implementation

The Master-Detail view is enabled using the `masterDetail` configuration in `dxDataGrid`.

### Example:

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: employees,
    keyExpr: "id",
    columns: ["name", "position", "department"],
    masterDetail: {
        enabled: true,
        template: function(container, options) {
            let employeeData = options.data;
            $("<div>").dxDataGrid({
                dataSource: orders.filter(o => o.employeeId === employeeData.id),
                columns: ["orderId", "orderDate", "amount"]
            }).appendTo(container);
        }
    }
});

```

### Key Properties:

- `enabled` - Enables the Master-Detail view.
- `template` - Defines the content of the detail row.
- `options.data` - Contains the row's data for the detail template.

---

## 2. Expanding and Collapsing Detail Rows

The Master-Detail rows can be expanded or collapsed programmatically.

### Expand a Row:

```jsx
dxDataGridInstance.expandRow(1);

```

### Collapse a Row:

```jsx
dxDataGridInstance.collapseRow(1);

```

---

## 3. Customizing Detail Content

The detail section can contain other UI components such as forms, charts, or nested grids.

### Example with Additional UI:

```jsx
masterDetail: {
    enabled: true,
    template: function(container, options) {
        let employee = options.data;
        $("<div>").dxForm({
            formData: employee,
            items: [
                { dataField: "name", editorType: "dxTextBox" },
                { dataField: "position", editorType: "dxTextBox" },
                { dataField: "hireDate", editorType: "dxDateBox" }
            ]
        }).appendTo(container);
    }
}

```

---

## 4. Nested Master-Detail View

You can implement multiple levels of master-detail views, where each detail row has its own nested details.

### Example:

```jsx
masterDetail: {
    enabled: true,
    template: function(container, options) {
        $("<div>").dxDataGrid({
            dataSource: getNestedData(options.data.id),
            masterDetail: {
                enabled: true,
                template: function(innerContainer, innerOptions) {
                    $("<div>").dxTextBox({
                        value: `Detail info for ${innerOptions.data.name}`
                    }).appendTo(innerContainer);
                }
            }
        }).appendTo(container);
    }
}

```

---

## 5. Performance Considerations

- **Lazy Loading:** Load detail data only when needed to improve performance.
- **Limit Nested Levels:** Too many nested levels may affect grid performance.
- **Efficient Filtering:** Use filtering techniques to retrieve only relevant detail data.

---

## Conclusion

The Master-Detail feature in `dxDataGrid` enhances data visualization by allowing users to drill down into specific records. With customizable templates, nested grids, and programmatic control, it provides a flexible solution for displaying hierarchical data efficiently.

---

## Export

### Required Packages for .NET Framework

To enable export functionality in a **.NET Framework** application, install the following NuGet packages:

```
Install-Package DevExtreme.AspNet.Data
Install-Package DevExtreme
Install-Package ExcelDataReader
Install-Package jsPDF
```

### Required CDN Links

Include the following **CDN links** in your project to ensure export functionality works properly:

```jsx
<!-- DevExtreme CSS -->
<link rel="stylesheet" href="https://cdn3.devexpress.com/jslib/latest/css/dx.light.css">

<!-- DevExtreme JavaScript -->
<script src="https://cdn3.devexpress.com/jslib/latest/js/dx.all.js"></script>

<!-- ExcelJS for Excel Export -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/exceljs/4.3.0/exceljs.min.js"></script>

<!-- FileSaver for handling file downloads -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2.0.5/FileSaver.min.js"></script>

<!-- jsPDF for PDF Export -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
```

### Adding Export Button in Toolbar

To add an export button in the toolbar, you can configure the `onToolbarPreparing` event:

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: data,
    export: {
        enabled: true,
        fileName: "DataGridExport"
    },
    onToolbarPreparing: function(e) {
        e.toolbarOptions.items.push({
            widget: "dxButton",
            location: "after",
            options: {
                icon: "export",
                text: "Export to Excel",
                onClick: function() {
                    e.component.exportToExcel(false);
                }
            }
        });
    }
});
```

This will add an **Export to Excel** button to the toolbar, allowing users to export grid data directly from the UI.

### Export to Excel

The **Export to Excel** functionality allows users to export grid data as an `.xlsx` file.

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: data,
    export: {
        enabled: true,
        fileName: "DataGridExport"
    },
    onExporting: function(e) {
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet("Main Sheet");
        DevExpress.excelExporter.exportDataGrid({
            component: e.component,
            worksheet: worksheet,
            autoFilterEnabled: true
        }).then(function() {
            workbook.xlsx.writeBuffer().then(function(buffer) {
                saveAs(new Blob([buffer], { type: "application/octet-stream" }), "DataGridExport.xlsx");
            });
        });
        e.cancel = true;
    }
});

```

### Export to CSV

To export data in CSV format, implement the following:

```jsx
$("#exportButton").dxButton({
    text: "Export to CSV",
    onClick: function() {
        let csvContent = "data:text/csv;charset=utf-8,";
        data.forEach(row => {
            csvContent += Object.values(row).join(",") + "\n";
        });
        let encodedUri = encodeURI(csvContent);
        let link = document.createElement("a");
        link.setAttribute("href", encodedUri);
        link.setAttribute("download", "DataGridExport.csv");
        document.body.appendChild(link);
        link.click();
    }
});

```

### Export to PDF

To export data to PDF:

```jsx
$("#gridContainer").dxDataGrid({
    export: {
        enabled: true
    },
    onExporting: function(e) {
        var doc = new jsPDF();
        DevExpress.pdfExporter.exportDataGrid({
            jsPDFDocument: doc,
            component: e.component
        }).then(function() {
            doc.save("DataGridExport.pdf");
        });
    }
});

```

## Adaptability

### Grid Adaptability Overview

- **Column Hiding**: Automatically hides lower priority columns on smaller screens.
- **Adaptive Toolbar**: Moves toolbar items into a dropdown menu when space is limited.
- **Scrolling Adjustments**: Uses horizontal scrolling for large datasets.

### Grid Columns Hiding Priority

```jsx
$("#gridContainer").dxDataGrid({
    dataSource: data,
    columns: [
        { dataField: "ID", hidingPriority: 2 },
        { dataField: "Name", hidingPriority: 1 },
        { dataField: "Email", hidingPriority: 3 }
    ],
    columnHidingEnabled: true
});

```