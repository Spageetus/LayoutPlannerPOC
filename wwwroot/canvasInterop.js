window.canvasInterop = {

    width: Number, 
    height: Number,
    cellSize: Number,
    ctx: any = undefined,
    canvas: any = undefined,
    components: any = [],

    setupCanvas: function (canvasElement, _cellSize) {
        if (!canvasElement) {
            return;
        }
        
        ctx = canvasElement.getContext("2d");
        canvas = canvasElement;
        canvasElement.width = canvasElement.clientWidth;
        canvasElement.height = canvasElement.clientHeight;
        width = canvasElement.width;
        height = canvasElement.height;
        cellSize = _cellSize;
        
    },

    drawGrid: function () {

        if (!ctx) {
            console.log("Canvas context does not exist...")
            return;
        }
        //console.log("Drawing grid...");
        let currX = 0;
        let currY = 0;
        let columns = Math.trunc(width / cellSize) +1;
        let rows = Math.trunc(height / cellSize) +1;

        ctx.lineWidth = 0.5;
        ctx.fillStyle = "black";
        ctx.beginPath();
        //draw vertical lines
        //TODO: Increase width of every 8th grid line (size of foundations)
        for (let i = 0; i < columns; i++) {
            currX += cellSize;
            currY = 0;
            ctx.moveTo(currX, currY);
            ctx.lineTo(currX, height);
        }
        ctx.stroke();
        currX = 0;
        currY = 0;
        //draw horizontal lines
        for (let i = 0; i < rows; i++) {
            currY += cellSize;
            currX = 0;
            ctx.moveTo(currX, currY);
            ctx.lineTo(width, currY);
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
        window.canvasInterop.drawGrid();
        window.canvasInterop.drawComponents();

    },


    addComponent: function(newComponent) {
        this.components.push(newComponent);
        console.log("Current components:");
        for (let c of this.components) {
            console.log(c);
        }
    },

    setComponents: function (componentsList) {
        this.components = componentsList;
        this.redraw();
    },

    drawComponents: function () {
        if (!ctx) return;
        ctx.globalAlpha = 0.8;
        console.log("Drawing Components...");
        for (let c of this.components) {
            console.log(c);
            ctx.fillStyle = c.color;
            ctx.fillRect(c.cellX * cellSize, c.cellY * cellSize, cellSize * c.width, cellSize * c.height);
        }
        ctx.globalAlpha = 1;
    }
};
