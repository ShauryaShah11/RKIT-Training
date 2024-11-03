// serialization :- convertin js object into format that can sent over the network

var data = { name: "John", age: 30 };
var jsonString = JSON.stringify(data); // Serializing the JS object to JSON string
console.log(jsonString); // Output: {"name":"John","age":30}

// deserialization :- convertin json string  to js object

var jsonString = '{"name":"John","age":30}';
var data = JSON.parse(jsonString); // De-serializing the JSON string to a JS object
console.log(data.name); // Output: John
