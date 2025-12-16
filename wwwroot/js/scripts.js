// In wwwroot/js/myJsFile.js


window.someJsFunction = (jsObject) => {
    console.log("Received object:", jsObject);
    console.log("Id:", jsObject.id);
    console.log("Name:", jsObject.name);
    console.log("Color:", jsObject.color);
    console.log("Length:", jsObject.length);
    console.log("Width:", jsObject.width);
    // Use the object properties as needed
};

