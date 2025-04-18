const data = [
  { Product: "Apple", Category: "Fruit", Quantity: 20, Price: 1.5 },
  { Product: "Banana", Category: "Fruit", Quantity: 35, Price: 0.5 },
  { Product: "Carrot", Category: "Vegetable", Quantity: 40, Price: 0.8 },
  { Product: "Cucumber", Category: "Fruit", Quantity: 50, Price: 1.2 },
  { Product: "Grapes", Category: "Fruit", Quantity: 25, Price: 2.0 },
  { Product: "Lettuce", Category: "Vegetable", Quantity: 30, Price: 1.0 },
  { Product: "Orange", Category: "Fruit", Quantity: 15, Price: 1.8 },
  { Product: "Tomato", Category: "Vegetable", Quantity: 20, Price: 1.3 },
  { Product: "Potato", Category: "Vegetable", Quantity: 60, Price: 0.4 },
  { Product: "Peach", Category: "Fruit", Quantity: 30, Price: 2.5 },
];

// Initialize the DataGrid with a custom summary for Quantity
$("#gridContainer").dxDataGrid({
  dataSource: data,
  columns: [
    {
      dataField: "Product",
      caption: "Product",
    },
    {
      dataField: "Category",
      caption: "Category",
      // groupIndex: 0
    },
    {
      dataField: "Quantity",
      caption: "Quantity",
    },
    {
      dataField: "Price",
      caption: "Price",
    },
    {
      type: "buttons",
      buttons: [
        {
          name: "edit",
          hint: "Edit",
          icon: "edit",
        },
        {
          name: "delete",
          hint: "Delete",
          icon: "trash",
        },
      ],
    },
  ],
  summary: {
    totalItems: [
      {
        column: "Quantity",
        name: "qunatitySummary",
        summaryType: "custom", // Specify that we're using a custom summary
        displayFormat: "Custom Total: {0}",
        // showInGroupFooter: true
      },
    ],
    calculateCustomSummary: function (options) {
      if (options.name === "qunatitySummary") {
        // There are three phases: 'start', 'calculate', and 'finalize'
        if (options.summaryProcess === "start") {
          // Initialize your custom accumulator.
          options.totalValue = 0;
        }
        if (options.summaryProcess === "calculate") {
          // Called once per each data item.
          // Here, we add the value from the current row to our accumulator.
          options.totalValue += options.value;
        }
        if (options.summaryProcess === "finalize") {
          // Final processing can be done here.
          // For this example, we don't need any additional processing,
          // so our final result is simply the accumulated totalValue.
        }
      }
    },
  },
  showBorders: true,
  editing: {
    mode: "cell",
    allowAdding: true,
    allowUpdating: true,
    allowDeleting: true,
  },
});
