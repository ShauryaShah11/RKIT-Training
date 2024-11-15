$(document).ready(function () {
    let addExpense = $("#addExpense");
    // Function to fetch categories from a JSON file
    async function fetchCategory() {
        try {
            const response = await fetch("./data/category.json"); // Fetch the category data
            if (!response.ok) {
                throw new Error("Network response was not ok"); // Throw an error if the response is not ok
            }
            const data = await response.json(); // Parse the JSON data
            return data.categories; // Return the categories
        } catch (error) {
            console.error(
                "There has been a problem with your fetch operation:",
                error
            ); // Log any errors that occur during the fetch
        }
    }

    // Function to populate the category select box with fetched categories
    async function populateCategorySelect() {
        const categories = await fetchCategory(); // Fetch the categories
        if (categories) {
            const $categorySelect = $("#category"); // Get the category select element
            categories.forEach((category) => {
                const newOption = `<option value="${category.name}">${category.name}</option>`;
                // Create a new option element and append it to the select box
                $categorySelect.append(newOption);
            });
        }
    }

    // Class to store expense data
    class Expenses {
        constructor(amount, expenseDate, category) {
            this.amount = Number(amount);
            this.date = expenseDate;
            this.category = category;
        }


        saveToSession() {
            sessionStorage.setItem("expense", JSON.stringify(this));
        }
    }

    $("#addExpense").click(function (event) {
        event.preventDefault();
        let amount = $("#amount").val();
        let expenseDate = $("#date").val();
        let category = $("#category").val();

        let expense = new Expenses(amount, expenseDate, category);
        expense.saveToSession();
        alert("Expense saved to session storage");
        window.location.href = "./table.html"

    });
    populateCategorySelect(); // Call the function to populate the select box
});
