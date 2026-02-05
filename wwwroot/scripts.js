function doStuff() {
    console.log("JS Interop is working :)");
}

function logString(str)
{
    console.log(str);
}

function GetElementById(elementId) {
    console.log("calling getelement by id");
    return document.getElementById(elementId);
}

function applyStyleForElement(styleOp) {
    var element = document.getElementById(styleOp.id);
    if (!element) return;

    element.style[styleOp.attrib] = styleOp.value;
}