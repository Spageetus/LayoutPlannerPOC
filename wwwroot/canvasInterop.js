window.canvasInterop = {
    //width: Number, 
    //height: Number,
    //cellSize: Number,
    ctx: any = undefined,
    canvas: any = undefined,
    components: any = [],
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
        
    },

    drawGrid: function () {

        if (!ctx) {
            console.log("Canvas context does not exist...")
            return;
        }
        //console.log("Drawing grid...");
        let currX = 0;
        let currY = 0;
        let columns = Math.trunc(canvasDimensions.width / canvasDimensions.cellSize) +1;
        let rows = Math.trunc(canvasDimensions.height / canvasDimensions.cellSize) +1;

        ctx.linewidth = 0.5;
        ctx.fillStyle = "black";
        ctx.beginPath();
        //draw vertical lines
        //TODO: Increase canvasDimensions.width of every 8th grid line (size of foundations)
        for (let i = 0; i < columns; i++) {
            currX += canvasDimensions.cellSize;
            currY = 0;
            ctx.moveTo(currX, currY);
            ctx.lineTo(currX, canvasDimensions.height);
        }
        ctx.stroke();
        currX = 0;
        currY = 0;
        //draw horizontal lines
        for (let i = 0; i < rows; i++) {
            currY += canvasDimensions.cellSize;
            currX = 0;
            ctx.moveTo(currX, currY);
            ctx.lineTo(canvasDimensions.width, currY);
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
        width = canvas.width;
        height = canvas.height;
        //console.log("Clearing canvas...");
    },

    redraw: function () {
        //console.log("redrawing canvas");
        window.canvasInterop.clearCanvas();

        ctx.translate(width / 2, height / 2);
        console.log(ctx.getTransform());

        window.canvasInterop.drawGrid();
        window.canvasInterop.drawComponents();
        ctx.fillRect(0, 0, 20, 20);
        
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
            console.log("Drawing component: " + c.name);
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
                continue;    
            }
            console.log("Drawing component's SVG...")
            //draw image from file
            const img = new Image(100, 100);

            //altering stored link so the JS can access it
            img.src = c.imageFilePath.replace("\\", "/").replace("wwwroot/", "");
            ctx.drawImage(img, c.x * canvasDimensions.cellSize, c.y * canvasDimensions.cellSize);            
        }
        console.log("Finished drawing components");
    }
};

canvasDimensions = {
    width: Number,
    height: Number,
    top: Number,
    bottom: Number,
    left: Number,
    right: Number,
    cellSize: Number
}