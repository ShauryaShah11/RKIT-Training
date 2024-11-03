// map function :- function creates a new array by applying a given function to each element of the original array
let numbers1 = [1, 2, 3, 4];
let squareNumbers = $.map(numbers1, function (value, index) {
    return value * value; // square each number
});
console.log(squareNumbers); // [1, 4, 9, 16]

//grep function :- The grep() function is used to filter an array based on a condition. It returns a new array that only contains the elements that satisfy the condition.

let numbers2 = [1, 2, 3, 4, 5, 6];
let evenNumbers = $.grep(numbers2, function (value, index) {
    return value % 2 === 0; // filter even numbers
});
console.log(evenNumbers); // [2, 4, 6]

//extend function :- The extend() function is used to merge the contents of two or more objects together into the first object.

let defaults = { name: "John", age: 25 };
let userSettings = { age: 30, location: "New York" };

var merged = $.extend({}, defaults, userSettings);
console.log(merged);
// { name: "John", age: 30, location: "New York" }

// each function :- The each() function is used to loop through arrays or objects and execute a function on each element. It's similar to JavaScript's forEach() but also works with objects.

var fruits = ["apple", "banana", "cherry"];

$.each(fruits, function (index, value) {
    console.log(index + ": " + value);
});
// Output:
// 0: apple
// 1: banana
// 2: cherry

// merge function :-
var array1 = [1, 2, 3];
var array2 = [4, 5, 6];

var combined = $.merge(array1, array2);
console.log(combined); // [1, 2, 3, 4, 5, 6]
