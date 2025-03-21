$(function () {
    // Array of fruits for basic dropdown demonstration
    const fruits = ["Apples", "Oranges", "Lemons", "Pears", "Pineapples"];
    let selectedValue = fruits[0]; // Default value set to "Apples"

    // Basic list dropdown implementation
    $("#listDropDownBoxContainer").dxDropDownBox({
        value: selectedValue, // Default selected item from array (can be any value from dataSource)
        dataSource: fruits, // Array of fruit names as the data source
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: e.component.option("dataSource"), // Populates list from dataSource
                selectionMode: "single", // Options: "single" (default), "multiple"
                onSelectionChanged: function (arg) {
                    e.component.option("value", arg.addedItems[0]); // Updates the selected value
                    e.component.close(); // Closes the dropdown on selection
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

    let selectionHistory = []; // Stores previous selections
    let currentHistoryIndex = -1; // Tracks the index of the current selection

    selectedValue = customers[0].ID; // Default selection set to first customer (Premier Buy)

    // DataGrid dropdown implementation
    $("#dataGridDropDownBoxContainer").dxDropDownBox({
        value: selectedValue, // Default selected value (first customer's ID)
        valueExpr: "ID", // Specifies the unique field in dataSource (default: "this")
        displayExpr: "companyName", // Defines the field to display in UI (default: null)
        dataSource: new DevExpress.data.ArrayStore({
            data: customers,
            key: "ID" // Uses ID as the unique key for data mapping
        }),
        contentTemplate: function (e) {
            const $dataGrid = $("<div>").dxDataGrid({
                dataSource: e.component.option("dataSource"), // Populates grid from dataSource
                columns: ["companyName", "city", "phone"], // Columns to display in grid
                height: 265, // Grid height (default: auto)
                selection: { mode: "single" }, // Options: "single" (default), "multiple", "none"
                selectedRowKeys: [selectedValue], // Pre-selects row based on default value
                onSelectionChanged: function (selectedItems) {
                    const keys = selectedItems.selectedRowKeys;
                    e.component.option("value", keys.length ? keys[0] : null); // Updates the selected value
                    e.component.close(); // Closes the dropdown
                }
            });
            return $dataGrid;
        }
    });

    // Enhanced dropdown with detailed configurations
    let dropDownBox = $("#dropDownBoxContainer").dxDropDownBox({
        value: null, // Default value (null means no selection)
        valueExpr: "ID", // Field used for unique identification (default: "this")
        displayExpr: function (item) { return `${item.companyName} (${item.city})`; }, // Custom display format
        displayValueFormatter: function (value) { return value ? `${value}` : "Select a value"; }, // Formats displayed value
        inputAttr: { placeholder: "Select a value" }, // Placeholder when no value is selected
        dataSource: new DevExpress.data.ArrayStore({ data: customers, key: "ID" }), // Customer data source

        dropDownButtonTemplate: function () {
            return $("<div>").addClass("dx-button").append("<span class='dx-icon dx-icon-user'></span>");
        },

        elementAttr: { id: "myDropDown", class: "custom-dropdown", "data-type": "dropdown-menu" }, // Custom HTML attributes

        focusStateEnabled: true, // Enables focus state (default: true)
        hint: 'Select a value', // Tooltip hint for input
        hoverStateEnabled: true, // Enables hover effect (default: true)
        isValid: true, // Default validation state (true = no errors)
        maxLength: 100, // Maximum input length (default: null)
        name: 'drop-down', // Name attribute for form submissions

        // Event Handlers
        onChange: function (e) { 
            selectionHistory.splice(currentHistoryIndex + 1);
            selectionHistory.push(e.value);
            currentHistoryIndex++;
        },
        onClosed: function (e) { if (!e.component.option("value")) console.log("No selection made"); },
        onCopy: function (e) {
            const selectedItem = customers.find(c => c.ID === e.component.option("value"));
            if (selectedItem) {
                e.originalEvent.clipboardData.setData('text/plain', 
                    `Company: ${selectedItem.companyName}\nCity: ${selectedItem.city}\nPhone: ${selectedItem.phone}`);
                e.originalEvent.preventDefault();
            }
        },
        onCut: function (e) {
            const selectedItem = customers.find(c => c.ID === e.component.option("value"));
            if (selectedItem) {
                e.originalEvent.clipboardData.setData('text/plain', `${selectedItem.companyName} (${selectedItem.city})`);
                e.component.option("value", null);
                e.originalEvent.preventDefault();
            }
        },
        onDisposing: function () { selectionHistory = []; currentHistoryIndex = -1; },
        onEnterKey: function (e) { if (!e.component.option("value")) e.component.open(); },
        onFocusIn: function () { console.log("Dropdown focused"); },
        onFocusOut: function () { if (!dropDownBox.option("value")) console.log("No value selected"); },
        onInitialized: function () { console.log("Dropdown initialized"); },
        onKeyDown: function (e) { if (["ArrowUp", "ArrowDown"].includes(e.event.key)) e.event.preventDefault(); e.component.open(); },
        onOpened: function () { console.log("Dropdown opened"); },
        onOptionChanged: function (e) { if (e.name === "value") console.log("Value changed to:", e.value); },
        onValueChanged: function (e) {
            if (e.value !== null) {
                const selectedItem = customers.find(c => c.ID === e.value);
                if (selectedItem) console.log(`Selected: ${selectedItem.companyName}`);
            }
        },

        readOnly: false, // If true, user cannot edit the input (default: false)
        rtlEnabled: false, // Right-to-left text direction (default: false)
        showClearButton: true, // Show "clear" button (default: false)
        showDropDownButton: true, // Show dropdown button (default: true)
        stylingMode: 'filled', // Options: 'outlined', 'filled', 'underlined' (default: 'outlined')

        // Validation
        validationError: null,
        validationErrors: [],
        validationMessageMode: "always", // Options: "auto", "always"
        validationStatus: "valid", // Options: "valid", "invalid", "pending"

        // Drop-down customization
        dropDownOptions: {
            width: "100%", // Width of the dropdown (default: auto)
            height: 200, // Height of dropdown (default: auto)
            fullScreen: false, // If true, dropdown takes full screen on mobile (default: false)
            closeOnOutsideClick: false, // Close dropdown when clicking outside (default: true)
            animation: { show: { type: "fade", duration: 300 }, hide: { type: "fade", duration: 300 } } // Fade animation
        },

        // Content template using DataGrid
        contentTemplate: function (e) {
            const $dataGrid = $("<div>").dxDataGrid({
                dataSource: e.component.option("dataSource"),
                columns: ["companyName", "city", "phone"],
                height: 265,
                selection: { mode: "single" },
                selectedRowKeys: [],
                onSelectionChanged: function (selectedItems) {
                    e.component.option("value", selectedItems.selectedRowKeys.length ? selectedItems.selectedRowKeys[0] : null);
                    e.component.close();
                }
            });
            return $dataGrid;
        }
    });

    // Dynamic control actions
    $("#clear").click(() => dropDownBox.clear());
    $("#blur").click(() => dropDownBox.blur());
    $("#focus").click(() => dropDownBox.focus());
    $("#repaint").click(() => dropDownBox.repaint());
    $("#open").click(() => dropDownBox.open());
    $("#reset").click(() => dropDownBox.reset());

    console.log(dropDownBox.getDataSource().items()); // Logs data source items
});
