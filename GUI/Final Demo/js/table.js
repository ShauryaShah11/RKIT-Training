$(document).ready(function () {
    async function fetchExpenses() {
        try {
            const response = await fetch("../data/expense_data.json");
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json(); // Parse the JSON data
            console.log(data);
            return data.expenses;
        } catch (error) {
            console.error('There has been a problem with your fetch operation:', error);
        }
    }

    async function populateExpenseTable() {
        const expenses = await fetchExpenses();
        if (expenses) {
            const $expenseTableBody = $("#expenseTableBody");
            $expenseTableBody.empty();
    
            expenses.forEach((expense, index) => {
              const newRow = `
                <tr>
                  <th scope="row">${index + 1}</th>
                  <td>${expense.category}</td>
                  <td>${expense.amount}</td>
                  <td>${expense.date}</td>
                </tr>
              `;
              $expenseTableBody.append(newRow); // Append new row to table body
            });
        }
    }

    populateExpenseTable(); 
});