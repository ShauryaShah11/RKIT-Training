$(function () {
    // Sample data for the first SelectBox
    let data = [{
        ID: 1,
        Name: 'Banana',
        Category: 'Fruits'
    }, {
        ID: 2,
        Name: 'Cucumber',
        Category: 'Vegetables'
    }, {
        ID: 3,
        Name: 'Apple',
        Category: 'Fruits'
    }, {
        ID: 4,
        Name: 'Tomato',
        Category: 'Vegetables'
    }, {
        ID: 5,
        Name: 'Apricot',
        Category: 'Fruits'
    }];

    // dxSelectBox with grouped data, custom options, and event handlers
    $("#selectBox").dxSelectBox({
        // Data source for the SelectBox, grouped by 'Category'
        dataSource: new DevExpress.data.DataSource({
            store: data,      // Data provided as an array
            type: "array",    // Type of data source (array)
            key: "ID",        // Unique key for each item
            group: "Category" // Grouping data by 'Category'
        }),

        // Display text for each item in the dropdown
        displayExpr: "Name",

        // The value of the selected item will be the 'ID'
        valueExpr: "ID",

        // Enables search within the dropdown
        searchEnabled: true,

        // Event triggered when the value changes
        onValueChanged: function (e) {
            console.log("Selected Value:", e.value);          // Logs the new value
            console.log("Previous Value:", e.previousValue); // Logs the previous value
        },

        // Group items in the dropdown by the specified category
        grouped: true,

        // Options for the dropdown (popup) itself
        dropDownOptions: {
            height: 250 // Sets the height of the dropdown
        },

        // Allows users to enter custom values not in the list
        acceptCustomValue: true,

        // Adds buttons: 'clear' to clear the value and 'dropDown' to open the dropdown
        buttons: ['clear', 'dropDown'],

        // Displays a clear button to remove the selection
        showClearButton: true,

        // Defers rendering until the dropdown is opened for the first time
        deferRendering: true,

        // Disables the SelectBox if set to true
        disabled: false,

        // Enables focus state for the SelectBox
        focusStateEnabled: true,

        // Provides a hint when the SelectBox is not focused
        hint: "Select a Product",

        // Event triggered when a custom value is created
        onCustomItemCreating: function (e) {
            var newValue = e.text;  // The custom value entered by the user
            var newId = data.length + 1; // Generate a new ID for the custom item

            // Add the new item to the data source
            var newItem = { ID: newId, Name: newValue, Category: "Custom" };
            data.push(newItem);

            e.customItem = newItem; // Set the custom item as the new value
            console.log("Custom Value Added:", newItem);
        },
    });

    // Simple SelectBox with predefined values
    $("#simple-select").dxSelectBox({
        value: simpleProducts[0],               // Default selected value
        items: simpleProducts,                  // List of items
        inputAttr: { 'aria-label': 'simple product' }, // Accessibility attribute
        readonly: true,                         // Makes the SelectBox read-only
        disabled: true                          // Disables the SelectBox
    });

    // SelectBox with a placeholder
    $("#placeholder-select").dxSelectBox({
        items: simpleProducts,
        inputAttr: { 'aria-label': 'simple product' },
        placeholder: "Select a Product"          // Placeholder text
    });

    // SelectBox with complex items and a custom display format
    $("#products-data-source").dxSelectBox({
        inputAttr: { 'aria-label': 'Product Id' },
        dataSource: new DevExpress.data.ArrayStore({
            data: products,
            key: "ID"
        }),
        displayExpr: function (item) {
            // Custom display format showing multiple properties of the product
            return `Name: ${item.Name}, Price: ${item.Price}, Inventory: ${item.Current_Inventory}, Category: ${item.Category}`;
        },
        valueExpr: 'ID',
        value: products[0].ID                   // Default selected value
    });

    // SelectBox with custom templates for field and items
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
        $("#selectBox").dxSelectBox("instance").reset();
        console.log("SelectBox Cleared");
    });
    $("#disableSelectBox").click(function () {
        $("#selectBox").dxSelectBox("instance").option("disabled", true);
        console.log("SelectBox Disabled");
    });
    $("#enableSelectBox").click(function () {
        $("#selectBox").dxSelectBox("instance").option("disabled", false);
        console.log("SelectBox Enabled");
    });
    $("#focusSelectBox").click(function () {
        $("#selectBox").dxSelectBox("instance").focus();
    });
    $("#repaintSelectBox").click(function () {
        $("#selectBox").dxSelectBox("instance").repaint();
    });
    $("#resetSelectBox").click(function () {
        $("#selectBox").dxSelectBox("instance").option("value", null);
        console.log("SelectBox Reset");
    });

    // // Update the value
    // $("#selectBox").dxSelectBox("instance").option("value", newValue);

    // // Enable or disable the SelectBox
    // $("#selectBox").dxSelectBox("instance").option("disabled", true);  // Disable
    // $("#selectBox").dxSelectBox("instance").option("disabled", false); // Enable

    // // Update the placeholder
    // $("#selectBox").dxSelectBox("instance").option("inputAttr", { placeholder: "New Placeholder" });

    // // Update the data source
    // $("#selectBox").dxSelectBox("instance").option("dataSource", newDataSource);

    // // Update the display expression
    // $("#selectBox").dxSelectBox("instance").option("displayExpr", newDisplayExpr);
});

// List of simple products
const simpleProducts = [
    'HD Video Player',
    'SuperHD Video Player',
    'SuperPlasma 50',
    'SuperLED 50',
    'SuperLED 42',
    'SuperLCD 55',
    'SuperLCD 42',
    'SuperPlasma 65',
    'SuperLCD 70',
    'Projector Plus',
    'Projector PlusHT',
    'ExcelRemote IR',
    'ExcelRemote BT',
    'ExcelRemote IP'
];

// Array of product objects with detailed properties
const products = [
    {
        ID: 1,
        Name: 'HD Video Player',
        Price: 330,
        Current_Inventory: 225,
        Backorder: 0,
        Manufacturing: 10,
        Category: 'Video Players',
        ImageSrc: 'https://js.devexpress.com/jQuery/Demos/WidgetsGallery/JSDemos/images/products/1-small.png',
    },
    {
        ID: 2,
        Name: 'SuperHD Player',
        Price: 400,
        Current_Inventory: 150,
        Backorder: 0,
        Manufacturing: 25,
        Category: 'Video Players',
        ImageSrc: 'https://js.devexpress.com/jQuery/Demos/WidgetsGallery/JSDemos/images/products/2-small.png',
    },
];