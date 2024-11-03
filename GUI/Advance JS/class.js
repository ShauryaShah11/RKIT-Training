class Car {
    constructor(brand) {
        this.brand = brand;
    }
    getBrand() {
        return this.brand;
    }
}

let myCar = new Car("Tesla");
console.log(myCar.getBrand()); // Tesla

// way 1 to create classes

// class Person {
//   constructor(name) {
//     this.name = name;
//   }
//   greet() {
//     console.log(`Hello, ${this.name}`);
//   }
// }

// way 2 to create classes
function Person(name) {
    this.name = name;
}
Person.prototype.greet = function () {
    console.log(`Hello, ${this.name}`);
};

let p = new Person("Shaurya");
p.greet();

// static class

class Calculator {
    static pi = 3.14159;

    static add(a, b) {
        return a + b;
    }
}

console.log(Calculator.pi); // 3.14159
console.log(Calculator.add(5, 3)); // 8
