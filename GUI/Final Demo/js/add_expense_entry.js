$(document).ready(function () {
    async function fetchCategory() {
        try {
            const response = await fetch("./data/category.json");
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            const data = await response.json(); // Parse the JSON data
            return data.categories;
        } catch (error) {
            console.error(
                "There has been a problem with your fetch operation:",
                error
            );
        }
    }

    async function populateCategorySelect() {
        const categories = await fetchCategory();
        if (categories) {
            const $categorySelect = $("#category");
            categories.forEach((category) => {
                const newOption = `<option value="${category.id}">${category.name}</option>`;
                // Create a new option element and append it to the select box
                $categorySelect.append(newOption);
            });
        }
    }

    populateCategorySelect(); // Call the function to populate the select box
});
