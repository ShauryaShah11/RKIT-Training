$(document).ready(function () {
    let expenses = []; // Store fetched data in a global variable
    let isAscending = {
        amount: true,
        category: true,
        date: true,
    }; // Track the sorting direction for each field
    let isFiltered = false; // Track the filter state

    // Default settings for the table display
    const defaultSettings = {
        showHighExpensesOnly: false,
        thresholdAmount: 100,
    };

    // User's custom settings (these can be changed as needed)
    const userSettings = {
        thresholdAmount: 50, // Custom threshold for filtering
    };

    // Merge default settings with user settings
    const settings = $.extend({}, defaultSettings, userSettings);

    // Function to fetch data using AJAX
    function fetchExpenses() {
        return $.ajax({
            url: "./data/expense_data.json",
            dataType: "json",
            success: function (data) {
                expenses = data.expenses; // Store fetched data
                renderExpenses(expenses); // Initially render the data
                updateTotalExpense(expenses); // Update total expense
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching data:", textStatus, errorThrown);
                $("#expenseTableBody").html(
                    "<tr><td colspan='4'>Failed to load data</td></tr>"
                );
            },
        });
    }

    // Function to render expenses in the table using $.each()
    function renderExpenses(data) {
        const $expenseTableBody = $("#expenseTableBody");
        $expenseTableBody.empty();

        if (data.length === 0) {
            $expenseTableBody.append(
                "<tr><td colspan='4'>No expenses found</td></tr>"
            );
            return;
        }

        $.each(data, function (index, expense) {
            const newRow = `
            <tr>
              <th scope="row">${index + 1}</th>
              <td>${expense.category}</td>
              <td>${expense.amount}</td>
              <td>${expense.date}</td>
            </tr>
          `;
            $expenseTableBody.append(newRow);
        });
    }

    // Function to update the total expense using $.map()
    function updateTotalExpense(data) {
        const amounts = $.map(data, function (expense) {
            return expense.amount; // Extract the amount
        });
        const total = amounts.reduce((sum, amount) => sum + amount, 0);
        $("#totalExpense").text(`$${total.toFixed(2)}`);
    }

    // Sort function with flexibility to sort by any field
    function sortExpenses(field) {
        isAscending[field] = !isAscending[field]; // Toggle sorting direction
        expenses.sort((a, b) => {
            if (typeof a[field] === "number") {
                return isAscending[field]
                    ? a[field] - b[field]
                    : b[field] - a[field];
            }
            if (typeof a[field] === "string") {
                return isAscending[field]
                    ? (a[field] > b[field] ? 1 : -1)
                    : (a[field] < b[field] ? 1 : -1);
            }            
            if (field === "date") {
                return isAscending[field]
                    ? new Date(a[field]) - new Date(b[field])
                    : new Date(b[field]) - new Date(a[field]);
            }
        });
        renderExpenses(expenses); // Render sorted data
    }

    // Filter function to display expenses above a specified threshold using $.grep()
    function filterExpensesByValue() {
        const filteredExpenses = $.grep(expenses, function (expense) {
            return expense.amount > settings.thresholdAmount; // Filter based on threshold
        });
        expenses = filteredExpenses; // Update the global expenses array
        renderExpenses(filteredExpenses);
        updateTotalExpense(filteredExpenses);
    }

    // Event listener for sorting by amount
    $("#sortByAmount").on("click", function () {
        sortExpenses("amount");

        // Update the sort icon based on direction
        const iconClass = isAscending.amount
            ? "fa-sort-amount-up"
            : "fa-sort-amount-down";
        $("#amountSortIcon")
            .removeClass("fa-sort fa-sort-amount-up fa-sort-amount-down")
            .addClass(iconClass);
    });

    // Event listener for sorting by category
    $("#sortByCategory").on("click", function () {
        sortExpenses("category");

        // Update the sort icon based on direction
        const iconClass = isAscending.category
            ? "fa-sort-alpha-up"
            : "fa-sort-alpha-down";
        $("#categorySortIcon")
            .removeClass("fa-sort fa-sort-alpha-up fa-sort-alpha-down")
            .addClass(iconClass);
    });

    // Event listener for sorting by date
    $("#sortByDate").on("click", function () {
        sortExpenses("date");

        // Update the sort icon based on direction
        const iconClass = isAscending.date
            ? "fa-sort-numeric-up"
            : "fa-sort-numeric-down";
        $("#dateSortIcon")
            .removeClass("fa-sort fa-sort-numeric-up fa-sort-numeric-down")
            .addClass(iconClass);
    });

    // Event listener for filtering by value
    $("#filterByValue").on("click", function () {
        isFiltered = !isFiltered; // Toggle filter state
        if (isFiltered) {
            filterExpensesByValue();
        } else {
            renderExpenses(expenses);
            updateTotalExpense(expenses);
        }

        // Update the filter icon based on state
        const iconClass = isFiltered ? "fa-filter" : "fa-filter";
        $("#valueFilterIcon").removeClass("fa-filter").addClass(iconClass);
    });

    // Merge additional fetched data (if any) with existing expenses using $.merge()
    function mergeAdditionalExpenses(newExpenses) {
        expenses = $.merge(expenses, newExpenses);
        renderExpenses(expenses);
        updateTotalExpense(expenses);
    }

    // Example: Suppose we fetch additional expenses and merge them
    fetchExpenses()
        .then(() => {
            const additionalExpenses = [
                {
                    item: "Internet",
                    amount: 30,
                    date: "2024-11-10",
                    category: "Utilities",
                },
                {
                    item: "Insurance",
                    amount: 150,
                    date: "2024-11-11",
                    category: "Health",
                },
            ];
            mergeAdditionalExpenses(additionalExpenses);
        })
        .catch((error) => {
            console.error("Error fetching expenses:", error);
        });
});
