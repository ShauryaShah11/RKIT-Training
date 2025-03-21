$(function () {
    // Sample data for the first SelectBox
    let data = [
        { ID: 1, Name: 'Banana', Category: 'Fruits' },
        { ID: 2, Name: 'Cucumber', Category: 'Vegetables' },
        { ID: 3, Name: 'Apple', Category: 'Fruits' },
        { ID: 4, Name: 'Tomato', Category: 'Vegetables' },
        { ID: 5, Name: 'Apricot', Category: 'Fruits' }
    ];

    // 🔹 dxSelectBox with grouped data, custom options, and event handlers
    let selectBoxInstance = $("#selectBox").dxSelectBox({
        // Data source for the SelectBox, grouped by 'Category'
        dataSource: new DevExpress.data.DataSource({
            store: data,      // Data provided as an array
            type: "array",    // Type of data source (array)
            key: "ID",        // Unique key for each item
            group: "Category" // Grouping data by 'Category'
        }),

        // Display text for each item in the dropdown
        displayExpr: "Name",   // Default: "this"

        // The value of the selected item will be the 'ID'
        valueExpr: "ID",       // Default: "this"

        // Default selected value (First Item)
        value: data[0].ID,     // Default: null

        // Enables search within the dropdown
        searchEnabled: true,   // Default: false

        // Group items in the dropdown by the specified category
        grouped: true,         // Default: false

        // Enables the clear button
        showClearButton: true, // Default: false

        // Enables case-sensitive search
        searchMode: "contains", // Options: "contains", "startswith", "equals"
        
        // Placeholder text when nothing is selected
        placeholder: "Select a Product", // Default: ""

        // Allows users to enter custom values not in the list
        acceptCustomValue: true, // Default: false

        // Enables custom search functionality
        searchExpr: "Name",   // Default: null (searches all fields)

        // Allows multiple selection
        multiSelectEnabled: false, // Default: false

        // Enables drop-down open button
        showDropDownButton: true, // Default: true

        // Enables shadow effect
        dropDownButtonTemplate: null, // Default: null (uses default button)

        // Disables the SelectBox
        disabled: false, // Default: false

        // Enables focus state
        focusStateEnabled: true, // Default: true

        // Enables the hover effect
        hoverStateEnabled: true, // Default: true

        // Makes the SelectBox read-only
        readOnly: false, // Default: false

        // Keyboard shortcut for accessibility
        accessKey: "s", // Default: null

        // Tooltip hint
        hint: "Select a Product", // Default: null

        // Defines the styling mode
        stylingMode: "outlined", // Options: "outlined", "underlined", "filled" (Default: "outlined")

        // Tab index for navigation
        tabIndex: 0, // Default: 0

        // Event triggered when the value changes
        onValueChanged: function (e) {
            console.log("Selected Value:", e.value);          // Logs the new value
            console.log("Previous Value:", e.previousValue); // Logs the previous value
        },

        // Event triggered when an option is changed dynamically
        onOptionChanged: function (e) {
            console.log(`Option Changed: ${e.name} -> ${e.value}`);
        },

        // Event triggered when a custom value is created
        onCustomItemCreating: function (e) {
            let newValue = e.text;  // The custom value entered by the user
            let newId = data.length + 1; // Generate a new ID for the custom item

            // Add the new item to the data source
            let newItem = { ID: newId, Name: newValue, Category: "Custom" };
            data.push(newItem);

            e.customItem = newItem; // Set the custom item as the new value
            console.log("Custom Value Added:", newItem);
        },
    }).dxSelectBox("instance");

    // 🔹 Simple SelectBox with predefined values
    $("#simple-select").dxSelectBox({
        value: simpleProducts[0],               // Default selected value
        items: simpleProducts,                  // List of items
        inputAttr: { 'aria-label': 'simple product' }, // Accessibility attribute
        readonly: false,                         // Default: false
        disabled: false                          // Default: false
    });

    // 🔹 SelectBox with a placeholder
    $("#placeholder-select").dxSelectBox({
        items: simpleProducts,
        inputAttr: { 'aria-label': 'simple product' },
        placeholder: "Select a Product"          // Default: ""
    });

    // 🔹 SelectBox with complex items and a custom display format
    $("#products-data-source").dxSelectBox({
        inputAttr: { 'aria-label': 'Product Id' },
        dataSource: new DevExpress.data.ArrayStore({
            data: products,
            key: "ID"
        }),
        displayExpr: function (item) {
            return `Name: ${item.Name}, Price: ${item.Price}, Inventory: ${item.Current_Inventory}, Category: ${item.Category}`;
        },
        valueExpr: 'ID',
        value: products[0].ID                   // Default selected value
    });

    // 🔹 SelectBox with custom templates for field and items
    $('#custom-templates').dxSelectBox({
        dataSource: products,
        inputAttr: { 'aria-label': 'Templated Product' },
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: products[0].ID,

        // Custom template for the field (input box)
        fieldTemplate(data, container) {
            const result = $(`<div class="container"><img alt='Product name' src='${data ? data.ImageSrc : ''}' /><div class='product-name'></div></div>`);
            result.find('.product-name')
                .dxTextBox({
                    value: data && data.Name,
                    readOnly: true,
                    inputAttr: { 'aria-label': 'Name' },
                });
            container.append(result);
        },

        // Custom template for items in the dropdown
        itemTemplate(data) {
            return `<div class='custom-item'><img alt='Product name' src='${data.ImageSrc}' /><div class='product-name'>${data.Name}</div></div>`;
        }
    });

    // 🔹 Button Handlers for Dynamic Updates
    $("#clearSelectBox").click(function () {
        selectBoxInstance.reset();
        console.log("SelectBox Cleared");
    });

    $("#disableSelectBox").click(function () {
        selectBoxInstance.option("disabled", true);
        console.log("SelectBox Disabled");
    });

    $("#enableSelectBox").click(function () {
        selectBoxInstance.option("disabled", false);
        console.log("SelectBox Enabled");
    });

    $("#focusSelectBox").click(function () {
        selectBoxInstance.focus();
    });

    $("#repaintSelectBox").click(function () {
        selectBoxInstance.repaint();
    });

    $("#resetSelectBox").click(function () {
        selectBoxInstance.option("value", null);
        console.log("SelectBox Reset");
    });

});

// 🔹 List of simple products
const simpleProducts = [
    'HD Video Player', 'SuperHD Video Player', 'SuperPlasma 50',
    'SuperLED 50', 'SuperLED 42', 'SuperLCD 55', 'SuperLCD 42',
    'SuperPlasma 65', 'SuperLCD 70', 'Projector Plus', 'Projector PlusHT',
    'ExcelRemote IR', 'ExcelRemote BT', 'ExcelRemote IP'
];

// 🔹 Array of product objects with detailed properties
const products = [
    { ID: 1, Name: 'HD Video Player', Price: 330, Current_Inventory: 225, Category: 'Video Players', ImageSrc: 'https://example.com/img1.png' },
    { ID: 2, Name: 'SuperHD Player', Price: 400, Current_Inventory: 150, Category: 'Video Players', ImageSrc: 'https://example.com/img2.png' },
];
