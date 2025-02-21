$(function () {
    // Array of fruits for basic dropdown demonstration
    const fruits = ["Apples", "Oranges", "Lemons", "Pears", "Pineapples"];
    let selectedValue = fruits[0];

    // Basic list dropdown implementation
    $("#listDropDownBoxContainer").dxDropDownBox({
        value: selectedValue,
        dataSource: fruits,
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    e.component.option("value", arg.addedItems[0]);
                    e.component.close();
                }
            });
            return $list;
        },
    });

    // Sample customer data array
    const customers = [
        { ID: 1, companyName: "Premier Buy", city: "Dallas", phone: "(233)2123-11" },
        { ID: 2, companyName: "ElectrixMax", city: "Naperville", phone: "(630)438-7800" },
        { ID: 3, companyName: "SuperMart", city: "Chicago", phone: "(312)555-0199" }
    ];

    // Selection history for tracking changes
    let selectionHistory = [];
    let currentHistoryIndex = -1;

    selectedValue = customers[0].ID;

    // DataGrid dropdown implementation
    $("#dataGridDropDownBoxContainer").dxDropDownBox({
        value: selectedValue,
        valueExpr: "ID",
        displayExpr: "companyName",
        dataSource: new DevExpress.data.ArrayStore({
            data: customers,
            key: "ID"
        }),
        contentTemplate: function (e) {
            const $dataGrid = $("<div>").dxDataGrid({
                dataSource: e.component.option("dataSource"),
                columns: ["companyName", "city", "phone"],
                height: 265,
                selection: { mode: "single" },
                selectedRowKeys: [selectedValue],
                onSelectionChanged: function (selectedItems) {
                    const keys = selectedItems.selectedRowKeys,
                        hasSelection = keys.length;
                    e.component.option("value", hasSelection ? keys[0] : null);
                    e.component.close();
                }
            });
            return $dataGrid;
        }
    });

    // Main enhanced dropdown implementation with detailed configuration
    let dropDownBox = $("#dropDownBoxContainer").dxDropDownBox({
        // Basic Configuration
        value: null,                    // Initial value
        valueExpr: "ID",                // Value expression (ID field)

        // Display Configuration
        displayExpr: function (item) {   // Display expression
            return `${item.companyName} (${item.city})`;
        },
        displayValueFormatter: function (value) {  // Display value formatter
            return value ? `${value}` : "Select a value";
        },

        // Input Attributes
        inputAttr: {                    // Input attributes
            placeholder: "Select a value"
        },

        // Data Source Configuration
        dataSource: new DevExpress.data.ArrayStore({  // Data source
            data: customers,
            key: "ID"
        }),

        // Custom Button Template
        dropDownButtonTemplate: function (e) {  // Custom button template
            return $("<div>")
                .addClass("dx-button")
                .append("<span class='dx-icon dx-icon-user'></span>");
        },

        // Element Attributes
        elementAttr: {                  // Element attributes
            id: "myDropDown",
            class: "custom-dropdown",
            "data-type": "dropdown-menu"
        },

        // State Configuration
        focusStateEnabled: true,        // Enable focus state
        hint: 'select a value',         // Hint text
        hoverStateEnabled: true,        // Enable hover state
        isValid: true,                  // Initial validation state
        maxLength: 100,                 // Maximum length of input
        name: 'drop-down',              // Name attribute

        // Event Handlers
        onChange: function (e) {          // Change event handler
            selectionHistory.splice(currentHistoryIndex + 1);
            selectionHistory.push(e.value);
            currentHistoryIndex++;
        },

        onClosed: function (e) {          // Closed event handler
            const currentValue = e.component.option("value");
            if (!currentValue) {
                console.log("No selection made");
            }
        },

        onCopy: function (e) {            // Copy event handler
            const selectedItem = customers.find(c => c.ID === e.component.option("value"));
            if (selectedItem) {
                e.originalEvent.clipboardData.setData('text/plain',
                    `Company: ${selectedItem.companyName}\nCity: ${selectedItem.city}\nPhone: ${selectedItem.phone}`);
                e.originalEvent.preventDefault();
            }
        },

        onCut: function (e) {             // Cut event handler
            const selectedItem = customers.find(c => c.ID === e.component.option("value"));
            if (selectedItem) {
                e.originalEvent.clipboardData.setData('text/plain',
                    `${selectedItem.companyName} (${selectedItem.city})`);
                e.component.option("value", null);
                e.originalEvent.preventDefault();
            }
        },

        onDisposing: function (e) {       // Disposing event handler
            selectionHistory = [];
            currentHistoryIndex = -1;
        },

        onEnterKey: function (e) {        // Enter key event handler
            const currentValue = e.component.option("value");
            if (!currentValue) {
                e.component.open();
            }
        },

        onFocusIn: function (e) {         // Focus in event handler
            console.log("Dropdown focused");
        },

        onFocusOut: function (e) {        // Focus out event handler
            const currentValue = e.component.option("value");
            if (!currentValue) {
                console.log("No value selected");
            }
        },

        onInitialized: function (e) {     // Initialized event handler
            console.log("Dropdown initialized");
        },

        onKeyDown: function (e) {         // Key down event handler
            if (e.event.key === "ArrowUp" || e.event.key === "ArrowDown") {
                e.event.preventDefault();
                e.component.open();
            }
        },

        onOpened: function (e) {          // Opened event handler
            console.log("Dropdown opened");
        },

        onOptionChanged: function (e) {   // Option changed event handler
            if (e.name === "value") {
                console.log("Value changed to:", e.value);
            }
        },

        onValueChanged: function (e) {    // Value changed event handler
            if (e.value !== null) {
                const selectedItem = customers.find(c => c.ID === e.value);
                if (selectedItem) {
                    console.log(`Selected: ${selectedItem.companyName}`);
                }
            }
        },

        // Visual Configuration
        readOnly: false,                // Read-only state
        rtlEnabled: false,              // Right-to-left support
        showClearButton: true,          // Show clear button
        showDropDownButton: true,       // Show dropdown button
        stylingMode: 'filled',          // Styling mode

        // Validation Configuration
        validationError: null,                // Validation error
        validationErrors: [],                // Validation errors
        validationMessageMode: "always",      // Validation message mode
        validationStatus: "valid",            // Validation status

        // Dropdown Panel Configuration
        dropDownOptions: {               // Dropdown panel options
            width: "100%",              // Width of the dropdown panel
            height: 200,                // Height of the dropdown panel
            fullScreen: false,          // Fullscreen mode
            closeOnOutsideClick: false, // Close on outside click
            animation: {                // Animation settings
                show: { type: "fade", duration: 300 },
                hide: { type: "fade", duration: 300 }
            }
        },

        // Content Template
        contentTemplate: function (e) {   // Content template
            const $dataGrid = $("<div>").dxDataGrid({
                dataSource: e.component.option("dataSource"),
                columns: ["companyName", "city", "phone"],
                height: 265,
                selection: { mode: "single" },
                selectedRowKeys: [],  // Removed undefined `selectedValue`
                onSelectionChanged: function (selectedItems) {
                    const keys = selectedItems.selectedRowKeys;
                    const hasSelection = keys.length;
                    e.component.option("value", hasSelection ? keys[0] : null);
                    e.component.close();
                }
            });
            return $dataGrid;
        },

        // Custom Value Configuration
        acceptCustomValue: true,         // Accept custom values
        activeStateEnabled: true,        // Enable active state

        // Custom Value Handler
        onCustomCreating: function (args) {  // Custom value creation handler
            if (args.text.trim()) {
                const newValue = {
                    ID: customers.length + 1,
                    companyName: args.text,
                    city: "Unknown",
                    phone: "N/A"
                };
                customers.push(newValue);
                args.customItem = newValue;
                console.log("New item created:", newValue);
            }
        },

        // Button Configuration
        buttons: [                       // Button configuration
            "clear",                     // Clear button
            "dropDown",                  // Dropdown button
            {
                name: "customButton",    // Custom button
                location: "after",
                options: {
                    icon: "plus",
                    hint: "Add New Item",
                    onClick: function () {
                        alert("Custom Button Clicked!");
                    }
                }
            }
        ],

        // Rendering Configuration
        deferRendering: true,            // Defer rendering
        disabled: false                  // Disabled state
    }).dxDropDownBox("instance");

    // Dynamic Updates
    $("#clear").click(function () {
        dropDownBox.clear();
    });

    $("#blur").click(function () {
        dropDownBox.blur();
    });

    $("#focus").click(function () {
        dropDownBox.focus();
    });

    $("#repaint").click(function () {
        dropDownBox.repaint();
    });

    $("#open").click(function () {
        dropDownBox.open();
    });

    $("#reset").click(function () {
        dropDownBox.reset();
    });

    let dataSource = dropDownBox.getDataSource();
    console.log(dataSource.items());

    // // Update the value
    // dropDownBox.option("value", newValue);

    // // Update the data source
    // dropDownBox.option("dataSource", newDataSource);

    // // Update the display format
    // dropDownBox.option("displayExpr", newDisplayExpr);

    // // Update the placeholder
    // dropDownBox.option("inputAttr", { placeholder: "New Placeholder" });

    // // Update the dropdown options
    // dropDownBox.option("dropDownOptions", {
    //     width: "80%",
    //     height: 300,
    //     closeOnOutsideClick: true
    // });

    // Sample hierarchical data
    const hierarchicalData = [
        {
            ID: 1,
            text: "Products",
            items: [
                {
                    ID: 11,
                    text: "Electronics",
                    items: [
                        { ID: 111, text: "Phones" },
                        { ID: 112, text: "Laptops" },
                        { ID: 113, text: "Tablets" }
                    ]
                },
                {
                    ID: 12,
                    text: "Home Appliances",
                    items: [
                        { ID: 121, text: "Kitchen" },
                        { ID: 122, text: "Living Room" }
                    ]
                }
            ]
        },
        {
            ID: 2,
            text: "Services",
            items: [
                { ID: 21, text: "Installation" },
                { ID: 22, text: "Repair" }
            ]
        }
    ];

    selectedValue = null;

    // Nested dropdown implementation
    $("#nestedDropDownContainer").dxDropDownBox({
        value: selectedValue,
        valueExpr: "ID",
        displayExpr: "text",
        placeholder: "Select an item",
        dataSource: hierarchicalData,
        contentTemplate: function (e) {
            const $treeView = $("<div>").dxTreeView({
                dataSource: e.component.option("dataSource"),
                dataStructure: "tree",
                keyExpr: "ID",
                displayExpr: "text",
                parentIdExpr: "parentId",
                selectionMode: "single",
                selectByClick: true,
                onItemClick: function (args) {
                    // Only set value and close if it's a leaf node (no children)
                    if (!args.itemData.items || args.itemData.items.length === 0) {
                        e.component.option("value", args.itemData.ID);
                        e.component.close();
                    }
                },
                itemTemplate: function (item) {
                    return $("<div>")
                        .addClass("nested-item")
                        .append(
                            $("<span>")
                                .text(item.text)
                                .addClass(item.items && item.items.length ? "parent-node" : "leaf-node")
                        );
                },
                width: "100%",
                height: "400px"
            });

            return $treeView;
        },
        dropDownOptions: {
            width: "100%",
            height: "auto",
            closeOnOutsideClick: true
        }
    });
});