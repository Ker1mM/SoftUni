function solve() {

    class Hardware {
        constructor(manufacturer) {
            if (new.target === Hardware) {
                throw new TypeError('Abstract class cannot be instantiated directly');
            }
            this.manufacturer = manufacturer;
        }
    }

    class Keyboard extends Hardware {
        constructor(manufacturer, responseTime) {
            super(manufacturer);
            this.responseTime = responseTime;
        }
    }

    class Monitor extends Hardware {
        constructor(manufacturer, width, height) {
            super(manufacturer);
            this.width = width;
            this.height = height;
        }
    }

    class Battery extends Hardware {
        constructor(manufacturer, expectedLife) {
            super(manufacturer);
            this.expectedLife = expectedLife;
        }
    }

    class Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace) {
            if (new.target === Computer) {
                throw new TypeError('Abstract class cannot be instantiated directly');
            }
            this.manufacturer = manufacturer;
            this.processorSpeed = processorSpeed;
            this.ram = ram;
            this.hardDiskSpace = hardDiskSpace;
        }
    }

    class Laptop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, weight, color, battery) {
            super(manufacturer, processorSpeed, ram, hardDiskSpace);
            this.weight = weight;
            this.color = color;
            this.battery = battery;
        }

        get battery() {
            return this._battery;
        }

        set battery(battery) {
            if (battery instanceof Battery) {
                this._battery = battery;
            } else {
                throw new TypeError('Battery should be of type Battery!');
            }
        }
    }

    class Desktop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, keyboard, monitor) {
            super(manufacturer, processorSpeed, ram, hardDiskSpace);
            this.keyboard = keyboard;
            this.monitor = monitor;
        }

        get keyboard() {
            return this._keyboard;
        }

        set keyboard(keyboard) {
            if (keyboard instanceof Keyboard) {
                this._keyboard = keyboard;
            } else {
                throw new TypeError('Keyboard should be of type Keyboard!');
            }
        }

        get monitor() {
            return this._monitor;
        }

        set monitor(monitor) {
            if (monitor instanceof Monitor) {
                this._monitor = monitor;
            } else {
                throw new TypeError('Monitor should be of type Monitor!');
            }
        }
    }

    return { Hardware, Keyboard, Monitor, Battery, Computer, Laptop, Desktop };
}