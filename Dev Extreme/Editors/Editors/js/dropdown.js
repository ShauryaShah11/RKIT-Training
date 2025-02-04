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
            })
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
        value: null,
        valueExpr: "ID",

        // Display Configuration
        displayExpr: function (item) {
            return `${item.companyName} (${item.city})`;
        },
        displayValueFormatter: function (value) {
            return value ? `${value}` : "Select a value";
        },

        // Input Attributes
        inputAttr: {
            placeholder: "Select a value"
        },

        // Data Source Configuration
        dataSource: new DevExpress.data.ArrayStore({
            data: customers,
            key: "ID"
        }),

        // Custom Button Template
        dropDownButtonTemplate: function (e) {
            return $("<div>")
                .addClass("dx-button")
                .append("<span class='dx-icon dx-icon-user'></span>");
        },

        // Element Attributes
        elementAttr: {
            id: "myDropDown",
            class: "custom-dropdown",
            "data-type": "dropdown-menu"
        },

        // State Configuration
        focusStateEnabled: true,
        hint: 'select a value',
        hoverStateEnabled: true,
        isValid: true,
        maxLength: 100,
        name: 'drop-down',

        // Event Handlers
        onChange: function (e) {
            selectionHistory.splice(currentHistoryIndex + 1);
            selectionHistory.push(e.value);
            currentHistoryIndex++;
        },

        onClosed: function (e) {
            const currentValue = e.component.option("value");
            if (!currentValue) {
                console.log("No selection made");
            }
        },

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
                e.originalEvent.clipboardData.setData('text/plain',
                    `${selectedItem.companyName} (${selectedItem.city})`);
                e.component.option("value", null);
                e.originalEvent.preventDefault();
            }
        },

        onDisposing: function (e) {
            selectionHistory = [];
            currentHistoryIndex = -1;
        },

        onEnterKey: function (e) {
            const currentValue = e.component.option("value");
            if (!currentValue) {
                e.component.open();
            }
        },

        onFocusIn: function (e) {
            console.log("Dropdown focused");
        },

        onFocusOut: function (e) {
            const currentValue = e.component.option("value");
            if (!currentValue) {
                console.log("No value selected");
            }
        },

        onInitialized: function (e) {
            console.log("Dropdown initialized");
        },

        onKeyDown: function (e) {        // ✅ Fixed typo (oonKeyDown -> onKeyDown)
            if (e.event.key === "ArrowUp" || e.event.key === "ArrowDown") {
                e.event.preventDefault();
                e.component.open();
            }
        },

        onOpened: function (e) {
            console.log("Dropdown opened");
        },

        onOptionChanged: function (e) {
            if (e.name === "value") {
                console.log("Value changed to:", e.value);
            }
        },

        onValueChanged: function (e) {
            if (e.value !== null) {
                const selectedItem = customers.find(c => c.ID === e.value);
                if (selectedItem) {
                    console.log(`Selected: ${selectedItem.companyName}`);
                }
            }
        },

        // Visual Configuration
        readOnly: false,
        rtlEnabled: false,
        showClearButton: true,
        showDropDownButton: true,
        stylingMode: 'filled',

        // Validation Configuration (Ensure these are defined)
        validationError: null,
        validationErrors: [],
        validationMessageMode: "always",
        validationStatus: "valid",

        // Dropdown Panel Configuration
        dropDownOptions: {
            width: "100%",
            height: 200,
            fullScreen: false,
            closeOnOutsideClick: false,
            animation: {
                show: { type: "fade", duration: 300 },
                hide: { type: "fade", duration: 300 }
            }
        },

        // Content Template
        contentTemplate: function (e) {
            const $dataGrid = $("<div>").dxDataGrid({
                dataSource: e.component.option("dataSource"),
                columns: ["companyName", "city", "phone"],
                height: 265,
                selection: { mode: "single" },
                selectedRowKeys: [],  // ✅ Removed undefined `selectedValue`
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
        acceptCustomValue: true,
        activeStateEnabled: true,

        // Custom Value Handler
        onCustomCreating: function (args) {
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
        buttons: [
            "clear",
            "dropDown",
            {
                name: "customButton",
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
        deferRendering: true,
        disabled: false
    }).dxDropDownBox("instance");

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

    $("#clear").click(function () {
        dropDownBox.clear();
    });

    $("#open").click(function () {
        dropDownBox.open();
    });

    $("#reset").click(function () {
        dropDownBox.reset();
    });

    let dataSource = dropDownBox.getDataSource();
    console.log(dataSource.items());

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