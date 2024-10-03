function example() {
  console.log(x); // undefined (because of hoisting)
  var x = 10;
  console.log(x); // 10
}
example();

if (true) {
  let y = 20;
  console.log(y); // 20
}
console.log(y); // Error: y is not defined (block-scoped)

const z = 30;
console.log(z); // 30
z = 40; // Error: Assignment to constant variable
