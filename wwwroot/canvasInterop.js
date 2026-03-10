window.canvasInterop = {
    //width: Number, 
    //height: Number,
    //cellSize: Number,
    ctx: any = undefined,
    canvas: any = undefined,
    components: any = [],
    heldComponent: any = undefined,
    //top: Number,
    //bottom: Number,
    //left: Number,
    //right: Number,
    
    

    setupCanvas: function (canvasElement, _cellSize) {
        if (!canvasElement) {
            return;
        }
        ctx = canvasElement.getContext("2d");
        canvas = canvasElement;

        //resize the canvas
        canvasElement.width = canvasElement.clientWidth;
        canvasElement.height = canvasElement.clientHeight;

        //update the dimension variables
        canvasDimensions.top = 0;
        canvasDimensions.bottom = canvasElement.height;
        canvasDimensions.left = 0;
        canvasDimensions.right = canvasElement.width;
        canvasDimensions.width = canvasElement.width;
        canvasDimensions.height = canvasElement.height;

        //setup variables for drawing on the canvas
        canvasDimensions.cellSize = _cellSize;

        this.translate(canvasDimensions.width / 2, canvasDimensions.height / 2);
    },

    drawGrid: function () {

        if (!ctx) {
            console.log("Canvas context does not exist...")
            return;
        }
        //console.log("Drawing grid...");
        let currX = 0;
        currX = Math.trunc(canvasDimensions.left/canvasDimensions.cellSize) * canvasDimensions.cellSize;
        let currY = 0;
        let columns = Math.trunc(canvasDimensions.width / canvasDimensions.cellSize) +1;
        let rows = Math.trunc(canvasDimensions.height / canvasDimensions.cellSize) +1;

        ctx.linewidth = 0.5;
        ctx.fillStyle = "black";
        ctx.beginPath();
        //draw vertical lines
        //TODO: Increase canvasDimensions.width of every 8th grid line (size of foundations)
        for (let i = 0; i < columns; i++) {
            
            currY = 0;
            ctx.moveTo(currX, canvasDimensions.top);
            ctx.lineTo(currX, canvasDimensions.bottom);
            currX += canvasDimensions.cellSize;
        }
        ctx.stroke();
        currX = 0;
        currY = Math.trunc(canvasDimensions.top /canvasDimensions.cellSize) * canvasDimensions.cellSize;
        //draw horizontal lines
        for (let i = 0; i < rows; i++) {
            
            currX = 0;
            ctx.moveTo(canvasDimensions.left, currY);
            ctx.lineTo(canvasDimensions.right, currY);
            currY += canvasDimensions.cellSize;
        }
        ctx.stroke();

        ctx.closePath();
    },

    clearCanvas: function () {
        if (!ctx) 
        {
            console.log("canvas does not exist")
            return;
        }
        //resizing canvas clears everything on it
        canvas.width = canvas.clientWidth;
        canvas.height = canvas.clientHeight;
        canvasDimensions.width = canvas.width;
        canvasDimensions.height = canvas.height;
        //console.log("Clearing canvas...");
    },

    redraw: function () {
        //console.log("redrawing canvas");
        window.canvasInterop.clearCanvas();
        ctx.setTransform(canvasDimensions.tMatrix);
        window.canvasInterop.drawGrid();
        window.canvasInterop.drawComponents();

        //Drawing a square around 0, 0
        ctx.strokeStyle = "red";
        ctx.lineWidth = 4;
        ctx.strokeRect(-20, -20, 40, 40);

        ctx.fillStyle = "black";
        console.log(canvasDimensions.tMatrix);
        
    },


    addComponent: function(newComponent) {
        this.components.push(newComponent);
    },

    setComponents: function (componentsList) {
        this.components = componentsList;
        this.redraw();
    },

    drawComponents: function () {
        console.log("   ");
        console.log("Drawing Components...");
        if (!ctx) return;
        for (let c of this.components) {
            this.drawComponent(c);
        }
        this.drawHeldComponent();
        console.log("Finished drawing components");
    },

    drawComponent: function (c) {
        //if component does not have an SVG image asset: draw one manually
        if (!c.imageFilePath) {
            console.log("Component does not have a custom SVG")
            //fill inside of component
            ctx.fillStyle = "grey";
            ctx.fillRect(c.x * canvasDimensions.cellSize, c.y * canvasDimensions.cellSize, c.width * canvasDimensions.cellSize, c.height * canvasDimensions.cellSize);


            //write name of component
            ctx.fillStyle = "white";
            ctx.font = "30px sans-serif";
            ctx.fillText(c.name, c.x * canvasDimensions.cellSize, (c.y + c.height / 2) * canvasDimensions.cellSize, c.width * canvasDimensions.cellSize);
            ctx.fillStyle = "black";

            //draw outline of component
            ctx.strokeRect(c.x * canvasDimensions.cellSize, c.y * canvasDimensions.cellSize, c.width * canvasDimensions.cellSize, c.height * canvasDimensions.cellSize);
            return;
        }
        console.log("Drawing component's SVG...")
        //draw image from file
        const img = new Image(100, 100);
        //altering stored link so the JS can access it
        img.src = c.imageFilePath.replace("\\", "/").replace("wwwroot/", "");
        ctx.drawImage(img, c.x * canvasDimensions.cellSize, c.y * canvasDimensions.cellSize);
    },

    drawHeldComponent: function () {
        if (this.heldComponent) {
            ctx.globalAlpha = 0.75;
            this.drawComponent(this.heldComponent);
            ctx.globalAlpha = 1;
        }
    },

    setHeldComponent: function (c) {
        this.heldComponent = c;
        console.log("changing held component");
        console.log(this.heldComponent);
    }, 

    setHeldComponentLocation: function (x, y) {
        if (this.heldComponent) {
            this.heldComponent.x = x;
            this.heldComponent.y = y;
        }
    },

    panUp: function () {
        console.log("panning up");
        this.translate(0, -32);
        this.redraw();
    },

    panDown: function () {
        console.log("panning down");
        this.translate(0, 32);
        this.redraw();
    },

    panLeft: function () {
        console.log("panning left");
        this.translate(-32, 0);
        this.redraw();
    },

    panRight: function () {
        console.log("panning right");
        this.translate(32, 0);
        this.redraw();
    },

    goHome: function () {
        this.resetTransform();
    },

    translate: function(x, y) {
        ctx.translate(x, y);
        this.updateDimensions(ctx.getTransform());
    },

    resetTransform: function () {
        ctx.setTransform(1, 0, 0, 1, canvasDimensions.width/2, canvasDimensions.height/2);
        this.updateDimensions(ctx.getTransform());
        this.redraw();
    },

    updateDimensions: function (transformMatrix) {
        //store transformation matrix
        canvasDimensions.tMatrix = transformMatrix;

        canvasDimensions.width = canvas.clientWidth;
        canvasDimensions.height = canvas.clientHeight;

        //update sides
        canvasDimensions.top = canvasDimensions.tMatrix.f * -1;
        canvasDimensions.bottom = canvasDimensions.top + canvasDimensions.height;

        canvasDimensions.left = canvasDimensions.tMatrix.e * -1;
        canvasDimensions.right = canvasDimensions.left + canvasDimensions.width;

        console.log("Updated dimensions:");
        console.log(canvasDimensions);
    },

    getCanvasTop: function()
    {
        return canvasDimensions.top;
    },

    getCanvasLeft: function () {
        return canvasDimensions.left;
    }
};

canvasDimensions = {
    width: Number,
    height: Number,
    top: Number,
    bottom: Number,
    left: Number,
    right: Number,
    cellSize: Number,
    tMatrix: DOMMatrix
    /*
        tMatrix parameters:
        a - horizontal scale
        b - horizontal skew (NOT USING)
        c - vertical skew (NOT USING) 
        d - vertical scale
        e - horizontal offset
        f - vertical offset
    */

}

