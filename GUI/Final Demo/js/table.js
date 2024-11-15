import { getCookie } from './cookieUtils.js';

$(document).ready(function () {
    let expenses = []; // Global variable to store fetched expense data
    let isAscending = {
        amount: true,
        category: true,
        date: true,
    }; // Object to track the sorting direction for each field
    let isFiltered = false; // Boolean to track the filter state

    // Default settings for the table display
    const defaultSettings = {
        showHighExpensesOnly: false,
        thresholdAmount: 100, // Default threshold amount for filtering
    };

    let userSettings = {};

    // Check if the user has already accepted the cookies
    if (getCookie("cookiesAccepted")) {
        // User's custom settings (these can be changed as needed)
        userSettings.thresholdAmount = 50;
    }

    // Merge default settings with user settings
    const settings = $.extend({}, defaultSettings, userSettings);

    // Function to fetch expense data using AJAX
    function fetchExpenses() {
        return $.ajax({
            url: "./data/expense_data.json",
            dataType: "json",
            success: function (data) {
                expenses = data.expenses; // Store fetched data in the global variable
                renderExpenses(expenses); // Initially render the fetched data
                updateTotalExpense(expenses); // Update the total expense display
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching data:", textStatus, errorThrown);
                $("#expenseTableBody").html(
                    "<tr><td colspan='4'>Failed to load data</td></tr>"
                );
            },
        });
    }

    // Function to render expenses in the table
    function renderExpenses(data) {
        const $expenseTableBody = $("#expenseTableBody");
        $expenseTableBody.empty(); // Clear the table body

        if (data.length === 0) {
            $expenseTableBody.append(
                "<tr><td colspan='4'>No expenses found</td></tr>"
            );
            return;
        }

        // Iterate over each expense and append it to the table
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

    // Function to update the total expense display
    function updateTotalExpense(data) {
        const amounts = $.map(data, function (expense) {
            return expense.amount; // Extract the amount from each expense
        });
        const total = amounts.reduce(
            (totalSum, amount) => totalSum + amount,
            0
        );
        $("#totalExpense").text(`$${total}`); // Display the total expense
    }

    // Function to sort expenses by a specified field
    function sortExpenses(field) {
        isAscending[field] = !isAscending[field]; // Toggle the sorting direction
        expenses.sort((a, b) => {
            if (typeof a[field] === "number") {
                return isAscending[field]
                    ? a[field] - b[field]
                    : b[field] - a[field];
            }
            if (typeof a[field] === "string") {
                return isAscending[field]
                    ? a[field] > b[field]
                        ? 1
                        : -1
                    : a[field] < b[field]
                    ? 1
                    : -1;
            }
            if (field === "date") {
                return isAscending[field]
                    ? new Date(a[field]) - new Date(b[field])
                    : new Date(b[field]) - new Date(a[field]);
            }
        });
        renderExpenses(expenses); // Render the sorted data
    }

    // Function to filter expenses by a specified threshold amount
    function filterExpensesByValue() {
        const filteredExpenses = $.grep(expenses, function (expense) {
            return expense.amount > settings.thresholdAmount; // Filter based on the threshold amount
        });
        renderExpenses(filteredExpenses); // Render the filtered data
        updateTotalExpense(filteredExpenses); // Update the total expense display
    }

    // Event listener for sorting by amount
    $("#sortByAmount").on("click", function () {
        sortExpenses("amount"); // Sort expenses by amount

        // Update the sort icon based on the sorting direction
        const iconClass = isAscending.amount
            ? "fa-sort-amount-up"
            : "fa-sort-amount-down";
        $("#amountSortIcon")
            .removeClass("fa-sort fa-sort-amount-up fa-sort-amount-down")
            .addClass(iconClass);
    });

    // Event listener for sorting by category
    $("#sortByCategory").on("click", function () {
        sortExpenses("category"); // Sort expenses by category

        // Update the sort icon based on the sorting direction
        const iconClass = isAscending.category
            ? "fa-sort-alpha-up"
            : "fa-sort-alpha-down";
        $("#categorySortIcon")
            .removeClass("fa-sort fa-sort-alpha-up fa-sort-alpha-down")
            .addClass(iconClass);
    });

    // Event listener for sorting by date
    $("#sortByDate").on("click", function () {
        sortExpenses("date"); // Sort expenses by date

        // Update the sort icon based on the sorting direction
        const iconClass = isAscending.date
            ? "fa-sort-numeric-up"
            : "fa-sort-numeric-down";
        $("#dateSortIcon")
            .removeClass("fa-sort fa-sort-numeric-up fa-sort-numeric-down")
            .addClass(iconClass);
    });

    // Event listener for filtering by value
    $("#filterByValue").on("click", function () {
        isFiltered = !isFiltered; // Toggle the filter state
        if (isFiltered) {
            filterExpensesByValue(); // Apply the filter
        } else {
            renderExpenses(expenses); // Render all expenses
            updateTotalExpense(expenses); // Update the total expense display
        }

        // Update the filter icon based on the filter state
        const iconClass = isFiltered
            ? "fa-filter-active"
            : "fa-filter-inactive";
        $("#valueFilterIcon")
            .removeClass("fa-filter-active fa-filter-inactive")
            .addClass(iconClass);
    });

    function fetchExpensesFromSessionStorage() {
        let sessionExpenses = JSON.parse(sessionStorage.getItem("expense"));
        return sessionExpenses ? [sessionExpenses] : [];
    }
    
    // Function to merge additional fetched data with existing expenses
    function mergeAdditionalExpenses(newExpenses) {
        let sessionExpenses = fetchExpensesFromSessionStorage();
        expenses = $.merge(expenses, sessionExpenses); // Store Session Expenses into expenses array
        let allExpenses = $.merge(expenses, newExpenses); // Merge the new expenses with the existing ones
        renderExpenses(allExpenses); // Render the merged data
        updateTotalExpense(allExpenses); // Update the total expense display
    }

    // Fetch initial expenses and merge additional expenses as an example
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
            mergeAdditionalExpenses(additionalExpenses); // Merge the additional expenses
        })
        .catch((error) => {
            console.error("Error fetching expenses:", error);
        });
});
