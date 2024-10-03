function fetchData() {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve("Data fetched");
    }, 1000);
  });
}

async function fetchAndLog() {
  console.log("Fetching...");
  const data = await fetchData();
  console.log(data); // 'Data fetched'
}

fetchAndLog();

console.log("hello world")

console.log(5 == '5'); // true (because '5' is converted to a number)
console.log(5 === '5'); // false (because number 5 is not the same as string '5')

console.log(5 != '5'); // false (because after coercion, they are the same)
console.log(5 !== '5'); // true (because 5 is a number and '5' is a string)

